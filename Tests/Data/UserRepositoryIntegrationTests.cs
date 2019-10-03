using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FutterlisteNg.Data;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;
using NUnit.Framework;

namespace FutterlisteNg.Tests.Data
{
    [TestFixture]
    public class UserRepositoryIntegrationTests : IntegrationTestBase
    {
        private IUserRepository _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UserRepository(Database);
        }

        [Test]
        public async Task Add_WithAlreadyExistingUser_ThrowsDuplicateException()
        {
            var user = new User("TU", "Test User");
            await _sut.AddAsync(user);

            Func<Task> act = async () => await _sut.AddAsync(user);

            await act.Should().ThrowAsync<DuplicateException>()
                .WithMessage("User with short name 'TU' already exists.");
        }

        [Test]
        public async Task FindAll_ShouldSortUsersCorrectly()
        {
            var result = (await _sut.FindAllAsync()).ToArray();

            result.Should().HaveCount(5);
            result[0].Username.Should().Be("Eric");
            result[1].Username.Should().Be("Stan");
            result[2].Username.Should().Be("Kenny");
            result[3].Username.Should().Be("Kyle");
            result[4].Username.Should().Be("Token");
        }

        [Test]
        public async Task FindAll_AfterAdd_ShouldFindAddedUser()
        {
            var user = new User("TU", "TestUser");
            await _sut.AddAsync(user);

            var result = (await _sut.FindAllAsync()).ToArray();

            result.Should().HaveCount(6);
            result.Should().ContainEquivalentOf(user);
        }

        [Test]
        public async Task Exists_WithExistentUser_ShouldReturnTrue()
        {
            var result = await _sut.Exists("Eric");
            
            result.Should().Be(true);
        }

        [Test]
        public async Task Exists_WithNonExistentUser_ShouldReturnTrue()
        {
            var result = await _sut.Exists("NonExistent");
            
            result.Should().Be(false);
        }

        [Test]
        public async Task Get_WithExistingUser_ShouldReturnUser()
        {
            var result = await _sut.GetAsync("Eric");

            result.Username.Should().Be("Eric");
        }

        [Test]
        public async Task Update_WithExistentUser_ShouldUpdateUser()
        {
            var user = new User("Eric", "UpdatedName");

            await _sut.UpdateAsync(user);

            var updatedUser = await _sut.GetAsync("Eric");
            updatedUser.Name.Should().Be("UpdatedName");
        }
    }
}
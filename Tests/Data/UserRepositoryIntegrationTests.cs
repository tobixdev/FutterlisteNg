using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FutterlisteNg.Data;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;
using NUnit.Framework;

namespace FutterlisteNg.UnitTests.Data
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
            var user = new User("Test User", "TU");
            await _sut.AddAsync(user);

            Func<Task> act = async () => await _sut.AddAsync(user);

            await act.Should().ThrowAsync<DuplicateException>();
        }
        
        [Test]
        public async Task GetByShortName_WithExistingUser_ShouldFindCorrectUser()
        {
            var result = await _sut.GetByShortNameAsync("DD");

            result.Should().NotBeNull();
            result.Name.Should().Be("Donald Duck");
            result.ShortName.Should().Be("DD");
        }
        
        [Test]
        public async Task GetByShortName_WithNonExistingUser_ShouldThrowNotFoundException()
        {
            Func<Task<User>> act = async () => await _sut.GetByShortNameAsync("Non Existent");

            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task FindAll_AfterAdd_ShouldFindAddedUser()
        {
            await _sut.AddAsync(new User("TestUser", "TU"));

            var result = (await _sut.FindAllAsync()).ToArray();

            result.Should().HaveCount(2);
            result[0].Name.Should().Be("Donald Duck");
            result[0].ShortName.Should().Be("DD");
            result[1].Name.Should().Be("TestUser");
            result[1].ShortName.Should().Be("TU");
        }
        
        [Test]
        public async Task Update_WithNonExistingUser_ShouldThrowNotFoundException()
        {
            var user = new User("Non Existent", "NE");

            Func<Task> act = async () => await _sut.UpdateAsync(user);

            await act.Should().ThrowAsync<NotFoundException>();
        }
        
        [Test]
        public async Task Update_WithExistingUser_ShouldUpdatesUser()
        {
            var user = new User("User", "U");
            await _sut.AddAsync(user);
            
            await _sut.UpdateAsync(new User("Updated User", "U"));

            var updatedUser = await _sut.GetByShortNameAsync("U");
            updatedUser.Name.Should().Be("Updated User");
        }
    }
}
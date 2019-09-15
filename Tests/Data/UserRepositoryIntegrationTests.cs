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
        public async Task FindByName_WithExistingUser_ShouldFindCorrectUser()
        {
            var result = (await _sut.FindByNameAsync("Donald Duck"));

            result.Should().NotBeNull();
            result.Name.Should().Be("Donald Duck");
            result.ShortName.Should().Be("DD");
        }
        
        [Test]
        public async Task FindByName_WithNonExistingUser_ShouldThrowNotFoundException()
        {
            Func<Task<User>> act = async () => await _sut.FindByNameAsync("Non Existent");

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
    }
}
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
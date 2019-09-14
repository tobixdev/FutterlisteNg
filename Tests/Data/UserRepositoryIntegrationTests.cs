using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
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
        public async Task FindAll_AfterAdd_ShouldFindAddedUser()
        {
            await _sut.AddAsync(new User("TestUser", "TU"));

            var result = (await _sut.FindAllAsync()).ToArray();

            result.Should().HaveCount(1);
            result[0].Name.Should().Be("TestUser");
            result[0].ShortName.Should().Be("TU");
        }
    }
}
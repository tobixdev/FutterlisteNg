using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;
using FutterlisteNg.Domain.Service;

namespace FutterlisteNg.UnitTests.Domain
{
    [TestFixture]
    public class UserServiceTest
    {
        private IUserRepository _userRepository;
        private IUserService _sut;

        [SetUp]
        public void SetUpBase()
        {
            _userRepository = A.Fake<IUserRepository>();
            _sut = new UserService(_userRepository);
        }

        [Test]
        public async Task All_WithEmpty_ReturnsEmpty()
        {
            A.CallTo(() => _userRepository.FindAllAsync()).Returns(new List<User>());
            
            var result = await _sut.FindAllAsync();
            
            Assert.That(result, Is.Empty);
        }

        [Test]
        public async Task All_WithNonEmpty_ReturnsNonEmpty()
        {
            var users = new List<User> {new User("Some User", "su")};
            A.CallTo(() => _userRepository.FindAllAsync()).Returns(users);
            
            var result = (await _sut.FindAllAsync()).ToList();
            
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Some User"));
            Assert.That(result[0].ShortName, Is.EqualTo("su"));
        }
    }
}
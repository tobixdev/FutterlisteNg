using System.Collections.Generic;
using FakeItEasy;
using FutterlisteNg.Domain.Data;
using FutterlisteNg.Domain.Model;
using FutterlisteNg.Domain.Services;
using NUnit.Framework;
using System.Linq;

namespace FutterlisteNg.UnitTests.Domain
{
    [TestFixture]
    public class UserServiceTest
    {
        private IUserRepository _userRepository;
        private IUserService _sut;

        [SetUp]
        public void SetUp()
        {
            _userRepository = A.Fake<IUserRepository>();
            _sut = new UserService(_userRepository);
        }

        [Test]
        public void All_WithEmpty_ReturnsEmpty()
        {
            A.CallTo(() => _userRepository.All()).Returns(new List<User>());
            
            var result = _sut.All();
            
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void All_WithNonEmpty_ReturnsNonEmpty()
        {
            var users = new List<User> {new User("Some User", "su")};
            A.CallTo(() => _userRepository.All()).Returns(users);
            
            var result = _sut.All().ToList();
            
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Some User"));
            Assert.That(result[0].ShortName, Is.EqualTo("su"));
        }
    }
}
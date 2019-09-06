using FutterlisteNg.Domain.Data;
using NUnit.Framework;

namespace FutterlisteNg.UnitTests.Domain
{
    [TestFixture]
    public class UserServiceTest
    {
        private IUserService _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new UserService();
        }

        [Test]
        public void TestInterOp()
        {
            var result = _sut.All;
            
            Assert.That(result, Is.Empty);
        }
    }
}
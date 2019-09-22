using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using FutterlisteNg.Data;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;
using FutterlisteNg.Domain.Exception;
using FutterlisteNg.Domain.Service;
using NUnit.Framework;

namespace FutterlisteNg.Tests.Domain
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
            var users = new List<User> {new User("su", "Some User")};
            A.CallTo(() => _userRepository.FindAllAsync()).Returns(users);
            
            var result = (await _sut.FindAllAsync()).ToList();
            
            result.Should().HaveCount(1);
            result[0].Name.Should().Be("Some User");
            result[0].Username.Should().Be("su");
        }

        [Test]
        public async Task Add_WithValidUser_ShouldDelegateCallToRepository()
        {
            var user = new User("test", "test");

            await _sut.AddAsync(user);

            A.CallTo(() => _userRepository.AddAsync(user)).MustHaveHappened(1, Times.Exactly);
        }

        [Test]
        public async Task Add_WithNoUsername_ShouldThrowValidationException()
        {
            var user = new User(string.Empty, "test");

            Func<Task> act = async () => await _sut.AddAsync(user);

            await act.Should().ThrowAsync<ValidationException>().WithMessage("User must have a Name and Username");
        }

        [Test]
        public async Task Add_WithNoName_ShouldThrowValidationException()
        {
            var user = new User("Username", string.Empty);

            Func<Task> act = async () => await _sut.AddAsync(user);

            await act.Should().ThrowAsync<ValidationException>().WithMessage("User must have a Name and Username");
        }

        [Test]
        public async Task Delete_WithNotExistentUser_ShouldThrowNotFoundException()
        {
            Func<Task> act = async () => await _sut.DeleteAsync("Not Exsitent");

            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("User with username 'Not Exsitent' does not exist.");
        }

        [Test]
        public async Task Delete_WithExistentUser_ShouldDelegateToRepository()
        {
            A.CallTo(() => _userRepository.Exists("Username")).Returns(true);
                
            await _sut.DeleteAsync("Username");

            A.CallTo(() => _userRepository.DeleteAsync("Username"))
                .MustHaveHappened(1, Times.Exactly);
        }

        [Test]
        public async Task Get_WithExistingUser_ShouldReturnUser()
        {
            var user = new User();
            A.CallTo(() => _userRepository.Exists("Eric")).Returns(true);
            A.CallTo(() => _userRepository.Get("Eric")).Returns(user);
            
            var result = await _sut.GetAsync("Eric");

            result.Should().BeSameAs(user);
        }

        [Test]
        public async Task Get_WithNotExistingUser_ShouldThrowNotFoundException()
        {
            Func<Task> act = async () => await _sut.GetAsync("Not Existing");

            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("User with username 'Not Existing' does not exist.");
        }

        [Test]
        public async Task Update_WithExistingUser_ShouldUpdateUser()
        {
            A.CallTo(() => _userRepository.Exists("Eric")).Returns(true);
            var user = new User("Eric", "Updated Name");
            
            await _sut.UpdateAsync(user);

            A.CallTo(() => _userRepository.UpdateAsync(user)).MustHaveHappened(1, Times.Exactly);
        }

        [Test]
        public async Task Update_WithNotExistingUser_ShouldThrowNotFoundException()
        {
            Func<Task> act = async () => await _sut.GetAsync("Not Existing");

            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("User with username 'Not Existing' does not exist.");
        }
    }
}
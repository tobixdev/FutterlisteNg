using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FutterlisteNg.Data;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;
using FutterlisteNg.Domain.Extensions;
using FutterlisteNg.Domain.Validation;
using log4net;
using ValidationException = FutterlisteNg.Domain.Exception.ValidationException;

namespace FutterlisteNg.Domain.Service
{
    public class UserService : IUserService
    {
        private static readonly ILog s_log = LogManager.GetLogger(typeof(UserService));
        
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        private IValidator<User> CreateValidator => new UserCreateValidator(_userRepository);
        private IValidator<User> UpdateValidator => new UserUpdateValidator(_userRepository);

        public async Task<IEnumerable<User>> FindAllAsync()
        {
            return await _userRepository.FindAllAsync();
        }

        public async Task<User> GetAsync(string username)
        {
            await EnsureUserExists(username);

            return await _userRepository.GetAsync(username);
        }

        public async Task AddAsync(User toAdd)
        {
            (await CreateValidator.ValidateAsync(toAdd)).ThrowIfInvalid();
            
            s_log.Info("Creating new User: " + toAdd);
            await _userRepository.AddAsync(toAdd);
        }

        public async Task DeleteAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username must not be null or empty.");
            
            await EnsureUserExists(username);
            
            s_log.Info("Deleting user " + username);
            await _userRepository.DeleteAsync(username);
        }

        public async Task UpdateAsync(User toUpdate)
        {
            (await UpdateValidator.ValidateAsync(toUpdate)).ThrowIfInvalid();
            await _userRepository.UpdateAsync(toUpdate);
        }

        private async Task EnsureUserExists(string username)
        {
            if (!await _userRepository.Exists(username))
                throw new NotFoundException($"User with username '{username}' does not exist.");
        }
    }
}
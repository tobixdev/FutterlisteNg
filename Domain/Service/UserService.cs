using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Data;
using FutterlisteNg.Data.Model;
using FutterlisteNg.Data.Repository;
using FutterlisteNg.Domain.Exception;
using log4net;

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

        public async Task<IEnumerable<User>> FindAllAsync()
        {
            return await _userRepository.FindAllAsync();
        }

        public async Task<User> GetAsync(string username)
        {
            await EnsureUserExists(username);

            return await _userRepository.Get(username);
        }

        public async Task AddAsync(User toAdd)
        {
            if (string.IsNullOrEmpty(toAdd.Name) || string.IsNullOrEmpty(toAdd.Username))
                throw new ValidationException("User must have a Name and Username");
            
            s_log.Info("Creating new User: " + toAdd);
            await _userRepository.AddAsync(toAdd);
        }

        public async Task DeleteAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ValidationException("Username must not be null or empty.");
            
            await EnsureUserExists(username);
            
            s_log.Info("Deleting user " + username);
            await _userRepository.DeleteAsync(username);
        }

        public async Task UpdateAsync(User toUpdate)
        {
            await EnsureUserExists(toUpdate.Username);

            await _userRepository.UpdateAsync(toUpdate);
        }

        private async Task EnsureUserExists(string username)
        {
            if (!await _userRepository.Exists(username))
                throw new NotFoundException($"User with username '{username}' does not exist.");
        }
    }
}
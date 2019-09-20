using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task Add(User toAdd)
        {
            if (string.IsNullOrEmpty(toAdd.Name) || string.IsNullOrEmpty(toAdd.Username))
                throw new ValidationException("User must have a Name and Username");
            
            s_log.Info("Creating new User: " + toAdd);
            await _userRepository.AddAsync(toAdd);
        }

        public async Task Delete(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ValidationException("Username must not be null or empty.");
            
            if (!(await _userRepository.Exists(username)))
                throw new ValidationException($"User with username '{username}' does not exist.");
            
            s_log.Info("Deleting user " + username);
            await _userRepository.Delete(username);
        }
    }
}
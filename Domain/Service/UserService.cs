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
            if (string.IsNullOrEmpty(toAdd.Name) || string.IsNullOrEmpty(toAdd.ShortName))
                throw new ValidationException("User must have a Name and ShortName");
            
            s_log.Info("Creating new User: " + toAdd);
            await _userRepository.AddAsync(toAdd);
        }
    }
}
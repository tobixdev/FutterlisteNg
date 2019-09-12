using System.Collections.Generic;
using System.Threading.Tasks;
using FutterlisteNg.Domain.Data;
using FutterlisteNg.Domain.Model;

namespace FutterlisteNg.Domain.Service
{
    public class UserService : IUserService
    {
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
            await _userRepository.Add(toAdd);
        }
    }
}
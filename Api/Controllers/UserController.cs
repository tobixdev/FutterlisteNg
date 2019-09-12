using System.Collections.Generic;
using System.Linq;
using FutterlisteNg.Domain.Model;
using FutterlisteNg.Domain.Services;
using FutterlisteNg.Shared;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace FutterlisteNg.Api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private static readonly ILog s_logger = LogManager.GetLogger(typeof(UserController));
        
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IEnumerable<UserViewModel> Get()
        {
            return _userService.All().Select(u => new UserViewModel(u.Name, u.ShortName));
        }

        [HttpPost]
        public void Create(UserViewModel toCreate)
        {
            s_logger.Info("Creating user " + toCreate);
            var user = new User(toCreate.Name, toCreate.ShortName);
            _userService.Add(user);
        }
    }
}

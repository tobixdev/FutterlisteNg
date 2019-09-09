using System.Collections.Generic;
using System.Linq;
using FutterlisteNg.Domain.Services;
using FutterlisteNg.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FutterlisteNg.Api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
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
    }
}

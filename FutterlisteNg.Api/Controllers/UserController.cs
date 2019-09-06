using System.Collections.Generic;
using FutterlisteNg.Domain.Model;
using FutterlisteNg.Domain.Services;
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
        public IEnumerable<User> Get()
        {
            return _userService.All;
        }
    }
}

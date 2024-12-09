using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderManagementData.Entities.Identity;
using OrderManagementServices.Dto.UserDto;
using OrderManagementServices.Services.UserServices;

namespace OrderManagementApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserServices _userServices;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IUserServices userServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userServices = userServices;
        }

        [HttpPost]
        [Route("api/Register")]
        public async Task<ActionResult<RegisterDto>> Register(RegisterDto input)
        {
            var user = await _userServices.Register(input);

            if (user is null)
            {
                return BadRequest("Email already Exists");
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("api/Login")]
        public async Task<ActionResult<string>> Login(LoginDto input)
        {

            var user = await _userServices.Login(input);

            if (user == null)
            {
                return Unauthorized("E-Mail or password is not correct");
            }

            return Ok(user);
        }

    }
}

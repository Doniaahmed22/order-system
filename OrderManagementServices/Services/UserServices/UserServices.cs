using Microsoft.AspNetCore.Identity;
using OrderManagementData.Entities.Identity;
using OrderManagementServices.Dto.UserDto;
using OrderManagementServices.Services.TokenServices;
using System.Security.Claims;


namespace OrderManagementServices.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenServices _tokenServices;

        public UserServices(UserManager<User> userManager, SignInManager<User> signInManager, ITokenServices tokenServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
        }


        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return null;
            }

            var token = _tokenServices.GenerateToken(user);
            
            return token;

        }


        public async Task<RegisterDto> Register(RegisterDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user is not null)
            {
                return null;
            }

            var appUser = new User
            {
                Email = input.Email,
                UserName = input.Username,
                Role = input.Role
            };

            var result = await _userManager.CreateAsync(appUser, input.Password);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).FirstOrDefault());
            }         

            return new RegisterDto
            {
                Email = input.Email,
                Username = input.Username,
                Role = input.Role
            };

        }
       

    }
}

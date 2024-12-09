using Microsoft.AspNetCore.Identity;
using OrderManagementData.Entities.Identity;
using OrderManagementServices.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.Services.UserServices
{
    public interface IUserServices
    {
        Task<string> Login(LoginDto loginDto);


        Task<RegisterDto> Register(RegisterDto input);

    }
}

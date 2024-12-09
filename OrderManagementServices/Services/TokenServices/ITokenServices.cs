using OrderManagementData.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.Services.TokenServices
{
    public interface ITokenServices
    {
        string GenerateToken(User user);

    }
}

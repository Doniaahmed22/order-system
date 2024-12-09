using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementServices.Services.EmailServices
{
    public interface IEmailServices
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}

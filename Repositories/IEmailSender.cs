using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toAddress, string subject, string body);
    }

}

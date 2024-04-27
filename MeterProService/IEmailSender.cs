using MeterProService.Models;
using System;
using System.Threading.Tasks;
using WebApi.Models;
namespace SendingEmail
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailModel emailModel,Trip tripDetail);
    }
}


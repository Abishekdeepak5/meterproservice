
using Microsoft.AspNetCore.Mvc;
using SendingEmail;
using WebApi.Models;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    
    public class EmailController :ControllerBase
    {
        private readonly IEmailSender _emailSender;
        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost]
        public  async Task<IActionResult> SendEmail([FromForm] EmailModel emailModel)
        {
            try
            {
                await _emailSender.SendEmailAsync(emailModel ,new());

                     return  Ok("Email send successfully");
            }
            catch(Exception ex) 
            {

                return BadRequest("email not sended");
            }
        }



    }
}

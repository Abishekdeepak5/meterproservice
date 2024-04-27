
using MeterProService.Data;
using MeterProService.DTO;
using MeterProService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendingEmail;
using WebApi.Models;

namespace MeterProService.Controllers
{
    //public class ImageController
    //{
    //}
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        private readonly MeterproDbContext _meterproDbContext;
        private readonly IEmailSender _emailSender;
        public ImageController(MeterproDbContext _meterproDbContext,IEmailSender _emailSender)
        {
            this._meterproDbContext = _meterproDbContext;
            this._emailSender = _emailSender;
        }
        [HttpGet("GetImage/{tripId?}")]
        public IActionResult GetImage(int? tripId)
        {
            try
            {
                Trip imageEntity = _meterproDbContext.Trip.FirstOrDefault(i => i.id == tripId);

                if (imageEntity == null)
                {
                    return NotFound();
                }
                  return File(imageEntity.ImageData, "image/jpeg"); 
            }
            catch(Exception ex)
            {
                return NotFound($"{ex.Message}");
            }
            // Assume you have an Entity Framework DbContext called ApplicationDbContext
        }

        [HttpPost("ReceiveScreenshot/{tripId?}")]
        [Authorize]
        public async Task<IActionResult> ReceiveScreenshot([FromForm] FileUploadDto file, int? tripId)
        {
            var userIdClaims = User.FindFirst("id");
            var id = userIdClaims?.Value;
            User_detail userDetail = _meterproDbContext.User_detail.FirstOrDefault(user => user.id.ToString() == id);

            if (file.image != null && file.image.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await file.image.CopyToAsync(ms);
                    byte[] imageBytes = ms.ToArray();

                    // Save the image to the database
                    SaveImageToDatabase(imageBytes, tripId,userDetail);
                    // call the email controller
                    
                    return Ok(imageBytes);
                }
            }

            return BadRequest("Invalid image file");
        }

        [HttpPost("RecScreenshot/{tripId?}")]
        [Authorize]
        public async Task<IActionResult> RecScreenshot([FromForm] TripImageDto emailDto , int? tripId)
        {
            try
            {
                var userIdClaims = User.FindFirst("id");
                var id = userIdClaims?.Value;
                User_detail userDetail = _meterproDbContext.User_detail.FirstOrDefault(user => user.id.ToString() == id);
                EmailModel emailModel= new EmailModel();
                emailModel.formFile = emailDto.formFile;
                emailModel.Message = emailDto.Message;
                emailModel.Subject = emailDto.Subject;
                emailModel.Email = userDetail.email;
                emailModel.userId = userDetail.id.ToString();
                var file = emailModel.formFile;
                if (file != null && file.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        byte[] imageBytes = ms.ToArray();

                        // Save the image to the database
                        Trip trip = SaveImageToDatabase(imageBytes, tripId, userDetail);
                        // call the email controller
                        await _emailSender.SendEmailAsync(emailModel,trip);
                        return Ok(imageBytes);
                    }
                }

                return BadRequest("Invalid image file");
            }
            catch(Exception )
            {
                throw;
            }
        }

        private Trip SaveImageToDatabase(byte[] imageBytes,int? tripId, User_detail userDetail)
        {
            try
            {
                var existingProduct = _meterproDbContext.Trip.FirstOrDefault(trip => trip.userId == userDetail.id && trip.id == tripId);
                var context = _meterproDbContext;
                
                 existingProduct.ImageData = imageBytes;
                context.SaveChanges();
                
                return existingProduct;
            }
            catch(Exception )
            {
                throw ;
            }
        }
        
    }
}

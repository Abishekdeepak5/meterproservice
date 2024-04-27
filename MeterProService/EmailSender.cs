using MailKit.Net.Smtp;
using MeterProService.Data;
using MeterProService.Models;
using MimeKit;
using SendingEmail;
using System;
using System.IO;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.View.EmailTemplate;

public class EmailSender : IEmailSender
{

    private readonly MeterproDbContext _meterproDbContext;
    public EmailSender(MeterproDbContext meterproDbContext)
    {
        _meterproDbContext = meterproDbContext;
    }
    public async Task SendEmailAsync(EmailModel emailModel,Trip tripDetail)
    {
        string email = emailModel.Email;
        string subject = emailModel.Subject;
        string body = emailModel.Message;
        string userId = emailModel.userId;
        var image = emailModel.formFile;


        var senderEmail = "aathisnr123@gmail.com";
        var password = "ynne zgez irkp jguw";

        try
        {
            // Load the HTML template
            string htmlTemplate = EmailTemplates.ProfessionalEmailTemplate;
            
            string fromAddress = tripDetail.startLocation;
            string toAddress = tripDetail.endLocation;
             string startTime = tripDetail.startTime.Value.TimeOfDay.ToString(@"hh\:mm");
            string  endTime = tripDetail.endTime.Value.TimeOfDay.ToString(@"hh\:mm");
             Console.WriteLine(startTime + "\n" + endTime);
            var cabDriver = getCabDriver(tripDetail.userId);
            string cabDriverName = cabDriver.first_name;

            var cab = getCab(tripDetail.cabId);
            string  cabNumber = cab.carNumber;

            string  noon = getTheNoon(tripDetail.startTime.Value.Hour);

           string totalCharge = Math.Round((double)tripDetail.price,2).ToString();
            
            string tripCharge = tripDetail.price.ToString();
            string subTotal = 0.ToString();
            string travelTime = getTravelTime(tripDetail);
            string distanceTraveled = tripDetail.miles.ToString();
            // Replace placeholders with actual values
            htmlTemplate = htmlTemplate.Replace("{SenderEmail}", senderEmail)
                                       .Replace("{Subject}", subject)
                                       .Replace("{Body}", body)
                                        .Replace("{fromAddress}", fromAddress)
                                        .Replace("{toAddress}", toAddress)
                                        .Replace("{startTime}", startTime)
                                        .Replace("{endTime}", endTime)
                                         .Replace("{cabDriverName}", cabDriverName)
                                         .Replace("{noon}", noon)
                                         .Replace("{totalCharge}",totalCharge)
                                         .Replace("{tripCharge}",tripCharge)
                                         .Replace("{subTotal}",subTotal)
                                         .Replace("{travelTime}",travelTime)
                                         .Replace("{distanceTraveled}",distanceTraveled)
                                         .Replace("{cabNumber}", cabNumber);
                                        

            // Embedding Images in C# Code
            var mapImagePart = new MimePart("image", "png")
            {
                ContentDisposition = new ContentDisposition(ContentDisposition.Inline),
                ContentTransferEncoding = ContentEncoding.Base64,
            };

            var carImagePart = new MimePart("image", "png")
            {
                ContentDisposition = new ContentDisposition(ContentDisposition.Inline),
                ContentTransferEncoding = ContentEncoding.Base64,
            };

            
            byte[] mapImageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                await emailModel.formFile.CopyToAsync(ms);
                mapImageBytes = ms.ToArray();
            }
            mapImagePart.Content = new MimeContent(new MemoryStream(mapImageBytes));
            mapImagePart.ContentId = "MapImage"; 

            

            
            htmlTemplate = htmlTemplate
                             
                        .Replace("src=\"cid:MapImage\"", $"src=\"cid:{mapImagePart.ContentId}\"");
            

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your Company", senderEmail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            
            var multipart = new Multipart("mixed");
            multipart.Add(new TextPart("html") { Text = htmlTemplate });
            multipart.Add(mapImagePart);
            //multipart.Add(carImagePart);

            // Set the multipart as the body of the message
            message.Body = multipart;

            // Send the email using MailKit
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 465, true);
                smtpClient.Authenticate(senderEmail, password);
                await smtpClient.SendAsync(message);
                smtpClient.Disconnect(true);
            }

            Console.WriteLine("Email sent successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex}");
            throw;
        }
    }

    private string getTravelTime(Trip tripDetail)
    {
        TimeSpan difference = (tripDetail.endTime.Value -  tripDetail.startTime.Value);
        int totalMinutes = (int)difference.TotalMinutes;
        int hours = totalMinutes / 60;
        int remainingMinutes = totalMinutes % 60;
        String result = remainingMinutes + " min";
        if (hours == 0)
            return result;
        return hours + " hr : " + result; 

    }

    private string getTheNoon(int hour)
    {
        if (hour >= 5 && hour < 12)
        {
            return "Morning";
        }
        else if (hour >= 12 && hour < 17)
        {
            return "Afternoon";
        }
        else if (hour >= 17 && hour < 20)
        {
            return "Evening";
        }
        else
        {
            return "Night";
        }
    }

    

    private Cab getCab(int? cabId)
    {
        
        return  _meterproDbContext.Cab.FirstOrDefault(cab => cab.id == cabId);

    }

    private User_detail? getCabDriver(int? userId)
    {
        try
        {
            
            var user = _meterproDbContext.User_detail.FirstOrDefault(user => user.id == userId);

            return user;
        }
        catch(Exception )
        {
            throw;
        }
    }


}

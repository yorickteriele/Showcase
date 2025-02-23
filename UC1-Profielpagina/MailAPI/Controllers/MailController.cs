using System.Net;
using System.Net.Mail;
using MailAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ShowcaseAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MailController : ControllerBase
{
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    public MailController(IConfiguration configuration)
    {
        _smtpUsername = configuration["SMTP:Username"] 
            ?? throw new ArgumentNullException("SMTP username is missing!");
        _smtpPassword = configuration["SMTP:Password"] 
            ?? throw new ArgumentNullException("SMTP password is missing!");
    }

    [HttpPost]
    public async Task<IActionResult> SendMail([FromBody] ContactRequest request)
    {
        if (request == null)
            return BadRequest();

        try
        {
            var mail = new MailMessage
            {
                From = new MailAddress(request.Email),
                Subject = request.Subject,
                Body = $"Naam: {request.FirstName} {request.LastName}\nTelefoon: {request.Phone}\nBericht:\n{request.Message}",
                IsBodyHtml = false
            };
            mail.To.Add("yorick.teriele@outlook.com");

            using (var smtp = new SmtpClient("sandbox.smtp.mailtrap.io", 2525))
            {
                smtp.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }

            return Ok("Mail sent");
        }
        catch
        {
            return StatusCode(500, "Internal server error");
        }
    }
}

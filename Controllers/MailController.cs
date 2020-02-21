namespace MailClientService.Controllers {
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using MailClientService.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase {
        private readonly SmtpConfig _smtpConfig;
        private readonly SmtpAccount _smtpAccount;
        public MailController(IConfiguration config) {
            _smtpConfig = config.GetSection("Smtp").Get<SmtpConfig>();
            _smtpAccount = config.GetSection("SmtpAccount").Get<SmtpAccount>();
        }
        [HttpPost]
        public async Task<ActionResult> SendMessage(Message message) {
            var from = new MailAddress(_smtpAccount.Account, _smtpConfig.DisplayName);
            var to = new MailAddress(message.To);
            var mailMessage = new MailMessage(from, to);
            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Body;
            mailMessage.IsBodyHtml = true;
            var smtpClient = new SmtpClient(_smtpConfig.Host, _smtpConfig.Port);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_smtpAccount.Account, _smtpAccount.Password);
            try {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (SmtpException) {
                return BadRequest();
            }
            return Ok();
        }
    }
}
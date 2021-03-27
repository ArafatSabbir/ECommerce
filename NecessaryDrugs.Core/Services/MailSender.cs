using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Services
{
    public class MailSender
    {
		private readonly IConfiguration _configuration;
		private readonly SmtpClient _client;
		public MailSender(IConfiguration configuration)
        {
			_configuration = configuration;
			_client = new SmtpClient();
            _client.Connect(_configuration["Smtp:Host"],
						int.Parse(_configuration["Smtp:Port"]),
						bool.Parse( _configuration["Smtp:UseSSL"]));

			// Note: only needed if the SMTP server requires authentication
			_client.Authenticate(_configuration["Smtp:UserName"], _configuration["Smtp:Password"]);
		}
		public void Dispose()
        {
			_client.Disconnect(true);
			_client.Dispose();
        }
		public void Send(List<MailMessage> messages)
		{
			foreach(var message in messages)
            {
				var mail = new MimeMessage();
				mail.From.Add(new MailboxAddress(message.SenderName, message.Sender));
				mail.To.Add(new MailboxAddress(string.Empty, message.Receiver));
				mail.Subject = message.Subject;

				mail.Body = new TextPart("plain")
				{
					Text = message.Body
				};
				_client.Send(mail);
			}
			
		}
	}
}

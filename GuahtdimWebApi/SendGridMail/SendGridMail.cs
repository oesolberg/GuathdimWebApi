using System.Configuration;
using System.Net;
using System.Net.Mail;
using SendGrid;

namespace GuahtdimWebApi.SendGridMail
{
	public static class SendGridMail
	{
		private static string EmailReceiver="EmailReceiver";
		private static string EmailSender="EmailSender";
		private static string MailUsername = "MailUsername";
		private static string MailPassword= "MailPassword";
		private static string MailApiKey="MailApiKey";

		public static string GetUserName()
		{
			return GetSendGridMailUsername();
		}

		public static string GetPassword()
		{
			return GetSendGridMailPassword();
		}

		public static string GetApiKey()
		{
			return GetSendGridMailApiKey();
		}
		public static void SendEmptyReservoirMail()
		{
			var recipients = GetEmailRecipient();
			var mailSender = GetEmailSender();
			var subject = GetEmailSubject();
			var emailBody = GetEmailBody();

			var newMessage = new SendGridMessage {From = mailSender};
			newMessage.AddTo(recipients);
			newMessage.Text = emailBody;
			newMessage.Subject = subject;

			var username = GetSendGridMailUsername();
			var password = GetSendGridMailPassword();
			var credentials = new NetworkCredential(username, password);

			//var apiKey = GetSendGridMailApiKey();

			// Create an Web transport for sending email.
			var transportWeb = new Web(credentials);

			// Send the email, which returns an awaitable task.
			var res=transportWeb.DeliverAsync(newMessage);

		}

		private static string GetSendGridMailApiKey()
		{
			var apiKey = ConfigurationManager.AppSettings[MailApiKey];
			if (apiKey.Trim().Length < 3)
			{
				throw new SmtpException("No apikey found for sendgridemail");
			}
			return apiKey;
		}

		private static string GetSendGridMailUsername()
		{

			var username = ConfigurationManager.AppSettings[MailUsername];
			if (username.Trim().Length < 3)
			{
				throw new SmtpException("No username found for sendgridemail");
			}
			return username;
		}

		private static string GetSendGridMailPassword()
		{

			var password = ConfigurationManager.AppSettings[MailPassword];
			if (password.Trim().Length < 3)
			{
				throw new SmtpException("No password found for sendgridemail");
			}
			return password;
		}

		private static string GetEmailRecipient()
		{
			var emailAsString = ConfigurationManager.AppSettings[EmailReceiver];
			if (emailAsString.Trim().Length < 3)
			{
				emailAsString = "oesolberg@hotmail.com";
			}
			return emailAsString;
		}

		private static MailAddress GetEmailSender()
		{
			var emailAsString = ConfigurationManager.AppSettings[EmailSender];
			if (emailAsString.Trim().Length < 3)
			{
				emailAsString = "no-reply@guahtdimgarden.com";
			}
			return new MailAddress(emailAsString,"No-Reply at GuahtdimGardenWebApi");
		}

		private static string GetEmailSubject()
		{
			return "GuahtdimGarden - Tomt i reservoir";
		}

		private static string GetEmailBody()
		{
			return "Det er registrert tomt i reservoir. På tide å fylle opp igjen.";
		}
	}
}
//using System;
//using System.Net;
//using System.Net.Mail;
//using Nancy;
//using SendGrid;

//namespace GuahtdimWebApi.SendGridMail
//{
//	public class SendMailTestModule : NancyModule
//	{
//		public SendMailTestModule() : base("/mailtest")
//		{
//			Get["/"] = parameters =>
//			{
//				var stringToWrite = "";
//				try
//				{
//					stringToWrite += "<br/>" + SendGridMail.GetApiKey();
//					stringToWrite += "<br/>" + SendGridMail.GetPassword();
//					stringToWrite += "<br/>" + SendGridMail.GetUserName();

//					SendGridMail.SendEmptyReservoirMail();


//					SendGridMessage myMessage = new SendGridMessage();
//					myMessage.AddTo("oesolberg@gmail.com");
//					myMessage.From = new MailAddress("no-reply@gmail.com", "Odd Erik Solbergg");
//					myMessage.Subject = "Testing the SendGrid Library";
//					myMessage.Text = "Hello World!";

//					// Create credentials, specifying your user name and password.
//					var credentials = new NetworkCredential(SendGridMail.GetUserName(), SendGridMail.GetPassword());

//					// Create an Web transport for sending email.
//					var transportWeb = new Web(credentials);

//					// Send the email, which returns an awaitable task.
//					transportWeb.DeliverAsync(myMessage);


//				}
//				catch (Exception ex)
//				{
//					//return ex.Message;
//					stringToWrite += "<br/>" + ex.Message;
//				}
//				return stringToWrite;
//			};

//		}
//	}
//}
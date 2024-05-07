using MyKidsReg.Models;
using MyKidsReg.Repositories;
using System.Net;
using System.Net.Mail;

namespace MyKidsReg.Services
{
    public class CommunicationService
    {
        private readonly IUserRepository _rep;
    

        public CommunicationService(IUserRepository rep)
        {
            _rep = rep;
        }

        public void SendEmail(string emailAddress, string subject, string body)
        {
            using (var client = new SmtpClient("smtp.mail.outlook.com"))
            {
                client.Port = 587;
                client.Credentials = new NetworkCredential("MyKidsReg@outlook.com", "MitSkoleProjekt2");
                client.EnableSsl = true;

                var message = new MailMessage(


                    from: new MailAddress("MyKidsReg@outlook.com"),
                    to: new MailAddress(emailAddress))
                {
                    Subject = subject,
                    Body = body
                };
                try
                {
                    client.Send(message);
                    Console.WriteLine($"Email sent to {emailAddress} with subject '{subject}' and body: {body}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while sending email: {ex.Message}");
                }
            }
        }
        public void SendSMS(string phoneNumber, string message)
        {
            Console.WriteLine($" SMS sendt til {phoneNumber} med besked : {message}");
        }
        public void SendTemoraryPassword(string username, string temporaryPassword, string deliveryMethod)
        {
            var user = _rep.GetUserByUsername(username);
            if (user == null)
            {
                throw new Exception("Brugeren findes ikke ");

            }
            switch (deliveryMethod.ToLower())
            {
                case "email":
                    SendEmail(user.E_mail, "MidlerTidigt Email", $"Din midlertidige Adgangskode er :{temporaryPassword}");
                    break;
                case "sms":
                    if (user.Mobil_nr != 0)
                    {
                        string mobil_number = user.Mobil_nr.ToString();
                        SendSMS(mobil_number, $"Din midlertidigt adgangskode er :{temporaryPassword}");
                    }
                    else
                    {
                        throw new Exception("Mobilnummeret er ikke tilgængeligt");
                    }

                    break;
                default:
                    Console.WriteLine("Ugyldig leveringsmetod.");
                    break;
            }

        }
    }
}

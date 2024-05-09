using MyKidsReg.Models;
using MyKidsReg.Repositories;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyKidsReg.Services.CommunicationsServices
{
    public class CommunicationService
    {
        private readonly IUserRepository _rep;

        public CommunicationService(IUserRepository rep)
        {
            _rep = rep;
        }

        public async Task SendEmail(string emailAddress, string subject, string body)
        {
            using (var client = new SmtpClient("send.one.com"))
            {
                client.Port = 587; // Portnummer for StartTLS
                client.Credentials = new NetworkCredential("muhammed@cosan.dk", "TestSkole");
                client.EnableSsl = true; // Aktiver StartTLS

                var message = new MailMessage(
                    from: new MailAddress("muhammed@cosan.dk"),
                    to: new MailAddress(emailAddress))
                {
                    Subject = subject,
                    Body = body
                };

                try
                {
                    await client.SendMailAsync(message);
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

        public async Task SendTemporaryPassword(string username, string temporaryPassword, string deliveryMethod)
        {
            Console.WriteLine("Sending temporary password...");
            var user = await _rep.GetUserByUsername(username);
            if (user == null)
            {
                throw new Exception("Brugeren findes ikke ");
            }

            switch (deliveryMethod.ToLower())
            {
                case "email":
                    DateTime dateTime = DateTime.Now;
                    await SendEmail(user.E_mail, "Midlertidigt Email", $"Din midlertidige Adgangskode til MyKidsReg er: {temporaryPassword} , /n du har 5 timer til at ændre din adgangskode fra nu : {dateTime}");
                    break;
                case "sms":
                    if (user.Mobil_nr != 0)
                    {
                        string mobilNumber = user.Mobil_nr.ToString();
                        SendSMS(mobilNumber, $"Din midlertidige adgangskode er: {temporaryPassword}");
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

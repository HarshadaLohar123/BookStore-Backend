using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Service
{
    public class EmailService
    {
        public static void SendMail(string email, string token)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("harshalohar12345@gmail.com", "aufipdjsusdmygjk");

                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(email);
                msgObj.From = new MailAddress("harshalohar12345@gmail.com");
                msgObj.Subject = "Password Reset Link";
                // msgObj.Body= $"www.fundooNote.com/reset-password/{token}";
                msgObj.Body = "<html><body><p><b>Hello </b>,<br/>click the below link for reset password.<br/>" +
                    $"http://localhost:4200/reset-password/{token}" +
                    "</p></body></html>";
                client.Send(msgObj);
            }

        }
    }
}
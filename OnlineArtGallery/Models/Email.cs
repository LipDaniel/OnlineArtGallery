using System.Net.Mail;
using System.Net;
using System.Collections.Generic;

namespace OnlineArtGallery.Models
{
    public class Email
    {
        public string MailFrom { get; set; }
        public List<string> MailTo { get; set; }
        public MailMessage MessageBody { get; set; }
        public SmtpClient Client { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HostMail { get; set; }
        public int PortMail { get; set; }
        public bool SSL { get; set; }

        public Email()
        {
            MailFrom = "vxp20xx@gmail.com";
            MailTo = new List<string>();
            MessageBody = new MailMessage();
            SSL = true;
            PortMail = 25;
            HostMail = "smtp.gmail.com";//or another email sender provider
        }

        public string SendMailCredential()
        {
            return SendEmail();
        }

        public string SendMailCredential(string toEmail)
        {
            MailFrom = "vxp20xx@gmail.com";
            MailTo.Add(toEmail);
            UserName = "vxp20xx@gmail.com";
            Password = "lsahxzfrluhhhfue";
            MessageBody.Subject = "Activate Email";
            MessageBody.Priority = MailPriority.High;
            MessageBody.IsBodyHtml = true;

            var bodyFormat = "<h1>Register Successful.</h1> <br/> " +
                "<h3>Your account is: {0}</h3> <br/>" +
                "<p>Thanks for your interest in joining XXXX XXXX XXXX! To complete your registration, we need you to verify your email address.</p>" +
                "<p>This is Email verify account. Please click link below to verify your account</p>";

            MessageBody.Body = string.Format(bodyFormat, toEmail);

            return SendEmail();
        }

        private string SendEmail()
        {
            try
            {
                var connectAccess = new SmtpPermission(SmtpAccess.Connect);
                Client = new SmtpClient(HostMail, PortMail)
                {
                    EnableSsl = SSL,
                    Credentials = new NetworkCredential(UserName, Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 500000
                };

                MessageBody.Sender = new MailAddress(MailFrom);
                MessageBody.From = new MailAddress(MailFrom);
                foreach (string item in MailTo)
                {
                    MessageBody.To.Add(new MailAddress(item));
                }

                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain,
                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                Client.UseDefaultCredentials = false;
                Client.EnableSsl = true;
                Client.Send(MessageBody);

                Client.SendAsyncCancel();
                MessageBody.Dispose();
                return "Success";
            }

            catch (System.Net.Mail.SmtpException ex)
            {
                string err = ex.Message;
                return "Failed";
            }
        }
    }
}

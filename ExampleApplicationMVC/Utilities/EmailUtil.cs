using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ExampleApplicationMVC.Utilities
{
    public class EmailUtil : IEmailUtil
    {
        private static readonly string _Email = ConfigurationManager.AppSettings["hotmailEmail"];
        private static readonly string _Pass = ConfigurationManager.AppSettings["emailPass"];
        private static readonly string _ToEmail = ConfigurationManager.AppSettings["FatalErrorEmailAddress"];
        private static readonly string _VirusTotal = "scan@virustotal.com";

        private MailMessage mailMsg;
        private SmtpClient smtpClient;
        
        public void SendEmail(String message)
        {
            try
            {
                Init();

                mailMsg.To.Add(_ToEmail);
                mailMsg.Subject = "ApplicationServiceEmail";

                mailMsg.Body = message;

                smtpClient.Send(mailMsg);

                mailMsg.Dispose();
                smtpClient.Dispose();
            }
            catch (Exception e)
            {
                ErrorProcess(e, message);
            }
        }

        public void SendEmail(Exception exception, String message)
        {
            try
            {
                Init();

                mailMsg.To.Add(_ToEmail);
                mailMsg.Subject = "ApplicationServiceError";

                mailMsg.Body = message +
                 Environment.NewLine +
                 exception.Message +
                 Environment.NewLine +
                 exception.StackTrace;

                smtpClient.Send(mailMsg);

                mailMsg.Dispose();
                smtpClient.Dispose();
            }
            catch (Exception)
            {
                ErrorProcess(exception, message);
            }
        }

        public void SendEmail(string subject, string message, string filePath)
        {
            try
            {
                Init();

                if (File.Exists(filePath))
                {
                    mailMsg.To.Add(_VirusTotal);
                    mailMsg.Subject = "SCAN";

                    var attachment = new Attachment(filePath);
                    mailMsg.Attachments.Add(attachment);
                }
                else
                {
                    mailMsg.To.Add(_ToEmail);
                    mailMsg.Subject = subject;

                    mailMsg.Body += Environment.NewLine + Path.GetFileName(filePath)
                                + Environment.NewLine + "WAS NOT FOUND!!!";
                }

                mailMsg.Body = message;

                smtpClient.Send(mailMsg);

                mailMsg.Dispose();
                smtpClient.Dispose();
            }
            catch (Exception e)
            {
                ErrorProcess(e, message);
            }
        }
        private void Init()
        {
            mailMsg = new MailMessage();
            smtpClient = new SmtpClient();

            smtpClient.Host = ConfigurationManager.AppSettings["smtpHost"];
            smtpClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["smtpSsl"]);
            smtpClient.Port = Convert.ToInt16(ConfigurationManager.AppSettings["smtpPort"]);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential(_Email, _Pass);

            mailMsg.From = new MailAddress(_Email);
            mailMsg.IsBodyHtml = false;
        }

        private void ErrorProcess(Exception error, string message)
        {
            Init();

            mailMsg.To.Add(_ToEmail);
            mailMsg.Subject = "ServiceEmailError";

            mailMsg.Body = message +
             Environment.NewLine +
             error.Message +
             Environment.NewLine +
             error.StackTrace;

            smtpClient.Send(mailMsg);

            mailMsg.Dispose();
            smtpClient.Dispose();
        }
    }
}
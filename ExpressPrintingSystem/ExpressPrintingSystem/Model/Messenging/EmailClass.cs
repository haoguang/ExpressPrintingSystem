using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ExpressPrintingSystem.Model.Messenging
{
    public class EmailClass
    {

        private string senderEmail;
        private List<string> receiverEmail;
        private string subject;
        private string content;
        private bool isHtmlEnabled;

        public EmailClass(string senderEmail, List<string> receiverEmail, string subject, string content, bool isHtmlEnabled)
        {
            this.senderEmail = senderEmail;
            this.receiverEmail = receiverEmail;
            this.subject = subject;
            this.content = content;
            this.isHtmlEnabled = isHtmlEnabled;
        }

        public EmailClass(string senderEmail, string receiverEmail, string subject, string content, bool isHtmlEnabled)
        {
            this.senderEmail = senderEmail;
            this.receiverEmail = new List<string>();
            this.receiverEmail.Add(receiverEmail);
            this.subject = subject;
            this.content = content;
            this.isHtmlEnabled = isHtmlEnabled;
        }

        public string SenderEmail
        {
            get { return senderEmail; }
            set { senderEmail = value; }
        }

        public List<string> ReceiverEmail
        {
            get { return receiverEmail; }
            set { receiverEmail = value; }
        }

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public string Content
        {
            get { return content;  }
            set { content = value; }
        }

        public bool IsHtmlEnabled
        {
            get { return isHtmlEnabled; }
            set { isHtmlEnabled = value; }
        }


        public void sendEmail(string user, string pass, string option)
        {
            SmtpClient SmtpServer = new SmtpClient(option);
            var mail = new MailMessage();
            mail.From = new MailAddress(senderEmail);

            for(int i=0; i<receiverEmail.Count; i++)
            {
                mail.To.Add(receiverEmail[i]);
            }
            
            mail.Subject = subject;
            mail.IsBodyHtml = isHtmlEnabled;
            string htmlBody;
            htmlBody = content;
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential(user, pass); ;
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }


        public const string STMP_HOTMAIL = "smtp.live.com";
        public const string STMP_GMAIL = "smtp.gmail.com";
    }
}
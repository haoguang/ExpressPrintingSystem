using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ExpressPrintingSystem.Model.Messenging
{
    public class EmailClass
    {

        private List<string> receiverEmail;
        private string subject;
        private string content;
        private bool isHtmlEnabled;

        public EmailClass(List<string> receiverEmail, string subject, string content, bool isHtmlEnabled)
        {
            this.receiverEmail = receiverEmail;
            this.subject = subject;
            this.content = content;
            this.isHtmlEnabled = isHtmlEnabled;
        }

        public EmailClass(string receiverEmail, string subject, string content, bool isHtmlEnabled)
        {
            this.receiverEmail = new List<string>();
            this.receiverEmail.Add(receiverEmail);
            this.subject = subject;
            this.content = content;
            this.isHtmlEnabled = isHtmlEnabled;
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

        public static bool isCredentialed()
        {
            return HttpContext.Current.Session["EmailCredential"] != null;
        }

        public static void generateCredential(string user, string pass, string stmpClient)
        {
            EmailCredential emailCredential = new EmailCredential(new NetworkCredential(user, pass), stmpClient);

            HttpContext.Current.Session["EmailCredential"] = emailCredential;
            
        }

        public static string getStmpClient(string emailProvider)
        {
            switch (emailProvider)
            {
                case PROVIDER_HOTMAIL:
                    return STMP_HOTMAIL;
                case PROVIDER_GMAIL:
                    return STMP_GMAIL;
                case PROVIDER_YAHOO:
                    return STMP_YAHOO;
                case PROVIDER_YAHOO_PLUS:
                    return STMP_YAHOO_PLUS;
                case PROVIDER_YAHOO_UK:
                    return STMP_YAHOO_UK;
                case PROVIDER_OFFICE365:
                    return STMP_OFFICE365;
                default:
                    return null;
            }
        }

        public static string getProviderName(string email)
        {
            if (email.ToLower().Contains("hotmail")|| email.ToLower().Contains("live") || email.ToLower().Contains("outlook"))
            {
                return PROVIDER_HOTMAIL;
            }else if (email.ToLower().Contains("gmail"))
            {
                return PROVIDER_GMAIL;
            }
            else if (email.ToLower().Contains("yahoo"))
            {
                return PROVIDER_YAHOO;
            }
            else if (email.ToLower().Contains("yahoo.com.uk"))
            {
                return PROVIDER_YAHOO_UK;
            }
            else if (email.ToLower().Contains("office365"))
            {
                return PROVIDER_OFFICE365;
            }
            else
            {
                return null;
            }


    }


        public bool sendEmail(EmailCredential emailCredential)
        {
            if(emailCredential != null)
            {
                

                var emailProvider = (JObject)JsonConvert.DeserializeObject(emailCredential.STMPClient);

                SmtpClient SmtpServer = new SmtpClient(emailProvider["server"].Value<string>());
                var mail = new MailMessage();
                mail.From = new MailAddress(emailCredential.Credential.UserName);

                for (int i = 0; i < receiverEmail.Count; i++)
                {
                    mail.To.Add(receiverEmail[i]);
                }

                mail.Subject = subject;
                mail.IsBodyHtml = isHtmlEnabled;
                string htmlBody;
                htmlBody = content;
                mail.Body = htmlBody;
                SmtpServer.Port = emailProvider["port"].Value<int>();
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = emailCredential.Credential;
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public static string populateActivationEmail(string companyName, string activationLink)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Model/Messenging/EmailTemplates/StaffActivationEmail.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{CompanyName}", companyName);
            body = body.Replace("{ActivationLink}", activationLink);

            return body;
        }


        public const string STMP_HOTMAIL = "{ \"name\":\"Hotmail\", \"server\":\"smtp.live.com\", \"port\":587 }";
        public const string STMP_GMAIL = "{ \"name\":\"Gmail\", \"server\":\"smtp.gmail.com\", \"port\":587 }";
        public const string STMP_YAHOO = "{ \"name\":\"Yahoo\", \"server\":\"smtp.mail.yahoo.com\", \"port\":465 }";
        public const string STMP_YAHOO_PLUS = "{ \"name\":\"Yahoo Plus\", \"server\":\"plus.smtp.mail.yahoo.com\", \"port\":465 }";
        public const string STMP_YAHOO_UK = "{ \"name\":\"Yahoo UK\", \"server\":\"smtp.mail.yahoo.co.uk\", \"port\":465 }";
        public const string STMP_OFFICE365 = "{ \"name\":\"OFFICE365\", \"server\":\"smtp.office365.com\", \"port\":587 }";

        public const string PROVIDER_HOTMAIL = "microsoft";
        public const string PROVIDER_GMAIL = "google";
        public const string PROVIDER_YAHOO = "yahoo";
        public const string PROVIDER_YAHOO_PLUS = "yahoo plus";
        public const string PROVIDER_YAHOO_UK = "yahoo uk";
        public const string PROVIDER_OFFICE365 = "office365";

    }

    public class EmailCredential
    {
        private NetworkCredential emailCredential;
        private string stmpClient;

        public EmailCredential(NetworkCredential emailCredential, string stmpClient)
        {
            this.emailCredential = emailCredential;
            this.stmpClient = stmpClient;
        }

        public NetworkCredential Credential
        {
            get { return emailCredential; }
            set { emailCredential = value; }
        }

        public string STMPClient
        {
            get { return stmpClient; }
            set { stmpClient = value; }
        }
    }


}
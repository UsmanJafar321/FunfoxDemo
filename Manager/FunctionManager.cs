using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Manager
{
    public class FunctionManager
    {
        public static string GetRandomNumber(int length)
        {

            var rnd = new Random();
            const string charPool = "1234567890";
            var rs = new StringBuilder();

            while (length-- > 0)
                rs.Append(charPool[(int)(rnd.NextDouble() * charPool.Length)]);

            return rs.ToString();
        }
        public static string FirstCharToUpper(string input)
        {
            string result = string.Empty;
            if (!String.IsNullOrEmpty(input))
            {
                input = input.ToLower();
                result =  input.First().ToString().ToUpper() + input.Substring(1);
            }
            return result;
        }
        public static DateTime DateTimeNow()
        {
            TimeZoneInfo pakTime = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
            DateTime curDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, pakTime);
            return curDate;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string ToSubString(string title,int number=0)
        {
            string result = string.Empty;
            result = title;
            if (!string.IsNullOrEmpty(title))
            {
                if (number>0)
                {
                    if (title.Length>number)
                    {
                        result=title.Substring(0, number);
                    }
                }
            }
            return result;
        }
        public static bool SendEmail(ref EmailMessage emailmessage,String DisplayName)
        {

            MailMessage mailmessage = new MailMessage();

            if (emailmessage.FromName != string.Empty)
            {
                try
                {
                    emailmessage.FromEmail = emailmessage.FromName + " <" + emailmessage.FromEmail + ">";

                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }

            }
            mailmessage.From = new MailAddress(emailmessage.FromEmail,DisplayName);

            foreach (string mailto in emailmessage.MailTo)
            {
                if (mailto != string.Empty)
                {
                    mailmessage.To.Add(new MailAddress(mailto));
                }
            }
            //cc email
            if (emailmessage.CCEmail != null)
            {
                foreach (string mailtocc in emailmessage.CCEmail)
                {
                    if (mailtocc != string.Empty)
                    {
                        mailmessage.CC.Add(new MailAddress(mailtocc));
                    }
                }
            }
            //bcc email
            if (emailmessage.BCCEmail != null)
            {
                foreach (string mailtobcc in emailmessage.BCCEmail)
                {
                    if (mailtobcc != string.Empty)
                    {
                        mailmessage.Bcc.Add(new MailAddress(mailtobcc));
                    }
                }
            }
            bool emailEnable =Convert.ToBoolean(ConfigurationSettings.AppSettings["IsEnableEmail"]); 
            mailmessage.Subject = emailmessage.Subject;
            mailmessage.Body = emailmessage.Body;
            mailmessage.IsBodyHtml = true;
            string smtp = ConfigurationSettings.AppSettings["Host"].ToString(); 
            SmtpClient smtpmail = new SmtpClient(smtp);
            smtpmail.UseDefaultCredentials = false;
            smtpmail.Port = Convert.ToInt32(ConfigurationSettings.AppSettings["Port"].ToString());
            string username = ConfigurationSettings.AppSettings["Username"].ToString();
            string password = ConfigurationSettings.AppSettings["Password"].ToString();
            smtpmail.Credentials = new System.Net.NetworkCredential(username.ToString(), password.ToString());
            smtpmail.EnableSsl = false;
            try
            {
                if (emailEnable)
                {
                    smtpmail.Send(mailmessage);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return true;
        }

    }
}

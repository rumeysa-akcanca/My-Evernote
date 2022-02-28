using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Common.Helper
{
    
    
        public class MailHelper
        {
            public static bool SendMail(string body, string to, string subject, bool isHtml = true)
            {
                return SendMail(body, new List<string> { to }, subject, isHtml);
            }

            public static bool SendMail(string body, List<string> to, string subject, bool isHtml = true)
            {
              //ıshtml body mi gönderiyorum yoksa basit bir metin mi
                bool result = false;

                try
                {
                    var message = new MailMessage();
                    message.From = new MailAddress(ConfigHelper.Get<string>("MailUser"));

                    to.ForEach(x =>
                    {
                        //to kısmına yazdığımız adresler tek tek eklenecek
                        message.To.Add(new MailAddress(x));
                    });

                    message.Subject = subject;//konu
                    message.Body = body;//gövde
                    message.IsBodyHtml = isHtml;

                    using (var smtp = new SmtpClient(
                        ConfigHelper.Get<string>("MailHost"),//hangi sunucu
                        ConfigHelper.Get<int>("MailPort")))//hangi port
                    {
                        smtp.EnableSsl = true;//ssl aktifleştirme
                        smtp.Credentials =
                            new NetworkCredential(
                                ConfigHelper.Get<string>("MailUser"),//git web.Config AppSettingden getir
                                ConfigHelper.Get<string>("MailPass"));
                       //Credentials = kimlik bilgileri

                        smtp.Send(message);//mail mesaj gönderimi
                        result = true;
                    }
                }
                catch (Exception)
                {

                }

                return result;
            }
        }
    
}

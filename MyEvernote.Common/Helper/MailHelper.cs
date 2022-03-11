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
          //Kodlar sabit
            public static bool SendMail(string body, string to, string subject, bool isHtml = true)
            {
              //tek bir adres alam metot ama aşağıdaki metodu kulllanarak mail göndermeyi yapıyor
                return SendMail(body, new List<string> { to }, subject, isHtml);
            }

            public static bool SendMail(string body, List<string> to, string subject, bool isHtml = true)
            {
              //liststring to : mesaj kimlere atılacak listesi
              //ıshtml body mi gönderiyorum yoksa basit bir metin mi
                bool result = false;

                try
                {
                    var message = new MailMessage();
                    message.From = new MailAddress(ConfigHelper.Get<string>("MailUser"));//mail adresi tanımlama
                    //ConfigHelper' a git  get metotuna string olarak okuyacaksın mailUser(Web config'deki anahtar kelime)' oku

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
                                ConfigHelper.Get<string>("MailUser"),//git web.Config AppSettingden getir diye set ediuoruz
                                ConfigHelper.Get<string>("MailPass"));
                       //Credentials = kimlik bilgileri

                        smtp.Send(message);//mail mesaj gönderimi
                        result = true;//başarılı olursa geriye true dönecek
                    }
                }
                catch (Exception)
                {
                  // eğer bir hata olursa catch'e düşüp geriye  result false dönecek
                }

                return result;
            }
        }
    
}

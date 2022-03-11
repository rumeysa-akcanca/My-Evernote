using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Common.Helper
{
  public  class ConfigHelper
    {
        public static  T Get<T>(string key)
        {//178.7.14
            //bir şeyler okuyup bize tür tip dönüşümü sağlayan class
            //static get metoduyla verilen bir anahtar değeri configuration/appsetting içinde okuyup
            //değerini geri döndürecek
            //appsetting bir collection
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
            //Bne ne tip verirsem onu döndürsün o yüzden generic yapıyoruz
            //changetype metot: ilk değeri sonra dönüştürülecek tipi ister(tip T)
            //changetype geriye obje döndürüyomuş ama bizim metot T döndürüyor  o yüzden (T)' ye cast et
            //Sonuc appsettingden aldığım değeri bu metotu çağırırkem verdiğim neyse o tipe dönüştür ve bana geri döndür
            //port numarası okurken integer alıcam, string ise string..
        }
    }
}
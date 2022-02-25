using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
   public class BusinessLayerResult<T> where T:class
    {
        public List<string> Errors { get; set; } //hata mesajlarını saklayan bir list
        public T Result { get; set; } //verilen tipten result isimli property

        public BusinessLayerResult()
        {
            Errors = new List<string>(); //içine eklenebilir halde dursun
        }
    }
}

using MyEvernote.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
   public class BusinessLayerResult<T> where T:class
    {
        public List<ErrorMessageObject> Errors { get; set; } //hata mesajlarını saklayan bir list
        public T Result { get; set; } //verilen tipten result isimli property

        public BusinessLayerResult()
        {
            Errors = new List<ErrorMessageObject>(); //içine eklenebilir halde dursun
        }
        public void AddError(ErrorMessageCode code,string message)
        {
            //kolay insert yapabilmek için
            Errors.Add(new ErrorMessageObject() {Code = code, Message = message });
        }
    }
}

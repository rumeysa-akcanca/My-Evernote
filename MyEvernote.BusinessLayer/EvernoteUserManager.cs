using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    class EvernoteUserManager
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();
        public EvernoteUser RegisterUser(RegisterViewModel data )
        {
            //kullanıcı username kontrolü 
            // kullanıcı e-posta kontrolü
            //Kayıt işlemi
            // Aktivasyon e-postası gönderimi
           EvernoteUser user = repo_user.Find(x => x.Username == data.Username || x.Email == data.Email);
            if (user !=null)
            {
                // user null değilse eşleşme geldi kullanıcı adı yada email kullanılıyor demek
                //kayıt işlemi devam edemez burda bir hata var //geriye ne dönecek
                throw new Exception("Kayıtlı kullanıcı ya da e-posta adresi");
                //bu hatayı homecontrollerda yakalamalıyız
                
            }
           
        }
    }
}

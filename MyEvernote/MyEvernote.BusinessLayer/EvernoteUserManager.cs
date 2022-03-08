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
   public class EvernoteUserManager
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();
        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data )
        {
            //kullanıcı username kontrolü 
            // kullanıcı e-posta kontrolü
            //Kayıt işlemi
            // Aktivasyon e-postası gönderimi
           EvernoteUser user = repo_user.Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();
            if (user !=null)
            {
                // user null değilse eşleşme geldi kullanıcı adı yada email kullanılıyor demek
                //kayıt işlemi devam edemez burda bir hata var //geriye ne dönecek
                // throw new Exception("Kayıtlı kullanıcı ya da e-posta adresi");
                //bu hatayı homecontrollerda yakalamalıyız
                if (user.Username == data.Username)
                {
                    layerResult.Errors.Add("Username registered.");
                }
                if (user.Email == data.Email)
                {
                    layerResult.Errors.Add("E-mail address registered.");
                }
            }
            else
            {
               int dbResult = repo_user.Insert(new EvernoteUser()
                {
                  Username = data.Username,
                  Email = data.Email,
                  Password = data.Password
                });
                if(dbResult > 0)
                {
                    layerResult.Result= repo_user.Find(x => x.Email == data.Email && x.Username == data.Username);//igili user nesnesi çekiliyo
                    //TODO : aktivasyon maili atılacak...
                    //layerResult.Result.ActivatedGuid
                }
            }
            return layerResult; 
        }
    }
}

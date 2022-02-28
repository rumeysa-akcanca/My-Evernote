using MyEvernote.Common.Helper;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
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
                    //layerResult.Errors.Add("Username registered.");
                    layerResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Username registered.");
                }
                if (user.Email == data.Email)
                {
                    //layerResult.Errors.Add("E-mail address registered.");
                    layerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "E-mail address registered.");
                }
            }
            else
            {
               int dbResult = repo_user.Insert(new EvernoteUser()
                {
                  Username = data.Username,
                  Email = data.Email,
                  Password = data.Password,
                  ActivateGuid = Guid.NewGuid(),
                  CretedOn = DateTime.Now,
                  ModifiedOn = DateTime.Now,
                  ModifiedUsername ="system",
                  IsActive = false,
                  IsAdmin =false,
                });
                if(dbResult > 0)
                {
                    layerResult.Result= repo_user.Find(x => x.Email == data.Email && x.Username == data.Username);

                    //TODO : aktivasyon maili atılacak...
                    //layerResult.Result.ActivatedGuid
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{user.ActivateGuid}";//kullanıcıya gidecek
                    string body = $"Hi {user.Name} {user.Surname}; <br><br>" +
                        $"<a href = '{activateUri}' target='_blank'>Click</a> to activate your account";//mesaj gövdesi body
                    MailHelper.SendMail(body,user.Email, "MyEvernote account activation ");
                }
            }
            return layerResult; 
        }
        public BusinessLayerResult<EvernoteUser> LoginUser(LoginViewModel data)
        {
            //giriş kontrolü
            //hesap aktive edilmiş mi
            //Kullanıcı adı ve şifre eşleşiyo mu? //eşleşiyosa kullanıcı nesnesi dönecek ve login işlemi başarılı
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();
            layerResult.Result = repo_user.Find(x => x.Username == data.Username && x.Password == data.Password);
           
            if (layerResult.Result!= null)//kullanıcı eşleşmisse
            {
                if (!layerResult.Result.IsActive)//eşleşmiş ama activate edilmemişse yine hata mesajı
                {
                    //layerResult.Errors.Add("User didn't active.Please check your mail.");
                    layerResult.AddError(ErrorMessageCode.UserIsNotActive, "User didn't active.");
                    layerResult.AddError(ErrorMessageCode.CheckYourEmail, "Please check your mail.");
                }
                
            }
            else//kullanıcı eşleşmemişse, hata mesajı dönmeli
            {
                //layerResult.Errors.Add("Username or password don't match");
                layerResult.AddError(ErrorMessageCode.UsernameOrPassWrong, "Username or password don't match.");
            }
            return layerResult;

        }
        //Hesap aktivasyon devamı aktifleştirme
        public BusinessLayerResult<EvernoteUser> ActivateUser(Guid activatedId)
        {
            //bu activated ıd'ye sahip bir user var mı?
            BusinessLayerResult<EvernoteUser> layerResult = new BusinessLayerResult<EvernoteUser>();
            layerResult.Result = repo_user.Find( x=> x.ActivateGuid == activatedId);
            if (layerResult.Result !=null)
            {
                if (layerResult.Result.IsActive)
                {
                    layerResult.AddError(ErrorMessageCode.UserAlreadyActive, "User already activated");
                    return layerResult;
                }
                layerResult.Result.IsActive = true;
                repo_user.Update(layerResult.Result);
            }
            else
            {
                layerResult.AddError(ErrorMessageCode.ActivatedIdDoesNotExists, "No user found to activate");
            }
            return layerResult;
        }
    }

}

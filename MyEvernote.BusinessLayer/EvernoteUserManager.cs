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
                  ProfileImageFilename="gandalffun.png",
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
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");//sitenin adresini siteUri değişkenine aldım
                    string activateUri = $"{siteUri}/Home/UserActivate/{layerResult.Result.ActivateGuid}";//kullanıcının geleceği link
                    string body = $"Hi {layerResult.Result.Username}; <br><br>" +
                        $"<a href = '{activateUri}' target='_blank'>Click</a> to activate your account";//mesaj gövdesi body
                    MailHelper.SendMail(body,layerResult.Result.Email, "MyEvernote account activation ");//default true html mesai içeriği var


                    //Gmail  üzerinden mail attığımız için gmail hesabının  bazı ayarlaru var c#'dan gmailden kod göndermek IMap gibi bişileri 
                    //açmak gerekiyo gmail üzerindeki ayarlardan
                }
            }
            return layerResult; 
        }

        public BusinessLayerResult<EvernoteUser> GetUserById(int id)
        {
            BusinessLayerResult<EvernoteUser> res = new BusinessLayerResult<EvernoteUser>();
            res.Result = repo_user.Find(x => x.Id == id);
            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "User not found");
                //kullanıcıyı bulamazsak hatayı belirtmiş olacağız
            }
            return res;//kullanıcı nesnemiz bize geri dönüyo
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
            layerResult.Result = repo_user.Find( x=> x.ActivateGuid == activatedId);//bu activatedıd'ye sahip bir user var mi?
            if (layerResult.Result !=null) //kulllaanıcı null değilse
            {
                if (layerResult.Result.IsActive) //kullanıcı aktif mi?
                {
                    //aktifse
                    layerResult.AddError(ErrorMessageCode.UserAlreadyActive, "User already activated");
                    return layerResult;
                }
                //aktif değilse aktifleştir
                layerResult.Result.IsActive = true;
                repo_user.Update(layerResult.Result);
            }
            else //eğer girilen ıd bulunamadıysa
            {
                layerResult.AddError(ErrorMessageCode.ActivatedIdDoesNotExists, "No user found to activate");
            }
            return layerResult;
        }
    }

}

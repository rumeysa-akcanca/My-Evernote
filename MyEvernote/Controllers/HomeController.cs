using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            //Rümeysaakcanca

            //Rümeysa
            // BusinessLayer.Test test = new BusinessLayer.Test();
            //Database contexti newle ,database yoksa oluştur
            //createifnotexist methodu örnek datanın oluşumunu tetiklemez,initializerın çalışmasını sağlamaz
            //test.InsertTest();
            //test.UpdateTest();
            //test.DeleteTest();
            //test.CommentTest
            //
            //ilk yöntemin devamı:categorycontroller üzerinden gelen view talebi ve model..
            //if (TempData["mm"]!=null)
            //{
            //    //tempdata içerisindeki notları model olarak al  kullan
            //    return View(TempData["mm"] as List<Note>);
            //}

            //tempdata boşsa normal bir notların hepsini listeleme
            NoteManager nm = new NoteManager();

            return View(nm.GetAllNote().OrderByDescending(x => x.ModifiedOn).ToList());//düzenleme tarihine göre tersten sıralayıp gönderme
            //queryable çağırsaydık...
            //return View(nm.GetAllNoteQueryable().OrderByDescending(x => x.ModifiedOn).ToList());
            //ıqueryable listesi dönecek
            //tolist kısmına geldiğinde orderbyle bir sorgu gidip sqlde çalışıp sqlde sıralayıp geri dönecek

        }


        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                //id'si bilinmeden ulaşamaya çalışırsa kötü bir istek gelmiiş demektir....
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryManager cm = new CategoryManager();
            Category category = cm.GetCategoryById(id.Value);//nullable tipin değeri gelmeli

            if (category == null)
            {
                return HttpNotFound();//bulunamadı
                //return RedirectToAction("Index", "Home"); //anasayfaya atma
            }
            return View("Index", category.Notes.OrderByDescending(x => x.ModifiedOn).ToList());//bu modeli index viewında görüntüle
        }

        public ActionResult MostLiked()
        {
            NoteManager nm = new NoteManager();
            //çalışmayan kısım
            return View("Index", nm.GetAllNote().OrderByDescending(x => x.LikeCount).ToList());
            
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Login()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model) //model geliyo olacak
        {
            if (ModelState.IsValid)//Model durumu geçerliyse kullanıcı adı ,şifre doluysa,login etmeyi dene
            {
                EvernoteUserManager eum = new EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> UserResult = eum.LoginUser(model);
                //Eğer hata varsa modelstate 'e bunu basıp sayfayı geri döndür
                if (UserResult.Errors.Count > 0)//login başarısızsa error gelicek
                {
                    if (UserResult.Errors.Find( x=> x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {
                        ViewBag.SetLink = "http://Home/Activate/1234-4567-78980";
                    }

                  UserResult.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                  return View(model);//yine aynı sayfayı aç
                }
                Session["login"] = UserResult.Result;//Session'a kullanıcı bilgi saklama
                return RedirectToAction("Index"); //indekse yönlendirme
                                                 
            }


            return View(model);//aynı modelde aynı sayfayı aç
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
           
            if(ModelState.IsValid)
            {
                //hata yakalama
              EvernoteUserManager eum = new BusinessLayer.EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> result = eum.RegisterUser(model);
                if (result.Errors.Count > 0)
                {
                    result.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                //mail atma işlemi?

                //EvernoteUser user = null;
                //try
                //{

                //     user  = evernoteUser.RegisterUser(model);

                //}
                //catch (Exception ex)
                //{
                //    //Gelen mesajı olduğu gibi model error'a veriyoz
                //    ModelState.AddModelError("", ex.Message);
                   
                //}



                //    if(model.Username == "aaa")
                //    {
                //        ModelState.AddModelError("", "Username is used");

                //    }
                //    if(model.Email == "aaaa@com")
                //    {
                //        ModelState.AddModelError("", "E-mail adress is used");

                //    }
                //    foreach (var item in ModelState)
                //    {
                //        if (item.Value.Errors.Count > 0)
                //        {
                //           return View(model); //hata varsa geri döndür
                //        }
                //    }

                //if (user == null)
                //{
                //    return View(model);
                //}
              return RedirectToAction("RegisterOk");// kayıt başarıllı sayfası
            }
            return View(model); //model geçerli değilse sayfaya modeli geri yolla
        }
        public ActionResult RegisterOk()
        {
            return View();
        }

         //Aktivasyon actionı
        public ActionResult UserActivate(Guid activate_id)
        {
            //maili aldık tıkladık buraya düşeceğiz
            // kullanıcı aktivasyonu sağlanacak
            //Gelen maildeki linki tıklayıp burdaki actiona düşüp bunun üzerinden aktive edildi hesabınız bıdı bıdı
            EvernoteUserManager eum = new EvernoteUserManager();
            BusinessLayerResult<EvernoteUser> res = eum.ActivateUser(activate_id);
            if (res.Errors.Count > 0)
            {
                TempData["errors"] = res.Errors;
                return RedirectToAction(" UserActivatedCancel");
            }
            return View();
        }
        public ActionResult UserActivatedOk()
        {
            return View();
        }
        public ActionResult UserActivatedCancel()
        {
            List<ErrorMessageObject> errors = null;
            if (TempData["errors"] != null)
            {
                errors = TempData["errors"] as List<ErrorMessageObject>;
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
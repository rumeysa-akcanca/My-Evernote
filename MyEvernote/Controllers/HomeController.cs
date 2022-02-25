using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
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
                //id'si bilinmeden ulaşamaya çalışırsa kötü bir istek gelmiiş demektir
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
            //Giriş kontrolü ve yönlendirme
            //Session'a kullanıcı bilgi saklama
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            //kullanıcı username kontrolü 
            // kullanıcı e-posta kontrolü
            //Kayıt işlemi
            // Aktivasyon e-postası gönderimi
            if(ModelState.IsValid)
            {
                //hata yakalama
              EvernoteUserManager eum = new BusinessLayer.EvernoteUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.RegisterUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }



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
            // kullanıcı aktivasyonu sağlanacak
            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
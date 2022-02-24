using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using MyEvernote.BusinessLayer;
using MyEvernote.Entities;

namespace MyEvernote.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        //bu bir yöntem:Tempdata ile Category listeleme
        //public ActionResult Select(int? id)
        //{
        //    if (id==null)
        //    {
        //        //id'si bilinmeden ulaşamaya çalışırsa kötü bir istek gelmiiş demektir
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    CategoryManager cm = new CategoryManager();
        //    Category category =  cm.GetCategoryById(id.Value);//nullable tipin değeri gelmeli

        //    if (category == null)
        //    {
        //        return HttpNotFound();//bulunamadı
        //        //return RedirectToAction("Index", "Home"); //anasayfaya atma
        //    }
        //    TempData["mm"] = category.Notes;
        //    return RedirectToAction("Index", "Home");

        //   // return View(category.Notes);//category geimişse içindeki notlarını list alıyo olcaz
        //}
    }
}
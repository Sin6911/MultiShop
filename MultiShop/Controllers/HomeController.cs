using MultiShop.Models.EShopV20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Controllers
{
    
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        public ActionResult Index()
        {
            var model = db.Categories
                .Where(c => c.Products.Count >= 4)
                .OrderBy(c => Guid.NewGuid()).ToList();
                
           
            return View(model);
        }

        public ActionResult Search()
        {
            var name = Request["term"];

            var data = db.Products
                .Where(p => p.Name.Contains(name))
                .Select(p => p.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult Category()
        {
            var model = db.Categories;
            return PartialView("_Category",model);
        }

        public ActionResult Special()
        {
            var model = db.Products.Where(p=>p.Special==true).Take(5);
            return PartialView("_Special", model);
        }
        //Download source code tại Sharecode.vn
        public ActionResult Saleoff()
        {
            var model = db.Products.Where(p => p.Discount>0).Take(1);
            return PartialView("_Saleoff", model);
        }

        
    }
}
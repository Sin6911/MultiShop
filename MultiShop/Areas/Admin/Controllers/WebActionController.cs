using MultiShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Areas.Admin.Controllers
{
    public class WebActionController : SecurityController
    {
        public ActionResult Index()
        {
            ViewBag.WebActions = sdb.WebActions;
            return View();
        }

        public ActionResult Insert(WebAction model)
        {
            try
            {
                sdb.WebActions.Add(model);
                sdb.SaveChanges();
                ModelState.AddModelError("", "Inserted");
            }
            catch
            {
                ModelState.AddModelError("", "Error");
            }

            ViewBag.WebActions = sdb.WebActions;
            return View("Index", model);
        }

        public ActionResult Update(WebAction model)
        {
            try
            {
                sdb.Entry(model).State = EntityState.Modified;
                sdb.SaveChanges();
                ModelState.AddModelError("", "Updated");
            }
            catch
            {
                ModelState.AddModelError("", "Error");
            }

            ViewBag.WebActions = sdb.WebActions;
            return View("Index", model);
        }

        public ActionResult Delete(int Id)
        {
            try
            {
                var model = sdb.WebActions.Find(Id);
                sdb.WebActions.Remove(model);
                sdb.SaveChanges();
                ModelState.AddModelError("", "Deleted");
            }
            catch
            {
                ModelState.AddModelError("", "Error");
            }

            ViewBag.WebActions = sdb.WebActions;
            return View("Index");
        }

        public ActionResult Edit(int Id)
        {
            var model = sdb.WebActions.Find(Id);

            ViewBag.WebActions = sdb.WebActions;
            return View("Index", model);
        }
	}
}
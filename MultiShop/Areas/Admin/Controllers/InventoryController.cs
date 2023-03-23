using MultiShop.Models.EShopV20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Areas.Admin.Controllers
{
    public class InventoryController : Controller
    {
        Model1 db = new Model1();
        //
        // GET: /Admin/Inventory/
        public ActionResult ByCategory()
        {
            var model = db.Products
                .GroupBy(p => p.Category)
                .Select(g => new ReportInfo
                {
                    Group = g.Key.NameVN,
                    Count = g.Sum(p => p.Quantity),
                    Sum = g.Sum(p => p.UnitPrice * p.Quantity),
                    Min = g.Min(p => p.UnitPrice),
                    Max = g.Max(p => p.UnitPrice),
                    Avg = g.Average(p => p.UnitPrice)
                });
            return View("Index", model);
        }
        public ActionResult BySupplier()
        {
            var model = db.Products
                .GroupBy(p => p.Supplier)
                .Select(g => new ReportInfo
                {
                    Group = g.Key.Name,
                    Count = g.Sum(p => p.Quantity),
                    Sum = g.Sum(p => p.UnitPrice * p.Quantity),
                    Min = g.Min(p => p.UnitPrice),
                    Max = g.Max(p => p.UnitPrice),
                    Avg = g.Average(p => p.UnitPrice)
                });
            return View("Index", model);
        }
	}
}
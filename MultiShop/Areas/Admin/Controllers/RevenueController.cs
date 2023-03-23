using MultiShop.Models.EShopV20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Areas.Admin.Controllers
{
    public class RevenueController : Controller
    {
        Model1 db = new Model1();

        public ActionResult byProduct(DateTime? Min = null, DateTime? Max = null)
        {
            if (Min == null)
            {
                Min = DateTime.MinValue;
            }
            if (Max == null)
            {
                Max = DateTime.MaxValue;
            }

            var model = db.OrderDetails
                .Where(d => d.Order.OrderDate >= Min && d.Order.OrderDate <= Max)
                .GroupBy(d => d.Product)
                .Select(g => new ReportInfo
                {
                    Group = g.Key.Name,
                    Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
                    Count = g.Sum(d => d.Quantity),
                    Min = g.Min(d => d.UnitPrice),
                    Max = g.Max(d => d.UnitPrice),
                    Avg = g.Average(d => d.UnitPrice)
                });
            return View("Index", model);
        }

        public ActionResult byCategory()
        {
            var model = db.OrderDetails
                .GroupBy(d => d.Product.Category)
                .Select(g => new ReportInfo
                {
                    Group = g.Key.NameVN,
                    Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
                    Count = g.Sum(d => d.Quantity),
                    Min = g.Min(d => d.UnitPrice),
                    Max = g.Max(d => d.UnitPrice),
                    Avg = g.Average(d => d.UnitPrice)
                });
            return View("Index", model);
        }

        public ActionResult bySupplier()
        {
            var model = db.OrderDetails
                .GroupBy(d => d.Product.Supplier)
                .Select(g => new ReportInfo
                {
                    Group = g.Key.Name,
                    Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
                    Count = g.Sum(d => d.Quantity),
                    Min = g.Min(d => d.UnitPrice),
                    Max = g.Max(d => d.UnitPrice),
                    Avg = g.Average(d => d.UnitPrice)
                });
            return View("Index", model);
        }

        public ActionResult byCustomer()
        {
            var model = db.OrderDetails
                .GroupBy(d => d.Order.Customer)
                .Select(g => new ReportInfo
                {
                    Group = g.Key.Fullname,
                    Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
                    Count = g.Sum(d => d.Quantity),
                    Min = g.Min(d => d.UnitPrice),
                    Max = g.Max(d => d.UnitPrice),
                    Avg = g.Average(d => d.UnitPrice)
                });
            return View("Index", model);
        }

        public ActionResult byYear()
        {
            var model = db.OrderDetails
                .GroupBy(d => d.Order.OrderDate.Year)
                .Select(g => new ReportInfo
                {
                    iGroup = g.Key,
                    Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
                    Count = g.Sum(d => d.Quantity),
                    Min = g.Min(d => d.UnitPrice),
                    Max = g.Max(d => d.UnitPrice),
                    Avg = g.Average(d => d.UnitPrice)
                })
                .OrderBy(i => i.iGroup);
            return View("Index", model);
        }

        public ActionResult byMonth()
        {
            var model = db.OrderDetails
                .GroupBy(d => d.Order.OrderDate.Month)
                .Select(g => new ReportInfo
                {
                    iGroup = g.Key,
                    Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
                    Count = g.Sum(d => d.Quantity),
                    Min = g.Min(d => d.UnitPrice),
                    Max = g.Max(d => d.UnitPrice),
                    Avg = g.Average(d => d.UnitPrice)
                })
                .OrderBy(i => i.iGroup);
            return View("Index", model);
        }

        public ActionResult byQuarter()
        {
            var model = db.OrderDetails
                .GroupBy(d => (d.Order.OrderDate.Month - 1) / 3 + 1)
                .Select(g => new ReportInfo
                {
                    iGroup = g.Key,
                    Sum = g.Sum(d => d.UnitPrice * d.Quantity * (1 - d.Discount)),
                    Count = g.Sum(d => d.Quantity),
                    Min = g.Min(d => d.UnitPrice),
                    Max = g.Max(d => d.UnitPrice),
                    Avg = g.Average(d => d.UnitPrice)
                })
                .OrderBy(i => i.iGroup);
            return View("Index", model);
        }
	}
}
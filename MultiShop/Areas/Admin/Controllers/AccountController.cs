using MultiShop.Models;
using MultiShop.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Areas.Admin.Controllers
{
    public class AccountController : SecurityController
    {
        public ActionResult Logoff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Login(String returnUrl)
        {
            if (returnUrl.Contains("/Admin/"))
            {
                Response.Redirect("/Admin/Account/Login?returnUrl=" + returnUrl);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(String UserName, String Password, string returnUrl)
        {
            var user = await UserManager.FindAsync(UserName, Password);
            if (user != null)
            {
                await SignInAsync(user, false);
                if (returnUrl == null)
                {
                    returnUrl = "/Admin/Master";
                }
                return Redirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
	}
}
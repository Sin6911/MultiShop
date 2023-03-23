using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MultiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Areas.Admin.Controllers
{
    public class SecurityController : Controller
    {
        protected ApplicationDbContext sdb = new ApplicationDbContext();

        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<ApplicationRole> RoleManager { get; private set; }

        public SecurityController()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(sdb));
            RoleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(sdb));
        }

        public void SignOut()
        {
            AuthenticationManager.SignOut();
        }

        public void SignIn(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            var identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            if (disposing && RoleManager != null)
            {
                RoleManager.Dispose();
                RoleManager = null;
            }
            base.Dispose(disposing);
        }
	}
}
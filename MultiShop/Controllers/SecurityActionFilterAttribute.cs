using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MultiShop.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using MultiShop.Models.EShopV20;

namespace MultiShop.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class SecurityActionFilterAttribute : ActionFilterAttribute
    {
        ApplicationDbContext sdb = new ApplicationDbContext();

        public UserManager<ApplicationUser> UserManager { get; private set; }
        public SecurityActionFilterAttribute()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(sdb));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (sdb.Roles.ToList().Count == null)
            {
                var role = new ApplicationRole()
                {
                    Name = "Administration"
                };
                sdb.Roles.Add(role);

                var user = new ApplicationUser()
                {
                    UserName = "Admin"
                };
                UserManager.Create(user, "123456");

                UserManager.AddToRole(user.Id, role.Name);
                sdb.SaveChanges();
            }
            //Download source code tại Sharecode.vn
            var uri = context.HttpContext.Request.Url.AbsoluteUri;
            if (uri.ToLower().Contains("/admin/"))
            {
                var ControllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;
                var ActionName = context.ActionDescriptor.ActionName;

                if (!context.HttpContext.Request.IsAuthenticated) // chưa đăng nhập
                {
                    if (!uri.ToLower().Contains("/login"))
                    {
                        context.HttpContext.Response.Redirect("/Admin/Account/Login?returnUrl=" + uri);
                    }
                }
                else
                {
                    var user = UserManager.FindByName(context.HttpContext.User.Identity.Name);
                    if (user.Roles.Count == 0) // không cấp vai trò -> không phải web master
                    {
                        context.HttpContext.Response.Redirect("/Admin/Account/Login?returnUrl=" + uri);
                    }
                    else
                    {
                        var roleIds = user.Roles.Select(r => r.RoleId).ToList();
                        var perms = sdb.Permissions
                            .Where(p => roleIds.Contains(p.RoleId))
                            .Where(p => p.Action.Name == ActionName
                                && p.Action.Controller == ControllerName).ToList();
                        if (perms.Count == 0) // chưa được nhập vào CSDL -> Không xử lý
                        {
                        }
                        else if (!perms.First().Allowable) // Không cho phép
                        {
                            context.HttpContext.Response.Redirect("/Admin/Account/Login?returnUrl=" + uri);
                        }
                        else // Cho phép
                        {
                            var actions = sdb.Permissions
                                .Where(p => roleIds.Contains(p.RoleId))
                                .Where(p => p.Action.Controller == ControllerName).ToList();
                            foreach (var a in actions)
                            {
                                context.Controller.ViewData[a.Action.Name] = a.Allowable;
                            }
                        }
                    }
                }
            }
        }
    }
}
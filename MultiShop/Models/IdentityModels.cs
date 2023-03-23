using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace MultiShop.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("MultiShop") { }

        public DbSet<WebAction> WebActions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {

    }

    public class ApplicationRole : IdentityRole
    {
        public virtual List<Permission> Permissions { get; set; }
    }

    public class WebAction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }

        public virtual List<Permission> Permissions { get; set; }
    }

    public class Permission
    {
        public int Id { get; set; }
        public int WebActionId { get; set; }
        public string RoleId { get; set; }
        public bool Allowable { get; set; }

        public virtual WebAction Action { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
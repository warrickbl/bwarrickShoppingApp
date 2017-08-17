using bwarrickShoppingApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace bwarrickShoppingApp.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u => u.Email == "blwarrick1107@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "blwarrick1107@gmail.com",
                    Email = "blwarrick1107@gmail.com",
                    FirstName = "Bri",
                    LastName = "Warrick",
                }, "briLeigh7!");
            }
            var userId = userManager.FindByEmail("blwarrick1107@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");
        }
    }
    
}
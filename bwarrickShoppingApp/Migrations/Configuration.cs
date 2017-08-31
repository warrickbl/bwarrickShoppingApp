using bwarrickShoppingApp.Models;
using bwarrickShoppingApp.Models.CodeFirst;
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

            context.Items.AddOrUpdate(i => i.Id, new Item
            {
                Name = "Navajo",
                MediaURL = "/Assets/Images/Mug5.jpg",
                Description = "<p><em><strong>Navajo Mug in Earth Tones</strong></em></p>",
                CreationDate = DateTime.Now,
                Price = 26
            },
            new Item
            {
                Name = "Amber",
                MediaURL = "/Assets/Images/Mug17.jpg",
                Description = "Amber Mug in Sunset ",
                CreationDate = DateTime.Now,
                Price = 16
            },
            new Item
            {
                Name = "Dahlia Set",
                MediaURL = "/Assets/Images/Mug18.jpg",
                Description = "Two Dahlia Mugs in Sunrise",
                CreationDate = DateTime.Now,
                Price = 40
            },
            new Item
            {
                Name = "Daisy",
                MediaURL = "/Assets/Images/Mug8.jpg",
                Description = "Daisy Mug in Ocean",
                CreationDate = DateTime.Now,
                Price = 22
            },
            new Item
            {
                Name = "Lilly",
                MediaURL = "/Assets/Images/Mug6.jpg",
                Description = "Lilly Mug in Midnight",
                CreationDate = DateTime.Now,
                Price = 22
            },
            new Item
            {
                Name = "Suzy",
                MediaURL = "/Assets/Images/Mug14.jpg",
                Description = "Suzy Mug in Charcoal",
                CreationDate = DateTime.Now,
                Price = 23
            },
            new Item
            {
                Name = "Tulip",
                MediaURL = "/Assets/Images/Mug19.jpg",
                Description = "Tulip Mug in Linen",
                CreationDate = DateTime.Now,
                Price = 25
            },
             new Item
             {
                 Name = "Sunny",
                 MediaURL = "/Assets/Images/Mug1.jpg",
                 Description = "Sunny Mug in Earth Tones",
                 CreationDate = DateTime.Now,
                 Price = 30
             });
        }
    }
    
}
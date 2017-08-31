using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bwarrickShoppingApp.Models
{
    public class Universal : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Find(User.Identity.GetUserId());
                ViewBag.FirstName = user.FirstName;
                ViewBag.LastName = user.LastName;
                ViewBag.FullName = user.FullName;
                
               
                var myCart = db.CartItems.AsNoTracking().Where(c => c.CustomerId == user.Id).ToList();
                ViewBag.TotalCartItems = myCart.Sum(c => c.Count);
                decimal Total = 0;
                if(myCart.Count != 0) {
                foreach (var item in myCart)
                {
                    Total += item.Count * item.Item.Price;
                }
                ViewBag.CartTotal = Total;
                }
                else
                {
                    ViewBag.CartTotal = 0;
                }

            }
            base.OnActionExecuting(filterContext);
        }
                       
    }
}
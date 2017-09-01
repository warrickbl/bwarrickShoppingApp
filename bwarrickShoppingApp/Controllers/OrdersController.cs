using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bwarrickShoppingApp.Models;
using bwarrickShoppingApp.Models.CodeFirst;
using Microsoft.AspNet.Identity;

namespace bwarrickShoppingApp.Controllers
{
    public class OrdersController : Universal
   
    {
        public ActionResult Confirmation(bool Completed, int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                order.Completed = true;
                db.SaveChanges();  
            }
            //if (order != null)
            //{
            //    order.Completed = false;
            //    db.Orders.Remove(order);
            //}
            var user = db.Users.Find(User.Identity.GetUserId());
            var cartItems = db.CartItems.Where(c => c.CustomerId == user.Id).ToList();
            foreach (var item in cartItems)
            {
                db.CartItems.Remove(item);
            }
            db.SaveChanges();
            return View();
        }
        // GET: Orders
        public ActionResult Index()
           
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            return View(user.Orders.ToList());
        }
        

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "Id,Address,City,State,ZipCode,Country,Phone,Total,OrderDate,CustomerId")] Order order)
        {
            
            var user = db.Users.Find(User.Identity.GetUserId());
                var cartItems = db.CartItems.Where(c => c.CustomerId == user.Id).ToList();
            if (user.CartItems.Count == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                order.OrderItems = cartItems.Select(o => new OrderItem
            {

                ItemId = o.ItemId,
                Quantity = o.Count,
                UnitPrice = o.Item.Price,

            }).ToList();

                order.OrderDate = DateTime.Now;
                order.CustomerId = user.Id;

                foreach (var CartItem in cartItems)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.ItemId = CartItem.ItemId;
                    orderItem.Quantity = CartItem.Count;
                    orderItem.UnitPrice = CartItem.Item.Price;
                }

                order.Total = order.OrderItems.Sum(t => t.Quantity * t.UnitPrice);
                if (ModelState.IsValid)
                {
                    order.OrderDate = DateTime.Now;
                    db.Orders.Add(order);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = order.Id });
                }
                
            
            
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,City,State,ZipCode,Country,Phone,Total,OrderDate,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

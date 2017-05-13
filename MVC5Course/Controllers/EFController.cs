using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();
        // GET: EF
        public ActionResult Index()
        {
            var all = db.Product.AsQueryable();
            var data = all.Where(p => p.is刪除 == false && p.Active == true && p.ProductName.Contains("Black"));

            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Edit(int id)
        {
            var data = db.Product.Find(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(int id,Product product)
        {
            if (ModelState.IsValid)
            {
                var data = db.Product.Find(id);
                data.Active = product.Active;
                data.Price = product.Price;
                data.ProductName = product.ProductName;
                data.Stock = product.Stock;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var data = db.Product.Find(id);

            //foreach (var item in data.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(item);
            //}

            //db.OrderLine.RemoveRange(data.OrderLine);

            //db.Product.Remove(data);
            //db.SaveChanges();

            //return RedirectToAction("Index");

            data.is刪除 = true;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = db.Database.SqlQuery<Product>("SELECT * FROM dbo.Product WHERE ProductId=@p0", id).FirstOrDefault();

            return View(data);
        }
    }
}
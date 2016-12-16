using SoftUniProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SoftUniProject.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: Product
        public ActionResult Index()
        {
            return View("List");
        }

        //
        //Get: Product/List
        public ActionResult List()
        {
            using (var db = new SoftUniDbContext())
            {
                //getproducts
                var products = db.Products
                    .ToList();

                return View(products);
            }
        }

        //
        //Get: Product/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new SoftUniDbContext())
            {

                var product = db.Products
                    .Where(a => a.ID == id)
                    .First();

                if (product == null)
                {
                    return HttpNotFound();
                }

                return View(product);
            }
        }

        //
        //Get: Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        //Post: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                //insert in DB
                using (var db = new SoftUniDbContext())
                {
                    //save
                    db.Products.Add(product);
                    db.SaveChanges();

                    return RedirectToAction("List");
                }
            }

            return View(product);
        }
        
        //
        //Get: Product/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new SoftUniDbContext())
            {
                //get products
                var product = db.Products
                    .Where(p => p.ID == id)
                    .First();

                if (!IsUserAuthorizedToEdit(product))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                //check if exists
                if (product==null)
                {
                    return HttpNotFound();
                }
                //pass to the view
                return View(product);
            }

        }

        //
        //Post: Product/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new SoftUniDbContext())
            {
                //get products
                var product = db.Products
                    .Where(p => p.ID == id)
                    .First();

                //check if exists
                if (product == null)
                {
                    return HttpNotFound();
                }

                //remove product
                db.Products.Remove(product);
                db.SaveChanges();

                //redirect to /product/list
                return RedirectToAction("List");
            }
        }

        //
        //Get: Product/Edit
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new SoftUniDbContext())
            {
                //get products
                var product = db.Products
                    .Where(p => p.ID == id)
                    .First();

                if (!IsUserAuthorizedToEdit(product))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                //check if exists
                if (product == null)
                {
                    return HttpNotFound();
                }

                //create view model
                var model = new ProductViewModel();
                model.ID = product.ID;
                model.Name = product.Name;
                model.Price = product.Price;
                model.Review = product.Review;
                

                //pass to view
                return View(model);
            }
        }


        //
        //Post: Product/Edit
        [HttpPost]
        [Authorize]
        public ActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new SoftUniDbContext())
                {
                    //get products
                    var product = db.Products
                        .FirstOrDefault(p => p.ID == model.ID);

                    //set the same props
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.Review = model.Review;

                    //save in db
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();

                    //redirect to /product/list
                    return RedirectToAction("List");
                }                
            }

            return View(model);
        }

        private bool IsUserAuthorizedToEdit(Product product)
        {
            bool isAdmin = this.User.IsInRole("Admin");

            return isAdmin;
        }
    }
}
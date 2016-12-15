using SoftUniProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
    }
}
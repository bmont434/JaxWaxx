using System;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MVCTest.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MVCTest.Controllers
{
    public class CartController : Controller
    {
        
        public IConfiguration Configuration { get; }

        public IActionResult ShoppingCart()
        {
            var key = "CartSession";
            // Retrieve if shopping cart session exists
            if (HttpContext.Session.GetString(key) != null)
            {
                bool duplicate = false;
                var str = HttpContext.Session.GetString(key);
                var obj = JsonConvert.DeserializeObject<List<Models.Product>>(str);

                //using duplicate value to ensure only 1 instance of an item in the cart
                foreach (var item in obj)
                {
                    if (item.ProductID == Convert.ToInt32(RouteData.Values["id"]))
                    {
                        duplicate = true;
                    }
                }

                // Retrieval
                if (RouteData.Values["id"] != null && duplicate == false)
                {
                    DBContext context = new DBContext();

                    int ID = Convert.ToInt32(RouteData.Values["id"]);

                    var AddedProduct = from p in context.Product where p.ProductID == ID select p;

                    //add item to session
                    foreach (var Product in AddedProduct)
                    {
                        obj.Add(Product);
                    }
                    var str1 = JsonConvert.SerializeObject(obj);
                    HttpContext.Session.SetString(key, str1);
                }

                ViewBag.Cart = obj;
                ViewBag.Total = Calculate(obj);
            }
            
            //session has not been initiated
            else
            {
                List<Object> obj = new List<Object>();
                ViewBag.Cart = obj;
                ViewBag.Total = 0;
            }
         return View();
                     
        }

        //Calculate the total price of items in shopping cart
        public string Calculate(List<Models.Product> obj)
        {
            decimal total = 0;
            foreach (Models.Product p in obj)
            {
                total = total + p.ProductPrice;
            }
            return string.Format("{0:C}", total);
        }

        //remove items from cart
        public IActionResult Remove()
        {
            var key = "CartSession";
            var str = HttpContext.Session.GetString(key);
            var obj = JsonConvert.DeserializeObject<List<Models.Product>>(str);

            obj.RemoveAll(p => p.ProductID == Convert.ToInt32(RouteData.Values["id"]));

            var str1 = JsonConvert.SerializeObject(obj);
            HttpContext.Session.SetString(key, str1);

            ViewBag.Cart = obj;
            ViewBag.Total = Calculate(obj);
            return View("ShoppingCart");
        }
    }
}
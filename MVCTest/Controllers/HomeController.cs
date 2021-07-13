using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCTest.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Http;
using MVCTest.Repository;
using Newtonsoft.Json;


namespace MVCTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {         
            return View();
        }

        public IActionResult Shop()
        {
           
            DBContext context = new DBContext();

            var Products = from p in context.Product select p;
            ViewBag.Prod = Products.ToList();

            //initiating session for shopping cart
            if (HttpContext.Session.GetString("CartSession") == null)
            {
                List<Object> Cprod = new List<Object>();
                var key1 = "CartSession";
                var str1 = JsonConvert.SerializeObject(Cprod);
                HttpContext.Session.SetString(key1, str1);
            }

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

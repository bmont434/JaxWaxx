using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models
{
    public class Product
    {
        public string ProductName { get; set; }
        public int ProductID { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
    }
}

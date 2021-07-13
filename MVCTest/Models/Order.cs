using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTest.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public int ProductID { get; set; }
    }
}

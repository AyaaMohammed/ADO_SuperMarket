using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int NumOfStock { get; set; }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
    }
}

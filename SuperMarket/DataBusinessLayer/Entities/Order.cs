using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public float TotalPrice { get; set; }   
        public string Payment_Method { get; set; }  
        public string Customer_Name { get; set; }
        public string Customer_Phone { get; set; }
        public int UserID { get; set; }
    }
}

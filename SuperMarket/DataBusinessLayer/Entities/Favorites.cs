using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusinessLayer
{
    public class Favorites
    {
        public int ID { get; set; }
        public DateTime Created_At { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }

    }
}

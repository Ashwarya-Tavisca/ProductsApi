using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductUI.Models
{
    public class CarProduct
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Price { get; set; }
        public string IsBooked { get; set; }
        public string isSaved { get; set; }
    }
}
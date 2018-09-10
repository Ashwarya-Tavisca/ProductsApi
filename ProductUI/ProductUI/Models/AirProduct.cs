using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductUI.Models
{
    public class AirProduct
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> ArrivalDate { get; set; }
        public Nullable<int> DepartureDate { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string IsBooked { get; set; }
        public string IsSaved { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductUI.Models
{
    public class HotelProduct
    {
        public int ProductId { get; set; }
        public string IsBooked { get; set; }
        public string IsSaved { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public Nullable<int> NoOfAvailableRooms { get; set; }
        public Nullable<int> RoomPrice { get; set; }
    }
}
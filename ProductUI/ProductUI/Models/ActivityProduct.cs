using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductUI.Models
{
    using System;
    using System.Collections.Generic;

    public partial class ActivityProduct
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Price { get; set; }
        public string ActivityType { get; set; }
        public string IsBooked { get; set; }
        public string IsSaved { get; set; }
    }
}
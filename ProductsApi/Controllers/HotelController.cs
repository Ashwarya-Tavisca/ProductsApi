using DatabaseLayer;
using ProductsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductsApi.Controllers
{
    public class HotelController : ApiController
    {

        [HttpGet]
        public IEnumerable<Hotel> displayHotel()
        {
            using (ProductsTableEntities obj = new ProductsTableEntities())
            {

                return obj.Hotels.ToList();
            }
        }


        [HttpPost]
        public void AddHotel([FromBody]Hotel HotelObject)

        {
            using (ProductsTableEntities productobject = new ProductsTableEntities())
            {
                var id = productobject.Hotels.Max(product => product.ProductId);
                int maximumid = Int32.Parse(id.ToString());
                maximumid += 1;
                HotelObject.ProductId = maximumid;
                productobject.Hotels.Add(HotelObject);
                productobject.SaveChanges();
            }

        }

        [HttpPut]
        public void book([FromBody]Item item)
        {
            using (ProductsTableEntities obj = new ProductsTableEntities())
            {
                if (item.type == "book")
                {
                    var refobj = obj.Hotels.Find(item.id);
                    string IsBooked = obj.Hotels.Find(item.id).IsBooked;
                    IsBooked = "true";

                    refobj.IsBooked = IsBooked;

                    obj.SaveChanges();
                }
                else
                {
                    var refobj = obj.Hotels.Find(item.id);
                    string IsSaved = obj.Hotels.Find(item.id).IsSaved;
                    IsSaved = "true";

                    refobj.IsSaved = IsSaved;

                    obj.SaveChanges();
                }
            }
        }
    }
}

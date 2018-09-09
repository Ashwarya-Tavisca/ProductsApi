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
    public class AirController : ApiController
    {

       
        [HttpGet]
        public IEnumerable<Air> displayAir()
        {
            using (ProductsTableEntities obj = new ProductsTableEntities())
            {

                return obj.Airs.ToList();
            }
        }

        [HttpPost]
        public void AddAirProduct([FromBody]Air airObject)

        {
            using (ProductsTableEntities productobject = new ProductsTableEntities())
            {
                var id = productobject.Airs.Max(product => product.ProductId);
                int maximumid = Int32.Parse(id.ToString());
                maximumid += 1;
                airObject.ProductId = maximumid;
                productobject.Airs.Add(airObject);
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
                    var refobj = obj.Airs.Find(item.id);
                    string IsBooked = obj.Airs.Find(item.id).IsBooked;
                    IsBooked = "true";

                    refobj.IsBooked = IsBooked;

                    obj.SaveChanges();
                }
                else
                {
                    var refobj = obj.Airs.Find(item.id);
                    string IsSaved = obj.Airs.Find(item.id).IsSaved;
                    IsSaved = "true";

                    refobj.IsSaved = IsSaved;

                    obj.SaveChanges();
                }
            }
        }
    }
}

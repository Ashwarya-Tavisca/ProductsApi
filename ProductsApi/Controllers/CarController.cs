using DatabaseLayer;
using ProductsApi.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductsApi.Controllers
{
    public class CarController : ApiController
    {
        [HttpGet]
        public IEnumerable<Car> displayCar()
        {
            using (ProductsTableEntities obj = new ProductsTableEntities())
            {

                return obj.Cars.ToList();
            }
        }

        [HttpPost]
        public void AddCarProduct([FromBody]Car carObject)

        {
            using (ProductsTableEntities productobject = new ProductsTableEntities())
            {
                var id = productobject.Cars.Max(product => product.ProductId);
                int maximumid = Int32.Parse(id.ToString());
                maximumid += 1;
                carObject.ProductId = maximumid;
                productobject.Cars.Add(carObject);
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
                    var refobj = obj.Cars.Find(item.id);
                    string IsBooked = obj.Cars.Find(item.id).IsBooked;
                    IsBooked = "true";

                    refobj.IsBooked = IsBooked;

                    obj.SaveChanges();
                }
                else
                {
                    var refobj = obj.Cars.Find(item.id);
                    string isSaved = obj.Cars.Find(item.id).isSaved;
                    isSaved = "true";

                    refobj.isSaved = isSaved;

                    obj.SaveChanges();
                }
            }
        }
    }
}

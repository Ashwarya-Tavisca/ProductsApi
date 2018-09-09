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
    public class ActivityController : ApiController
    {
        [HttpGet]
        public IEnumerable<Activity> displayActivity()
        {
            using (ProductsTableEntities obj = new ProductsTableEntities())
            {

                return obj.Activities.ToList();
            }
        }

        [HttpPost]
        public void AddActivityProduct([FromBody]Activity activityObject)

        {
            using (ProductsTableEntities productobject = new ProductsTableEntities())
            {
                var id = productobject.Activities.Max(product => product.ProductId);
                int maximumid = Int32.Parse(id.ToString());
                maximumid += 1;
                activityObject.ProductId = maximumid;
                productobject.Activities.Add(activityObject);
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
                    var refobj = obj.Activities.Find(item.id);
                    string IsBooked = obj.Activities.Find(item.id).IsBooked;
                    IsBooked = "true";

                    refobj.IsBooked = IsBooked;

                    obj.SaveChanges();
                }
                else
                {
                    var refobj = obj.Activities.Find(item.id);
                    string IsSaved = obj.Activities.Find(item.id).IsSaved;
                    IsSaved = "true";

                    refobj.IsSaved = IsSaved;

                    obj.SaveChanges();
                }
            }
        }
    }
}

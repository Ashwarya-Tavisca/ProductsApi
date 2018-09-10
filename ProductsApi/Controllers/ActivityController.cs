using DatabaseLayer;
using Newtonsoft.Json.Linq;
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
        ProductsTableEntities obj = new ProductsTableEntities();
        Activity activityProduct = new Activity();
        [HttpGet]
        public IEnumerable<Activity> displayActivity()
        {
            using (ProductsTableEntities obj = new ProductsTableEntities())
            {

                return obj.Activities.ToList();
            }
        }
        [HttpPost]
        public void Post([FromBody]JObject jsonFormatInput)
        {
            obj.Activities.Add(jsonFormatInput.ToObject<Activity>());
            obj.SaveChanges();
        }

        [HttpPut]
        [Route("api/Activity/Book/{id}")]
        public void Book([FromUri] int id)
        {

            activityProduct = obj.Activities.Find(id);
            activityProduct.IsBooked = "true";
            obj.SaveChanges();
        }


        [HttpPut]
        [Route("api/Activity/Save/{id}")]
        public void Save([FromUri] int id)
        {
            activityProduct = obj.Activities.Find(id);
            activityProduct.IsSaved = "true";
            obj.SaveChanges();
        }

        [HttpGet]
        [Route("api/Activity/GetSavedItems")]
        public IEnumerable<Activity> GetSavedItems()
        {
            IEnumerable<Activity> enumerable = displayActivity();
            List<Activity> activityItems = enumerable.ToList();
            List<Activity> activitySavedItems = new List<Activity>();

            for (int i = 0; i < activityItems.Count; i++)
            {
                activityProduct = activityItems[i];
                if (activityProduct.IsSaved == "true")
                {
                    activitySavedItems.Add(activityProduct);
                }
            }
            return activitySavedItems;
        }


        [HttpGet]
        [Route("api/Activity/GetBookedItems")]
        public IEnumerable<Activity> GetBookedItems()
        {
            IEnumerable<Activity> enumerable = displayActivity();
            List<Activity> activityItems = enumerable.ToList();
            List<Activity> activityBookedItems = new List<Activity>();

            for (int i = 0; i < activityItems.Count; i++)
            {
                activityProduct = activityItems[0];
                if (activityProduct.IsBooked == "true")
                {
                    activityBookedItems.Add(activityProduct);
                }
            }
            return activityBookedItems;
        }
    }
}

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
    public class AirController : ApiController
    {
        ProductsTableEntities obj = new ProductsTableEntities();
        Air airProduct = new Air();
        [HttpGet]
        public IEnumerable<Air> displayAir()
        {
            using (ProductsTableEntities obj = new ProductsTableEntities())
            {

                return obj.Airs.ToList();
            }
        }

        [HttpPost]
        public void Post([FromBody]JObject jsonFormatInput)
        {
          
            obj.Airs.Add(jsonFormatInput.ToObject<Air>());
            obj.SaveChanges();
        }

        [HttpPut]
        [Route("api/Air/Book/{id}")]
        public void Book([FromUri] int id)
        {

            airProduct = obj.Airs.Find(id);
            airProduct.IsBooked = "true";
            obj.SaveChanges();
        }


        [HttpPut]
        [Route("api/Air/Save/{id}")]
        public void Save([FromUri] int id)
        {

            airProduct = obj.Airs.Find(id);
            airProduct.IsSaved = "true";
            obj.SaveChanges();
        }


        [HttpGet]
        [Route("api/Air/GetSavedItems")]
        public IEnumerable<Air> GetSavedItems()
        {
            IEnumerable<Air> enumerable = displayAir();
            List<Air> airItems = enumerable.ToList();
            List<Air> airSavedItems = new List<Air>();

            for (int i = 0; i < airItems.Count; i++)
            {
                airProduct = airItems[i];
                if (airProduct.IsSaved == "true")
                {
                    airSavedItems.Add(airProduct);
                }
            }
            return airSavedItems;
        }


        [HttpGet]
        [Route("api/Air/GetBookedItems")]
        public IEnumerable<Air> GetBookedItems()
        {
            IEnumerable<Air> enumerable = displayAir();
            List<Air> airItems = enumerable.ToList();
            List<Air> airBookedItems = new List<Air>();

            for (int i = 0; i < airItems.Count; i++)
            {
                airProduct = airItems[i];
                if (airProduct.IsBooked == "true")
                {
                    airBookedItems.Add(airProduct);
                }
            }
            return airBookedItems;
        }
        
    }
}

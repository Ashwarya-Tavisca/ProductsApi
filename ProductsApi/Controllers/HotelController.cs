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
    public class HotelController : ApiController
    {
        ProductsTableEntities obj = new ProductsTableEntities();
        Hotel hotelProduct = new Hotel();

        [HttpGet]
        public IEnumerable<Hotel> displayHotel()
        {
            using (ProductsTableEntities obj = new ProductsTableEntities())
            {

                return obj.Hotels.ToList();
            }
        }
        [HttpPost]
        public void Post([FromBody]JObject jsonFormatInput)
        {
            obj.Hotels.Add(jsonFormatInput.ToObject<Hotel>());
            obj.SaveChanges();
        }

        [HttpPut]
        [Route("api/Hotel/Book/{id}")]
        public void Book([FromUri] int id)
        {

            hotelProduct = obj.Hotels.Find(id);
            hotelProduct.IsBooked = "true";
            obj.SaveChanges();
        }


        [HttpPut]
        [Route("api/Hotel/Save/{id}")]
        public void Save([FromUri] int id)
        {
            hotelProduct = obj.Hotels.Find(id);
            hotelProduct.IsSaved = "true";
            obj.SaveChanges();
        }

        [HttpGet]
        [Route("api/Hotel/GetSavedItems")]
        public IEnumerable<Hotel> GetSavedItems()
        {
            IEnumerable<Hotel> enumerable = displayHotel();
            List<Hotel> hotelItems = enumerable.ToList();
            List< Hotel> hotelSavedItems = new List<Hotel>();

            for (int i = 0; i < hotelItems.Count; i++)
            {
                hotelProduct = hotelItems[i];
                if (hotelProduct.IsSaved == "true")
                {
                    hotelSavedItems.Add(hotelProduct);
                }
            }
            return hotelSavedItems;
        }


        [HttpGet]
        [Route("api/Hotel/GetBookedItems")]
        public IEnumerable<Hotel> GetBookedItems()
        {
            IEnumerable<Hotel> enumerable = displayHotel();
            List<Hotel> hotelItems = enumerable.ToList();
            List<Hotel> hotelBookedItems = new List<Hotel>();

            for (int i = 0; i < hotelItems.Count; i++)
            {
                hotelProduct = hotelItems[i];
                if (hotelProduct.IsBooked == "true")
                {
                    hotelBookedItems.Add(hotelProduct);
                }
            }
            return hotelBookedItems;
        }

      
    }
}

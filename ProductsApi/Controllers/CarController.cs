using DatabaseLayer;
using Newtonsoft.Json.Linq;
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
        ProductsTableEntities obj = new ProductsTableEntities();
        Car carProduct = new Car();

        [HttpGet]
        public IEnumerable<Car> displayCar()
        {
            using (ProductsTableEntities obj = new ProductsTableEntities())
            {

                return obj.Cars.ToList();
            }
        }
        [HttpPost]
        public void Post([FromBody]JObject jsonFormatInput)
        {
            obj.Cars.Add(jsonFormatInput.ToObject<Car>());
            obj.SaveChanges();
        }

        [HttpPut]
        [Route("api/Car/Book/{id}")]
        public void Book([FromUri] int id)
        {

            carProduct = obj.Cars.Find(id);
            carProduct.IsBooked = "true";
            obj.SaveChanges();
        }

        [HttpPut]
        [Route("api/Car/Save/{id}")]
        public void Save([FromUri] int id)
        {

            carProduct = obj.Cars.Find(id);
            carProduct.isSaved = "true";
            obj.SaveChanges();
        }

        [HttpGet]
        [Route("api/Car/GetSavedItems")]
        public IEnumerable<Car> GetSavedItems()
        {
            IEnumerable<Car> enumerable = displayCar();
            List<Car> carItems = enumerable.ToList();
            List<Car> carSavedItems = new List<Car>();

            for (int i = 0; i < carItems.Count; i++)
            {
                carProduct = carItems[i];
                if (carProduct.isSaved == "true")
                {
                    carSavedItems.Add(carProduct);
                }
            }
            return carSavedItems;
        }


        [HttpGet]
        [Route("api/Car/GetBookedItems")]
        public IEnumerable<Car> GetBookedItems()
        {
            IEnumerable<Car> enumerable = displayCar();
            List<Car> carItems = enumerable.ToList();
            List<Car> carBookedItems = new List<Car>();

            for (int i = 0; i < carItems.Count; i++)
            {
                carProduct = carItems[i];
                if (carProduct.IsBooked == "true")
                {
                    carBookedItems.Add(carProduct);
                }
            }
            return carBookedItems;
        }

    }
}

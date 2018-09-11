using Newtonsoft.Json;
using ProductUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ProductUI.Controllers
{
    public class CarController : Controller
    {

        public ActionResult GetCarProducts()
        {
            List<CarProduct> list = GetCarProductsList();

            return View(list);
        }

        public List<CarProduct> GetCarProductsList()
        {
            string url = "http://localhost:59069/";

            List<CarProduct> carProduct = new List<CarProduct>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("/api/Car").Result;

                if (response.IsSuccessStatusCode)
                {
                    string CarProductResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<CarProduct>>(CarProductResponse);

                }
            }
            return carProduct;
        }

        //Insert
        public List<CarProduct> AddProductIntoDB(CarProduct carProduct)
        {

            string url = "http://localhost:59069/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                carProduct.IsBooked = "false";
                carProduct.isSaved = "false";
                var httpContent = new StringContent(JsonConvert.SerializeObject(carProduct), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("/api/Car", httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    string CarProductResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<CarProduct>>(CarProductResponse);

                }
            }
            return null;
        }

        public ActionResult AddCarProduct()
        {
            return View();
        }

        public List<CarProduct> SaveProductIntoDB(int productId)
        {

            string url = "http://localhost:59069/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var httpContent = new StringContent(JsonConvert.SerializeObject(productId), Encoding.UTF8, "application/json");

                List<CarProduct> list = GetCarProductsList();

                CarProduct dummy = list.Find(item => item.ProductId == productId);
                if (dummy.IsBooked == "true")
                    ViewData["CantSave"] = "true";

                HttpResponseMessage response = client.PutAsync("/api/Car/Save/" + productId, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    string CarProductResponse = response.Content.ReadAsStringAsync().Result;
                    JsonConvert.DeserializeObject<List<CarProduct>>(CarProductResponse);

                }
                HttpResponseMessage response1 = client.GetAsync("/api/Car").Result;
                if (response1.IsSuccessStatusCode)
                {
                    string CarProductResponse = response1.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<CarProduct>>(CarProductResponse);
                }
            }
            return null;
        }

        public List<CarProduct> BookProductIntoDB(int productId)
        {

            string url = "http://localhost:59069/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var httpContent = new StringContent(JsonConvert.SerializeObject(productId), Encoding.UTF8, "application/json");

                List<CarProduct> list = GetCarProductsList();
                CarProduct dummy = list.Find(item => item.ProductId == productId);
                if (dummy.IsBooked == "true")
                    ViewData["CantBook"] = "true";

                HttpResponseMessage response = client.PutAsync("/api/Car/Book/" + productId, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    string CarProductResponse = response.Content.ReadAsStringAsync().Result;
                    JsonConvert.DeserializeObject<List<CarProduct>>(CarProductResponse);
                }
                HttpResponseMessage response1 = client.GetAsync("/api/Car").Result;
                if (response1.IsSuccessStatusCode)
                {
                    string CarProductResponse = response1.Content.ReadAsStringAsync().Result; 
                    return JsonConvert.DeserializeObject<List<CarProduct>>(CarProductResponse);
                }
            }
            return null;
        }
        public ActionResult OnBookClick(int productId)
        {
            List<CarProduct> list = BookProductIntoDB(productId);
            return View(list);
        }
        public ActionResult OnSaveClick(int productId)
        {
            List<CarProduct> list = SaveProductIntoDB(productId);
            return View(list);
        }

        public ActionResult GetSavedProds()
        {
            List<CarProduct> list = GetSavedProducts();
            return View(list);
        }

        public List<CarProduct> GetSavedProducts()
        {
            string url = "http://localhost:59069/";

            List<CarProduct> carProduct = new List<CarProduct>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("/api/Car/GetSavedItems").Result;

                if (response.IsSuccessStatusCode)
                {
                    string CarProductResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<CarProduct>>(CarProductResponse);
                }
            }
            return carProduct;

        }

        public ActionResult GetBookedProds()
        {
            List<CarProduct> list = GetBookedProducts();
            return View(list);
        }
        public List<CarProduct> GetBookedProducts()
        {
            string url = "http://localhost:59069/";

            List<CarProduct> carProduct = new List<CarProduct>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("/api/Car/GetBookedItems").Result;

                if (response.IsSuccessStatusCode)
                {
                    string CarProductResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<CarProduct>>(CarProductResponse);
                }
            }
            return carProduct;

        }
    }
}
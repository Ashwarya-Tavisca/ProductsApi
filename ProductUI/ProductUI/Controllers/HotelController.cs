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
    public class HotelController : Controller
    {
  
        public ActionResult GetHotelProducts()
        {
            List<HotelProduct> list = GetHotelProductsList();

            return View(list);
        }

        public List<HotelProduct> GetHotelProductsList()
        {
            string url = "http://localhost:59069/";

            List<HotelProduct> hotelProduct = new List<HotelProduct>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("/api/Hotel").Result;

                if (response.IsSuccessStatusCode)
                {   
                    string HotelProductResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<HotelProduct>>(HotelProductResponse);

                }
            }
            return hotelProduct;
        }
        public List<HotelProduct> AddProductIntoDB(HotelProduct hotelProduct)
        {

            string url = "http://localhost:59069/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                hotelProduct.IsBooked = "false";
                hotelProduct.IsSaved = "false";
                var httpContent = new StringContent(JsonConvert.SerializeObject(hotelProduct), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("/api/Hotel", httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    string HotelProductResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<HotelProduct>>(HotelProductResponse);

                }
            }
            return null;
        }

        public ActionResult AddHotelProduct()
        {
            return View();
        }

        public List<HotelProduct> SaveProductIntoDB(int productId)
        {

            string url = "http://localhost:59069/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var httpContent = new StringContent(JsonConvert.SerializeObject(productId), Encoding.UTF8, "application/json");

                List<HotelProduct> list = GetHotelProductsList();

                HotelProduct dummy = list.Find(item => item.ProductId == productId);
                if (dummy.IsBooked == "true")
                    ViewData["CantSave"] = "true";

                HttpResponseMessage response = client.PutAsync("/api/Hotel/Save/" + productId, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    string HotelProductResponse = response.Content.ReadAsStringAsync().Result;
                    JsonConvert.DeserializeObject<List<HotelProduct>>(HotelProductResponse);

                }
                HttpResponseMessage response1 = client.GetAsync("/api/Hotel").Result;
                if (response1.IsSuccessStatusCode)
                { 
                    string HotelProductResponse = response1.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<HotelProduct>>(HotelProductResponse);
                }
            }
            return null;
        }

        public List<HotelProduct> BookProductIntoDB(int productId)
        {

            string url = "http://localhost:59069/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var httpContent = new StringContent(JsonConvert.SerializeObject(productId), Encoding.UTF8, "application/json");

                List<HotelProduct> list = GetHotelProductsList();
                HotelProduct dummy = list.Find(item => item.ProductId == productId);
                if (dummy.IsBooked == "true")
                    ViewData["CantBook"] = "true";

                HttpResponseMessage response = client.PutAsync("/api/Hotel/Book/" + productId, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    string HotelProductResponse = response.Content.ReadAsStringAsync().Result;
                    JsonConvert.DeserializeObject<List<HotelProduct>>(HotelProductResponse);
                }
                HttpResponseMessage response1 = client.GetAsync("/api/Hotel").Result;
                if (response1.IsSuccessStatusCode)
                {
                    string HotelProductResponse = response1.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<HotelProduct>>(HotelProductResponse);
                }
            }
            return null;
        }
        public ActionResult OnBookClick(int productId)
        {
            List<HotelProduct> list = BookProductIntoDB(productId);
            return View(list);
        }
        public ActionResult OnSaveClick(int productId)
        {
            List<HotelProduct> list = SaveProductIntoDB(productId);
            return View(list);
        }

        public ActionResult GetSavedProds()
        {
            List<HotelProduct> list = GetSavedProducts();
            return View(list);
        }

        public List<HotelProduct> GetSavedProducts()
        {
            string url = "http://localhost:59069/";

            List<HotelProduct> hotelProduct = new List<HotelProduct>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("/api/Hotel/GetSavedItems").Result;

                if (response.IsSuccessStatusCode)
                {
                    string HotelProductResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<HotelProduct>>(HotelProductResponse);
                }
            }
            return hotelProduct;

        }

        public ActionResult GetBookedProds()
        {
            List<HotelProduct> list = GetBookedProducts();
            return View(list);
        }
        public List<HotelProduct> GetBookedProducts()
        {
            string url = "http://localhost:59069/";

            List<HotelProduct> hotelProduct = new List<HotelProduct>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("/api/Hotel/GetBookedItems").Result;

                if (response.IsSuccessStatusCode)
                {
                    string HotelProductResponse = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<HotelProduct>>(HotelProductResponse);
                }
            }
            return hotelProduct;

        }
    }
}
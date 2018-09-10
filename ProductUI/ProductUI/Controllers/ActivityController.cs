using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class ActivityController : Controller
    {
        // GET: Activity
        public ActionResult GetActivityProducts()
        {
            List<ActivityProduct> list = GetActivityProductsList();

            return View(list);
        }

        public List<ActivityProduct> GetActivityProductsList()
        {
            string url = "http://localhost:59069/";

            List<ActivityProduct> activityProduct = new List<ActivityProduct>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("/api/Activity").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    string ActivityProductResponse = response.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    return JsonConvert.DeserializeObject<List<ActivityProduct>>(ActivityProductResponse);

                }
            }
            return activityProduct;
        }

        //Insert
        public List<ActivityProduct> AddProductIntoDB(ActivityProduct activityProduct)
        {

            string url = "http://localhost:59069/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                activityProduct.IsBooked = "false";
                activityProduct.IsSaved = "false";
                var httpContent = new StringContent(JsonConvert.SerializeObject(activityProduct), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("/api/Activity", httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    string ActivityProductResponse = response.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    return JsonConvert.DeserializeObject<List<ActivityProduct>>(ActivityProductResponse);

                }
            }
            return null;
        }

        public ActionResult AddActivityProduct()
        {
            return View();
        }

        public List<ActivityProduct> SaveProductIntoDB(int productId)
        {

            string url = "http://localhost:59069/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var httpContent = new StringContent(JsonConvert.SerializeObject(productId), Encoding.UTF8, "application/json");

                List<ActivityProduct> list = GetActivityProductsList();

                ActivityProduct dummy = list.Find(item => item.ProductId == productId);
                if (dummy.IsBooked == "true")
                    ViewData["CantSave"] = "true";

                HttpResponseMessage response = client.PutAsync("/api/Activity/Save/" + productId, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    string ActivityProductResponse = response.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    JsonConvert.DeserializeObject<List<ActivityProduct>>(ActivityProductResponse);

                }
                HttpResponseMessage response1 = client.GetAsync("/api/Activity").Result;
                if (response1.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    string ActivityProductResponse = response1.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    return JsonConvert.DeserializeObject<List<ActivityProduct>>(ActivityProductResponse);
                }
            }
            return null;
        }

        public List<ActivityProduct> BookProductIntoDB(int productId)
        {

            string url = "http://localhost:59069/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var httpContent = new StringContent(JsonConvert.SerializeObject(productId), Encoding.UTF8, "application/json");

                List<ActivityProduct> list = GetActivityProductsList();
                ActivityProduct dummy = list.Find(item => item.ProductId == productId);
                if (dummy.IsBooked == "true")
                    ViewData["CantBook"] = "true";

                HttpResponseMessage response = client.PutAsync("/api/Activity/Book/" + productId, httpContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    string ActivityProductResponse = response.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                     JsonConvert.DeserializeObject<List<ActivityProduct>>(ActivityProductResponse);
                }
                HttpResponseMessage response1 = client.GetAsync("/api/Activity").Result;
                if (response1.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    string ActivityProductResponse = response1.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    return JsonConvert.DeserializeObject<List<ActivityProduct>>(ActivityProductResponse);
                }
            }
            return null;
        }
        public ActionResult OnBookClick(int productId)
        {
            List<ActivityProduct> list = BookProductIntoDB(productId);
            return View(list);
        }
        public ActionResult OnSaveClick(int productId)
        {
            List<ActivityProduct> list = SaveProductIntoDB(productId);
            return View(list);
        }

        public ActionResult GetSavedProds()
        {
            List<ActivityProduct> list = GetSavedProducts();
            return View(list);
        }

        public List<ActivityProduct> GetSavedProducts()
        {
            string url = "http://localhost:59069/";

            List<ActivityProduct> activityProduct = new List<ActivityProduct>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("/api/Activity/GetSavedItems").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    string ActivityProductResponse = response.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    return JsonConvert.DeserializeObject<List<ActivityProduct>>(ActivityProductResponse);
                }
            }
            return activityProduct;

        }

        public ActionResult GetBookedProds()
        {
            List<ActivityProduct> list = GetBookedProducts();
            return View(list);
        }
        public List<ActivityProduct> GetBookedProducts()
        {
            string url = "http://localhost:59069/";

            List<ActivityProduct> activityProduct = new List<ActivityProduct>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("/api/Activity/GetBookedItems").Result;

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    string ActivityProductResponse = response.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list  
                    return JsonConvert.DeserializeObject<List<ActivityProduct>>(ActivityProductResponse);
                }
            }
            return activityProduct;

        }
    }
}

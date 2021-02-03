using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PropertyWebApp.Models;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;
using PropertyWebApp.DataAccess;

namespace PropertyWebApp.Controllers
{
    public class PropertiesController : Controller
    {
        private PropertyDbContext db = new PropertyDbContext();

        private readonly IHttpClientFactory _clientFactory;

        //public IEnumerable<PropertyViewModel> Properties {get; set;}

        const string BASE_URL = "https://samplerspubcontent.blob.core.windows.net/public/properties.json";
        public PropertiesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        string responseString = null;
        public async Task<IActionResult> Index()
        {
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{BASE_URL}");
            message.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(message);

            if(response.IsSuccessStatusCode)
            {
                responseString= await response.Content.ReadAsStringAsync();
                var properties = JsonConvert.DeserializeObject<PropertyWebApp.Models.PropertyViewModel>(responseString);
            }
            return View();
        }

        public async Task<ActionResult> GetAPItringAsync(Property property)
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Property>>(responseString);
            return View(model);
        }
        public IActionResult Create() 
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("propertyId, address, yearBuilt, listPrice, monthlyRent")] Property property)
        //{
        //    if (ModelState.IsValid) {
        //        HttpContent httpContent = new StringContent (Newtonsoft.Json.JsonConvert.SerializeObject (property), Encoding.UTF8);
        //        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue ("application/json");

        //        var message = new HttpRequestMessage ();
        //        message.Content = httpContent;
        //        message.Method = HttpMethod.Post;
        //        message.RequestUri = new Uri ($"{BASE_URL}api/properties");

        //        HttpClient client = _clientFactory.CreateClient ();
        //        HttpResponseMessage response = await client.SendAsync (message);

        //        var result = await response.Content.ReadAsStringAsync ();

        //        return RedirectToAction (nameof (Index));
        //    }

        //    return View (property);
        //}
    }
}
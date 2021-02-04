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
using PropertyWebApp.ViewModels;
using System.Linq;

namespace PropertyWebApp.Controllers
{
    public class PropertiesController : Controller
    {

        private readonly IHttpClientFactory _clientFactory;

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

            if (response.IsSuccessStatusCode)
            {
                responseString = await response.Content.ReadAsStringAsync();
                var propertiesResponse = JsonConvert.DeserializeObject<PropertyWebApp.Models.PropertyViewModel>(responseString);
                var listOfProperties = propertiesResponse.Properties.Select(prop => new ListPropertyViewModel
                {
                    propertyId = prop.Id,
                    address = prop.Address?.Address1 + " " + prop.Address?.City + " " + prop.Address?.Country + " " + prop.Address?.State + " " + prop.Address?.Zip,
                    yearBuilt = prop.Physical == null ? 0 : prop.Physical.YearBuilt,
                    listPrice = prop.Financial == null ? 1 : prop.Financial.ListPrice,
                    monthlyRent = prop.Financial == null ? 0 : prop.Financial.MonthlyRent
                })
                .ToList();

                return View(listOfProperties);
            }
            return View();
        }

        //public async Task<ActionResult> GetAPItringAsync(Property property)
        //{
        //    var model = JsonConvert.DeserializeObject<IEnumerable<Property>>(responseString);
        //    return View(model);
        //}
        //public IActionResult Create() 
        //{
        //    return View();
        //}

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
        [HttpPost]
        public ActionResult Save(ListPropertyViewModel model)
        {
            return Json(new { message = "Hola" });
        }
    }
}
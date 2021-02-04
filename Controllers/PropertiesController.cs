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

        
        [HttpPost]
        public ActionResult Save(ListPropertyViewModel model)
        {
            return Json(new { message = "Hola" });
        }
    }
}
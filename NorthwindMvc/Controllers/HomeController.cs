using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindMvc.Models;
using NorthwinDB;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Newtonsoft.Json;

namespace NorthwindMvc.Controllers
{
    public class HomeController : Controller
    {
        private Northwind db;
        private readonly ILogger<HomeController> _logger;

        private readonly IHttpClientFactory clientFactory;
        public HomeController(ILogger<HomeController> logger, Northwind injectedContext,
                                IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            db = injectedContext;
            clientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel
            {
                VisitorCount = (new Random()).Next(1, 1001),
                Categories= db.Categories.ToList(),
                Products= db.Products.ToList()
            };
            return View(model); // pass model to view
        }

        [Route("private")]
        public IActionResult Privacy()
        {
            return View();
        }

       /* [Authorize(Roles = "Sales,Marketing" )]
        public IActionResult SalesAndMarketingEmployeesOnly()
        {
        return View();
        }*/

        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public IActionResult AboutUs()
        {
        return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public IActionResult ProductDetail(int? id) {
            if(!id.HasValue)
            {
                return NotFound("You must pass a product ID in the route, for example,/Home/ProductDetail/21");
            }
            var model = db.Products.SingleOrDefault(p => p.ProductID == id);
            if(model == null)
            {
                return
                NotFound($"Product with ID of {id} not found.");
            }
            return View(model);// pass model to view and then return result
        }

        public IActionResult ProductsThatCostMoreThan(decimal? price){
            if(!price.HasValue){
                return NotFound("You must pass a product price in the query string, for example,/Home/ProductsThatCostMoreThan?price=50");
            }

            IEnumerable<Product> model = db.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .AsEnumerable()
            // switch to client-side
            .Where(p => p.UnitPrice > price);

            if(model.Count() == 0)
            {
                return NotFound($"No products cost more than {price:C}.");
            }
            ViewData["MaxPrice"] = price.Value.ToString("C");
            return View(model);// pass model to view
        }

        public IActionResult CategoryDetail(int? id) {
            if(!id.HasValue)
            {
                return NotFound("You must pass a Category ID in the route, for example,/Home/CategoryDetail/21");
            }
            var model = db.Categories.Include(p => p.Products).SingleOrDefault(c => c.CategoryID == id);
            if(model == null)
            {
                return
                NotFound($"Category with ID of {id} not found.");
            }
            return View(model);// pass model to view and then return result
        }

        public async Task<IActionResult> Customers(string country){
	        string uri;
            if(string.IsNullOrEmpty(country)){
                ViewData["Title"] = "All Customers Worldwide";
                uri ="api/customers/";
            }
            else{
                ViewData["Title"] = $"Customers in {country}";
                uri = $"api/customers/?country={country}";
            }
            
            var client = clientFactory.CreateClient(name:"NorthwindService");
            var request = new HttpRequestMessage(method:HttpMethod.Get, requestUri:uri);
            HttpResponseMessage response = await client.SendAsync(request);
            string jsonString = await response.Content.ReadAsStringAsync();
            IEnumerable<Customer> model = JsonConvert.DeserializeObject<IEnumerable<Customer>>(jsonString);
            return View(model);
        }

        public async Task<IActionResult> Employees(string country){
	        string uri;
            if(string.IsNullOrEmpty(country)){
                ViewData["Title"] = "All Employees Worldwide";
                uri ="api/employees/";
            }
            else{
                ViewData["Title"] = $"Employees in {country}";
                uri = $"api/employees/?country={country}";
            }
            
            var client = clientFactory.CreateClient(name:"NorthwindService");
            var request = new HttpRequestMessage(method:HttpMethod.Get, requestUri:uri);
            HttpResponseMessage response = await client.SendAsync(request);
            string jsonString = await response.Content.ReadAsStringAsync();
            IEnumerable<Employee> model = JsonConvert.DeserializeObject<IEnumerable<Employee>>(jsonString);
            return View(model);
        }   
    }
}

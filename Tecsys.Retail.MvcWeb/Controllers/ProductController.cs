using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tecsys.Retail.Model;
using Tecsys.Retail.RestApiClient;
using System.Linq;

namespace Tecsys.Retail.MvcWeb.Controllers
{
    public class ProductController : RetailController
    {
        private ProductApiClient _apiClient;

        public ProductController()
        {
        }

        public ProductController(ProductApiClient apiClient)
        {
            _apiClient = apiClient;
        }


        public async Task<ActionResult> GetCars()
        {
            try
            {
                //Thread.Sleep(5000);
                var cars = await _apiClient.GetCarsAsync();
                ViewBag.Action = "GetCarsAsync";
                return View("Products", cars);

            }
            catch (Exception ex)
            {
                throw new System.Net.Http.HttpRequestException("Error: Failed to gets Cars", ex);
            }

        }


        [HttpGet]
        [Route("Product/Products")]
        public async Task<ActionResult> Products(string searchText)
        {
            try
            {
                Thread.Sleep(5000);

                IEnumerable<IProductModel> products = await _apiClient.GetProductsAsync(searchText);
                ViewBag.Action = "Products";
                ViewBag.SearchText = searchText;
                ViewBag.ProductCount = products.Count();
                return View("Products", products);

            }
            catch (Exception ex)
            {
                throw new System.Net.Http.HttpRequestException("Error: Failed to search for products", ex);
            }
        }
    }
}

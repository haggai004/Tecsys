using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Tecsys.Retail.Biz;
using Tecsys.Retail.Domain;
using System.Web.Http.Cors;

namespace Tecsys.Retail.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        public async Task<JArray> GetCars()
        {
            try
            {
                IEnumerable<Domain.IProduct> cars = await _productService.GetCarsAsync();
                string jsonString =
                    JsonConvert.SerializeObject(
                        cars,
                        Formatting.Indented,
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                    );

                HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = httpContent;
                response.RequestMessage = this.Request;

                response = Request.CreateResponse(HttpStatusCode.OK, cars, System.Net.Http.Formatting.JsonMediaTypeFormatter.DefaultMediaType);

                JArray jArray = JArray.FromObject(cars);
                return jArray;
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException)
                    throw ex;
                else
                    throw new HttpRequestException("Error: Failed to get cars", ex);
            }                                                           
                                                                                                 
        }

        // GET api/product/searchText
        [Route("{searchText}")]
        public async Task<IEnumerable<IProduct>> GetProducts(string searchText)
        {
            try
            {
                HttpResponseMessage response;
                if (string.IsNullOrEmpty(searchText))
                {
                    response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error: null searchText");
                    throw new HttpResponseException(response);
                }

                return await _productService.GetProductsAsync(searchText);
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException)
                    throw ex;
                else
                    throw new HttpRequestException("Error: Failed to search for products", ex);
            }
        }

    }
}

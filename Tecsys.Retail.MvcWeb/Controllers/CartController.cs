using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tecsys.Retail.RestApiClient;
using Tecsys.Retail.Model;

namespace Tecsys.Retail.MvcWeb.Controllers
{
    public class CartController : RetailController
    {
        private ICartApiClient _cartApiClient;
        TypeMapping.ITypeMapper _typeMapper;

        public CartController()
        {

        }

        public CartController(ICartApiClient cartApiClient,TypeMapping.ITypeMapper typeMapper)
        {
            _cartApiClient = cartApiClient;
            _typeMapper = typeMapper;
        }

        // GET: Cart/NewCartItemGetAsync
        [HttpGet]
        public async Task<PartialViewResult> NewCartItemGetAsync(string cartId,int productId)
        {
            try
            {
                var cartItem = await _cartApiClient.CreateCartItemAsync(cartId,productId);
                CartItemModel cartItemModel = _typeMapper.Map<ICartItemModel, CartItemModel>(cartItem);

                return PartialView("AddCartItem", cartItemModel);
            }
            catch(Exception ex)
            {
                throw new HttpRequestException("Error: Failed to create a new Cart Item", ex);
            }
        }


        // POST: Cart/AddCartItemPost
        [HttpPost]
        public async Task<ActionResult> AddCartItemAsync(CartItemModel cartItemModel)
        {
            try
            {
                if (cartItemModel?.Quantity == 0)
                {
                    this.ModelState.AddModelError("Quantity", "Error: Quatity is 0");
                    throw new ArgumentException("Error: Quantity of of the selected product is 0");
                }

                if(!ModelState.IsValid)
                {
                    //for development only
                    var errors = ModelState.ToDictionary(e => e.Key, e=>e.Value);
                    JArray jarr = JArray.FromObject(errors);
                    throw new Exception($"ModeState is Invalid. Errors; {jarr}");
                    //return new  HttpStatusCodeResult(HttpStatusCode.ExpectationFailed, jarr.ToString());
                }

                if (ModelState.IsValid)
                {
                    var response = await _cartApiClient.AddCartItemAsync(cartItemModel);

                    string json = "{\"HttpStatusCode\":\"OK\"}";
                    return Json(new { responseText = "OK" });
                }

                return PartialView("AddCartItem", cartItemModel);
            }
            catch(Exception ex)
            {
                throw new HttpRequestException("Error: Failed to create a new Cart Item", ex);
                //return Json(new { responseText = "Failure" });

            }
        }

    }
}

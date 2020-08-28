using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Tecsys.Retail.Domain;
using Tecsys.Retail.Model;
using Tecsys.Retail.TypeMapping;
using System.Web.Http.Cors;
using Tecsys.Retail.Biz;


namespace Tecsys.Retail.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Cart")]
    public class CartController : ApiController
    {
        private readonly ICartService _cartBiz;
        private ITypeMapper _mapper;

        public CartController()
        {

        }

        public CartController(ICartService cartBiz, ITypeMapper mapper)
        {
            _cartBiz = cartBiz;
            _mapper = mapper;
        }

        public async Task<ICart> GetCart(string cartId)
        {
            var cart = await _cartBiz.GetCartAsync(cartId);
            return cart;
        }

        public async Task<HttpResponseMessage> AddCartItem(CartItem cartItem)
        {
            try
            {
                if (!this.ModelState.IsValid)
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"$cartItem Failed Validation"));

                await _cartBiz.AddCartItemAsync(cartItem);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                if (ex is HttpResponseException)
                    throw ex;
                else
                    throw new HttpRequestException("Error: Failed to add a cart item", ex);
            }
        }

        [HttpGet]
        [Route("{NewCartItemGet}")]
        public async Task<ICartItem> NewCartItemGet(string cartId, int productId)
        {
            try
            {
                ICartItem cartItem = await _cartBiz.CreateCartItemAsync(cartId, productId);
                return cartItem;
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException)
                    throw ex;
                else
                    throw new HttpRequestException("Error: Failed to create a new cart item", ex);
            }
        }

        public async Task<ICartItem> GetCartItem(string itemId)
        {
            try
            {
                return await _cartBiz.GetCartItemAsync(itemId);
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException)
                    throw ex;
                else
                    throw new HttpRequestException("Error: Failed to get cart item", ex);
            }
        }
    }
}
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
using Tecsys.Retail.Interface.Biz;

namespace Tecsys.Retail.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CartItemController : ApiController
    {
        private readonly ICartBiz _cartItemService;
        private ITypeMapper _mapper;

        public CartItemController(ICartBiz cartItemService, ITypeMapper mapper)
        {
            _cartItemService = cartItemService;
            _mapper=mapper;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddToCart([FromBody] Model.CartItemModel cartItemModel)
        {
            try
            {
                if (!this.ModelState.IsValid)
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"$cartItem Failed Validation"));

                Domain.ICartItem cartItem = _mapper.Map<Model.CartItemModel, Domain.ICartItem>(cartItemModel);
                await _cartItemService.AddCartItem(cartItem);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotModified, ex.Message));
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}

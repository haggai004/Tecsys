

using System.Collections.Generic;
using System.Threading.Tasks;
using Tecsys.Retail.Domain;
using Tecsys.Retail.Repository;
using Tecsys.Retail.TypeMapping;
using Unity;

namespace Tecsys.Retail.Biz
{
    public class CartService : ICartService
    {
        readonly ICartRepository _cartRepository;
        readonly ITypeMapper _typeMapper;
        IProductService _productBiz;
        IUnityContainer _unityContainer;

        public CartService(IUnityContainer unityContainer,  ICartRepository cartRepository, IProductService productBiz,ITypeMapper typeMapper)
        {
            _unityContainer = unityContainer;
            _cartRepository = cartRepository;
            _typeMapper = typeMapper;
            _productBiz = productBiz;
        }

        public async Task<int> AddOrUpdateCartItemAsync(Domain.ICartItem cartItem)
        {
            Ef.CartItem cartItemEntity = _typeMapper.Map<Domain.ICartItem,  Ef.CartItem>(cartItem);
            return await Task.Run(() => _cartRepository.AddOrUpdateCartItemAsync(cartItemEntity));
        }

        public async Task<ICartItem> GetCartItemAsync(string itemId)
        {
            Ef.CartItem cartItemEntity = await Task.Run(() => _cartRepository.GetCartItemAsync(itemId));
            return _typeMapper.Map<Ef.CartItem, Domain.ICartItem>(cartItemEntity);
        }

        public async Task<List<ICartItem>> GetCartItemsAsync(string cartId)
        {
            List<Ef.CartItem> cartItemEntity = await Task.Run(() => _cartRepository.GetCartItemsAsync(cartId));
            return _typeMapper.Map<Ef.CartItem, Domain.ICartItem>(cartItemEntity);
        }

        public async Task<ICartItem> CreateCartItemAsync(string cartId, int productId)
        {
            var product = await _productBiz.GetProductAsync(productId);
            var cartItem = _unityContainer.Resolve<ICartItem>();
            cartItem.ItemId = System.Guid.NewGuid().ToString();
            cartItem.CartId = cartId;
            cartItem.ProductId = productId;
            cartItem.Product = product;

            return cartItem;
        }

        public async Task<ICart> GetCartAsync(string cartId)
        {
            ICart cart = new Cart();
            cart.CartId = cartId;
            cart.CartItems = await this.GetCartItemsAsync(cartId);
            return cart;
        }

    }
}

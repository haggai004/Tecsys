// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Tecsys.Retail.Biz;
using Unity;
using Tecsys.Retail.IocContainer;
using Tecsys.Retail.Domain;
using Tecsys.Retail.RestApi.Controllers;

namespace Tecsys.Retail.NUnitTests
{
    [TestFixture]
    public class CartControllerIntegrationTests
    {
        private IUnityContainer _container;
        private CartController _cartItemController;
        private ICartService _cartItemService;

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _container = UnityConfig.Container;
            _cartItemController = _container.Resolve<CartController>();
            _cartItemService = _container.Resolve<ICartService>();
        }

        [Test]
        public async Task AddCartItemAsync()
        {
            //Arrange
            CartItem cartItem = new CartItem();
            string itemId = Guid.NewGuid().ToString();
            string cartId = Guid.NewGuid().ToString();
            int productId = 3;
            int quantity = 10;
            DateTime dateCreated = DateTime.Now;
            cartItem.ItemId = itemId;
            cartItem.CartId = cartId;
            cartItem.Quantity = quantity;
            cartItem.DateCreated = dateCreated;
            cartItem.ProductId = productId;

            await _cartItemController.AddCartItemAsync(cartItem);

            Domain.ICartItem newCartItem = await _cartItemService.GetCartItemAsync(itemId);

            //Assert
            Assert.IsNotNull(newCartItem);
            Assert.IsTrue(newCartItem.CartId == cartId);
            Assert.IsTrue(newCartItem.ItemId == itemId);
            Assert.IsTrue(newCartItem.ProductId == productId);
            Assert.IsTrue(newCartItem.Quantity == quantity);
        }
    }
}

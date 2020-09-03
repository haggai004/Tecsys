// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity;
using Tecsys.Retail.IocContainer;
using Tecsys.Retail.Domain;
using Tecsys.Retail.Biz;

namespace Tecsys.Retail.NUnitTests
{
    [TestFixture]
    public class CartServiceintegrationTests
    {
        private IUnityContainer _container;
        private ICartService _cartBiz;
        string _cartId = Guid.NewGuid().ToString();

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _container = UnityConfig.Container;
            _cartBiz = _container.Resolve<ICartService>();
        }
        
        [Test]
        public async Task AddToCartIntegrationTest()
        {
            //Case #1 Add new
            //Arrange

            ICartItem cartItem = _container.Resolve<ICartItem>();
            string itemId = Guid.NewGuid().ToString();
            int productId = 3;
            int quantity = 10;
            DateTime dateCreated = DateTime.Now;
            cartItem.ItemId = itemId;
            cartItem.CartId = _cartId;
            cartItem.Quantity = quantity;
            cartItem.DateCreated = dateCreated;
            cartItem.ProductId = 3;
            int count = await _cartBiz.AddOrUpdateCartItemAsync(cartItem);

            Assert.IsTrue(count==1);

            Domain.ICartItem newCartItem = await _cartBiz.GetCartItemAsync(itemId);

            //Assert
            Assert.IsNotNull(newCartItem);
            Assert.IsTrue(newCartItem.CartId == _cartId);
            Assert.IsTrue(newCartItem.ItemId == itemId);
            Assert.IsTrue(newCartItem.ProductId == productId);
            Assert.IsTrue(newCartItem.Quantity == quantity);

            //Update existing
            int newQuantity = 999;
            cartItem.Quantity = newQuantity;
            count = await _cartBiz.AddOrUpdateCartItemAsync(cartItem);
            Assert.IsTrue(count==1);

            Domain.ICartItem updatedCartItem = await _cartBiz.GetCartItemAsync(itemId);
            Assert.IsNotNull(updatedCartItem);
            Assert.IsTrue(updatedCartItem.CartId == _cartId);
            Assert.IsTrue(updatedCartItem.ItemId == itemId);
            Assert.IsTrue(updatedCartItem.ProductId == productId);
            Assert.IsTrue(updatedCartItem.Quantity == newQuantity+quantity);
        }
    }
}

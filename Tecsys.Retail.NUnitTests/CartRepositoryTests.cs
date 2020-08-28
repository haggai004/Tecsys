// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity;
using Tecsys.Retail.IocContainer;
using Tecsys.Retail.Repository;

namespace Tecsys.Retail.NUnitTests
{
    [TestFixture]
    public class CartRepositoryTests
    {
        private IUnityContainer _container;
        private string _cartId;
        private ICartRepository _itemRepository;

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _container = UnityConfig.Container;
            _cartId = Guid.NewGuid().ToString();
            _itemRepository = _container.Resolve<ICartRepository>();
        }

        [Test]
        public async Task AddToCart()
        {
            //Arrange
            string itemId = Guid.NewGuid().ToString();
            int productId = 3;
            int quantity = 10;
            DateTime dateCreated = DateTime.Now;
            Ef.CartItem cartItem = new Ef.CartItem
            {
                ItemId =  itemId, 
                CartId = _cartId,
                Quantity = quantity,
                DateCreated = dateCreated,
                ProductId = 3
            };

            //Act
            bool isSuccess = await _itemRepository.AddCartItemAsync(cartItem);

            //Assert
            Assert.IsTrue(isSuccess);

            Ef.CartItem newCartItem = await _itemRepository.GetCartItemAsync(itemId);

            //Assert
            Assert.IsNotNull(newCartItem);
            Assert.IsTrue(newCartItem.CartId==_cartId);
            Assert.IsTrue(newCartItem.ItemId==itemId);
            Assert.IsTrue(newCartItem.ProductId==productId);
            Assert.IsTrue(newCartItem.Quantity==quantity);
        }

    }
}

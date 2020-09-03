// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity;
using Tecsys.Retail.IocContainer;
using Tecsys.Retail.Domain;
using Tecsys.Retail.Biz;
using Tecsys.Retail.RestApiClient;
using Tecsys.Retail.Model;
using Moq;
using Moq.AutoMock;
using Moq.Protected;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Tecsys.Retail.RestApi.Controllers;
using Tecsys.Retail.TypeMapping;
using Tecsys.Retail.Repository;

namespace Tecsys.Retail.NUnitTests
{
    [TestFixture]
    public class CartServiceUnitTests
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
        public async Task AddOrUpdateCartItemAsync()
        {
            //Case #1 Add new
            //ARRANGE

            ICartItem cartItem = _container.Resolve<ICartItem>();
            string itemId = Guid.NewGuid().ToString();
            string cartId = Guid.NewGuid().ToString();
            int productId = 3;
            int quantity = 10;
            DateTime dateCreated = DateTime.Now;
            cartItem.ItemId = itemId;
            cartItem.CartId = cartId;
            cartItem.Quantity = quantity;
            cartItem.DateCreated = dateCreated;
            cartItem.ProductId = 3;

            var cartRepositoryMock = new Mock<ICartRepository>();
            cartRepositoryMock.Setup(x => x.AddOrUpdateCartItemAsync(It.IsAny<Ef.CartItem>())).ReturnsAsync(1);

            Domain.IProduct product = new Product();
            product.ProductId = productId;
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.GetProductAsync(It.IsAny<int>())).ReturnsAsync(product);

            ITypeMapper typeMapper = _container.Resolve<ITypeMapper>();
            ICartService cartService = new CartService(_container, cartRepositoryMock.Object, productServiceMock.Object, typeMapper);

            //ACT
            int count = await cartService.AddOrUpdateCartItemAsync(cartItem);

            //ASSERT
            Assert.IsTrue(count==1);

            //ARRANGE
            cartRepositoryMock.Setup(x => x.AddOrUpdateCartItemAsync(It.IsAny<Ef.CartItem>())).ReturnsAsync(0);

            //ACT
            count = await cartService.AddOrUpdateCartItemAsync(cartItem);

            //ASSERT
            Assert.IsTrue(count==0);

        }
    }
}

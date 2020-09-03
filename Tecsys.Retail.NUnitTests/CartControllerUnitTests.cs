// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity;
using Tecsys.Retail.IocContainer;
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
using Tecsys.Retail.Biz;
using Tecsys.Retail.Domain;
using Tecsys.Retail.TypeMapping;

namespace Tecsys.Retail.NUnitTests
{
    [TestFixture]
    public class CartControllerUnitTests
    {
        private IUnityContainer _container;
        private CartController _cartController;
        private ICartService _cartService;

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _container = UnityConfig.Container;
            _cartController = _container.Resolve<CartController>();
            _cartService = _container.Resolve<ICartService>();
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

            Task<bool> task = new Task<bool>(() => false);
            var cartServiceMock = new Mock<ICartService>();
            cartServiceMock.Setup(x => x.AddOrUpdateCartItemAsync(cartItem)).ReturnsAsync(1);
            ITypeMapper typeMapper = _container.Resolve<ITypeMapper>();

            CartController cartController =new CartController(cartServiceMock.Object,typeMapper);

            //ACT
            var response = await cartController.AddCartItemAsync(cartItem);
            
            //ASSERT
            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode==HttpStatusCode.OK);

            //ARRANGE
            cartServiceMock.Setup(x => x.AddOrUpdateCartItemAsync(cartItem)).ReturnsAsync(0);
            //ACT
            response = await cartController.AddCartItemAsync(cartItem);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.ExpectationFailed);
        }
    }
}

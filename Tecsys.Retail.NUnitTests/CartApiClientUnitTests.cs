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

namespace Tecsys.Retail.NUnitTests
{
    [TestFixture]
    public class CartApiClientTests
    {
        private IUnityContainer _container;
        private ICartApiClient _cartApiClient;
        string _cartId = Guid.NewGuid().ToString();

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _container = UnityConfig.Container;
            _cartApiClient = _container.Resolve<ICartApiClient>();
        }

        [Test]
        public async Task CreateCartItem()
        {
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "GetStringAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("[{'id':1,'value':'1'}]"),
               })
               .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            Random rand = new Random(3);
            int productId = rand.Next(1, 16);//valid productIds: 1-16

            // ACT

            ICartItemModel result = await _cartApiClient.CreateCartItemAsync(_cartId, productId);



            // ASSERT
            result.Should().NotBeNull(); // this is fluent assertions here...
            result.Id.Should().Be(1);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/test/whatever");

            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1), // we expected a single external request
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Get  // we expected a GET request
                  && req.RequestUri == expectedUri // to this uri
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task AddCartItem()
        {
            //Integration Testing

            //Arrange
            Random rand = new Random(3);
            int productId = rand.Next(1, 16);//valid productIds: 1-16

            //Act
            ICartItemModel cartItem = await _cartApiClient.CreateCartItemAsync(_cartId, productId);

            //Assert
            Assert.IsNotNull(cartItem);
            Assert.IsNotNull(cartItem.ItemId);
            Assert.IsNotEmpty(cartItem.ItemId);
            Assert.IsNotNull(cartItem.ProductModel);
            Assert.AreEqual(productId, cartItem.ProductId);
            Assert.AreEqual(productId, cartItem.ProductModel.ProductId);

            //Act
            var response = await _cartApiClient.AddCartItemAsync(cartItem);

            //Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            //Act
            var cartItemActual = await _cartApiClient.GetCartItemAsync(cartItem.ItemId);

            //Assert
            Assert.IsNotNull(cartItemActual);
            Assert.AreEqual(cartItem.ItemId, cartItemActual.ItemId);

        }
    }
}

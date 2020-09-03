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
using Tecsys.Retail.TypeMapping;
//using Telerik.JustMock;
//using Telerik.JustMock.AutoMock;




namespace Tecsys.Retail.NUnitTests
{
    [TestFixture]
    public class CartApiClientUnitTests
    {
        private IUnityContainer _container;
        string _cartId = Guid.NewGuid().ToString();

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _container = UnityConfig.Container;
        }


        [Test]
        public async Task CreateCartItemAsync()
        {
            // ARRANGE
            string json = "{\"ItemId\":\"11111111111111111111111\",\"CartId\":\"2222222222222222222222\",\"Quantity\":0,\"DateCreated\":\"0001-01-01T00:00:00\",\"ProductId\":1,\"Product\":{\"Description\":\"This convertible car is fast! The engine is powered by a neutrino based battery (not included).Power it up and let it go!\",\"CategoryId\":1,\"ImagePath\":\"carconvert.png\",\"UnitPrice\":22.5,\"ProductName\":\"Convertible Car\",\"ProductId\":1}}";
            Model.CartItemModel expectedCartItem = JsonConvert.DeserializeObject<Model.CartItemModel>(json);

            Moq.Mock<CustomHttpHandler> httpHandlerMock = new Moq.Mock<CustomHttpHandler>(Moq.MockBehavior.Strict);
            httpHandlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  //regardless of the method in the HttpClient class you use (GetAsync, PostAsync, etc.) - the SendAsync method is used by the HttpMessageHandler class.
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(json),
               })
               .Verifiable();

            HttpClient httpClient = new HttpClient(httpHandlerMock.Object);

            Random rand = new Random(3);
            int productId = rand.Next(1, 16);//valid productIds: 1-16
            ITypeMapper typeMapper = _container.Resolve<ITypeMapper>();
            CartApiClient client = new CartApiClient(httpClient, typeMapper);

            //ACT
            ICartItemModel actualCartItem = await client.CreateCartItemAsync(_cartId, productId);

            //ASSERT
            Assert.IsNotNull(actualCartItem);
            Assert.AreEqual(expectedCartItem.ItemId, actualCartItem.ItemId);
            Assert.AreEqual(expectedCartItem.ProductId, actualCartItem.ProductId);
            Assert.AreEqual(expectedCartItem.Quantity, actualCartItem.Quantity);
            Assert.AreEqual(expectedCartItem.DateCreated, actualCartItem.DateCreated);
            Assert.AreEqual(expectedCartItem.DateCreated, actualCartItem.DateCreated);
        }

        [Test]
        public async Task AddCartItemAsync()
        {
            //Arrange

            string json = "{\"ItemId\":\"11111111111111111111111\",\"CartId\":\"2222222222222222222222\",\"Quantity\":0,\"DateCreated\":\"0001-01-01T00:00:00\",\"ProductId\":1,\"Product\":{\"Description\":\"This convertible car is fast! The engine is powered by a neutrino based battery (not included).Power it up and let it go!\",\"CategoryId\":1,\"ImagePath\":\"carconvert.png\",\"UnitPrice\":22.5,\"ProductName\":\"Convertible Car\",\"ProductId\":1}}";
            Model.CartItemModel cartItem = JsonConvert.DeserializeObject<Model.CartItemModel>(json);

            Moq.Mock<CustomHttpHandler> httpHandlerMock = new Moq.Mock<CustomHttpHandler>(Moq.MockBehavior.Strict);
            httpHandlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  //regardless of the method in the HttpClient class you use (GetAsync, PostAsync, etc.) - use the SendAsync method of the HttpMessageHandler class.
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK
               })
               .Verifiable();

            HttpClient httpClient = new HttpClient(httpHandlerMock.Object);
            ITypeMapper typeMapper = _container.Resolve<ITypeMapper>();
            CartApiClient client = new CartApiClient(httpClient, typeMapper);

            //Act
            var response = await client.AddCartItemAsync(cartItem);

            //Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

        }
    }
}

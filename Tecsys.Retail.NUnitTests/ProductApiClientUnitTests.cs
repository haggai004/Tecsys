// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity;
using Tecsys.Retail.IocContainer;
using Tecsys.Retail.RestApiClient;
using Tecsys.Retail.Model;
using Moq.AutoMock;
using Moq.Protected;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Moq;
using System.Net;

namespace Tecsys.Retail.NUnitTests
{
    [TestFixture]
    public class ProductApiClientUnitTests
    {
        private IUnityContainer _container;
        private IProductApiClient _productApiClient;

       [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _container = UnityConfig.Container;
            _productApiClient = _container.Resolve<IProductApiClient>();
        }

        [Test]
        public async Task GetCarsAsync()
        {
            //Arrange
            Model.ProductModel productModel1 = new ProductModel
            {
                ProductId = 1,
                ProductName = "FastCar",
                Description = "FastCar Description",
                UnitPrice = 100.00,
                ImagePath = "C:\\FastCar.img"
            };

            Model.ProductModel productModel2 = new ProductModel
            {
                ProductId = 2,
                ProductName = "SlowCar",
                Description = "SlowCar Description",
                UnitPrice = 1.00,
                ImagePath = "C:\\SlowCar.img"
            };

            List<Model.ProductModel> expectedCarModels = new List<ProductModel> { productModel1, productModel2 };
            string json = JsonConvert.SerializeObject(expectedCarModels);

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
            ProductApiClient client = new ProductApiClient(httpClient);

            //ACT
            IEnumerable<IProductModel> actualCarModels = await client.GetCarsAsync();

            //ASSERT
            Assert.IsNotNull(actualCarModels);
            Assert.AreEqual(expectedCarModels.Count(), actualCarModels.Count());
            Assert.AreEqual(expectedCarModels[0].ProductId, actualCarModels.ToList()[0].ProductId);
            Assert.AreEqual(expectedCarModels[0].ProductName, actualCarModels.ToList()[0].ProductName);
            Assert.AreEqual(expectedCarModels[0].ImagePath, actualCarModels.ToList()[0].ImagePath);
            Assert.AreEqual(expectedCarModels[0].UnitPrice, actualCarModels.ToList()[0].UnitPrice);

            Assert.AreEqual(expectedCarModels[1].ProductId, actualCarModels.ToList()[1].ProductId);
            Assert.AreEqual(expectedCarModels[1].ProductName, actualCarModels.ToList()[1].ProductName);
            Assert.AreEqual(expectedCarModels[1].ImagePath, actualCarModels.ToList()[1].ImagePath);
            Assert.AreEqual(expectedCarModels[1].UnitPrice, actualCarModels.ToList()[1].UnitPrice);

        }

        [Test]
        public async Task GetProductsAsync()
        {
            //ARRANGE
            Model.ProductModel productModel1 = new ProductModel
            {
                ProductId = 1,
                ProductName = "FastCar",
                Description = "FastCar Description",
                UnitPrice = 100.00,
                ImagePath = "C:\\FastCar.img"
            };

            Model.ProductModel productModel2 = new ProductModel
            {
                ProductId = 2,
                ProductName = "SlowCar",
                Description = "SlowCar Description",
                UnitPrice = 1.00,
                ImagePath = "C:\\SlowCar.img"
            };

            List<Model.ProductModel> expectedProductModels = new List<ProductModel> { productModel1, productModel2 };
            string json = JsonConvert.SerializeObject(expectedProductModels);

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
            ProductApiClient client = new ProductApiClient(httpClient);

            string name = "Convertible";

            //ACT
            var actualProductModels = await client.GetProductsAsync(name);
            
            //ASSERT
            Assert.IsNotNull(actualProductModels);
            Assert.AreEqual(expectedProductModels.Count(), actualProductModels.Count());
            Assert.AreEqual(expectedProductModels[0].ProductId, actualProductModels.ToList()[0].ProductId);
            Assert.AreEqual(expectedProductModels[0].ProductName, actualProductModels.ToList()[0].ProductName);
            Assert.AreEqual(expectedProductModels[0].ImagePath, actualProductModels.ToList()[0].ImagePath);
            Assert.AreEqual(expectedProductModels[0].UnitPrice, actualProductModels.ToList()[0].UnitPrice);

            Assert.AreEqual(expectedProductModels[1].ProductId, actualProductModels.ToList()[1].ProductId);
            Assert.AreEqual(expectedProductModels[1].ProductName, actualProductModels.ToList()[1].ProductName);
            Assert.AreEqual(expectedProductModels[1].ImagePath, actualProductModels.ToList()[1].ImagePath);
            Assert.AreEqual(expectedProductModels[1].UnitPrice, actualProductModels.ToList()[1].UnitPrice);

        }
    }
}

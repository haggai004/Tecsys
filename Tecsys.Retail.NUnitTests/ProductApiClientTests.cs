// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity;
using Tecsys.Retail.IocContainer;
using Tecsys.Retail.RestApiClient;
using Tecsys.Retail.Model;

namespace Tecsys.Retail.NUnitTests
{
    [TestFixture]
    public class ProductApiClientTests
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
        public async Task GetCars()
        {
            //Arrange
            IEnumerable<IProductModel> carModels = await _productApiClient.GetCarsAsync();

            Assert.IsNotNull(carModels);
            Assert.AreEqual(carModels.Count(), 5);

        }

        [Test]
        public async Task GetProductsByName()
        {
            //Arrange
            string name = "Convertible";
            //Act
            var productModels = await _productApiClient.GetProductsAsync(name);
            //Assert
            Assert.IsNotEmpty(productModels);

            //Arrange
            name = "Conv";
            //Act
            productModels = await _productApiClient.GetProductsAsync(name);
            //Assert
            Assert.IsNotEmpty(productModels);

            //Arrange
            name = "DoesNotExist";
            //Act
            productModels = await _productApiClient.GetProductsAsync(name);
            //Assert
            Assert.IsEmpty(productModels);

            //Arrange
            name = "neutrino";
            //Act
            productModels = await _productApiClient.GetProductsAsync(name);
            //Assert
            Assert.IsNotEmpty(productModels);
        }
    }
}

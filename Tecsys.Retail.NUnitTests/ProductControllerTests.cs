// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity;
using Tecsys.Retail.IocContainer;
using Tecsys.Retail.Model;
using Tecsys.Retail.RestApi.Controllers;

namespace Tecsys.Retail.NUnitTests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private IUnityContainer _container;
        private ProductController _productController;

       [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _container = UnityConfig.Container;
            _productController = _container.Resolve<ProductController>();

        }
        
        [Test]
        public async Task GetCars()
        {
            //Arrange
            var jArray = await _productController.GetCarsAsync();
            var carModels = jArray.ToObject<IEnumerable<ProductModel>>();

            Assert.IsNotNull(carModels);
            Assert.IsNotNull(carModels.All(c=>c!=null));
            Assert.AreEqual(5, carModels.Count());
        }

        [Test]
        public async Task GetProductsByName()
        {
            //Arrange
            string name = "Convertible";
            //Act
            var products = await _productController.GetProductsAsync(name);
            //Assert
            Assert.IsNotEmpty(products);

            //Arrange
            name = "Conv";
            //Act
            products = await _productController.GetProductsAsync(name);
            //Assert
            Assert.IsNotEmpty(products);

            //Arrange
            name = "DoesNotExist";
            //Act
            products = await _productController.GetProductsAsync(name);
            //Assert
            Assert.IsEmpty(products);
        }
    }
}

// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Tecsys.Retail.Biz;
using Unity;
using Tecsys.Retail.IocContainer;
using Tecsys.Retail.Domain;

namespace Tecsys.Retail.NUnitTests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private IUnityContainer _container;
        private IProductService _productService;

       [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _container = UnityConfig.Container;
            _productService = _container.Resolve<IProductService>();

        }

        [SetUp]
        public void Setup()
        {

        }

        [TearDown]
        public void Teardown()
        {
            
        }

        [Test]
        public async Task GetCars()
        {
            //Arrange
            IEnumerable<IProduct> cars = await _productService.GetCarsAsync();

            Assert.IsNotEmpty(cars);
            Assert.AreEqual(cars.Count(),5);

        }

        [Test]
        public async Task GetProductsByName()
        {
            //Arrange
            string name = "Convertible";
            //Act
            IEnumerable<IProduct> products = await _productService.GetProductsAsync(name);
            //Assert
            Assert.IsNotEmpty(products);

            //Arrange
            name = "Conv";
            //Act
            products = await _productService.GetProductsAsync(name);
            //Assert
            Assert.IsNotEmpty(products);

            //Arrange
            name = "DoesNotExist";
            //Act
            products = await _productService.GetProductsAsync(name);
            //Assert
            Assert.IsEmpty(products);
        }
    }
}

// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity;
using Tecsys.Retail.IocContainer;
using Tecsys.Retail.Repository;

namespace Tecsys.Retail.NUnitTests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private IUnityContainer _container;

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _container = UnityConfig.Container;
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
            IProductRepository repo = _container.Resolve<IProductRepository>();
            List<Ef.Product> cars = await repo.GetCars();

            Assert.IsTrue(cars.All(c => c.Category.CategoryName.Equals("Cars")));
            Assert.IsNotEmpty(cars);
            Assert.AreEqual(cars.Count,5);

        }

        [Test]
        public async Task GetProductsByName()
        {
            //Arrange
            string name = "Convertible";
            IProductRepository repo = _container.Resolve<IProductRepository>();

            List<Ef.Product> products = await repo.GetProducts(name);

            Assert.IsTrue(products.All(c => c.ProductName.ToLower().Contains(name.ToLower())|| c.Description.ToLower().Contains(name.ToLower())));
            Assert.IsNotEmpty(products);

            //Arrange
            name = "Conv";
            products = await repo.GetProducts(name);

            Assert.IsTrue(products.All(c => c.ProductName.ToLower().Contains(name.ToLower()) || c.Description.ToLower().Contains(name.ToLower())));
            Assert.IsNotEmpty(products);

            //Arrange
            name = "DoesNotExist";
            products = await repo.GetProducts(name);
            Assert.IsEmpty(products);

        }
    }
}

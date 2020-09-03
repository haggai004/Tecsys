// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity;
using Tecsys.Retail.IocContainer;
using Tecsys.Retail.Repository;
using System.Data.Entity;

using Tecsys.Retail.Domain;
using Tecsys.Retail.Biz;
using Tecsys.Retail.RestApiClient;
using Tecsys.Retail.Model;
//using Moq;
//using Moq.AutoMock;
//using Moq.Protected;
//using System.Net.Http;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Tecsys.Retail.RestApi.Controllers;
using Tecsys.Retail.TypeMapping;

using Telerik.JustMock;
using Telerik.JustMock.AutoMock;
using Telerik.JustMock.XUnit;

namespace Tecsys.Retail.NUnitTests
{
    [TestFixture]
    public class CartRepositoryUnitTests
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
        public async Task AddOrUpdateCartItemAsync()
        {
            //ARRANGE
            string itemId = Guid.NewGuid().ToString();
            int productId = 3;
            int quantity = 10;
            Ef.CartItem cartItem = new Ef.CartItem
            {
                ItemId = itemId,
                CartId = _cartId,
                Quantity = quantity,
                DateCreated = DateTime.Now,
                ProductId = productId
            };

            var dbContextMock = Mock.Create<Ef.WingtiptoysEntities>();
            var cartRepository = _container.Resolve<CartRepository>();
            Mock.Arrange(() => cartRepository.DbContext).Returns(dbContextMock);

            Mock.Arrange(() => cartRepository.DbContext.CartItems.FirstOrDefault(Arg.IsAny<Expression<Func<Ef.CartItem, bool>>>())).Returns(cartItem);
            Mock.Arrange(() => cartRepository.DbContext.Database.ExecuteSqlCommand(Arg.IsAny<string>(), Args.Ignore())).Returns(1);

            int result = await cartRepository.AddOrUpdateCartItemAsync(cartItem);

        }
    }
}

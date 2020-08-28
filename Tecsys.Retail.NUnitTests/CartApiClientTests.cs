﻿// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using Unity;
using Tecsys.Retail.IocContainer;
using Tecsys.Retail.RestApiClient;
using Tecsys.Retail.Model;

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
            //Arrange
            Random rand = new Random(3);
            int productId = rand.Next(1, 16);//valid productIds: 1-16

            //Act
            ICartItemModel cartItem = await _cartApiClient.CreateCartItem(_cartId,productId);

            //Assert
            Assert.IsNotNull(cartItem);
            Assert.IsNotNull(cartItem.ItemId);
            Assert.IsNotEmpty(cartItem.ItemId);
            Assert.IsNotNull(cartItem.ProductModel);
            Assert.AreEqual(productId, cartItem.ProductId);
            Assert.AreEqual(productId, cartItem.ProductModel.ProductId);
        }

        [Test]
        public async Task AddCartItem()
        {
            //Integration Testing

            //Arrange
            Random rand = new Random(3);
            int productId = rand.Next(1, 16);//valid productIds: 1-16

            //Act
            ICartItemModel cartItem = await _cartApiClient.CreateCartItem(_cartId, productId);

            //Assert
            Assert.IsNotNull(cartItem);
            Assert.IsNotNull(cartItem.ItemId);
            Assert.IsNotEmpty(cartItem.ItemId);
            Assert.IsNotNull(cartItem.ProductModel);
            Assert.AreEqual(productId, cartItem.ProductId);
            Assert.AreEqual(productId, cartItem.ProductModel.ProductId);

            //Act
            var response = await _cartApiClient.AddCartItem(cartItem);

            //Assert
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            //Act
            var cartItemActual = await _cartApiClient.GetCartItem(cartItem.ItemId);

            //Assert
            Assert.IsNotNull(cartItemActual);
            Assert.AreEqual(cartItem.ItemId, cartItemActual.ItemId);

        }
    }
}

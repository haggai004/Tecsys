using AutoMapper;
using Tecsys.Retail.Biz;
using Tecsys.Retail.Domain;
using Tecsys.Retail.RestApiClient;
using Tecsys.Retail.Model;
using Unity;
using Unity.Lifetime;
using Tecsys.Retail.Repository;
using Tecsys.Retail.TypeMapping;

namespace Tecsys.Retail.IocContainer
{
    /// <summary>
    /// Specifies the Unity configuration for the main _container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container

        static UnityConfig()
        {
            Container = new UnityContainer();
            RegisterTypes(Container);
        }


        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container { get; private set; }

        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity _container.
        /// </summary>
        /// <param name="container">The unity _container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ITypeMapper, TypeMapper>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProduct, Product>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICartItem, CartItem>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICartItemModel, CartItemModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICartRepository, CartRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProductRepository, ProductRepository>(new ContainerControlledLifetimeManager());

            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            });

            container.RegisterInstance(mapperConfig,  new ContainerControlledLifetimeManager());

            IMapper mapper = mapperConfig.CreateMapper();
            container.RegisterInstance(mapper,new ContainerControlledLifetimeManager());

            container.RegisterType<ITypeMapper, TypeMapper>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProductService, ProductService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICartService, CartService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProductApiClient,ProductApiClient>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICartApiClient,CartApiClient>(new ContainerControlledLifetimeManager());



        }
    }
}
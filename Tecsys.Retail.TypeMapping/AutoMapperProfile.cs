
using AutoMapper;

namespace Tecsys.Retail.TypeMapping
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {

            CreateMap<Ef.Product, Domain.IProduct>()
               .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryID))
               .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductID)).ReverseMap();

            CreateMap<Ef.Product, Domain.Product>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryID))
               .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductID)).ReverseMap();

            CreateMap<Domain.IProduct, Model.IProductModel>()
                 .ConstructUsing(m => new Model.ProductModel()
                 {
                     ProductId = m.ProductId,
                     ProductName = m.ProductName,
                     UnitPrice = m.UnitPrice,
                     ImagePath = m.ImagePath,
                     Description = m.Description
                 });

            CreateMap<Model.IProductModel, Domain.IProduct>()
                 .ConstructUsing(m => new Domain.Product()
                 {
                     ProductId = m.ProductId,
                     ProductName = m.ProductName,
                     UnitPrice = m.UnitPrice,
                     ImagePath = m.ImagePath,
                     Description = m.Description
                 });

            CreateMap<Domain.ICartItem, Ef.CartItem>().ReverseMap();
            CreateMap<Ef.CartItem, Domain.CartItem>().ReverseMap();

            CreateMap<Model.CartItemModel, Domain.CartItem>()
                .ConstructUsing(m => new Domain.CartItem()
                {
                    CartId = m.CartId,
                    ItemId = m.ItemId,
                    DateCreated = m.DateCreated,
                    ProductId = m.ProductId,
                    Quantity = m.Quantity,
                    Product = new Domain.Product()
                    {
                        ProductId = m.ProductModel.ProductId,
                        ProductName = m.ProductModel.ProductName,
                        UnitPrice = m.ProductModel.UnitPrice,
                        ImagePath = m.ProductModel.ImagePath,
                        Description = m.ProductModel.Description
                    }
                });

            CreateMap<Domain.CartItem, Model.CartItemModel>()
                .ConstructUsing(m => new Model.CartItemModel()
                {
                    CartId = m.CartId,
                    ItemId = m.ItemId,
                    DateCreated = m.DateCreated,
                    ProductId = m.ProductId,
                    Quantity = m.Quantity,
                    ProductModel = new Model.ProductModel()
                    {
                        ProductId = m.Product.ProductId,
                        ProductName = m.Product.ProductName,
                        UnitPrice = m.Product.UnitPrice,
                        ImagePath = m.Product.ImagePath,
                        Description = m.Product.Description
                    }
                });
        }
    }
}

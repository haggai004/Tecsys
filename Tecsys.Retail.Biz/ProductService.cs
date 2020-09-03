using System.Collections.Generic;
using System.Threading.Tasks;
using Tecsys.Retail.Domain;
using Tecsys.Retail.Repository;
using Tecsys.Retail.TypeMapping;

namespace Tecsys.Retail.Biz
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        ITypeMapper _typeMapper;

        public ProductService(IProductRepository productRepository, ITypeMapper typeMapper)
        {
            _productRepository = productRepository;
            _typeMapper = typeMapper;
        }

        public async Task<IEnumerable<IProduct>> GetCarsAsync()
        {
            List<Ef.Product> productEntities = await _productRepository.GetCars();
            List<Domain.IProduct> cars = _typeMapper.Map<Ef.Product, Domain.IProduct>(productEntities);
            return cars;
        }

        public async Task<IEnumerable<IProduct>> GetProductsAsync(string name)
        {
            List<Ef.Product> productEntities = await _productRepository.GetProducts(name);
            List<Domain.IProduct> products = _typeMapper.Map<Ef.Product, Domain.IProduct>(productEntities);
            return products;
        }

        public async Task<IProduct> GetProductAsync(int productId)
        {
            Ef.Product productEntity = await _productRepository.GetProductAsync(productId);
            Domain.IProduct product = _typeMapper.Map<Ef.Product, Domain.IProduct>(productEntity);
            return product;
        }
    }
}


using DapperDemo.Entities;

namespace DapperDemo.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetProducts();

        public Product GetProductById(int productId);

        public void CreateProduct(ProductDTO product);

        public void UpdateProduct(int productId, ProductDTO company);

        public void DeleteProduct(int productId);

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId);
    }
}



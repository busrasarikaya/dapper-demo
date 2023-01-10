using Dapper;
using DapperDemo.Entities;
using DapperDemo.Helpers;
using DapperDemo.Interfaces;
using System.Data;

namespace DapperDemo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ConnectionHelper _connectionHelper;
        public ProductRepository(ConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public IEnumerable<Product> GetProducts()
        {
            var query = "SELECT * FROM Products";
            using var connection = _connectionHelper.CreateSqlConnection();
            var products = connection.Query<Product>(query);
            return products.ToList();
        }

        public Product GetProductById(int productId)
        {
            var query = "SELECT * FROM Products WHERE ProductId = @ProductId";
            using var connection = _connectionHelper.CreateSqlConnection();
            var product = connection.QueryFirstOrDefault<Product>(query, new { ProductId = productId });
            return product;
        }


        public void CreateProduct(ProductDTO product)
        {
            var query = "INSERT INTO Products (ProductName, SupplierID , CategoryID, QuantityPerUnit, " +
                        "UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel)" +
                        "VALUES(@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, " +
                        "@UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel)";

            var parameters = new DynamicParameters();
            parameters.Add("ProductName", product.ProductName, DbType.String);
            parameters.Add("SupplierID", product.SupplierID, DbType.Int32);
            parameters.Add("CategoryID", product.CategoryID, DbType.Int32);
            parameters.Add("QuantityPerUnit", product.QuantityPerUnit, DbType.String);
            parameters.Add("UnitPrice", product.UnitPrice, DbType.Decimal);
            parameters.Add("UnitsInStock", product.UnitsInStock, DbType.Int16);
            parameters.Add("UnitsOnOrder", product.UnitsOnOrder, DbType.Int16);
            parameters.Add("ReorderLevel", product.ReorderLevel, DbType.Int16);

            using var connection = _connectionHelper.CreateSqlConnection();
            connection.Execute(query, parameters);
        }


        public void UpdateProduct(int productId, ProductDTO product)
        {
            var query = "UPDATE Products SET ProductName = @ProductName, SupplierID = @SupplierID, " +
                        "CategoryID = @CategoryID, QuantityPerUnit = @QuantityPerUnit, UnitPrice = @UnitPrice, " +
                        "UnitsInStock = @UnitsInStock, UnitsOnOrder = @UnitsOnOrder, ReorderLevel = @ReorderLevel " +
                        "WHERE ProductID = @ProductID";

            var parameters = new DynamicParameters();
            parameters.Add("ProductID", productId, DbType.Int32);
            parameters.Add("ProductName", product.ProductName, DbType.String);
            parameters.Add("SupplierID", product.SupplierID, DbType.Int32);
            parameters.Add("CategoryID", product.CategoryID, DbType.Int32);
            parameters.Add("QuantityPerUnit", product.QuantityPerUnit, DbType.String);
            parameters.Add("UnitPrice", product.UnitPrice, DbType.Decimal);
            parameters.Add("UnitsInStock", product.UnitsInStock, DbType.Int16);
            parameters.Add("UnitsOnOrder", product.UnitsOnOrder, DbType.Int16);
            parameters.Add("ReorderLevel", product.ReorderLevel, DbType.Int16);

            using var connection = _connectionHelper.CreateSqlConnection();
            connection.Execute(query, parameters);
        }

        public void DeleteProduct(int productId)
        {
            var query = "DELETE FROM Products WHERE ProductID = @ProductID";
            using var connection = _connectionHelper.CreateSqlConnection();
            connection.Execute(query, new { productId });
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            using var connection = _connectionHelper.CreateSqlConnection();
            var products = connection.Query<Product>("GetProductsByCategoryId",
                                                     new { CategoryId = categoryId },
                                                     commandType: CommandType.StoredProcedure);
            return products;
        }
    }
}








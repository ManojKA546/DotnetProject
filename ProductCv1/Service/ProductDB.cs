using System.Data; // Required for IDbConnection
using Microsoft.Data.SqlClient; // Required for SqlConnection (to use with Dapper)
using Dapper; 

public class ProductDB : Iproduct
{
    private readonly IDbConnection _dbConnection;

    public ProductDB(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _dbConnection.QueryAsync<Product>("SELECT * FROM Product");
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Product WHERE ProductId = @ProductId", new { ProductId = id });
    }

   public async Task AddProductAsync(Product product)
    {
        // Query to insert a new product into the Product table
        var query = "INSERT INTO Product (Name, SalesPrice, MRP, CategoryId) " +
                    "VALUES (@Name, @SalesPrice, @MRP, @CategoryId)";
        
        await _dbConnection.ExecuteAsync(query, product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        // Query to update an existing product in the Product table
        var query = "UPDATE Product SET Name = @Name, SalesPrice = @SalesPrice, " +
                    "MRP = @MRP, CategoryId = @CategoryId WHERE ProductId = @ProductId";
        
        await _dbConnection.ExecuteAsync(query, product);
    }

    public async Task DeleteProductAsync(int id)
    {
        // Query to delete a product by ProductId
        var query = "DELETE FROM Product WHERE ProductId = @ProductId";
        
        await _dbConnection.ExecuteAsync(query, new { ProductId = id });
    }
}

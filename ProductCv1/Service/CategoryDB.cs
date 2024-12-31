using System.Data; // Required for IDbConnection
using Microsoft.Data.SqlClient; // Required for SqlConnection (to use with Dapper)
using Dapper; 

public class CategoryDB : Icategory
{
    private readonly IDbConnection _dbConnection;

    public CategoryDB(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Category>> GetAllCategoryAsync()
    {
        return await _dbConnection.QueryAsync<Category>("SELECT * FROM Category");
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Category>("SELECT * FROM Category WHERE CategoryId = @CategoryId", new { CategoryId = id });
    }

   public async Task AddCategoryAsync(Category cat)
    {
        // Query to insert a new product into the Product table
         var query = "INSERT INTO Category (Name) VALUES (@Name)";

        await _dbConnection.ExecuteAsync(query, cat);
    }

    public async Task UpdateCategoryAsync(Category cat)
    {
        // Query to update an existing product in the Product table
        var query = "UPDATE Category SET Name = @Name  WHERE CategoryId = @CategoryId  ";
        
        await _dbConnection.ExecuteAsync(query, cat);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        // Query to delete a product by ProductId
        var query = "DELETE FROM Category WHERE CategoryId = @CategoryId";
        
        await _dbConnection.ExecuteAsync(query, new { CategoryId = id });
    }
}

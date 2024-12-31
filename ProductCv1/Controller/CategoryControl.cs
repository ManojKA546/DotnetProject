using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data; // Required for IDbConnection

[ApiController]
[Route("[controller]")]
public class CategoryControl : ControllerBase
{
    private readonly CategoryDB _categorydb;

    // Constructor should match the class name
    public CategoryControl(CategoryDB categorydb)
    {
        _categorydb = categorydb; // Correctly assign the parameter to the field
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>>  GetAllCategoryAsync()
    {
        var category = await _categorydb. GetAllCategoryAsync();
        return Ok(category);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryByIdAsync(int id)
    {
        var product = await _categorydb.GetCategoryByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }  

    // API to add a new product
    [HttpPost]
    public async Task<ActionResult> AddCategoryAsync([FromBody] Category category)
    {
        if (category == null)
        {
            return BadRequest("Category data is required");
        }

        await _categorydb.AddCategoryAsync(category);
        return CreatedAtAction(nameof(GetCategoryByIdAsync), new { id = category.CategoryId }, category);
    }

    // API to update an existing product
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id, [FromBody] Category product)
    {
        if (product == null || product.CategoryId != id)
        {
            return BadRequest("Product data is invalid");
        }

        var existingProduct = await _categorydb.GetCategoryByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        await _categorydb.UpdateCategoryAsync(product);
        return NoContent(); // Indicates success with no content to return
    }

    // API to delete a product by id
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategoryAsync(int id)
    
    {
        var existingCategory = await _categorydb.GetCategoryByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound();
        }

        await _categorydb.DeleteCategoryAsync(id);
        return NoContent(); // Success without returning any data
    }
}

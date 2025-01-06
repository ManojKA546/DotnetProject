using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryDB _categorydb;

    public CategoryController(CategoryDB categorydb)
    {
        _categorydb = categorydb;
    }

    // Get all categories
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategoriesAsync()
    {
        var categories = await _categorydb.GetAllCategoryAsync();
        return Ok(categories);
    }

    // Get category by ID
    [HttpGet("details/{id}")]
    public async Task<ActionResult<Category>> GetCategoryByIdAsync(int id)
    {
        var category = await _categorydb.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound($"Category with ID {id} not found.");
        }
        return Ok(category);
    }

    // Add a new category
    [HttpPost("add")]
    public async Task<ActionResult> AddCategoryAsync([FromBody] Category category)
    {
        if (category == null)
        {
            return BadRequest("Category data is required.");
        }

        await _categorydb.AddCategoryAsync(category);
        return CreatedAtAction(nameof(GetCategoryByIdAsync), new { id = category.CategoryId }, category);
    }

    // Update an existing category
    [HttpPut("update/{id}")]
    public async Task<ActionResult> UpdateCategoryAsync(int id, [FromBody] Category category)
    {
        if (category == null || category.CategoryId != id)
        {
            return BadRequest("Category data is invalid.");
        }

        var existingCategory = await _categorydb.GetCategoryByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound($"Category with ID {id} not found.");
        }

        await _categorydb.UpdateCategoryAsync(category);
        return NoContent();
    }

    // Delete a category by ID
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteCategoryAsync(int id)
    {
        var existingCategory = await _categorydb.GetCategoryByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound($"Category with ID {id} not found.");
        }

        await _categorydb.DeleteCategoryAsync(id);
        return NoContent();
    }
}

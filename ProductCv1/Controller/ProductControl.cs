using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductDB _productdb;

    // Constructor should match the class name
    public ProductController(ProductDB productdb)
    {
        _productdb = productdb; // Correctly assign the parameter to the field
    }

    // Get all products
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _productdb.GetAllProductsAsync();
        return Ok(products);
    }

    // Get product by ID
    [HttpGet("details/{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _productdb.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound($"Product with ID {id} not found.");
        }
        return Ok(product);
    }

    // Add a new product
    [HttpPost("add")]
    public async Task<ActionResult> AddProduct([FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest("Product data is required.");
        }

        await _productdb.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
    }

    // Update an existing product
    [HttpPut("update/{id}")]
    public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
    {
        if (product == null || product.ProductId != id)
        {
            return BadRequest("Product data is invalid.");
        }

        var existingProduct = await _productdb.GetProductByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound($"Product with ID {id} not found.");
        }

        await _productdb.UpdateProductAsync(product);
        return NoContent(); // Indicates success with no content to return
    }

    // Delete a product by ID
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var existingProduct = await _productdb.GetProductByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound($"Product with ID {id} not found.");
        }

        await _productdb.DeleteProductAsync(id);
        return NoContent();
    }
}

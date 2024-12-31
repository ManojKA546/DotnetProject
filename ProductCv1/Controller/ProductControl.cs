using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data; // Required for IDbConnection

[ApiController]
[Route("[controller]")]
public class ProductControl : ControllerBase
{
    private readonly ProductDB _productdb;

    // Constructor should match the class name
    public ProductControl(ProductDB productdb)
    {
        _productdb = productdb; // Correctly assign the parameter to the field
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        var products = await _productdb.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
        var product = await _productdb.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }  

    // API to add a new product
    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest("Product data is required");
        }

        await _productdb.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
    }

    // API to update an existing product
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
    {
        if (product == null || product.ProductId != id)
        {
            return BadRequest("Product data is invalid");
        }

        var existingProduct = await _productdb.GetProductByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        await _productdb.UpdateProductAsync(product);
        return NoContent(); // Indicates success with no content to return
    }

    // API to delete a product by id
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var existingProduct = await _productdb.GetProductByIdAsync(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        await _productdb.DeleteProductAsync(id);
        return NoContent(); // Success without returning any data
    }
}

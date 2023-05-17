using L00177784_Project.Data;
using L00177784_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L00177784_Project.Controllers
{
    /// <summary>
    /// Controller for Products
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly LoyaltyGroupsContext _context;

        /// <summary>
        /// Constructor to connect to the database
        /// </summary>
        /// <param name="context">Db context for connection</param>
        public ProductsController(LoyaltyGroupsContext context)
        {
            _context = context;
        }

        // GET: api/Products
        /// <summary>
        /// Endpoint to get a list of products
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        /// <summary>
        /// Endpoint to get a product by Id
        /// </summary>
        /// <param name="id">Id of product to be returned</param>
        /// <returns>Product with corrosponding Id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        /// <summary>
        /// Endpoint to update a product based on Id
        /// </summary>
        /// <param name="id">Id of product to be updated</param>
        /// <param name="product">Object of type product to update with</param>
        /// <returns>Task Done</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        /// <summary>
        /// Endpoint to create a product
        /// </summary>
        /// <param name="product">Object of type product</param>
        /// <returns>Product with Id</returns>
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'LoyaltyGroupsContext.Products'  is null.");
            }
            else
            {
                if (product.Sku != null)
                {
                    var skuExists = _context.Products.FirstOrDefault(x => x.Sku == product.Sku);
                    if (skuExists != null)
                    {
                        return Problem("Product Sku already exists.");
                    }
                }
                if (product.Barcode != null)
                {
                    var barcodeExists = _context.Products.FirstOrDefault(x => x.Barcode == product.Barcode);
                    if (barcodeExists != null)
                    {
                        return Problem("Product Barcode already exists.");
                    }
                }
                if (string.IsNullOrEmpty(product.Barcode) && product.Sku == null)
                    {
                        return Problem("Product Barcode oro SKU must be entered.");
                    }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            
        }

        // DELETE: api/Products/5
        /// <summary>
        /// Endpoint to delte a product based on Id
        /// </summary>
        /// <param name="id">Id to be deleted</param>
        /// <returns>Task Done</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
        /// <summary>
        /// Private method to return a Loyalty Group based on a loyalty group id
        /// </summary>
        /// <param name="product">Product that group is for</param>
        /// <returns>Loyalty group based on product group id</returns>
        private LoyaltyGroup GetGroup(Product product) 
        { 
            return _context.LoyaltyGroups.First(x => x.Id == product.LoyaltyGroup_Id);
        }

        /// <summary>
        /// Private method to update a product based on a group id
        /// </summary>
        /// <param name="product">Product that needs to be updated</param>
        /// <param name="groupId">Id to be updated to</param>
        /// <returns>Task Done</returns>
        private async Task UpdateLoyaltyGroup(Product product, int groupId)
        {
            product.LoyaltyGroup_Id = groupId;
            await _context.SaveChangesAsync();
        }
    }
}
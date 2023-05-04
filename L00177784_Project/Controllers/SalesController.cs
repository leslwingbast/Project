using L00177784_Project.Data;
using L00177784_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L00177784_Project.Controllers
{
    /// <summary>
    /// Controller to receive sales information
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly LoyaltyGroupsContext _context;

        /// <summary>
        /// Constructor with connection to database
        /// </summary>
        /// <param name="context">DB contect to update</param>
        public SalesController(LoyaltyGroupsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Endpoint to return all sales
        /// </summary>
        /// <returns>Lost of sales</returns>
        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
          if (_context.Sales == null)
          {
              return NotFound();
          }
            return await _context.Sales.ToListAsync();
        }

        /// <summary>
        /// Endpoint to get sale by Id
        /// </summary>
        /// <param name="id">Id of sale to be returned</param>
        /// <returns>Sale of corrosponding Id</returns>
        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
          if (_context.Sales == null)
          {
              return NotFound();
          }
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // PUT: api/Sales/5
        /// <summary>
        /// Update a sale based on an Id
        /// </summary>
        /// <param name="id">Id of sale to be updated</param>
        /// <param name="sale">Object of sale type that will update sale with above id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, Sale sale)
        {
            if (id != sale.Id)
            {
                return BadRequest();
            }

            _context.Entry(sale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
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

        // POST: api/Sales
        /// <summary>
        /// Endpoint to create a deal with a sale
        /// </summary>
        /// <param name="sale">Object of type sale</param>
        /// <returns>Sale updated and loyalty scheme updated</returns>
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(Sale sale)
        {
          if (_context.Sales == null)
          {
              return Problem("Entity set 'LoyaltyGroupsContext.Sales'  is null.");
          }
            // Add sale to database
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            // Call sale service to process sale
            var scheme = ProcessSale(sale);
            if (scheme == null)
            {
                return NotFound("Sale not processed");
            }
            else 
            {
                return CreatedAtAction("GetSale", new { id = sale.Id }, sale);
            }
            
        }

        /// <summary>
        /// Endpoint to delete a sale based on Id
        /// </summary>
        /// <param name="id">Id of sale to be deleted</param>
        /// <returns>Task Done</returns>
        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            if (_context.Sales == null)
            {
                return NotFound();
            }
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Private method to see if sale exists based on Id
        /// </summary>
        /// <param name="id">Id of sale to check</param>
        /// <returns>Boolean of whether sale exists or not</returns>
        private bool SaleExists(int id)
        {
            return (_context.Sales?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Method to process a sale and update the loyalty scheme accordingle
        /// </summary>
        /// <param name="saleToProcess">Object of Sale type to process</param>
        /// <returns></returns>
        private LoyaltyScheme ProcessSale(Sale saleToProcess)
        {
            // Get the product from barcode or sku
            var saleProduct = _context.Products.First(x => x.Sku == saleToProcess.Sku || x.Barcode == saleToProcess.Barcode);

            // Get the loyalty group that product is belonging to
            var loyaltyGroup = saleProduct.LoyaltyGroup;

            // Get the scheme containing the customer and the loyalty group
            var selectedScheme = _context.LoyaltySchemes.FirstOrDefault(x => x.CustomerId == saleToProcess.CustomerId && x.LoyaltyGroup_Id == loyaltyGroup.Id);

            // Check if a scheme exists for the customer and loyalty group
            if (selectedScheme != null)
            {
                // Check if the bag in the sale was free
                if (saleToProcess.IsFree != true)
                {
                    // Update the sale as processed and save
                    saleToProcess.Processed = true;
                    _context.Sales.Update(saleToProcess);

                    // Call method to update the loyalty scheme with amount of bags in sale
                    return UdateScheme(selectedScheme, saleToProcess.Qty, (int)selectedScheme.RemainingItems);
                }
                else
                {
                    // Update sale as processed and save
                    saleToProcess.Processed = true;
                    _context.Sales.Update(saleToProcess);

                    // Reset the scheme to 0 and update the time the last free item was given
                    return ResetScheme(selectedScheme, saleToProcess.DateTime);
                }
            }
            else
            {
                // Check if product exists
                if (saleProduct == null)
                {
                    Console.WriteLine("Product does not exist!");
                }

                // Check if loyalty group exists
                if (loyaltyGroup != null)
                {
                    // Create a new scheme and assign values
                    var newScheme = new LoyaltyScheme()
                    {
                        LoyaltyGroup = loyaltyGroup,
                        CustomerId = saleToProcess.CustomerId,
                        LastFreeBag = null,
                        RemainingItems = loyaltyGroup.Threshold - saleToProcess.Qty,
                        LoyaltyGroup_Id = loyaltyGroup.Id,
                        GroupName = loyaltyGroup.Name,
                    };
                    // Mark sale as processed and add to database
                    saleToProcess.Processed = true;
                    _context.LoyaltySchemes.Add(newScheme);
                    _context.SaveChanges();

                    // Return new scheme
                    return newScheme;
                }
                else
                {
                    // Warning that product is not in a loyalty group
                    Console.WriteLine("Product is not added to a loyalty group!");
                    return null;
                }
            }
        }

        /// <summary>
        /// Method to update a loyalty scheme counting towards free item
        /// </summary>
        /// <param name="currentScheme">Scheme to be updated</param>
        /// <param name="quantity">Quantity in sale</param>
        /// <param name="count">Current count of bags left towards free item</param>
        /// <returns></returns>
        private LoyaltyScheme UdateScheme(LoyaltyScheme currentScheme, int quantity, int count)
        {
            if ((count - quantity) > 0)
            {
                // Update the remaining cuantity and save changes
                currentScheme.RemainingItems = count - quantity;
                _context.LoyaltySchemes.Update(currentScheme);
                _context.SaveChanges();
            }
            // Return updated scheme
            return currentScheme;
        }

        /// <summary>
        /// Method to reset free bag counts once a free bag is recorded
        /// </summary>
        /// <param name="currentScheme">Scheme to be reset</param>
        /// <param name="freeBag">Date of sale of last free bag</param>
        /// <returns></returns>
        private LoyaltyScheme ResetScheme(LoyaltyScheme currentScheme, DateTime freeBag)
        {
            // Reset remaining bags to Threshold
            currentScheme.RemainingItems = currentScheme.LoyaltyGroup.Threshold;
            // Set the date of last free bag
            currentScheme.LastFreeBag = freeBag;
            // Save changes
            _context.SaveChanges();
            // Return updated scheme
            return currentScheme;
        }

    }
}

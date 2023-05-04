using L00177784_Project.Data;
using L00177784_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L00177784_Project.Controllers
{
    /// <summary>
    /// Controller for Loyalty Schemes to access from a front end
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltySchemesController : ControllerBase
    {
        private readonly LoyaltyGroupsContext _context;
        /// <summary>
        /// Constructor to access the database
        /// </summary>
        /// <param name="context">DBConext for database</param>
        public LoyaltySchemesController(LoyaltyGroupsContext context)
        {
            _context = context;
        }

        // GET: api/LoyaltySchemes
        /// <summary>
        /// Endpoint to get list of Loyalty Schemes
        /// </summary>
        /// <returns>List of loyalty schemes in database</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoyaltyScheme>>> GetLoyaltySchemes()
        {
            if (_context.LoyaltySchemes == null)
            {
                return NotFound();
            }
            return await _context.LoyaltySchemes.ToListAsync();
        }

        // GET: api/LoyaltySchemes/5
        /// <summary>
        /// Endpoint to get a loyaltyscheme by Id
        /// </summary>
        /// <param name="id">Id of loyalty scheme to be returned</param>
        /// <returns>Object of type LoyaltyScheme</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LoyaltyScheme>> GetLoyaltyScheme(int id)
        {
            if (_context.LoyaltySchemes == null)
            {
                return NotFound();
            }
            var loyaltyScheme = await _context.LoyaltySchemes.FindAsync(id);

            if (loyaltyScheme == null)
            {
                return NotFound();
            }

            return loyaltyScheme;
        }


        // GET: api/LoyaltySchemes/Customer/5
        /// <summary>
        /// Endpoint to return all schemes connected to customer Id
        /// </summary>
        /// <param name="customer_id">Id of customer to return schemes for</param>
        /// <returns></returns>
        [HttpGet("Customer/{customer_id}")]
        public async Task<ActionResult<IEnumerable<LoyaltyScheme>>> GetCustomerLoyaltyScheme(int customer_id)
        {
            if (_context.LoyaltySchemes == null)
            {
                return NotFound();
            }
            var schemes = await _context.LoyaltySchemes.Where(x => x.CustomerId == customer_id).ToList();
            return schemes;

        }

        // PUT: api/LoyaltySchemes/5
        /// <summary>
        /// Endpoint to update a loyalty scheme
        /// </summary>
        /// <param name="id">Id of loyalty scheme to update</param>
        /// <param name="loyaltyScheme">Object of type LoyaltyScheme</param>
        /// <returns>Taks Complete</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoyaltyScheme(int id, LoyaltyScheme loyaltyScheme)
        {
            if (id != loyaltyScheme.Id)
            {
                return BadRequest();
            }

            _context.Entry(loyaltyScheme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoyaltySchemeExists(id))
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

        // POST: api/LoyaltySchemes
        /// <summary>
        /// Endpoint to create Loyalty Scheme in the database
        /// </summary>
        /// <param name="loyaltyScheme">Object of type LoyaltyScheme</param>
        /// <returns>Saved Loyalty Scheme</returns>
        [HttpPost]
        public async Task<ActionResult<LoyaltyScheme>> PostLoyaltyScheme(LoyaltyScheme loyaltyScheme)
        {
            if (_context.LoyaltySchemes == null)
            {
                return Problem("Entity set 'LoyaltyGroupsContext.LoyaltySchemes'  is null.");
            }
            _context.LoyaltySchemes.Add(loyaltyScheme);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoyaltyScheme", new { id = loyaltyScheme.Id }, loyaltyScheme);
        }

        // DELETE: api/LoyaltySchemes/5
        /// <summary>
        /// Endpoint to delete a loyalty scheme
        /// </summary>
        /// <param name="id">Id of loyalty scheme to be deleted</param>
        /// <returns>Task Done</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoyaltyScheme(int id)
        {
            if (_context.LoyaltySchemes == null)
            {
                return NotFound();
            }
            var loyaltyScheme = await _context.LoyaltySchemes.FindAsync(id);
            if (loyaltyScheme == null)
            {
                return NotFound();
            }

            _context.LoyaltySchemes.Remove(loyaltyScheme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Private method to see if a scheme exists
        /// </summary>
        /// <param name="id">Id of scheme to check</param>
        /// <returns>Boolean whether scheme exists or not</returns>
        private bool LoyaltySchemeExists(int id)
        {
            return (_context.LoyaltySchemes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
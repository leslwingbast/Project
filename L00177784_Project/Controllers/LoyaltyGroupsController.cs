using L00177784_Project.Data;
using L00177784_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L00177784_Project.Controllers
{
    /// <summary>
    /// Controller used for access to the LoyaltyGroups in the database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyGroupsController : ControllerBase
    {
        private readonly LoyaltyGroupsContext _context;

        /// <summary>
        /// Constructor including context for acccess to the database
        /// </summary>
        /// <param name="context">DBcontext for accessing database</param>
        public LoyaltyGroupsController(LoyaltyGroupsContext context)
        {
            _context = context;
        }

        // GET: api/LoyaltyGroups
        /// <summary>
        /// Get endpoint to get list of loyalty groups
        /// </summary>
        /// <returns>List of loyalty groups in database</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoyaltyGroup>>> GetLoyaltyGroups()
        {
            if (_context.LoyaltyGroups == null)
            {
                return NotFound();
            }
            return await _context.LoyaltyGroups.ToListAsync();
        }

        // GET: api/LoyaltyGroups/5
        /// <summary>
        /// Endpoint to get a loyalty group based on Id
        /// </summary>
        /// <param name="id">Id of loyalty group</param>
        /// <returns>Object of type LoyaltyGroup</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LoyaltyGroup>> GetLoyaltyGroup(int id)
        {
            if (_context.LoyaltyGroups == null)
            {
                return NotFound();
            }
            var loyaltyGroup = await _context.LoyaltyGroups.FindAsync(id);

            if (loyaltyGroup == null)
            {
                return NotFound();
            }

            return loyaltyGroup;
        }

        // PUT: api/LoyaltyGroups/5
        /// <summary>
        /// Endpoint to update a loyalty group based on Id
        /// </summary>
        /// <param name="id"></param>Id of loyalty group to be updated
        /// <param name="loyaltyGroup"></param>Loaylty group object with updated details
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoyaltyGroup(int id, LoyaltyGroup loyaltyGroup)
        {
            if (id != loyaltyGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(loyaltyGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoyaltyGroupExists(id))
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

        // POST: api/LoyaltyGroups
        /// <summary>
        /// Endpoint to create a loyalty group
        /// </summary>
        /// <param name="loyaltyGroup">Object of type LoyaltyGroup</param>
        /// <returns>Saved Loyalty Group</returns>
        [HttpPost]
        public async Task<ActionResult<LoyaltyGroup>> PostLoyaltyGroup(LoyaltyGroup loyaltyGroup)
        {
            if (_context.LoyaltyGroups == null)
            {
                return Problem("Entity set 'LoyaltyGroupsContext.LoyaltyGroups'  is null.");
            }
            _context.LoyaltyGroups.Add(loyaltyGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoyaltyGroup", new { id = loyaltyGroup.Id }, loyaltyGroup);
        }

        // DELETE: api/LoyaltyGroups/5
        /// <summary>
        /// Endpoint to delete a loyalty group based on Id
        /// </summary>
        /// <param name="id"></param>Id of loyalty group to be deleted
        /// <returns>Action completed</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoyaltyGroup(int id)
        {
            if (_context.LoyaltyGroups == null)
            {
                return NotFound();
            }
            var loyaltyGroup = await _context.LoyaltyGroups.FindAsync(id);
            if (loyaltyGroup == null)
            {
                return NotFound();
            }

            _context.LoyaltyGroups.Remove(loyaltyGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Private method to check if a loyalty group exists based on id
        /// </summary>
        /// <param name="id"></param>id to check if exists
        /// <returns>Boolean of whethere loyalty group exists or not</returns>
        private bool LoyaltyGroupExists(int id)
        {
            return (_context.LoyaltyGroups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
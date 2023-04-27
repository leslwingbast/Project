using L00177784_Project.Data;
using L00177784_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L00177784_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyGroupsController : ControllerBase
    {
        private readonly LoyaltyGroupsContext _context;

        public LoyaltyGroupsController(LoyaltyGroupsContext context)
        {
            _context = context;
        }

        // GET: api/LoyaltyGroups
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        private bool LoyaltyGroupExists(int id)
        {
            return (_context.LoyaltyGroups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
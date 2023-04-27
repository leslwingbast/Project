using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using L00177784_Project.Data;
using L00177784_Project.Models;

namespace L00177784_Project.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoyaltySchemesController : ControllerBase
{
    private readonly LoyaltyGroupsContext _context;

    public LoyaltySchemesController(LoyaltyGroupsContext context)
    {
        _context = context;
    }

    // GET: api/LoyaltySchemes
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

    // PUT: api/LoyaltySchemes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

    private bool LoyaltySchemeExists(int id)
    {
        return (_context.LoyaltySchemes?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}

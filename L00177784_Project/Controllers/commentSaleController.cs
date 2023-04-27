using Microsoft.AspNetCore.Mvc;
using L00177784_Project.Services;
using L00177784_Project.Models;
using Microsoft.EntityFrameworkCore;
using L00177784_Project.Services.SaleService;
using L00177784_Project.Data;

namespace L00177784_Project.Controllers;

[Route("api/[controller]")]
[ApiController]
public class commentSaleController : ControllerBase
{

    // POST: api/Sale
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<LoyaltyScheme>> PostSale(Sale sale)
    {
        ISaleService saleService = new SaleService();
        return saleService.ProcessSale(sale);
    }
}

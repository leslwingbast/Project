using L00177784_Project.Models;

namespace L00177784_Project.Services.SaleService;

public interface ISaleService
{
    LoyaltyScheme ProcessSale(Sale saleToProcess);
}

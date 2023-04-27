using L00177784_Project.Data;
using L00177784_Project.Models;

namespace L00177784_Project.Services.SaleService;

public class SaleService : ISaleService
{
    private readonly LoyaltyGroupsContext LoyaltyContext;
    public SaleService(LoyaltyGroupsContext loyaltyGroupConext)
    {
        LoyaltyContext = loyaltyGroupConext;
    }
    public LoyaltyScheme ProcessSale(Sale saleToProcess)
    {

        var saleProduct = LoyaltyContext.Products.First(x => x.Sku == saleToProcess.Sku || x.Barcode == saleToProcess.Barcode);

        var loyaltyGroup = LoyaltyContext.LoyaltyGroups.First(x => x.Products.Contains(saleProduct));

        var selectedScheme = LoyaltyContext.LoyaltySchemes.FirstOrDefault(x => x.CustomerId == saleToProcess.CustomerId && x.LoyaltyGroup_Id == loyaltyGroup.Id);

        if (selectedScheme != null)
        {
            if (saleToProcess.IsFree != true)
            {
                saleToProcess.Processed = true;
                LoyaltyContext.Sales.Update(saleToProcess);
                return UdateScheme(selectedScheme, saleToProcess.Qty, (int)selectedScheme.RemainingItems);
            }
            else
            {
                saleToProcess.Processed = true;
                LoyaltyContext.Sales.Update(saleToProcess);
                return ResetScheme(selectedScheme, saleToProcess.DateTime);
            }
        }
        else
        {
            if (saleProduct == null)
            {
                Console.WriteLine("Product does not exist!");
                return null;
            }
            if (loyaltyGroup != null)
            {
                var newScheme = new LoyaltyScheme()
                {
                    LoyaltyGroup = loyaltyGroup,
                    CustomerId = saleToProcess.CustomerId,
                    LastFreeBag = null,
                    RemainingItems = loyaltyGroup.Threshold - saleToProcess.Qty,
                    LoyaltyGroup_Id = loyaltyGroup.Id,
                    GroupName = loyaltyGroup.Name,
                };
                saleToProcess.Processed = true;
                LoyaltyContext.LoyaltySchemes.Add(newScheme);
                LoyaltyContext.SaveChanges();
                return newScheme;
            }
            else
            {
                Console.WriteLine("Product is not added to a loyalty group!");
                return null;
            }
        }
    }
    public LoyaltyScheme UdateScheme(LoyaltyScheme currentScheme, int quantity, int count)
    { 
        if((count - quantity) > 0)
        {
            currentScheme.RemainingItems = count - quantity;
            LoyaltyContext.LoyaltySchemes.Update(currentScheme);
            LoyaltyContext.SaveChanges();
        }
        return currentScheme;
    }

    public LoyaltyScheme ResetScheme(LoyaltyScheme currentScheme, DateTime freeBag)
    {
        currentScheme.RemainingItems = currentScheme.LoyaltyGroup.Threshold;
        currentScheme.LastFreeBag = freeBag;
        LoyaltyContext.SaveChanges();
        return currentScheme;
    }


}

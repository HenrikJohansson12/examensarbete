using API.Mappers;
using API.Models;
using API.Requests;
using Database;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IProductService
{
    Task<List<MapOfferDto>>GetUnmappedOffers();
    Task<bool> MapOffers(List<UpdateOffersDto> offers);
}
public class ProductService:IProductService
{
    private WebApiDbContext _dbContext;

    public ProductService(WebApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<MapOfferDto>>GetUnmappedOffers() 
    {
        var response = new List<MapOfferDto>();
        var offers = await  _dbContext.ProductRecords
            .Where(p => p.IsReviewed == false).Take(10).ToListAsync();
        
        for (int i = 0; i < offers.Count; i++)
        {
            response.Add(ProductRecordToUnmappedOffer.To(offers[i]));
        }

        return response;
    }

    public async Task<bool> MapOffers(List<UpdateOffersDto> offers)
    {
        try
        {
            for (int i = 0; i < offers.Count; i++)
            {
                var productRecord =
                    await _dbContext.ProductRecords.FirstOrDefaultAsync(p => p.Id == offers[i].ProductRecordId);
                productRecord.CategoryId = offers[i].CategoryId;
                productRecord.IngredientId = offers[i].IngredientId;
                productRecord.IsReviewed = true;
                await   _dbContext.SaveChangesAsync();
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
            
        }
    
    }
}
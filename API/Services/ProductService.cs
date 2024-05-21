using API.Mappers;
using API.Models;
using Database;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IProductService
{
    Task<List<MapOfferDto>>GetUnmappedOffers();
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
}
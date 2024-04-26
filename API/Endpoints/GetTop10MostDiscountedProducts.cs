using API.Mappers;
using API.Responses;
using Database;
using Database.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints;

public class GetTop10MostDiscountedProducts: EndpointWithoutRequest<GetTop10MostDiscountedProductsResponse>
{
    private WebApiDbContext _dbContext;

    public GetTop10MostDiscountedProducts(WebApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public override void Configure()
    {
        Get("/api/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync( CancellationToken ct)
    {
        
        
     var list =  await _dbContext.ProductRecords.ToListAsync();
     var resultList = new List<ProductRecordDTO>();
     foreach (var record in list)
     {
      resultList.Add(ProductRecordToDTO.To(record));
     }

     Response.Result = resultList;
        await SendAsync(Response);

    }
}
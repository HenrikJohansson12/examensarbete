namespace API.Properties.Services;

public interface IStoreServiceFactory
{
    IStoreService Create(string type);
}

public class StoreServiceFactory : IStoreServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public StoreServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IStoreService Create(string type)
    {
        switch (type)
        {
            case "Willys":
                return _serviceProvider.GetRequiredService<WillysService>();
            case "Ica":
                return _serviceProvider.GetRequiredService<IcaService>();
            default:
                throw new KeyNotFoundException($"StoreService of type {type} not found");
        }
    }
}
using Microsoft.Extensions.Configuration;

namespace RealEstate.IntegrationTests;

public class Configuration
{
    private static IConfiguration? _configuration;
    
    public static IConfiguration GetConfiguration()
    {
        if (_configuration != null)
            return _configuration;
        
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();
        
        return _configuration;
    }
}
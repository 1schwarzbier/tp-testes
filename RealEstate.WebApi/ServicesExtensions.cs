using RealEstate.Database;
using RealEstate.Services.PropertyService;

namespace RealEstate.WebApi;

public static class ServicesExtensions
{
    public static void ConfigureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddControllers();
        
        // Configuring Swagger/OAS
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
        
        serviceCollection.AddSingleton<ISqlConnectionFactory>(new SqlConnectionFactory(configuration.GetConnectionString("database")));
        serviceCollection.AddScoped<IPropertyService, PropertyService>();
        
        serviceCollection.AddCors(c =>
        {
            c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
        });
    }

    public static void Configure(this WebApplication webApplication)
    {
        if (webApplication.Environment.IsDevelopment())
            webApplication.UseDeveloperExceptionPage();
        
        webApplication.UseSwagger();
        webApplication.UseSwaggerUI();
        
        webApplication.UseHttpsRedirection();
        webApplication.UseAuthorization();
        webApplication.MapControllers();
    }
}
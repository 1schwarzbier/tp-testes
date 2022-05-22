namespace RealEstate.WebApi;

public static class ServicesExtensions
{
    public static void ConfigureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddControllers();
        
        // Configuring Swagger/OAS
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
        
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
using System.Data;
using Dapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Configuration;
using RealEstate.Database;
using RealEstate.Models;
using RealEstate.Services.PropertyService;
using RealEstate.Services.PropertyService.Models;

namespace RealEstate.IntegrationTests.Services.PerpertyService;

public class UpdatePropertyTests
{
    private ISqlConnectionFactory _sqlConnectionFactory;
    private IPropertyService _propertyService;
    
    [SetUp]
    public void Setup()
    {
        var config = Configuration.GetConfiguration();
        _sqlConnectionFactory = new SqlConnectionFactory(config.GetConnectionString("database"));
    }
    
    [Test]
    public async Task Add_Property_Test()
    {
        // Arrange
        var newProperty = new InsertPropertyRequest
        {
            Id = Guid.NewGuid(),
            Name = "[NEW] Apartment Integration Test",
            Description = "Nice local",
            ImageUrl = null,
            Type = "House",
            SaleMode = "Sale",
            Address = "1014 29th St",
            Size = 1000,
            ContactInfo = "Jack",
            Price = 1,
            BathroomCount = 3,
            RoomCount = 3,
            ParkingCount = 3,
            IsActive = true,
            IsCommercial = false,
        };
        
        using var connection = _sqlConnectionFactory.GetConnection();
        
        if (connection.State != ConnectionState.Open)
            connection.Open();

        await connection.ExecuteAsync(
            @"INSERT INTO [Platform].[Properties] ([Id], [Name], [Description], [ImageURL], [Type], [SaleMode], [Address], [Size], [ContactInfo], [Price], [BathroomCount], [RoomCount], [ParkingCount], [IsActive], [IsCommercial])
                    VALUES (@Id, @Name, @Description, @ImageURL, @Type, @SaleMode, @Address, @Size, @ContactInfo, @Price, @BathroomCount, @RoomCount, @ParkingCount, @IsActive, @IsCommercial)",
            newProperty);
        
        // Act
        var updatedProperty = new UpdatePropertyRequest
        {
            Id = newProperty.Id,
            Name = "[UPDATED] Apartment Integration Test",
            Description = "Nice local",
            ImageUrl = null,
            Type = "House",
            SaleMode = "Sale",
            Address = "1014 29th St",
            Size = 1000,
            ContactInfo = "Jack",
            Price = 1_000_000,
            BathroomCount = 3,
            RoomCount = 3,
            ParkingCount = 3,
            IsActive = false,
            IsCommercial = false,
        };
        
        _propertyService = new PropertyService(_sqlConnectionFactory);
        await _propertyService.Update(updatedProperty);
        
        var insertedProperty = (await connection.QueryAsync<Property>(
            @"SELECT * 
                    FROM [Platform].[Properties] 
                    WHERE
                        id = @Id",
            new
            {
                updatedProperty.Id
            }
        )).Single();
        
        // Assert
        using (new AssertionScope())
        {
            insertedProperty.Name.Should().Be(updatedProperty.Name);
            insertedProperty.Price.Should().Be(updatedProperty.Price);
            insertedProperty.IsActive.Should().Be(updatedProperty.IsActive);
        }
        
        // TearDown
        await connection.ExecuteAsync(
            @"DELETE FROM [Platform].[Properties]
                     WHERE Id = @Id", 
            new
            {
                updatedProperty.Id
            }
        );
    }
}
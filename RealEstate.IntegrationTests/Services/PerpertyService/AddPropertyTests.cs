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

public class AddPropertyTests
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
            Name = "Apartment Integration Test",
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
            IsActive = true,
            IsCommercial = false,
        };
        
        // Act
        _propertyService = new PropertyService(_sqlConnectionFactory);
        await _propertyService.Insert(newProperty);
        
        // Assert
        using var connection = _sqlConnectionFactory.GetConnection();
        
        if (connection.State != ConnectionState.Open)
            connection.Open();
        
        var insertedProperty = (await connection.QueryAsync<Property>(
            @"SELECT * 
                    FROM [Platform].[Properties] 
                    WHERE
                        id = @Id",
            new
            {
                newProperty.Id
            }
        )).Single();
        
        using (new AssertionScope())
        {
            insertedProperty.Id.Should().Be(newProperty.Id);
            insertedProperty.Name.Should().Be(newProperty.Name);
            insertedProperty.Description.Should().Be(newProperty.Description);
            insertedProperty.ImageUrl.Should().Be(newProperty.ImageUrl);
            insertedProperty.Type.Should().Be(newProperty.Type);
            insertedProperty.SaleMode.Should().Be(newProperty.SaleMode);
            insertedProperty.Address.Should().Be(newProperty.Address);
            insertedProperty.Size.Should().Be(newProperty.Size);
            insertedProperty.ContactInfo.Should().Be(newProperty.ContactInfo);
            insertedProperty.Price.Should().Be(newProperty.Price);
            insertedProperty.BathroomCount.Should().Be(newProperty.BathroomCount);
            insertedProperty.RoomCount.Should().Be(newProperty.RoomCount);
            insertedProperty.ParkingCount.Should().Be(newProperty.ParkingCount);
            insertedProperty.IsActive.Should().Be(newProperty.IsActive);
            insertedProperty.IsCommercial.Should().Be(newProperty.IsCommercial);
        }
        
        // TearDown
        await connection.ExecuteAsync(
            @"DELETE FROM [Platform].[Properties]
                     WHERE Id = @Id", 
            new
            {
                newProperty.Id
            }
        );
    }
}
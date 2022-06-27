using Dapper;
using FluentAssertions;
using FluentAssertions.Equivalency;
using FluentAssertions.Execution;
using Microsoft.Extensions.Configuration;
using RealEstate.Database;
using RealEstate.Services.PropertyService;
using RealEstate.Services.PropertyService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.IntegrationTests.Services.PerpertyService;

public class GetPropertyTests
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
    public async Task Get_Property_Test()
    {
        // Arrange
        var newProperty = new InsertPropertyRequest
        {
            Id = Guid.NewGuid(),
            Name = "[GET] Apartment Integration Test",
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

        using var connection = _sqlConnectionFactory.GetConnection();

        if (connection.State != ConnectionState.Open)
            connection.Open();

        await connection.ExecuteAsync(
            @"INSERT INTO [Platform].[Properties] ([Id], [Name], [Description], [ImageURL], [Type], [SaleMode], [Address], [Size], [ContactInfo], [Price], [BathroomCount], [RoomCount], [ParkingCount], [IsActive], [IsCommercial])
                    VALUES (@Id, @Name, @Description, @ImageURL, @Type, @SaleMode, @Address, @Size, @ContactInfo, @Price, @BathroomCount, @RoomCount, @ParkingCount, @IsActive, @IsCommercial)",
            newProperty);

        // Act
        _propertyService = new PropertyService(_sqlConnectionFactory);
        var insertedProperty = await _propertyService.Get(newProperty.Id);

        // Assert
        using (new AssertionScope())
        {
            insertedProperty.Resource?.Id.Should().Be(newProperty.Id);
            insertedProperty.Resource?.Name.Should().Be(newProperty.Name);
            insertedProperty.Resource?.Description.Should().Be(newProperty.Description);
            insertedProperty.Resource?.ImageUrl.Should().Be(newProperty.ImageUrl);
            insertedProperty.Resource?.Type.Should().Be(newProperty.Type);
            insertedProperty.Resource?.SaleMode.Should().Be(newProperty.SaleMode);
            insertedProperty.Resource?.Address.Should().Be(newProperty.Address);
            insertedProperty.Resource?.Size.Should().Be(newProperty.Size);
            insertedProperty.Resource?.ContactInfo.Should().Be(newProperty.ContactInfo);
            insertedProperty.Resource?.Price.Should().Be(newProperty.Price);
            insertedProperty.Resource?.BathroomCount.Should().Be(newProperty.BathroomCount);
            insertedProperty.Resource?.RoomCount.Should().Be(newProperty.RoomCount);
            insertedProperty.Resource?.ParkingCount.Should().Be(newProperty.ParkingCount);
            insertedProperty.Resource?.IsActive.Should().Be(newProperty.IsActive);
            insertedProperty.Resource?.IsCommercial.Should().Be(newProperty.IsCommercial);
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
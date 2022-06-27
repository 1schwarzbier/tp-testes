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

public class DeletePropertyTests
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
    public async Task Delete_Property_Test()
    {
        // Arrange
        var IdGuid = Guid.NewGuid();
        
            var newProperty = new InsertPropertyRequest
        {
            Id = IdGuid,
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
        
        using var connection = _sqlConnectionFactory.GetConnection();
        
        if (connection.State != ConnectionState.Open)
            connection.Open();

        await connection.ExecuteAsync(
            @"INSERT INTO [Platform].[Properties] ([Id], [Name], [Description], [ImageURL], [Type], [SaleMode], [Address], [Size], [ContactInfo], [Price], [BathroomCount], [RoomCount], [ParkingCount], [IsActive], [IsCommercial])
                    VALUES (@Id, @Name, @Description, @ImageURL, @Type, @SaleMode, @Address, @Size, @ContactInfo, @Price, @BathroomCount, @RoomCount, @ParkingCount, @IsActive, @IsCommercial)",
            newProperty);

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

        await _propertyService.Delete(IdGuid);

        var deleteProperty = (await connection.QueryAsync<Property>(
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
            deleteProperty.Id.Should().Be(newProperty.Id);
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
    }
}

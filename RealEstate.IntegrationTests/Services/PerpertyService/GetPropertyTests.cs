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

        var getProperty = await _propertyService.Get(IdGuid);

        //Assert
        using (new AssertionScope())
        {
            getProperty.Id.Should().Be(newProperty.Id);
            getProperty.Name.Should().Be(newProperty.Name);
            getProperty.Description.Should().Be(newProperty.Description);
            getProperty.ImageUrl.Should().Be(newProperty.ImageUrl);
            getProperty.Type.Should().Be(newProperty.Type);
            getProperty.SaleMode.Should().Be(newProperty.SaleMode);
            getProperty.Address.Should().Be(newProperty.Address);
            getProperty.Size.Should().Be(newProperty.Size);
            getProperty.ContactInfo.Should().Be(newProperty.ContactInfo);
            getProperty.Price.Should().Be(newProperty.Price);
            getProperty.BathroomCount.Should().Be(newProperty.BathroomCount);
            getProperty.RoomCount.Should().Be(newProperty.RoomCount);
            getProperty.ParkingCount.Should().Be(newProperty.ParkingCount);
            getProperty.IsActive.Should().Be(newProperty.IsActive);
            getProperty.IsCommercial.Should().Be(newProperty.IsCommercial);
        }
    }
}

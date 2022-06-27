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
        var newProperty = new InsertPropertyRequest
        {
            Id = Guid.NewGuid(),
            Name = "[DELETE] Apartment Integration Test",
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
        await _propertyService.Delete(newProperty.Id);

        // Assert
        var propertyCount = await connection.ExecuteScalarAsync<int>(
            @"SELECT COUNT(*) 
                    FROM [Platform].[Properties] 
                    WHERE
                        id = @Id
                ",
            new
            {
                newProperty.Id
            }
        );

        using (new AssertionScope())
        {
            propertyCount.Should().Be(0);
        }
    }
}
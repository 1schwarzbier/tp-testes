using System.Data;
using Dapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Configuration;
using RealEstate.Database;
using RealEstate.Models;
using RealEstate.Services;
using RealEstate.Services.PropertyService;

namespace RealEstate.IntegrationTests.Services.PerpertyService;

public class GetAllPropertiesTest
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
    public async Task Get_All_Properties_Test()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;
        
        // Act
        _propertyService = new PropertyService(_sqlConnectionFactory);
        var response = await _propertyService.GetAll(
            new PaginatedRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            }
        );
        
        // Assert
        using var connection = _sqlConnectionFactory.GetConnection();
        
        if (connection.State != ConnectionState.Open)
            connection.Open();
        
        var properties = (await connection.QueryAsync<Property>(
            @"SELECT *
                    FROM [Platform].[Properties]
                    ORDER BY [Id]
                    OFFSET @OffsetCount ROWS
                    FETCH NEXT @PageSize ROWS ONLY
            ",
            new
            {
                PageSize = pageSize,
                OffsetCount = (pageNumber - 1) * pageSize
            }
        )).ToList();

        using (new AssertionScope())
        {
            response.Resource?.Data?.Count().Should().Be(properties.Count);
        }
    }
}
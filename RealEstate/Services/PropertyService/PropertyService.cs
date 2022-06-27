using System.Data;
using System.Net;
using Dapper;
using RealEstate.Database;
using RealEstate.Models;
using RealEstate.Services.PropertyService.Models;
using RealEstate.Services.PropertyService.Validator;

namespace RealEstate.Services.PropertyService;

public class PropertyService : IPropertyService
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public PropertyService(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<ServiceResponse<Property>> Get(Guid id)
    {
        using var connection = _sqlConnectionFactory.GetConnection();

        if (connection.State != ConnectionState.Open)
            connection.Open();
            
        var property = (await connection.QueryAsync<Property>(
            @"SELECT * 
                    FROM [Platform].[Properties] 
                    WHERE
                        id = @Id",
            new
            {
                Id = id
            }
        )).SingleOrDefault();

        if (property == default(Property))
        {
            return new ServiceResponse<Property>
            {
                IsSuccessful = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                ServiceErrors = new []{ $"The specified 'id' = {id} was not found." }
            };
        }

        return new ServiceResponse<Property>
        {
            IsSuccessful = true,
            StatusCode = (int) HttpStatusCode.OK,
            Resource = property
        };
    }

    public async Task<ServiceResponse<PaginatedList<Property>>> GetAll(PaginatedRequest request)
    {
        using var connection = _sqlConnectionFactory.GetConnection();

        if (connection.State != ConnectionState.Open)
            connection.Open();

        var propertiesCount = await connection.ExecuteScalarAsync<int>(
            @"SELECT COUNT(*) FROM [Platform].[Properties]");

        if (propertiesCount == 0)
        {
            return new ServiceResponse<PaginatedList<Property>>
            {
                IsSuccessful = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                ServiceErrors = new[] {"No properties found in the database. Please Contact support!"}
            };
        }

        var properties = (await connection.QueryAsync<Property>(
            @"SELECT *
                FROM [Platform].[Properties]
                ORDER BY [Id]
                OFFSET @OffsetCount ROWS
                FETCH NEXT @PageSize ROWS ONLY",
            new
            {
                request.PageSize,
                OffsetCount = (request.PageNumber - 1) * request.PageSize
            }
        )).ToList();
            
        return new ServiceResponse<PaginatedList<Property>>
        {
            IsSuccessful = true,
            StatusCode = (int) HttpStatusCode.OK,
            Resource = new PaginatedList<Property>
            {
                Data = properties,
                Total = propertiesCount,
                PageIndex = request.PageNumber,
                PageSize = request.PageSize,
                PageTotal = properties.Count
            }
        };
    }

    public async Task<ServiceResponse<object>> Insert(InsertPropertyRequest request)
    {
        using var connection = _sqlConnectionFactory.GetConnection();

        if (connection.State != ConnectionState.Open)
            connection.Open();
            
        var validator = new InsertPropertyValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return new ServiceResponse<object>
            {
                IsSuccessful = false,
                StatusCode = (int)HttpStatusCode.BadRequest,
                ServiceErrors = result.Errors.Select(e => e.ToString()).ToArray()
            };
        }
            
        await connection.ExecuteAsync(
            @"INSERT INTO [Platform].[Properties] ([Id], [Name], [Description], [ImageURL], [Type], [SaleMode], [Address], [Size], [ContactInfo], [Price], [BathroomCount], [RoomCount], [ParkingCount], [IsActive], [IsCommercial])
                    VALUES (@Id, @Name, @Description, @ImageURL, @Type, @SaleMode, @Address, @Size, @ContactInfo, @Price, @BathroomCount, @RoomCount, @ParkingCount, @IsActive, @IsCommercial)",
            request);

        return new ServiceResponse<object>
        {
            IsSuccessful = true,
            StatusCode = (int) HttpStatusCode.NoContent
        };
    }

    public async Task<ServiceResponse<object>> Update(UpdatePropertyRequest request)
    {
        using var connection = _sqlConnectionFactory.GetConnection();

        if (connection.State != ConnectionState.Open)
            connection.Open();
            
        var validator = new UpdatePropertyValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return new ServiceResponse<object>
            {
                IsSuccessful = false,
                StatusCode = (int)HttpStatusCode.BadRequest,
                ServiceErrors = result.Errors.Select(e => e.ToString()).ToArray()
            };
        }
            
        var updatedRows = await connection.ExecuteAsync(
            @"UPDATE [Platform].[Properties] 
                    SET [Name] = @Name, 
                        [Description] = @Description, 
                        [ImageURL] = @ImageURL, 
                        [Type] = @Type, 
                        [SaleMode] = @SaleMode, 
                        [Address] = @Address, 
                        [Size] = @Size, 
                        [ContactInfo] = @ContactInfo, 
                        [Price] = @Price, 
                        [BathroomCount] = @BathroomCount, 
                        [RoomCount] = @RoomCount, 
                        [ParkingCount] = @ParkingCount, 
                        [IsActive] = @IsActive, 
                        [IsCommercial] = @IsCommercial
                    WHERE
                        [Id] = @Id",
            request);
            
        if (updatedRows == 0)
        {
            return new ServiceResponse<object>
            {
                IsSuccessful = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                ServiceErrors = new[] {$"The specified 'id' = {request.Id} was not found."}
            };
        }

        return new ServiceResponse<object>
        {
            IsSuccessful = true,
            StatusCode = (int) HttpStatusCode.NoContent
        };
    }

    public async Task<ServiceResponse<object>> Delete(Guid id)
    {
        using var connection = _sqlConnectionFactory.GetConnection();

        if (connection.State != ConnectionState.Open)
            connection.Open();

        var deletedRows = await connection.ExecuteAsync(
            @"DELETE FROM [Platform].[Properties]
                     WHERE Id = @Id", 
            new
            {
                Id = id
            }
        );

        if (deletedRows == 0)
        {
            return new ServiceResponse<object>
            {
                IsSuccessful = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                ServiceErrors = new[] {$"The specified 'id' = {id} was not found."}
            };
        }

        return new ServiceResponse<object>
        {
            IsSuccessful = true,
            StatusCode = (int) HttpStatusCode.NoContent
        };
    }
}
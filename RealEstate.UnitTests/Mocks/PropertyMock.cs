using RealEstate.Models;
using RealEstate.Services;
using RealEstate.Services.PropertyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.UnitTests.Mocks
{
    public static class PropertyMock
    {
        public static readonly Property PROPERTY_ENTITY_1 =
            new Property
            {
                Name = "Apartment three room",
                Description = "Nice local",
                ImageUrl = null,
                Type = "House",
                SaleMode = "Sale",
                Address = "1014 29th St",
                Size = 1000,
                ContactInfo = "Jackson",
                Price = 9564566341,
                BathroomCount = 3,
                RoomCount = 3,
                ParkingCount = 2,
                IsActive = true,
                IsCommercial = true,
            };

        public static readonly Property PROPERTY_ENTITY_2 =
            new Property
            {
                Name = "Apartment Four room",
                Description = "Nice local",
                ImageUrl = null,
                Type = "House",
                SaleMode = "Sale",
                Address = "900 20th St",
                Size = 1000,
                ContactInfo = "Joao",
                Price = 9564566341,
                BathroomCount = 3,
                RoomCount = 4,
                ParkingCount = 2,
                IsActive = true,
                IsCommercial = false,
            };

        public static readonly IEnumerable<Property> PROPERTY_ENTITY_LIST = new List<Property>
            {
                PROPERTY_ENTITY_1,
                PROPERTY_ENTITY_2
            }.AsEnumerable();


        public static readonly PaginatedList<Property> PAGINATED_LIST_MODEL =
            new PaginatedList<Property>
            {
                Data = PROPERTY_ENTITY_LIST,
                PageIndex = 1,
                PageSize = 2,
                PageTotal = 2,
                Total = 2
            };

        public static readonly ServiceResponse<PaginatedList<Property>> SERVICE_RESPONSE_GET_ALL = 
            new ServiceResponse<PaginatedList<Property>>
            {
                StatusCode = 200,
                IsSuccessful = true,
                Resource = PAGINATED_LIST_MODEL,
            };

        public static readonly ServiceResponse<PaginatedList<Property>> SERVICE_RESPONSE_GET_ALL_EMPTY =
            new ServiceResponse<PaginatedList<Property>>
            {
                StatusCode = 404,
                IsSuccessful = false,
                Resource = null,
            };

        public static readonly PaginatedRequest PAGINATED_REQUEST_MODEL_1 =
            new PaginatedRequest
            {
                PageNumber = 1,
                PageSize = 2,
            };

        public static readonly PaginatedRequest PAGINATED_REQUEST_MODEL_2 =
            new PaginatedRequest
            {
                PageNumber = 0,
                PageSize = 2,
            };

        public static readonly Guid GUID_1 = new("936DA01F-9ABD-4d9d-80C7-02AF85C822A8");

        public static readonly Guid GUID_2 = new("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4");

        public static readonly ServiceResponse<Property> SERVICE_RESPONSE_GET =
            new ServiceResponse<Property>
            {
                StatusCode = 200,
                IsSuccessful = true,
                Resource = PROPERTY_ENTITY_1,
            };

        public static readonly ServiceResponse<Property> SERVICE_RESPONSE_GET_NULL =
            new ServiceResponse<Property>
            {
                StatusCode = 404,
                IsSuccessful = false,
                Resource = null,
            };

        public static readonly InsertPropertyRequest INSERT_PROPERTY_REQUEST_1 =
            new InsertPropertyRequest
            {
                Name = "Apartment three room",
                Description = "Nice local",
                ImageUrl = null,
                Type = "House",
                SaleMode = "Sale",
                Address = "1014 29th St",
                Size = 1000,
                ContactInfo = "Jackson",
                Price = 9564566341,
                BathroomCount = 3,
                RoomCount = 3,
                ParkingCount = 2,
                IsActive = true,
                IsCommercial = true,
            };

        public static readonly InsertPropertyRequest INSERT_PROPERTY_REQUEST_2 =
            new InsertPropertyRequest
            {
                Name = "Apartment Four room",
                Description = "Nice local",
                ImageUrl = null,
                Type = "House",
                SaleMode = "Sale",
                Address = "900 20th St",
                Size = 1000,
                ContactInfo = "Joao",
                Price = 9564566341,
                BathroomCount = 3,
                RoomCount = 4,
                ParkingCount = 2,
                IsActive = true,
                IsCommercial = false,
            };

        public static readonly UpdatePropertyRequest UPDATE_PROPERTY_REQUEST_1 =
            new UpdatePropertyRequest
            {
                Id = GUID_1,
                Name = "Apartment three room",
                Description = "Nice local",
                ImageUrl = null,
                Type = "House",
                SaleMode = "Sale",
                Address = "1014 29th St",
                Size = 1000,
                ContactInfo = "Jackson",
                Price = 9564566341,
                BathroomCount = 3,
                RoomCount = 3,
                ParkingCount = 2,
                IsActive = true,
                IsCommercial = true,
            };

        public static readonly UpdatePropertyRequest UPDATE_PROPERTY_REQUEST_2 =
            new UpdatePropertyRequest
            {
                Id = GUID_2,    
                Name = "Apartment Four room",
                Description = "Nice local",
                ImageUrl = null,
                Type = "House",
                SaleMode = "Sale",
                Address = "900 20th St",
                Size = 1000,
                ContactInfo = "Joao",
                Price = 9564566341,
                BathroomCount = 3,
                RoomCount = 4,
                ParkingCount = 2,
                IsActive = true,
                IsCommercial = false,
            };

        public static readonly ServiceResponse<object> SERVICE_RESPONSE_SUCESS =
            new ServiceResponse<object>
            {
                StatusCode = 204,
                IsSuccessful = true,
                Resource = null,
            };

        public static readonly ServiceResponse<object> SERVICE_RESPONSE_FAILURE =
            new ServiceResponse<object>
            {
                StatusCode = 204,
                IsSuccessful = false,
                Resource = null,
            };
    }
}

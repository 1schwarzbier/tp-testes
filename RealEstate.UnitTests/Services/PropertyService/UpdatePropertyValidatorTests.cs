using FluentValidation.TestHelper;
using NUnit.Framework;
using RealEstate.Services.PropertyService.Models;
using RealEstate.Services.PropertyService.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.UnitTests.Services.PropertyService;

[TestFixture]
public class UpdatePropertyValidatorTests
{
    private UpdatePropertyValidator _validator = null!;

    [SetUp]
    public void Setup()
    {
        _validator = new UpdatePropertyValidator(10);
    }

    [Test]
    public async Task Should_throw_error_when_id_is_empty()
    {
        var model = new UpdatePropertyRequest
        {
            Id = new(),
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Id);
    }

    [Test]
    public async Task Should_not_throw_error_when_id_is_specified()
    {
        var model = new UpdatePropertyRequest
        {
            Id = new("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"),
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Id);
    }

    [Test]
    public async Task Should_throw_error_when_Name_is_null()
    {
        var model = new UpdatePropertyRequest
        {
            Name = null
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Name);
    }

    [Test]
    public async Task Should_not_throw_error_when_name_is_specified()
    {
        var model = new UpdatePropertyRequest
        {
            Name = "3 Bedroom Apartment"
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Name);
    }

    [Test]
    public async Task Should_throw_error_when_description_is_null()
    {
        var model = new UpdatePropertyRequest
        {
            Description = null,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Description);
    }

    [Test]
    public async Task Should_not_throw_error_when_description_is_specified()
    {
        var model = new UpdatePropertyRequest
        {
            Description = "The apartment complex is just a few minutes from the beach and also from the Flamengo subway station!",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Description);
    }

    [Test]
    public async Task Should_throw_error_when_imageurl_is_invalid()
    {
        var model = new UpdatePropertyRequest
        {
            ImageUrl = "ht://i.imgur.com/doi8lPN.jpeg",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.ImageUrl);
    }

    [Test]
    public async Task Should_accept_null_string_as_imageurl()
    {
        var model = new UpdatePropertyRequest
        {
            ImageUrl = null,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.ImageUrl);
    }

    [Test]
    public async Task Should_accept_empty_string_as_imageurl()
    {
        var model = new UpdatePropertyRequest
        {
            ImageUrl = "",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.ImageUrl);
    }

    [Test]
    public async Task Should_not_throw_error_when_imageurl_is_valid()
    {
        var model = new UpdatePropertyRequest
        {
            ImageUrl = "https://i.imgur.com/doi8lPN.jpeg",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.ImageUrl);
    }

    [Test]
    public async Task Should_throw_error_when_type_is_null()
    {
        var model = new UpdatePropertyRequest
        {
            Type = null,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Type);
    }

    [Test]
    public async Task Should_throw_error_when_type_is_string_empty()
    {
        var model = new UpdatePropertyRequest
        {
            Type = "",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Type);
    }

    [Test]
    public async Task Should_throw_error_when_type_is_invalid()
    {
        var model = new UpdatePropertyRequest
        {
            Type = "apartment complex",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Type);
    }


    [Test]
    public async Task Should_not_throw_error_when_type_is_apartment()
    {
        var model = new UpdatePropertyRequest
        {
            Type = "Apartment",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Type);
    }

    [Test]
    public async Task Should_not_throw_error_when_type_is_house()
    {
        var model = new UpdatePropertyRequest
        {
            Type = "House",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Type);
    }

    [Test]
    public async Task Should_throw_error_when_salemode_is_null()
    {
        var model = new UpdatePropertyRequest
        {
            SaleMode = null,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.SaleMode);
    }

    [Test]
    public async Task Should_throw_error_when_salemode_is_string_empty()
    {
        var model = new UpdatePropertyRequest
        {
            SaleMode = "",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.SaleMode);
    }

    [Test]
    public async Task Should_throw_error_when_salemode_is_invalid()
    {
        var model = new UpdatePropertyRequest
        {
            SaleMode = "Lend",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.SaleMode);
    }


    [Test]
    public async Task Should_not_throw_error_when_salemode_is_sale()
    {
        var model = new UpdatePropertyRequest
        {
            SaleMode = "Sale",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.SaleMode);
    }

    [Test]
    public async Task Should_not_throw_error_when_salemode_is_rent()
    {
        var model = new UpdatePropertyRequest
        {
            SaleMode = "Rent",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.SaleMode);
    }

    [Test]
    public async Task Should_throw_error_when_address_is_string_empty()
    {
        var model = new UpdatePropertyRequest
        {
            Address = "",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Address);
    }

    [Test]
    public async Task Should_not_throw_error_when_address_is_valid()
    {
        var model = new UpdatePropertyRequest
        {
            Address = "Mt Vernon Ave",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Address);
    }

    [Test]
    public async Task Should_throw_error_when_size_is_zero()
    {
        var model = new UpdatePropertyRequest
        {
            Size = 0,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Size);
    }

    [Test]
    public async Task Should_not_throw_error_when_size_is_greater_than_zero()
    {
        var model = new UpdatePropertyRequest
        {
            Size = 1,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Size);
    }

    [Test]
    public async Task Should_throw_error_when_contactinfo_is_string_empty()
    {
        var model = new UpdatePropertyRequest
        {
            ContactInfo = "",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.ContactInfo);
    }

    [Test]
    public async Task Should_not_throw_error_when_contactinfo_is_valid()
    {
        var model = new UpdatePropertyRequest
        {
            ContactInfo = "Jackson",
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.ContactInfo);
    }

    [Test]
    public async Task Should_throw_error_when_price_is_zero()
    {
        var model = new UpdatePropertyRequest
        {
            Price = 0,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Price);
    }

    [Test]
    public async Task Should_not_throw_error_when_price_is_greater_than_zero()
    {
        var model = new UpdatePropertyRequest
        {
            Price = 1,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Price);
    }

    [Test]
    public async Task Should_throw_error_when_bathroom_count_is_zero()
    {
        var model = new UpdatePropertyRequest
        {
            BathroomCount = 0,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.BathroomCount);
    }

    [Test]
    public async Task Should_not_throw_error_when_bathroom_count_is_greater_than_zero()
    {
        var model = new UpdatePropertyRequest
        {
            BathroomCount = 1,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.BathroomCount);
    }

    [Test]
    public async Task Should_throw_error_when_room_count_is_zero()
    {
        var model = new UpdatePropertyRequest
        {
            RoomCount = 0,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.RoomCount);
    }

    [Test]
    public async Task Should_not_throw_error_when_room_count_is_greater_than_zero()
    {
        var model = new UpdatePropertyRequest
        {
            RoomCount = 1,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.RoomCount);
    }

    [Test]
    public async Task Should_throw_error_when_parking_count_is_zero()
    {
        var model = new UpdatePropertyRequest
        {
            ParkingCount = -1,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldHaveValidationErrorFor(prop => prop.ParkingCount);
    }

    [Test]
    public async Task Should_not_throw_error_when_parking_count_is_zero()
    {
        var model = new UpdatePropertyRequest
        {
            ParkingCount = 0,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.ParkingCount);
    }

    [Test]
    public async Task Should_not_throw_error_when_parking_count_is_greater_than_zero()
    {
        var model = new UpdatePropertyRequest
        {
            ParkingCount = 1,
        };
        var result = await _validator.TestValidateAsync(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.ParkingCount);
    }
}
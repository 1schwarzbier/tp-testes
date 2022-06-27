using NUnit.Framework;
using FluentValidation.TestHelper;
using RealEstate.Services.PropertyService.Models;
using RealEstate.Services.PropertyService.Validator;

namespace RealEstate.UnitTests.Services.PropertyService;

[TestFixture]
public class InsertPropertyValidatorTests
{
    private InsertPropertyValidator _validator = null!;

    [SetUp]
    public void Setup()
    {
        _validator = new InsertPropertyValidator();
    }

    [Test]
    public void Should_throw_error_when_Name_is_null() 
    {
        var model = new InsertPropertyRequest
        {
            Name = null
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Name);
    }

    [Test]
    public void Should_not_throw_error_when_name_is_specified() 
    {
        var model = new InsertPropertyRequest
        {
            Name = "3 Bedroom Apartment"
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Name);
    }

    [Test]
    public void Should_throw_error_when_description_is_null()
    {
        var model = new InsertPropertyRequest
        {
            Description = null,
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Description);
    }

    [Test]
    public void Should_not_throw_error_when_description_is_specified()
    {
        var model = new InsertPropertyRequest
        {
            Description = "The apartment complex is just a few minutes from the beach and also from the Flamengo subway station!",
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Description);
    }

    [Test]
    public void Should_throw_error_when_imageurl_is_invalid()
    {
        var model = new InsertPropertyRequest
        {
            ImageUrl = "ht://i.imgur.com/doi8lPN.jpeg",
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.ImageUrl);
    }

    [Test]
    public void Should_accept_null_string_as_imageurl()
    {
        var model = new InsertPropertyRequest
        {
            ImageUrl = null,
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.ImageUrl);
    }

    [Test]
    public void Should_accept_empty_string_as_imageurl()
    {
        var model = new InsertPropertyRequest
        {
            ImageUrl = "",
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.ImageUrl);
    }

    [Test]
    public void Should_not_throw_error_when_imageurl_is_valid()
    {
        var model = new InsertPropertyRequest
        {
            ImageUrl = "https://i.imgur.com/doi8lPN.jpeg",
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.ImageUrl);
    }

    [Test]
    public void Should_throw_error_when_type_is_null()
    {
        var model = new InsertPropertyRequest
        {
            Type = null,
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Type);
    }

    [Test]
    public void Should_throw_error_when_type_is_string_empty()
    {
        var model = new InsertPropertyRequest
        {
            Type = "",
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Type);
    }

    [Test]
    public void Should_throw_error_when_type_is_invalid()
    {
        var model = new InsertPropertyRequest
        {
            Type = "apartment complex",
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Type);
    }


    [Test]
    public void Should_not_throw_error_when_type_is_apartment()
    {
        var model = new InsertPropertyRequest
        {
            Type = "Apartment",
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Type);
    }

    [Test]
    public void Should_not_throw_error_when_type_is_house()
    {
        var model = new InsertPropertyRequest
        {
            Type = "House",
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Type);
    }

    [Test]
    public void Should_throw_error_when_salemode_is_null()
    {
        var model = new InsertPropertyRequest
        {
            SaleMode = null,
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.SaleMode);
    }

    [Test]
    public void Should_throw_error_when_salemode_is_string_empty()
    {
        var model = new InsertPropertyRequest
        {
            SaleMode = "",
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.SaleMode);
    }

    [Test]
    public void Should_throw_error_when_salemode_is_invalid()
    {
        var model = new InsertPropertyRequest
        {
            SaleMode = "Lend",
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.SaleMode);
    }


    [Test]
    public void Should_not_throw_error_when_salemode_is_sale()
    {
        var model = new InsertPropertyRequest
        {
            SaleMode = "Sale",
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.SaleMode);
    }

    [Test]
    public void Should_not_throw_error_when_salemode_is_rent()
    {
        var model = new InsertPropertyRequest
        {
            SaleMode = "Rent",
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.SaleMode);
    }

    [Test]
    public void Should_throw_error_when_address_is_string_empty()
    {
        var model = new InsertPropertyRequest
        {
            Address = "",
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Address);
    }

    [Test]
    public void Should_not_throw_error_when_address_is_valid()
    {
        var model = new InsertPropertyRequest
        {
            Address = "Mt Vernon Ave",
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Address);
    }

    [Test]
    public void Should_throw_error_when_size_is_zero()
    {
        var model = new InsertPropertyRequest
        {
            Size = 0,
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Size);
    }

    [Test]
    public void Should_not_throw_error_when_size_is_greater_than_zero()
    {
        var model = new InsertPropertyRequest
        {
            Size = 1,
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Size);
    }

    [Test]
    public void Should_throw_error_when_contactinfo_is_string_empty()
    {
        var model = new InsertPropertyRequest
        {
            ContactInfo = "",
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.ContactInfo);
    }

    [Test]
    public void Should_not_throw_error_when_contactinfo_is_valid()
    {
        var model = new InsertPropertyRequest
        {
            ContactInfo = "Jackson",
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.ContactInfo);
    }

    [Test]
    public void Should_throw_error_when_price_is_zero()
    {
        var model = new InsertPropertyRequest
        {
            Price = 0,
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Price);
    }

    [Test]
    public void Should_not_throw_error_when_price_is_greater_than_zero()
    {
        var model = new InsertPropertyRequest
        {
            Price = 1,
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Price);
    }

    [Test]
    public void Should_throw_error_when_bathroom_count_is_zero()
    {
        var model = new InsertPropertyRequest
        {
            BathroomCount = 0,
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.BathroomCount);
    }

    [Test]
    public void Should_not_throw_error_when_bathroom_count_is_greater_than_zero()
    {
        var model = new InsertPropertyRequest
        {
            BathroomCount = 1,
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.BathroomCount);
    }

    [Test]
    public void Should_throw_error_when_room_count_is_zero()
    {
        var model = new InsertPropertyRequest
        {
            RoomCount = 0,
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.RoomCount);
    }

    [Test]
    public void Should_not_throw_error_when_room_count_is_greater_than_zero()
    {
        var model = new InsertPropertyRequest
        {
            RoomCount = 1,
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.RoomCount);
    }

    [Test]
    public void Should_throw_error_when_parking_count_is_zero()
    {
        var model = new InsertPropertyRequest
        {
            ParkingCount = -1,
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.ParkingCount);
    }

    [Test]
    public void Should_not_throw_error_when_parking_count_is_zero()
    {
        var model = new InsertPropertyRequest
        {
            ParkingCount = 0,
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.ParkingCount);
    }

    [Test]
    public void Should_not_throw_error_when_parking_count_is_greater_than_zero()
    {
        var model = new InsertPropertyRequest
        {
            ParkingCount = 1,
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.ParkingCount);
    }
}
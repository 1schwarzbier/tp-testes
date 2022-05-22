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
    public void Should_have_error_when_Name_is_null() 
    {
        var model = new InsertPropertyRequest
        {
            Name = null
        };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(prop => prop.Name);
    }

    [Test]
    public void Should_not_have_error_when_name_is_specified() 
    {
        var model = new InsertPropertyRequest
        {
            Name = "Apartamento 3 Quartos"
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(prop => prop.Name);
    }
}
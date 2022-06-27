using FluentValidation;
using RealEstate.Services.PropertyService.Models;

namespace RealEstate.Services.PropertyService.Validator;

public class UpdatePropertyValidator : AbstractValidator<UpdatePropertyRequest>
{
    public UpdatePropertyValidator()
    {
        RuleFor(r => r.Id).NotEmpty();
        RuleFor(r => r.Name).NotEmpty();
        RuleFor(r => r.Description).NotEmpty();
        RuleFor(r => r.ImageUrl).Must(CustomValidators.LinkMustBeAUri);
        RuleFor(r => r.Type).Must(CustomValidators.MustBeValidPropertyType)
            .WithMessage("The specified 'Type' is not valid. The options are (Apartment, House)");
        RuleFor(r => r.SaleMode).Must(CustomValidators.MustBeValidSaleMode)
            .WithMessage("The specified 'SaleMode' is not valid. The options are (Sale, Rent)");
        RuleFor(r => r.Address).NotEmpty();
        RuleFor(r => r.Size).GreaterThan(0);
        RuleFor(r => r.ContactInfo).NotEmpty();
        RuleFor(r => r.Price).GreaterThan(0);
        RuleFor(r => r.BathroomCount).GreaterThan(0);
        RuleFor(r => r.RoomCount).GreaterThan(0);
        RuleFor(r => r.ParkingCount).GreaterThanOrEqualTo(0);
    }
}
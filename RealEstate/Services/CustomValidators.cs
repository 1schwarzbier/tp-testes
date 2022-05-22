namespace RealEstate.Services;

public static class CustomValidators
{
    private static IEnumerable<string> PropertyType => new []{ "Apartment", "House"};
    private static IEnumerable<string> SaleMode => new []{ "Sale", "Rent"};
    
    public static bool MustBeValidPropertyType(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && PropertyType.Contains(value.Trim());
    }
    
    public static bool MustBeValidSaleMode(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && SaleMode.Contains(value.Trim());
    }
    
    public static bool LinkMustBeAUri(string? link)
    {
        if (string.IsNullOrWhiteSpace(link))
            return true;

        return Uri.TryCreate(link, UriKind.Absolute, out var outUri)
               && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
    }
}
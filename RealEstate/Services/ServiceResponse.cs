namespace RealEstate.Services;

public class ServiceResponse<T>
{
    public bool IsSuccessful { get; set; }
    public int StatusCode { get; set; }
    public string[]? ServiceErrors { get; set; }
    public T? Resource { get; set; }
}
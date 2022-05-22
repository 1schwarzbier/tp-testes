namespace RealEstate.Models;

public class Property: Entity
{
    public string Name { get; set; } = "Untitled";
    public string Description { get; set; } = "Description";
    public string? ImageUrl { get; set; }
    public string Type { get; set; } = "House";
    public string SaleMode { get; set; } = "Sale";
    public string Address { get; set; } = "Address";
    public decimal Size { get; set; }
    public string ContactInfo { get; set; } = "Contact";
    public decimal Price { get; set; }
    public int BathroomCount { get; set; }
    public int RoomCount { get; set; }
    public int ParkingCount { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsCommercial { get; set; } = false;
}
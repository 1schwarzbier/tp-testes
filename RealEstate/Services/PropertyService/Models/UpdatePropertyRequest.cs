namespace RealEstate.Services.PropertyService.Models
{
    public class UpdatePropertyRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Type { get; set; }
        public string? SaleMode { get; set; }
        public string? Address { get; set; } = "Address";
        public decimal Size { get; set; }
        public string? ContactInfo { get; set; } = "Contact";
        public decimal Price { get; set; }
        public int BathroomCount { get; set; }
        public int RoomCount { get; set; }
        public int? ParkingCount { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsCommercial { get; set; } = false;
    }
}


namespace DreamHouse.Core.Application.Dtos.Filters
{
    public class PropertiesFilter
    {
        public string? Code { get; set; }
        public string? Type { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
    }
}

namespace DreamHouse.Core.Application.ViewModels.Property
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int SquareMeter { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int TypePropertyId { get; set; }
        public int TypeSaleId { get; set; }
        public string AgentId { get; set; }



        //
        public string TypePropertyName { get; set; }
        public string TypeSaleName { get; set; }
        public List<string> Improvements { get; set; }
    }
}

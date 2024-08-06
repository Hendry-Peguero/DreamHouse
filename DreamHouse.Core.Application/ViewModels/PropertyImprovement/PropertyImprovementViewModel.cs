using DreamHouse.Core.Application.ViewModels.Improvement;
using DreamHouse.Core.Application.ViewModels.Property;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.ViewModels.PropertyImprovement
{
    public class PropertyImprovementViewModel
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int ImprovementId { get; set; }

        //NAV
        public PropertyViewModel Property { get; set; }
        public ImprovementViewModel Improvement { get; set; }
    }
}

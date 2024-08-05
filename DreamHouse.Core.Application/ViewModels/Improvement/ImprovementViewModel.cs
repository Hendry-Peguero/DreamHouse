using DreamHouse.Core.Application.ViewModels.PropertyImprovement;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.ViewModels.Improvement
{
    public class ImprovementViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //NAV
        public ICollection<PropertyImprovementViewModel> ImprovementProperties { get; set; }

    }
}

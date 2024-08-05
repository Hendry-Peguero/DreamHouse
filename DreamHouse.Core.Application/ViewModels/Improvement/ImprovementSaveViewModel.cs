using System.ComponentModel.DataAnnotations;

namespace DreamHouse.Core.Application.ViewModels.Improvement
{
    public class ImprovementSaveViewModel
    {
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}

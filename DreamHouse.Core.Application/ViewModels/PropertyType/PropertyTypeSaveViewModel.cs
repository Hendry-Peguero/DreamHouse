using System.ComponentModel.DataAnnotations;

namespace DreamHouse.Core.Application.ViewModels.PropertyType
{
    public class PropertyTypeSaveViewModel
    {
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text,ErrorMessage ="You have to write a string name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text, ErrorMessage = "Is required")]
        public string Description { get; set; }
    }
}

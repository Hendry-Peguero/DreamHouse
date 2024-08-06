using System.ComponentModel.DataAnnotations;

namespace DreamHouse.Core.Application.ViewModels.SaleType
{
    public class SaleTypeSaveViewModel
    {
        public int? Id { get; set; }

        [Required]
        [DataType(DataType.Text,ErrorMessage ="Write a correct name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text, ErrorMessage = "Write a correct description")]
        public string Description { get; set; }
    }
}

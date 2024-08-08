using DreamHouse.Core.Application.CustomValidations.Property;
using DreamHouse.Core.Application.ViewModels.Improvement;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.SaleType;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DreamHouse.Core.Application.ViewModels.Property
{
    public class PropertySaveViewModel
    {
        [Range(1,int.MaxValue,ErrorMessage = "Select a valid option")]
        public int TypePropertyId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a valid option")]
        public int TypeSaleId { get; set; }

        [Required(ErrorMessage = "Select one or more options")]
        [NoZeroInList(ErrorMessage = "Cannot select the first option")]
        public List<int> IdSelectedImprovements { get; set; }

        [Required(ErrorMessage = "The price field cannot be empty")]
        [DataType(DataType.Currency)]
        public double? Price { get; set; }

        [Required(ErrorMessage = "The description field cannot be empty")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The square meter field cannot be empty")]
        public int? SquareMeter { get; set; }

        [Required(ErrorMessage = "The bedrooms field cannot be empty")]
        public int? Bedrooms { get; set; }

        [Required(ErrorMessage = "The bathrooms field cannot be empty")]
        public int? Bathrooms { get; set; }

        [Required(ErrorMessage = "The image field cannot be empty")]
        public List<IFormFile> Images { get; set; }
        public List<string>? ImagesUrl { get; set; }


        // Properties for hide
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? AgentId { get; set; }


        // Lists
        public List<PropertyTypeViewModel>? PropertyTypes { get; set; }
        public List<SaleTypeViewModel>? SaleTypes { get; set; }
        public List<ImprovementViewModel>? Improvements { get; set; }
    }
}

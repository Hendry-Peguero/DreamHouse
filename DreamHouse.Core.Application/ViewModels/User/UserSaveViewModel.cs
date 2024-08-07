using DreamHouse.Core.Application.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DreamHouse.Core.Application.ViewModels.User
{
    public class UserSaveViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "The name field cannot be empty")]
        [DataType(DataType.Text)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "The lastname field cannot be empty")]
        [DataType(DataType.Text)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "The idcard field cannot be empty")]
        [DataType(DataType.Text)]
        public string? IdCard { get; set; }

        [Required(ErrorMessage = "You must put a phone")]
        [DataType(DataType.Text)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "The Email field cannot be empty")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The username field cannot be empty")]
        [DataType(DataType.Text)]
        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        public string? UserType { get; set; }


        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public string? ImageUrl { get; set; }


        public List<string>? Roles { get; set; }
        public int? Status { get; set; }
        public bool HasError { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
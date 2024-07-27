using Microsoft.AspNetCore.Identity;

namespace DreamHouse.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        //public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string IdCard { get; set; }
        public int Status { get; set; }
    }
}

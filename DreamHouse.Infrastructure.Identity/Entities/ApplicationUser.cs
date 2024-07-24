using Microsoft.AspNetCore.Identity;

namespace DreamHouse.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Id { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string Cedula { get; set; }
        public int Status { get; set; }

    }
}

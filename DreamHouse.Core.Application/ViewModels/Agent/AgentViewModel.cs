using System.Text.Json.Serialization;

namespace DreamHouse.Core.Application.ViewModels.Agent
{
    public class AgentViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdCard { get; set; }
        public string UserName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public int Status { get; set; }

        [JsonIgnore]
        public string? UserType { get; set; }
        public int NumberPropertiesAssigned { get; set; }
    }
}

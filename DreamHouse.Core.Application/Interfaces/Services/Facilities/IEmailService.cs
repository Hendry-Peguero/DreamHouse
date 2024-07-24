using DreamHouse.Core.Application.Dtos.Email;

namespace DreamHouse.Core.Application.Interfaces.Services.Facilities
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}

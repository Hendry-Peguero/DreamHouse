using Microsoft.AspNetCore.Http;
using DreamHouse.Core.Application.Enums;

namespace DreamHouse.Core.Application.Interfaces.Helpers
{
    public interface IImageHelper
    {
        void RemoveImage(string entityId, EGroupImage group);
        string SaveImage(IFormFile file, string entityId, EGroupImage group);
        string UpdateImage(IFormFile file, string relativeImagePath);
    }
}
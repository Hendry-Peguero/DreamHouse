using Microsoft.AspNetCore.Http;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Interfaces.Helpers;

namespace DreamHouse.Core.Application.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public string SaveImage(IFormFile file, string entityId, EGroupImage group)
        {
            //Check the file

            if (file == null) { return string.Empty; }

            //Folder creation

            string relativeFolderPath = $"/Images/{Enum.GetName(group)}/{entityId}";
            string absoluteFolderPath = AddAbsolutePath($"wwwroot{relativeFolderPath}");

            if (!Directory.Exists(absoluteFolderPath))
            {
                Directory.CreateDirectory(absoluteFolderPath);
            }

            //Image creation

            Guid randomImageName = Guid.NewGuid();
            string extensionImage = file.FileName.Split('.')[^1];
            string newImageName = $"{randomImageName}.{extensionImage}";
            string absoluteImagePath = Path.Combine(absoluteFolderPath, newImageName);

            using (FileStream stream = new FileStream(absoluteImagePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            string relativeImagePath = $"{relativeFolderPath}/{newImageName}";

            return relativeImagePath;
        }

        public string UpdateImage(IFormFile file, string relativeImagePath)
        {
            // Check the file

            if (file == null) return relativeImagePath;

            // Check the relativeImagePath

            if (string.IsNullOrEmpty(relativeImagePath)) return relativeImagePath;

            // Add new image to replace the old

            string[] relativeOldImagePath = relativeImagePath.Split('/');
            string oldImageId = relativeOldImagePath[3];
            string oldImageGroup = relativeOldImagePath[2];
            string relativeNewImagePath = SaveImage(
                file,
                oldImageId,
                (EGroupImage)Enum.Parse(typeof(EGroupImage), oldImageGroup)
            );

            // Delete Old Image

            string absoluteOldImagePath = AddAbsolutePath($"wwwroot{relativeImagePath}");

            if (File.Exists(absoluteOldImagePath))
            {
                File.Delete(absoluteOldImagePath);
            }

            return relativeNewImagePath;
        }

        public void RemoveImage(string entityId, EGroupImage group)
        {
            //Folder Info

            string relativeFolderPath = $"/Images/{Enum.GetName(group)}/{entityId}";
            string absoluteFolderPath = AddAbsolutePath($"wwwroot{relativeFolderPath}");

            //Delete all of that folder

            if (Directory.Exists(absoluteFolderPath))
            {
                DirectoryInfo directory = new(absoluteFolderPath);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(absoluteFolderPath);
            }
        }

        private string AddAbsolutePath(string path)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), path);
        }
    }
}

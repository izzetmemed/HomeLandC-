using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace MyApi.Method
{
    public class UploadImageAndGetPath
    {
        public string UploadImage(Cloudinary cloudinary, IFormFile image, string cloudinaryImagePath)
        {
            using (var stream = image.OpenReadStream())
            {
                cloudinaryImagePath = cloudinaryImagePath.TrimStart('/');
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(image.FileName, stream),
                    PublicId = cloudinaryImagePath,
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                return uploadResult.SecureUri.ToString();
            }
        }
    }
}

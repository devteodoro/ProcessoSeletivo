using System.Drawing;
using System.Drawing.Imaging;

namespace ProcessoSeletivo.Api.Helpers
{
    public static class ImageHelper
    {
        public static string ConvertImageToBase64JPG(IFormFile formFile)
        {
            using (var stream = formFile.OpenReadStream())
            {
                using (var image = Image.FromStream(stream))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, ImageFormat.Jpeg);
                        byte[] imageBytes = memoryStream.ToArray();
                        string base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }
            }
        }
    }
}

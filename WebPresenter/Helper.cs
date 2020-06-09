using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebPresenter {
    public class Helper {
        public static void Swap<T>(ref T a, ref T b) {
            T c = a;
            a = b;
            b = c;
        }

        public static string GetStringFromFile(IFormFile file) {
            var str = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream())) {
                while (reader.Peek() >= 0)
                    str.AppendLine(reader.ReadLine());
            }

            return str.ToString();
        }

        public static async Task<string[]> GetImagesFromFile(IFormFile file) {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return await GetImagesFromStream(memoryStream);
        }

        public static async Task<string[]> GetImagesFromStream(MemoryStream stream) {
            Image image = Image.FromStream(stream);
            
            var dimension = new FrameDimension(image.FrameDimensionsList[0]);
            int pageCount = image.GetFrameCount(dimension);
            var images = new string[pageCount];
            
            for (int i = 0; i < pageCount; i++) {
                await using var memoryStream = new MemoryStream();
                image.SelectActiveFrame(dimension, i);
                image.Save(memoryStream, ImageFormat.Bmp);
                images[i] = Convert.ToBase64String(memoryStream.ToArray());
            }
            
            return images;
        }
    }
}
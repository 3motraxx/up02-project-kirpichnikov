using System;
using System.Drawing;
using System.IO;

namespace ProductCard
{
    public static class Resources
    {
        public const string PATH_PICTURE =
            "resources/picture.png";

        public const string PATH_LOGO =
            "resources/logo.png";

        public const string PATH_ICON =
            "resources/icon.ico";

        public static Image? LoadImage(
            string path,
            Size size)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return null;
                }

                using (Image original = Image.FromFile(path))
                {
                    return new Bitmap(
                        original,
                        size.Width,
                        size.Height
                    );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    $"Ошибка загрузки {path}: {e.Message}"
                );

                return null;
            }
        }

        public static Image? LoadImageProportional(
            string path,
            Size maxSize)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return null;
                }

                using (Image original = Image.FromFile(path))
                {
                    int width = original.Width;
                    int height = original.Height;

                    double scale = Math.Min(
                        (double)maxSize.Width / width,
                        (double)maxSize.Height / height
                    );

                    if (scale > 1)
                    {
                        scale = 1;
                    }

                    int newWidth =
                        (int)(width * scale);

                    int newHeight =
                        (int)(height * scale);

                    return new Bitmap(
                        original,
                        newWidth,
                        newHeight
                    );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    $"Ошибка загрузки {path}: {e.Message}"
                );

                return null;
            }
        }

        public static Image? GetProductImage(
            string? imagePath,
            Size size)
        {
            if (string.IsNullOrWhiteSpace(imagePath) ||
                !File.Exists(imagePath))
            {
                return LoadImage(
                    PATH_PICTURE,
                    size
                );
            }

            return LoadImage(
                imagePath,
                size
            );
        }
    }
}
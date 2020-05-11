using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Media.Imaging;

namespace GemstonesBusinessSystem.Utils
{
    class HandleImage
    {
        // Hàm hiển thị hình ảnh từ một string
        public static BitmapImage GetImage(string imageSourceString)
        {
           
            var img = System.Drawing.Image.FromStream(new MemoryStream(System.Convert.FromBase64String(imageSourceString)));
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
            BitmapImage result = BitmapToImageSource(bmp);
            return result;
        }

        // Hàm chuyển đổi image thành một string
        public static string ImageToString(string imagePath)
        {
            int width = 150;
            int height = 150;

            var source = Bitmap.FromFile(imagePath);
            var result = (Bitmap)ResizeImageKeepAspectRatio(source, width, height);
            result.Save("../../Images/NoName.png");

            byte[] imageArray = System.IO.File.ReadAllBytes("../../Images/NoName.png");
            return System.Convert.ToBase64String(imageArray);
        }

        // Hàm chuyển đổi bitmap thành image
        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        // Hàm resize ảnh mà vẫn giữ tỉ lệ gốc
        public static System.Drawing.Image ResizeImageKeepAspectRatio(System.Drawing.Image source, int width, int height)
        {
            System.Drawing.Image result = null;
            try
            {
                if (source.Width != width || source.Height != height)
                {
                    // Resize image
                    float sourceRatio = (float)source.Width / source.Height;
                    using (var target = new Bitmap(width, height))
                    {
                        using (var g = System.Drawing.Graphics.FromImage(target))
                        {
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = SmoothingMode.HighQuality;

                            // Scaling
                            float scaling;
                            float scalingY = (float)source.Height / height;
                            float scalingX = (float)source.Width / width;
                            if (scalingX < scalingY) scaling = scalingX; else scaling = scalingY;
                            int newWidth = (int)(source.Width / scaling);
                            int newHeight = (int)(source.Height / scaling);

                            // Correct float to int rounding
                            if (newWidth < width) newWidth = width;
                            if (newHeight < height) newHeight = height;

                            // See if image needs to be cropped
                            int shiftX = 0;
                            int shiftY = 0;
                            if (newWidth > width)
                            {
                                shiftX = (newWidth - width) / 2;
                            }

                            if (newHeight > height)
                            {
                                shiftY = (newHeight - height) / 2;
                            }

                            // Draw image
                            g.DrawImage(source, -shiftX, -shiftY, newWidth, newHeight);
                        }
                        result = (System.Drawing.Image)target.Clone();
                    }
                }
                else
                {
                    // Image size matched the given size
                    result = (System.Drawing.Image)source.Clone();
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }
    }
}

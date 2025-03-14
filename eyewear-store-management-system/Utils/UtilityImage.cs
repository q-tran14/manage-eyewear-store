using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eyewear_store_management_system.Utils
{
    public static class UtilityImage
    {
        public static Bitmap AdjustImage(Bitmap original, float exposure = 0f, float contrast = 1f, float saturation = 1f, float temperature = 0f, float tint = 0f, float highlights = 0f, float shadows = 0f)
        {
            Bitmap newImg = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(newImg))
            {
                ImageAttributes attributes = new ImageAttributes();

                // Chuyển đổi giá trị nhiệt độ & tint
                float tempRed = 1 + (temperature / 100f);
                float tempBlue = 1 - (temperature / 100f);
                float tintGreen = 1 + (tint / 100f);
                float tintMagenta = 1 - (tint / 100f);

                // Điều chỉnh tương phản và bão hòa
                float satR = 0.3086f * (1 - saturation) + saturation;
                float satG = 0.6094f * (1 - saturation) + saturation;
                float satB = 0.0820f * (1 - saturation) + saturation;

                float[][] colorMatrixElements =
                {
            new float[] {contrast * satR * tempRed, 0, 0, 0, 0},
            new float[] {0, contrast * satG * tintGreen, 0, 0, 0},
            new float[] {0, 0, contrast * satB * tempBlue * tintMagenta, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {exposure, exposure, exposure, 0, 1}
        };

                ColorMatrix matrix = new ColorMatrix(colorMatrixElements);
                attributes.SetColorMatrix(matrix);

                // Áp dụng ma trận màu
                g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                            0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            }
            return newImg;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageProcessingTool.Shared
{
    static public class ImageCommon
    {
        public static Bitmap GetGrayScaledImage(Bitmap Bmp)
        {
            int rgb;
            Color c;

            for (int y = 0; y < Bmp.Height; y++)
                for (int x = 0; x < Bmp.Width; x++)
                {
                    c = Bmp.GetPixel(x, y);
                    rgb = (int)((c.R + c.G + c.B) / 3);
                    Bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            return Bmp;
        }
        public static Bitmap GetBitMapFromHttpPostedFileBase(Stream stream)
        {
            Bitmap bmp = new Bitmap(stream);
            Bitmap bpmnew = new Bitmap(bmp.Width, bmp.Height);
            Graphics canvas = Graphics.FromImage(bmp);
            canvas = Graphics.FromImage(bpmnew);
            canvas.DrawImage(bmp, new Rectangle(0, 0,
            bpmnew.Width, bpmnew.Height), 0, 0, bmp.Width, bmp.Height,
            GraphicsUnit.Pixel);
            return bpmnew;
        }

    }
}
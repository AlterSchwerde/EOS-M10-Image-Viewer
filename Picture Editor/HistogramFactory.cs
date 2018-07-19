using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Picture_Editor
{
    class HistogramFactory
    {

        #region CreateHistogram
        public static Bitmap CreateHistogram(string filename)
        {
            Bitmap bmp = new Bitmap(filename);

            int[] countPixel = new int[256];

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpdata = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpdata.Scan0;
            int bytes = bmp.Width * bmp.Height * 3;
            byte[] grayValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

            int maxPixel = 0;
            Array.Clear(countPixel, 0, 256);
            for (int i = 0; i < bytes; i++)
            {
                byte temp = grayValues[i];
                countPixel[temp]++;
                if (countPixel[temp] > maxPixel)
                { maxPixel = countPixel[temp]; }
            }

            System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes); bmp.UnlockBits(bmpdata);


            Bitmap bitmap = new Bitmap(312, 400);
            Graphics g = Graphics.FromImage(bitmap);

            Pen curPen = new Pen(Brushes.Black, 1);

            g.DrawLine(curPen, 20, 240, 275, 240);      //unten
            g.DrawLine(curPen, 20, 240, 20, 30);        //links 
            g.DrawLine(curPen, 275, 240, 275, 30);      //rechts
            g.DrawLine(curPen, 20, 30, 275, 30);        //oben

            g.DrawLine(curPen, 71, 240, 71, 30);        // linie 1
            g.DrawLine(curPen, 122, 240, 122, 30);      // linie 2
            g.DrawLine(curPen, 173, 240, 173, 30);      // linie 3
            g.DrawLine(curPen, 224, 240, 224, 30);      // linie 4

            g.DrawString("0", new Font("New Timer", 8), Brushes.Black, new PointF(16, 242));

            g.DrawLine(curPen, 71, 240, 71, 242);
            g.DrawString("51", new Font("New Timer", 8), Brushes.Black, new PointF(63, 242));

            g.DrawLine(curPen, 122, 240, 122, 242);
            g.DrawString("102", new Font("New Timer", 8), Brushes.Black, new PointF(111, 242));

            g.DrawLine(curPen, 173, 240, 173, 242);
            g.DrawString("153", new Font("New Timer", 8), Brushes.Black, new PointF(162, 242));

            g.DrawLine(curPen, 224, 240, 224, 242);
            g.DrawString("204", new Font("New Timer", 8), Brushes.Black, new PointF(213, 242));

            g.DrawLine(curPen, 275, 240, 275, 242);
            g.DrawString("255", new Font("New Timer", 8), Brushes.Black, new PointF(264, 242));

            g.DrawLine(curPen, 71, 240, 71, 242);

            double tempo = 0;
            for (int i = 0; i < 256; i++)
            {
                tempo = 200.0 * countPixel[i] / maxPixel;
                g.DrawLine(curPen, 20 + i, 240, 20 + i, 240 - (int)tempo);
            }
            curPen.Dispose();
            bmp.Dispose();

            return bitmap;
        }
        #endregion

    }
}

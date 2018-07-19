using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Picture_Editor
{
    class ImageFactory
    {

        #region OpenImage
        static public Image OpenImage(string filename)
        {
            int width;
            int height;

            Image image;
            using (Stream bs = File.Open(filename, FileMode.Open))
            {
                Image img = Image.FromStream(bs);
                PropertyItem[] properties = img.PropertyItems;

                width = img.Width;
                height = img.Height;

                image = new Bitmap(width, height, img.PixelFormat);

                Bitmap bmp = new Bitmap(width, height, img.PixelFormat);

                //int i = 0;
                foreach (PropertyItem property in properties)
                {      
                        bmp.SetPropertyItem(property);
                        image.SetPropertyItem(property);   
                }
                


                using (Graphics gr = Graphics.FromImage(image))
                {
                    gr.DrawImage(img, new Rectangle(0, 0, width, height));
                }
                bs.Close();
            }

           

            Form1.Textbox18 = image.Width + "x" + image.Height + " px";
            
            Form1.Textbox19 = GetImageRatio(Convert.ToDouble(width), Convert.ToDouble(height));

            return image;
        }
        #endregion


        #region GetBitmapWithProperties
        static public Bitmap GetBitmapWithProperties(string filename)
        {
            Bitmap newBitmap;

            using (Stream bs = File.Open(filename, FileMode.Open))
            {
                Image orgImage = Image.FromStream(bs, true, true);
                newBitmap = new Bitmap(orgImage);
                MemoryStream stream = new MemoryStream();
                foreach (var property in orgImage.PropertyItems)
                    newBitmap.SetPropertyItem(property);

                bs.Dispose();
            }

            return newBitmap;
        }
        #endregion


        #region ChangePropertyDateTime
        public static Bitmap ChangePropertyDateTime(Bitmap bitmap)
        {
            Encoding _Encoding = Encoding.UTF8;

            foreach (var property in bitmap.PropertyItems)
            {
                if (property.Id == 306 || property.Id == 36867 || property.Id == 36868) //SubsecTime
                {
                    property.Value = _Encoding.GetBytes(Form1.Datetime.ToString("yyyy:MM:dd HH:mm:ss") + '\0');
                    bitmap.SetPropertyItem(property);
                }
            }

            return bitmap;
        }
        #endregion


        #region
        public static void SaveBitmap(Bitmap bitmap, string filename)
        {
            MemoryStream stream = new MemoryStream();

            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            bitmap.Save(stream, GetEncoder(ImageFormat.Jpeg), encoderParameters);

            stream.Position = 0;
            byte[] image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);

            File.WriteAllBytes(filename, image);
        }
        #endregion


        #region
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        #endregion



        static string GetImageRatio(double width, double height)
        {
            string valueRatio = null;

            double ratio = Convert.ToDouble(width) / Convert.ToDouble(height);
            ratio = Math.Round(ratio, 1);

            if (ratio == 1)
            {
                valueRatio = "1:1";
            }
            else if (ratio == 1.3)
            {
                valueRatio = "4:3";
            }
            else if (ratio == 1.5)
            {
                valueRatio = "3:2";
            }
            else if (ratio == 1.8)
            {
                valueRatio = "16:9";
            }



            return valueRatio;
        }




        static public PropertyItem[] GetProperties(string filename)
        {
            PropertyItem[] properties;

            using (Stream bs = File.Open(filename, FileMode.Open))
            {
                Image img = Image.FromStream(bs);
                properties = img.PropertyItems;
                bs.Close();
            }

            return properties;
        }


        #region ResizeImage
        static public Image ResizeImage(Image origImage, float width, float height)
        {
            int newWidth;
            int newHeight;
            Image newImage;


            int origWidth = origImage.Width;
            int origHeight = origImage.Height;

            float percentWidth = (float)width / (float)origWidth;
            float percentHeight = (float)height / (float)origHeight;
            float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
        
            newWidth = (int)(origWidth * percent);
            newHeight = (int)(origHeight * percent);

            newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(origImage, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }
        #endregion

    }
}

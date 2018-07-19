using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace Picture_Editor
{
    class GPSFactory
    {
        //public static void SetGPSTags(string filename, Image image, PropertyItem[] prop2, double lat, double lon)
        //{

        //    Image Pic1 = Image.FromFile(@"C:\temp\Pic1.jpg");

        //    //Image Pic2 = Image.FromFile(filename);
        //    Image Pic2 = image;
        //    PropertyItem[] prop = Pic1.PropertyItems;
            
        //    PropertyItem[] properties = image.PropertyItems;
        //    int numberProbertries = properties.Length;
        
        //        Int16 n = 0;


        //        for (int i = 0; i < prop2.Length; i++)
        //        {
        //            PropertyItem property = prop2[i];
        //        }

        //        foreach (PropertyItem item in prop)
        //        {
        //            if (item.Id == 0x0002) //GPSLatitude;
        //            {
        //                //byte[] value = item.Value;
        //                //item.Value = doubleCoordinateToRationalByteArray(52.502817);
        //                item.Value = doubleCoordinateToRationalByteArray(lat);

        //                PropertyItem pi = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));

        //               pi = image.GetPropertyItem(270);
        //                pi.Type = 5;
        //                pi.Value = doubleCoordinateToRationalByteArray(lat);
        //                pi.Len = 24;
        //                pi.Id = 0x0002;
        //                Pic2.SetPropertyItem(pi);
        //            }
        //            else if (item.Id == 0x0004) //GPSLongitude
        //            {
        //                ////item.Value = doubleCoordinateToRationalByteArray(13.494399);
        //                //item.Value = doubleCoordinateToRationalByteArray(lon);
        //                //properties[1] = item;
        //                //Pic2.SetPropertyItem(properties[1]);
        //            }
        //        }
            
        //    Pic2.Save(@"C:\temp\test.jpg");

        //}








        public static void SetGPSTags(string filename, double lat, double lon)
        {
            byte[] image;

            using (Stream bs = File.Open(filename, FileMode.Open))
            {
                var theImage = Image.FromStream(bs, true, true);
                Bitmap bitmap = new Bitmap(theImage);
                MemoryStream stream = new MemoryStream();
                foreach (var property in theImage.PropertyItems)
                {
                    if (property.Id == 270) //SubsecTime
                    {
                        property.Type = 5;
                        property.Value = doubleCoordinateToRationalByteArray(lat);
                        property.Len = 24;
                        //property.Id = 0x0002;
                        property.Id = 2;
                        bitmap.SetPropertyItem(property);
                    }

                    //else if (property.Id == 514) //GPSLatitude;
                    //{
                    //    property.Type = 5;
                    //    property.Value = doubleCoordinateToRationalByteArray(lon);
                    //    property.Len = 24;
                    //    //property.Id = 0x0004;
                    //    property.Id = 4;
                    //    bitmap.SetPropertyItem(property);
                    //}


                    if (property.Type == 5 && property.Len == 24)
                    {
                        MessageBox.Show(property.Id.ToString());
                    }

                //else if (property.Id == 4097 && lon < 0)  //SubsecTimeDigitized
                    //{
                    //    property.Type = 2;
                    //    char value = 'W';
                    //    property.Value = BitConverter.GetBytes(value);
                    //    property.Len = 3;
                    //    //property.Id = 0x0003;
                    //    property.Id = 3;
                    //    bitmap.SetPropertyItem(property);
                    //}

                    else
                        bitmap.SetPropertyItem(property);
   
                }

                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                bitmap.Save(stream, GetEncoder(ImageFormat.Jpeg), encoderParameters);

                stream.Position = 0;
                image = new byte[stream.Length + 1];
                stream.Read(image, 0, image.Length);
            }



            File.WriteAllBytes(filename, image);
        }



        public static void GPSTagSaved(string coordinates, string filename)
        {
            //string coordinates = coordDialog.ReturnText;
            int indexBlank = coordinates.IndexOf(" ");

            string latPoint = coordinates.Substring(0, indexBlank - 1);
            string lat = GetCoordinateComma(latPoint);               
            double latitude = Convert.ToDouble(lat);

            string lonPoint = coordinates.Substring(indexBlank);
            string lon = GetCoordinateComma(lonPoint);
            double longitude = Convert.ToDouble(lon);

            SetGPSTags(filename, latitude, longitude);

            Form1.Textbox3 = GetTrimmedCommaCoordinate(lat);
            Form1.Textbox4 = GetTrimmedCommaCoordinate(lon);
        }


        public static string GetCoordinateComma(string coordinate)
        {
            string coordinateComma = coordinate;

            coordinateComma = coordinate.Replace(".", ",");

            return coordinateComma;
        }


        public static string GetTrimmedCommaCoordinate(string coordinate)
        {
            string trimmedCoordinate = coordinate;

            int length = trimmedCoordinate.Length;
            int index = trimmedCoordinate.IndexOf(',');
            if (length - index > 5)
                trimmedCoordinate = coordinate.Substring(0, index + 5);

            return trimmedCoordinate;
        }




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












        //52.502817, 13.494399

        public static byte[] doubleCoordinateToRationalByteArray(double doubleVal)
        {
            Double temp;
            temp = Math.Abs(doubleVal);

            // degrees
            int degrees = Convert.ToInt16(Math.Truncate(temp));
            temp = (temp - degrees) * 60;
            byte[] byteDegree = BitConverter.GetBytes(degrees);
            //MessageBox.Show("0: " + byteDegree[0]);


            // minutes
            int minutes = Convert.ToInt16(Math.Truncate(temp));
            temp = (temp - minutes) * 60;
            byte[] byteMinutes = BitConverter.GetBytes(minutes);
            //MessageBox.Show("8: " + byteMinutes[0]);


            // seconds
            int seconds = Convert.ToInt16(Math.Truncate(temp));
            byte[] byteSeconds = BitConverter.GetBytes(seconds);
            //MessageBox.Show("16: " + byteSeconds[0]);

            // milliseconds
            int milliseconds = Convert.ToInt32(Math.Truncate(temp * 1000.0));
            byte[] byteMilliseconds = BitConverter.GetBytes(milliseconds);
            //MessageBox.Show("16: " + byteMilliseconds[0]);

            byte[] result = new byte[24];
            Array.Copy(BitConverter.GetBytes(degrees), 0, result, 0, 4);
            Array.Copy(BitConverter.GetBytes(1), 0, result, 4, 4);
            Array.Copy(BitConverter.GetBytes(minutes), 0, result, 8, 4);
            Array.Copy(BitConverter.GetBytes(1), 0, result, 12, 4);
            Array.Copy(BitConverter.GetBytes(milliseconds), 0, result, 16, 4);
            Array.Copy(BitConverter.GetBytes(1000), 0, result, 20, 4);

            return result;
        }


    }
}

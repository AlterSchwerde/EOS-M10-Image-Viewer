using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using ExifLibrary;

namespace Picture_Editor
{
    class Try
    {
        
        //Image img0 = Image.FromFile(@"C:\temp\Pic1.jpg");

        public static GPSLatitudeLongitude GetLatitudeDummy()
        {
            ExifFile file = ExifFile.Read(@"C:\temp\Pic1.jpg");
            GPSLatitudeLongitude location = file.Properties[ExifTag.GPSLatitude] as GPSLatitudeLongitude;
            location.Degrees.Set(22, 0);

            return location;
        }


        public static void SetGPSTags(string filename)
        {

            Image Pic1 = Image.FromFile(@"C:\temp\Pic1.jpg");
            Image Pic2 = Image.FromFile(filename);
            PropertyItem[] prop = Pic1.PropertyItems;
            PropertyItem[] prop2 = Pic2.PropertyItems;

            int numberLat = -1;
            int numberLong = -1;


            Int16 n = 0;
            //Int16 k = prop2.Length;

            for (int i = 0; i < prop2.Length; i++)
            {
                
                
                PropertyItem property = prop2[i];
                
                if (property.Id == 0x010e)
                {
                    numberLat = i;
                }
                else if (property.Id == 0x9286) //UserComment
                {
                    numberLong = i;
                }
            }


            //if (numberLat == -1 || numberLong == -1)
            //    return;

            foreach (var item in prop)
            {
                if (item.Id == 0x0002)  //GPSLatitude;
                {                 
                    //byte[] value = item.Value;
                    item.Value = doubleCoordinateToRationalByteArray(52.502817);
                    prop2[0] = item;
                    Pic2.SetPropertyItem(prop2[0]);
                }
                else if (item.Id == 0x0004) //GPSLongitude
                {
                    item.Value = doubleCoordinateToRationalByteArray(13.494399);
                    prop2[1] = item;
                    Pic2.SetPropertyItem(prop2[1]);                
                }
            }

            Pic2.Save(filename);

            byte[] lat = doubleCoordinateToRationalByteArray(50.8512333333333);


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
            int milliseconds = Convert.ToInt32(Math.Truncate(temp* 1000.0));
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

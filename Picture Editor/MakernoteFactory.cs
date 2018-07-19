using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Windows.Forms;
using AForge.Math;
using ExifLib;
using ExifLibrary;

namespace Picture_Editor
{
    class MakernoteFactory
    {
        //1 = BYTE An 8-bit unsigned integer.,
        //2 = ASCII An 8-bit byte containing one 7-bit ASCII code. The final byte is terminated with NULL.,
        //3 = SHORT A 16-bit (2-byte) unsigned integer,
        //4 = LONG A 32-bit (4-byte) unsigned integer,
        //5 = RATIONAL Two LONGs. The first LONG is the numerator and the second LONG expresses the
        //denominator.,
        //7 = UNDEFINED An 8-bit byte that can take any value depending on the field definition,
        //9 = SLONG A 32-bit (4-byte) signed integer (2's complement notation),
        //10 = SRATIONAL Two SLONGs. The first SLONG is the numerator and the second SLONG is the


        #region GetMakernotes
        public static List<Makernote> GetMakernotes(Byte[] allBytes)
        {
            List<Makernote> listMakernotes = new List<Makernote>();

            byte[] makernotes = allBytes;
            Makernote makernote = new Makernote();

            //bool bigEndian = false;
            int lengthByte = 2;
            int numberBadBytes = 2;
            int endMakernotes = 542;
            int beginningDataBytes = 992;
            string message = "";


            //for (int i = 2; i < 35 * 12; i++)
            for (int i = 2; i < 47 * 12; i++)
            {
                makernote = new Makernote();

                byte[] bytesMakernote = new byte[12];
                for (int n = 0; n < 12; n++)
                    bytesMakernote[n] = makernotes[i + n];

                // ID
                byte[] bytesID = { bytesMakernote[0], bytesMakernote[1] };
                string id = ConverterFactory.ByteToHex(bytesID, false);
                makernote.Id = id;

                // Type
                byte[] bytesType = { bytesMakernote[2], bytesMakernote[3] };
                Int16 type = ConverterFactory.ByteToInt16(bytesType, true);
                makernote.Type = type;

                // isHex / Name
                bool isHex = false;
                bool bigEndian = false;
                string name = GetNameMakernote(id, out isHex, out bigEndian);


                // Length
                byte[] bytesLength = { bytesMakernote[4], bytesMakernote[5], bytesMakernote[6], bytesMakernote[7] };
                Int16 length = ConverterFactory.ByteToInt16(bytesLength, true);
                makernote.Length = length;

                // Offest
                byte[] bytesOffset = { bytesMakernote[8], bytesMakernote[9], bytesMakernote[10], bytesMakernote[11] };
                Int16 offset = ConverterFactory.ByteToInt16(bytesOffset, true);
                makernote.Offset = offset;


                if (id == "0027")
                {
                    int l = 0;
                }




                Dictionary<int, string> dictValues = new Dictionary<int, string>();

                if (offset == 0)
                {
                    dictValues = ConverterFactory.GetValues(allBytes, bytesMakernote, bigEndian, type, isHex, length, offset);
                }
                else
                    dictValues = ConverterFactory.GetValues(allBytes, bytesMakernote, bigEndian, type, isHex, length, offset);

                makernote.Values = dictValues;

                listMakernotes.Add(makernote);
                i = i + 11;
            }

            File.WriteAllText(@"c:\temp\tags.txt", message);

            return listMakernotes;
        }
        #endregion


        #region GetValuesString
        public static Dictionary<int, string> GetValuesString(byte[] bytes, bool bigEndian)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            int length = bytes.Length;
            int index = 0;
            for (int i = 0; i < length; i++)
            {
                byte[] intByte = { bytes[i] };
                string value = Encoding.ASCII.GetString(bytes);
                dict.Add(index, value);
                index++;
            }


            return dict;
        }
        #endregion


        #region GetValuesHex
        public static Dictionary<int, string> GetValuesHex(byte[] bytes, bool bigEndian)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            int length = bytes.Length;
            int index = 0;
            for (int i = 0; i < length; i = i + 2)
            {
                byte[] intByte = { bytes[i], bytes[i + 1] };
                string value = ConverterFactory.ByteToHex(intByte, bigEndian);
                dict.Add(index, value);
                index++;
            }


            return dict;
        }
        #endregion


        #region GetValuesInt8
        public static Dictionary<int, string> GetValuesInt8(byte[] bytes)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            int length = bytes.Length;
            int index = 0;
            for (int i = 0; i < length; i++)
            {
                byte[] intByte = { bytes[i] };
                int value = ConverterFactory.ByteToInt8(intByte);
                dict.Add(index, value.ToString());
                index++;
            }


            return dict;
        }
        #endregion


        #region GetValuesInt16
        public static Dictionary<int, string> GetValuesInt16(byte[] bytes, bool bigEndian)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            int length = bytes.Length;
            int index = 0;
            for (int i = 0; i < length; i = i + 2)
            {
                byte[] intByte = { bytes[i], bytes[i + 1] };
                int value = ConverterFactory.ByteToInt16(intByte, bigEndian);
                dict.Add(index, value.ToString());
                index++;
            }


            return dict;
        }
        #endregion


        #region GetValuesInt32
        public static Dictionary<int, string> GetValuesInt32(byte[] bytes, bool bigEndian)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();

            int length = bytes.Length;
            int index = 0;
            for (int i = 0; i < length; i = i + 4)
            {
                byte[] intByte = { bytes[i], bytes[i + 1], bytes[i + 2], bytes[i + 3] };
                string value = ConverterFactory.ByteToInt64(intByte, bigEndian).ToString();
                dict.Add(index, value);
                index++;
            }


            return dict;
        }
        #endregion


        #region GetNameMakernote
        public static string GetNameMakernote(string id, out bool isHex, out bool bigEndian)
        {
            string name = "<unbekannt>";
            isHex = true;
            bigEndian = true;


            switch (id)
            {
                case "0001":
                    name = "Canon CameraSettings Tags";
                    isHex = true;
                    bigEndian = true;
                    break;
                case "0003":
                    name = "CanonFlashInfo";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "0004":
                    name = "Canon ShotInfo Tags";
                    isHex = true;
                    bigEndian = true;
                    break;
                case "0006":
                    name = "CanonImageType";
                    isHex = true;
                    bigEndian = true;
                    break;
                case "0007":
                    name = "CanonFirmwareVersion";
                    isHex = true;
                    bigEndian = true;
                    break;
                case "0008":
                    name = "FileNumber";
                    isHex = true;
                    bigEndian = true;
                    break;
                case "0010":
                    name = "CanonModelID";
                    isHex = true;
                    bigEndian = true;
                    break;
                case "0013":
                    name = "ThumbnailImageValidArea";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "001C":
                    name = "DateStampMode";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "001E":
                    name = "FirmwareRevision";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "0023":
                    name = "Categories";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "0027":
                    name = "ContrastInfo";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "0028":
                    name = "ImageUniqueID";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "0035":
                    name = "TimeInfo";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "0038":
                    name = "BatteryType";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "003C":
                    name = "AFInfo3";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "0093":
                    name = "CanonFileInfo";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "0096":
                    name = "InternalSerialNumber";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "0099":
                    name = "CustomFunctions2";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "009A":
                    name = "AspectInfo";
                    isHex = false;
                    bigEndian = true;
                    break;
                case "00A0":
                    isHex = true;
                    bigEndian = true;
                    name = "ProcessingInfo";
                    break;
                case "00AA":
                    isHex = false;
                    bigEndian = true;
                    name = "MeasuredColor";
                    break;
                case "00D0":
                    isHex = false;
                    bigEndian = true;
                    name = "VRDOffset";
                    break;
                case "4008":
                    isHex = false;
                    bigEndian = true;
                    name = "PictureStyleUserDef";
                    break;
                case "4009":
                    isHex = false;
                    bigEndian = true;
                    name = "PictureStylePC";
                    break;
                case "4010":
                    isHex = false;
                    bigEndian = true;
                    name = "CustomPictureStyleFileName";
                    break;
                case "4016":
                    isHex = false;
                    bigEndian = true;
                    name = "VignettingCorr2";
                    break;
                case "4018":
                    isHex = false;
                    bigEndian = true;
                    name = "LightingOpt";
                    break;
                case "4020":
                    isHex = false;
                    bigEndian = true;
                    name = "AmbienceInfo";
                    break;
                case "4024":
                    isHex = false;
                    bigEndian = true;
                    name = "FilterInfo";
                    break;



                case "0022":
                    name = "Canon ShotInfo Tags";
                    isHex = false;
                    break;

            }




            return name;
        }
        #endregion

    }
}

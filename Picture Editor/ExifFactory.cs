using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using ExifLib;

namespace Picture_Editor
{
    class ExifFactory
    {

        #region GetValue
        public static string GetValue(ExifTags tag, ExifReader reader, string type)
        {
            string value = null;

            if (type == "Byte")
                value = GetTagValueByte(tag, reader).ToString();
            else if (type == "UInt16")
                value = GetTagValueInt16(tag, reader).ToString();
            else if (type == "UInt32")
                value = GetTagValueInt32(tag, reader).ToString();
            else if (type == "Double")
                value = GetTagValueDouble(tag, reader).ToString();
            else if (type == "String")
                value = GetTagValueString(tag, reader).ToString();
            else if (type == "Byte[]")
            {
                Byte[] valByteArray = GetTagValueByteArray(tag, reader);
                if (valByteArray.Length >= 4)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i == 4)
                        {
                            if (valByteArray.Length > 4)
                                value = value + ", ...";
                        }
                        else
                        {
                            if (value == null)
                                value = valByteArray[i].ToString();
                            else
                                value = value + ", " + valByteArray[i];
                        }
                    }
                }
            }
            else if (type == "Double[]")
            {
                Double[] valDoubleArray = GetTagValueDoubleArray(tag, reader);
                if (valDoubleArray.Length >= 4)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i == 4)
                        {
                            if (valDoubleArray.Length > 4)
                                value = value + ", ...";
                        }
                        else
                        {
                            if (value == null)
                                value = valDoubleArray[i].ToString();
                            else
                                value = value + ", " + valDoubleArray[i];
                        }
                    }
                }
            }

            return value;
        }
        #endregion


        #region GetTagValueByte
        public static byte? GetTagValueByte(ExifTags tag, ExifReader reader)
        {
            byte? valueByte = null;
            byte value = 0;
            bool success = true;

            try
            {
                reader.GetTagValue<byte>(tag, out value);
            }
            catch
            {
                success = false;
            }

            if (success)
                valueByte = (byte?)value;


            return valueByte;
        }
        #endregion


        #region GetTagValueInt16
        public static int? GetTagValueInt16(ExifTags tag, ExifReader reader)
        {
            int? valueInt = null;
            UInt16 value = 0;
            bool success = true;

            try
            {
                reader.GetTagValue<UInt16>(tag, out value);
            }
            catch
            {
                success = false;
            }

            if (success)
                valueInt = Convert.ToInt16(value);

            return valueInt;
        }
        #endregion


        #region GetTagValueInt32
        public static int? GetTagValueInt32(ExifTags tag, ExifReader reader)
        {
            int? valueInt = null;
            UInt32 value = 0;
            bool success = true;

            try
            {
                reader.GetTagValue<UInt32>(tag, out value);
            }
            catch
            {
                success = false;
            }

            if (success)
                valueInt = Convert.ToInt16(value);

            return valueInt;
        }
        #endregion


        #region GetTagValueDouble
        public static double? GetTagValueDouble(ExifTags tag, ExifReader reader)
        {
            double? valueDouble = null;
            double value = 0;
            bool success = true;

            try
            {
                reader.GetTagValue<double>(tag, out value);
            }
            catch
            { }

            if (success)
                valueDouble = Convert.ToDouble(value);

            return valueDouble;
        }
        #endregion


        #region GetTagValueString
        public static string GetTagValueString(ExifTags tag, ExifReader reader)
        {
            string value = null;

            try
            {
                reader.GetTagValue<string>(tag, out value);
            }
            catch
            { }

            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                value = "<empty>";
            return value;
        }
        #endregion


        #region GetTagValueByteArray
        public static byte[] GetTagValueByteArray(ExifTags tag, ExifReader reader)
        {
            byte[] value = null;
            try
            {
                reader.GetTagValue<byte[]>(tag, out value);
            }
            catch
            { }

            return value;
        }
        #endregion


        #region GetTagValueDoubleArray
        public static double[] GetTagValueDoubleArray(ExifTags tag, ExifReader reader)
        {
            double[] valueDouble = null;

            try
            {
                reader.GetTagValue<double[]>(tag, out valueDouble);
            }
            catch
            { }

            return valueDouble;
        }
        #endregion




        #region Exif-Tags
        public static ExifTags GetTag(string id, out string name, out string type)
        {
            //string name;
            ExifTags tag = new ExifTags();

            name = null;
            type = null;

            switch (id)
            {
                case "10e":
                    tag = ExifTags.ImageDescription;
                    name = "ImageDescription";
                    type = "String";
                    break;

                case "10f":
                    tag = ExifTags.Make;
                    name = "Make";
                    type = "String";
                    break;

                case "110":
                    tag = ExifTags.Model;
                    name = "Model";
                    type = "String";
                    break;

                case "112":
                    tag = ExifTags.Orientation;
                    name = "Orientation";
                    type = "UInt16";
                    break;

                case "11a":
                    tag = ExifTags.XResolution;
                    name = "XResolution";
                    type = "Double";
                    break;

                case "11b":
                    tag = ExifTags.YResolution;
                    name = "YResolution";
                    type = "Double";
                    break;

                case "128":
                    tag = ExifTags.ResolutionUnit;
                    name = "ResolutionUnit";
                    type = "UInt16";
                    break;

                case "132":
                    tag = ExifTags.DateTime;
                    name = "ModifyDate";
                    type = "String";
                    break;

                case "13b":
                    tag = ExifTags.Artist;
                    name = "Artist";
                    type = "String";
                    break;

                case "213":
                    tag = ExifTags.YCbCrPositioning;
                    name = "YCbCrPositioning";
                    type = "UInt16";
                    break;

                case "8298":
                    tag = ExifTags.Copyright;
                    name = "Copyright";
                    type = "String";
                    break;

                case "829a":
                    tag = ExifTags.ExposureTime;
                    name = "ExposureTime";
                    type = "Double";
                    break;

                case "829d":
                    tag = ExifTags.FNumber;
                    name = "FNumber";
                    type = "Double";
                    break;

                case "8822":
                    tag = ExifTags.ExposureProgram;
                    name = "ExposureProgram";
                    type = "UInt16";
                    break;

                case "8827":
                    tag = ExifTags.ISOSpeedRatings;
                    name = "ISO";
                    type = "UInt16";
                    break;

                case "8830":
                    tag = ExifTags.SensitivityType;
                    name = "SensitivityType";
                    type = "UInt16";
                    break;

                case "8832":
                    tag = ExifTags.RecommendedExposureIndex;
                    name = "RecommendedExposureIndex";
                    type = "UInt32";
                    break;

                case "9000":
                    tag = ExifTags.ExifVersion;
                    name = "ExifVersion";
                    type = "Byte[]";
                    break;

                case "9003":
                    tag = ExifTags.DateTimeOriginal;
                    name = "DateTimeOriginal";
                    type = "String";
                    break;

                case "9004":
                    tag = ExifTags.DateTimeDigitized;
                    name = "CreateDate";
                    type = "String";
                    break;

                case "9101":
                    tag = ExifTags.ComponentsConfiguration;
                    name = "ComponentsConfiguration";
                    type = "Byte[]";
                    break;

                case "9102":
                    tag = ExifTags.CompressedBitsPerPixel;
                    name = "CompressedBitsPerPixel";
                    type = "Double";
                    break;

                case "9201":
                    tag = ExifTags.ShutterSpeedValue;
                    name = "ShutterSpeedValue";
                    type = "Double";
                    break;

                case "9202":
                    tag = ExifTags.ApertureValue;
                    name = "ApertureValue";
                    type = "Double";
                    break;

                case "9204":
                    tag = ExifTags.ExposureBiasValue;
                    name = "ExposureCompensation";
                    type = "Double";
                    break;

                case "9207":
                    tag = ExifTags.MeteringMode;
                    name = "MeteringMode";
                    type = "UInt16";
                    break;

                case "9209":
                    tag = ExifTags.Flash;
                    name = "Flash";
                    type = "UInt16";
                    break;

                case "920a": ;
                    tag = ExifTags.FocalLength;
                    name = "FocalLength";
                    type = "Double";
                    break;

                case "927c":
                    tag = ExifTags.MakerNote;
                    name = "MakerNote";
                    type = "Byte[]";
                    break;

                case "9286":
                    tag = ExifTags.UserComment;
                    name = "UserComment";
                    type = "Byte[]";
                    break;

                case "9290":
                    tag = ExifTags.SubsecTime;
                    name = "SubSecTime";
                    type = "String";
                    break;

                case "9291":
                    tag = ExifTags.SubsecTimeOriginal;
                    name = "SubSecTimeOriginal";
                    type = "String";
                    break;

                case "9292":
                    tag = ExifTags.SubsecTimeDigitized;
                    name = "SubSecTimeDigitized";
                    type = "String";
                    break;

                case "a000":
                    tag = ExifTags.FlashpixVersion;
                    name = "FlashpixVersion";
                    type = "Byte[]";
                    break;

                case "a001":
                    tag = ExifTags.ColorSpace;
                    name = "ColorSpace";
                    type = "UInt16";
                    break;

                case "a002":
                    tag = ExifTags.PixelXDimension;
                    name = "ExifImageWidth";
                    type = "UInt16";
                    break;

                case "a003":
                    tag = ExifTags.PixelYDimension;
                    name = "ExifImageHeight";
                    type = "UInt16";
                    break;

                case "a20e":
                    tag = ExifTags.FocalPlaneXResolution;
                    name = "FocalPlaneXResolution";
                    type = "Double";
                    break;

                case "a20f":
                    tag = ExifTags.FocalPlaneYResolution;
                    name = "FocalPlaneYResolution";
                    type = "Double";
                    break;

                case "a210": ;
                    tag = ExifTags.FocalPlaneResolutionUnit;
                    name = "FocalPlaneResolutionUnit";
                    type = "UInt16";
                    break;

                case "a217":
                    tag = ExifTags.SensingMethod;
                    name = "SensingMethod";
                    type = "UInt16";
                    break;

                case "a300":
                    tag = ExifTags.FileSource;
                    name = "FileSource";
                    type = "Byte";
                    break;

                case "a401":
                    tag = ExifTags.CustomRendered;
                    name = "CustomRendered";
                    type = "UInt16";
                    break;

                case "a402":
                    tag = ExifTags.ExposureMode;
                    name = "ExposureMode";
                    type = "UInt16";
                    break;

                case "a403":
                    tag = ExifTags.WhiteBalance;
                    name = "WhiteBalance";
                    type = "UInt16";
                    break;

                case "a404":
                    tag = ExifTags.DigitalZoomRatio;
                    name = "DigitalZoomRatio";
                    type = "Double";
                    break;

                case "a406":
                    tag = ExifTags.SceneCaptureType;
                    name = "SceneCaptureType";
                    type = "UInt16";
                    break;

                case "a430":
                    tag = ExifTags.CameraOwnerName;
                    name = "OwnerName";
                    type = "String";
                    break;

                case "a431":
                    tag = ExifTags.BodySerialNumber;
                    name = "SerialNumber";
                    type = "String";
                    break;

                case "a432":
                    tag = ExifTags.LensSpecification;
                    name = "LensInfo";
                    type = "Double[]";
                    break;

                case "a434":
                    tag = ExifTags.LensModel;
                    name = "LensModel";
                    type = "String";
                    break;

                case "a435":
                    tag = ExifTags.LensSerialNumber;
                    name = "LensSerialNumber";
                    type = "String";
                    break;

                case "201":
                    tag = ExifTags.JPEGInterchangeFormat;
                    name = "JPEGInterchangeFormat";
                    type = "UInt16";
                    break;

                case "202":
                    tag = ExifTags.JPEGInterchangeFormatLength;
                    name = "JPEGInterchangeFormatLength";
                    type = "UInt16";
                    break;

                case "":
                    tag = ExifTags.GPSLatitudeRef;
                    name = "GPSLatitude";
                    type = "???";
                    break;

                case "2":
                    tag = ExifTags.GPSLatitude;
                    name = "GPSLatitude";
                    type = "???";
                    break;

                case "3":
                    tag = ExifTags.GPSLongitudeRef;
                    name = "GPSLongitudeRef";
                    type = "???";
                    break;

                case "4":
                    tag = ExifTags.GPSLongitude;
                    name = "GPSLongitude";
                    type = "???";
                    break;

                //case "5041":
                //    tag = ExifTags.ExifInteroperabilityVersion;
                //    break;

                //case "5042":
                //    tag = ExifTags.ExifInteroperabilityVersion;
                //    break;

                //case "1001":
                //    tag = ExifTags.RelatedImageWidth;
                //    break;

                //case "1002":
                //    name = "RelatedImageHeight";
                //    tag = ExifTags.MakerNote;
                //    break;
                //case "0":
                //    name = "RelatedImageHeight";
                //    tag = ExifTags.MakerNote;
                //    break;
                //case "501b":
                //    name = "ThumbnailData";
                //    tag = ExifTags.ThumbnailData;
                //    break;
                //case "5023":
                //    name = "ThumbnailCompression";
                //    tag = ExifTags.MakerNote;
                //    break;
                //case "502d":
                //    name = "ThumbnailXResolution";
                //    tag = ExifTags.MakerNote;
                //    break;
                //case "502e":
                //    name = "ThumbnailYResolution";
                //    tag = ExifTags.MakerNote;
                //    break;
                //case "5030":
                //    name = "ThumbnailTransferFunction";
                //    tag = ExifTags.MakerNote;
                //    break;
                //case "5090":
                //    name = "???";
                //    break;
                //case "5091":
                //    name = "???";
                //    break;
            }

            return tag;
        }
        #endregion

    }
}

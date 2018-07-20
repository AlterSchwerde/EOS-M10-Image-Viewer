using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ExifLib;

namespace Picture_Editor
{
    class CameraSettingsFactory
    {

        public static void PrintCameraSettings(Makernote camerasettings, string filename, string outputfile)
        {
            string tagName = null;
            string value = null;
            string seperator = "----------------------------------------------------------------------";
            int index;

            string output = "Camera Settings für Bild: " + Path.GetFileName(outputfile) + Environment.NewLine + Environment.NewLine;

            for (int i = 0; i < camerasettings.Length - 1; i++)
            {
                index = i + 1;
                tagName = CameraSettingsFactory.GetCameraSettingName(index);
                string firstPart =  "Index: " + index.ToString() + Environment.NewLine +
                                    "Tagname: " + tagName + Environment.NewLine +
                                    "Wert: ";
                value = camerasettings.Values[index].ToString();
                value = ConverterFactory.HexstringToInt32(value).ToString();

                output = output + seperator + Environment.NewLine + firstPart + value + Environment.NewLine + Environment.NewLine;
            }

            File.WriteAllText(outputfile, output);

        }

        //https://exifinfo.org/detail/2F3KGfsca3o0xVXnkXHigQ

        #region GetNameMakernote
        public static string GetCameraSettingName(int index)
        {
            string name = "<unbekannt>";

            switch (index)
            {
                case 1:
                    name = "MacroMode";
                    break;
                case 2:
                    name = "SelfTimer";
                    break;
                case 3:
                    name = "Quality";
                    break;
                case 4:
                    name = "CanonFlashMode";
                    break;
                case 5:
                    name = "ContinuousDrive";
                    break;
                case 6:
                    name = "unknown";
                    break;
                case 7:
                    name = "MacroMode";
                    break;
                case 8:
                    name = "unknown";
                    break;
                case 9:
                    name = "RecordMode";
                    break;
                case 10:
                    name = "CanonImageSize";
                    break;
                case 11:
                    name = "EasyMode";
                    break;
                case 12:
                    name = "DigitalZoom";
                    break;
                case 13:
                    name = "Contrast";
                    break;
                case 14:
                    name = "Saturation";
                    break;
                case 15:
                    name = "Sharpness";
                    break;
                case 16:
                    name = "CameraISO";
                    break;
                case 17:
                    name = "MeteringMode";
                    break;
                case 18:
                    name = "FocusRange";
                    break;
                case 19:
                    name = "AFPoint";
                    break;
                case 20:
                    name = "CanonExposureMode";
                    break;
                case 21:
                    name = "unknown";
                    break;
                case 22:
                    name = "LensType";
                    break;
                case 23:
                    name = "MaxFocalLength";
                    break;
                case 24:
                    name = "MinFocalLength";
                    break;
                case 25:
                    name = "FocalUnits";
                    break;
                case 26:
                    name = "MaxAperture";
                    break;
                case 27:
                    name = "MinAperture";
                    break;
                case 28:
                    name = "FlashActivity";
                    break;
                case 29:
                    name = "FlashBits";
                    break;
                case 30:
                    name = "unknown";
                    break;
                case 31:
                    name = "unknown";
                    break;
                case 32:
                    name = "FocusContinuous";
                    break;
                case 33:
                    name = "AESetting";
                    break;
                case 34:
                    name = "ImageStabilization";
                    break;
                case 35:
                    name = "DisplayAperture";
                    break;
                case 36:
                    name = "ZoomSourceWidth";
                    break;
                case 37:
                    name = "ZoomTargetWidth";
                    break;
                case 38:
                    name = "unknown";
                    break;
                case 39:
                    name = "SpotMeteringMode";
                    break;
                case 40:
                    name = "PhotoEffect";
                    break;
                case 41:
                    name = "ManualFlashOutput";
                    break;
                case 42:
                    name = "ColorTone";
                    break;
                case 43:
                    name = "unknown";
                    break;
                case 44:
                    name = "unknown";
                    break;
                case 45:
                    name = "unknown";
                    break;
                case 46:
                    name = "SRAWQuality";
                    break;
            }

            return name;
        }
        #endregion

        public static string GetValueMacroMode(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "1":
                    correspondingValue = "Macro";
                    break;
                case "2":
                    correspondingValue = "Normal";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueQuality(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "1":
                    correspondingValue = "Economy";
                    break;
                case "2":
                    correspondingValue = "Normal";
                    break;
                case "3":
                    correspondingValue = "Fine";
                    break;
                case "4":
                    correspondingValue = "RAW";
                    break;
                case "5":
                    correspondingValue = "Superfine";
                    break;
                case "7":
                    correspondingValue = "CRAW";
                    break;
                case "130":
                    correspondingValue = "Normal Movie";
                    break;
                case "131":
                    correspondingValue = "Movie";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueFlashMode(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "0":
                    correspondingValue = "Off";
                    break;
                case "1":
                    correspondingValue = "Auto";
                    break;
                case "2":
                    correspondingValue = "On";
                    break;
                case "3":
                    correspondingValue = "Red-eye reduction";
                    break;
                case "4":
                    correspondingValue = "Slow-sync";
                    break;
                case "5":
                    correspondingValue = "Red-eye reduction (Auto)";
                    break;
                case "6":
                    correspondingValue = "Red-eye reduction (On)";
                    break;
                case "16":
                    correspondingValue = "External flash";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueContinuousDrive(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "0":
                    correspondingValue = "Single";
                    break;
                case "1":
                    correspondingValue = "Continuous";
                    break;
                case "2":
                    correspondingValue = "Movie";
                    break;
                case "3":
                    correspondingValue = "Continuous, Speed Priority";
                    break;
                case "4":
                    correspondingValue = "Continuous, Low";
                    break;
                case "5":
                    correspondingValue = "Continuous, High";
                    break;
                case "6":
                    correspondingValue = "Silent Single";
                    break;
                case "9":
                    correspondingValue = "Single, Silent";
                    break;
                case "10":
                    correspondingValue = "Continuous, Silent";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueFocusMode(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "0":
                    correspondingValue = "One-shot AF";
                    break;
                case "1":
                    correspondingValue = "AI Servo AF";
                    break;
                case "2":
                    correspondingValue = "AI Focus AF";
                    break;
                case "3":
                    correspondingValue = "Manual Focus (3)";
                    break;
                case "4":
                    correspondingValue = "Single";
                    break;
                case "5":
                    correspondingValue = "Continuous";
                    break;
                case "6":
                    correspondingValue = "Manual Focus (6)";
                    break;
                case "16":
                    correspondingValue = "Pan Focus";
                    break;
                case "256":
                    correspondingValue = "AF + MF";
                    break;
                case "512":
                    correspondingValue = "Movie Snap Focus";
                    break;
                case "519":
                    correspondingValue = "Movie Servo AF";
                    break;
            }
            return correspondingValue;
        }
        
        public static string GetValueRecordMode(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "1":
                    correspondingValue = "JPEG";
                    break;
                case "2":
                    correspondingValue = "CRW+THM";
                    break;
                case "3":
                    correspondingValue = "AVI+THM";
                    break;
                case "4":
                    correspondingValue = "TIF";
                    break;
                case "5":
                    correspondingValue = "TIF+JPEG";
                    break;
                case "6":
                    correspondingValue = "CR2";
                    break;
                case "7":
                    correspondingValue = "CR2+JPEG";
                    break;
                case "9":
                    correspondingValue = "MOV";
                    break;
                case "10":
                    correspondingValue = "MP4";
                    break;
                case "11":
                    correspondingValue = "CRM";
                    break;
                case "13":
                    correspondingValue = "CR3";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueImageSize(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "0":
                    correspondingValue = "Large";
                    break;
                case "1":
                    correspondingValue = "Medium";
                    break;
                case "2":
                    correspondingValue = "Small";
                    break;
                case "5":
                    correspondingValue = "Medium 1";
                    break;
                case "6":
                    correspondingValue = "Medium 2";
                    break;
                case "7":
                    correspondingValue = "Medium 3";
                    break;
                case "8":
                    correspondingValue = "Postcard";
                    break;
                case "9":
                    correspondingValue = "Widescreen";
                    break;
                case "10":
                    correspondingValue = "Medium Widescreen";
                    break;
                case "14":
                    correspondingValue = "Small 1";
                    break;
                case "15":
                    correspondingValue = "Small 2";
                    break;
                case "16":
                    correspondingValue = "Small 3";
                    break;
                case "128":
                    correspondingValue = "640x480 Movie";
                    break;
                case "129":
                    correspondingValue = "Medium Movie";
                    break;
                case "130":
                    correspondingValue = "Small Movie";
                    break;
                case "137":
                    correspondingValue = "1280x720 Movie";
                    break;
                case "142":
                    correspondingValue = "1920x1080 Movie";
                    break;
                case "143":
                    correspondingValue = "4096x2160 Movie";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueEasyMode(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "2":
                    correspondingValue = "Landscape";
                    break;
                case "8":
                    correspondingValue = "Portrait";
                    break;
                case "9":
                    correspondingValue = "Sports";
                    break;
                case "10":
                    correspondingValue = "Macro";
                    break;
                case "47":
                    correspondingValue = "Fisheye Effect";
                    break;
                case "48":
                    correspondingValue = "Miniature Effect";
                    break;
                case "51":
                    correspondingValue = "High Dynamic Range";
                    break;
                case "52":
                    correspondingValue = "Handheld Night Scene";
                    break;
                case "58":
                    correspondingValue = "Toy Camera Effect";
                    break;
                case "68":
                    correspondingValue = "Food";
                    break;
                case "62":
                    correspondingValue = "Soft Focus";
                    break;
                case "78":
                    correspondingValue = "Keativassistent";
                    break;
                case "79":
                    correspondingValue = "Selbstporträt";
                    break;
                case "80":
                    correspondingValue = "Körnigkeit S/ W";
                    break;
                case "81":
                    correspondingValue = "Ölgemälde-Effekt";
                    break;
                case "82":
                    correspondingValue = "Aquarell-Effekt";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueDigitalZoom(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "0":
                    correspondingValue = "None";
                    break;
                case "1":
                    correspondingValue = "2x";
                    break;
                case "2":
                    correspondingValue = "4x";
                    break;
                case "3":
                    correspondingValue = "Other";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueContrast(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "0":
                    correspondingValue = "Normal";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueSaturation(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "0":
                    correspondingValue = "Normal";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueCameraISO(string value)
        {
            string correspondingValue = "Auto";

            switch (value)
            {
                case "0":
                    correspondingValue = "No Auto";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueMeteringMode(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "0":
                    correspondingValue = "Default";
                    break;
                case "1":
                    correspondingValue = "Spot";
                    break;
                case "2":
                    correspondingValue = "Average";
                    break;
                case "3":
                    correspondingValue = "Evaluative";
                    break;
                case "4":
                    correspondingValue = "Partial";
                    break;
                case "5":
                    correspondingValue = "Center-weighted average";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueFocusRange(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "0":
                    correspondingValue = "Manual";
                    break;
                case "1":
                    correspondingValue = "Auto";
                    break;
                case "2":
                    correspondingValue = "Not Known";
                    break;
                case "3":
                    correspondingValue = "Macro";
                    break;
                case "4":
                    correspondingValue = "Very Close";
                    break;
                case "5":
                    correspondingValue = "Close";
                    break;
                case "6":
                    correspondingValue = "Middle Range";
                    break;
                case "7":
                    correspondingValue = "Far Range";
                    break;
                case "8":
                    correspondingValue = "Pan Focus";
                    break;
                case "9":
                    correspondingValue = "Super Macro";
                    break;
                case "10":
                    correspondingValue = "Infinity";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueManualAFPointSelection(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "2005":
                    correspondingValue = "Manual AF point selection";
                    break;
                case "3000":
                    correspondingValue = "None (MF)";
                    break;
                case "3001":
                    correspondingValue = "Auto AF point selection";
                    break;
                case "3002":
                    correspondingValue = "Right";
                    break;
                case "3003":
                    correspondingValue = "Center";
                    break;
                case "3004":
                    correspondingValue = "Left";
                    break;
                case "4001":
                    correspondingValue = "Auto AF point selection";
                    break;
                case "4006":
                    correspondingValue = "Face Detect";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueCanonExposureMode(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "0":
                    correspondingValue = "Easy";
                    break;
                case "1":
                    correspondingValue = "Program AE";
                    break;
                case "2":
                    correspondingValue = "Shutter speed priority AE";
                    break;
                case "3":
                    correspondingValue = "Aperture-priority AE";
                    break;
                case "4":
                    correspondingValue = "Manual";
                    break;
                case "5":
                    correspondingValue = "Depth-of-field AE";
                    break;
                case "6":
                    correspondingValue = "M-Dep";
                    break;
                case "7":
                    correspondingValue = "Bulb";
                    break;
            }
            return correspondingValue;
        }

        public static string GetValueLensType(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "4153":
                    correspondingValue = "Canon EF-M 15-45mm f/3.5-6.3 IS STM";
                    break;
               
            }
            return correspondingValue;
        }

        public static string GetValueFocusContinuous(string value)
        {
            string correspondingValue = "n/a";

            switch (value)
            {
                case "0":
                    correspondingValue = "Single";
                    break;
                case "1":
                    correspondingValue = "Continuous";
                    break;
                case "8":
                    correspondingValue = "Manual";
                    break;

            }
            return correspondingValue;
        }

        

    }
}

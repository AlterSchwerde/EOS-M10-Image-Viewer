using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using ExifLib;

namespace Picture_Editor
{
    class FormFactory
    {

        #region FillForm
        public static void FillForm(string filenameImage, out UInt16 orientation)
        {
            orientation = 1;
            Byte[] bytesMakernote = null;

            try
            {
                using (ExifReader reader = new ExifReader(filenameImage))
                {
                    //Dateiname
                    string filename;
                    filename = Path.GetFileNameWithoutExtension(filenameImage);
                    Form1.Textbox0 = filename;


                    //Datum & Uhrzeit
                    DateTime dateTime;
                    if (reader.GetTagValue<DateTime>(ExifTags.DateTime, out dateTime))
                    {
                        Form1.Datetime = dateTime;
                        
                        string date = dateTime.ToShortDateString();
                        string time = dateTime.ToShortTimeString();
                        Form1.Textbox1 = date;
                        Form1.Textbox2 = time;
                    }
                    else
                    {
                        Form1.Textbox1 = "";
                        Form1.Textbox2 = "";
                    }


                    //Breitengrad
                    Double[] latitude;
                    if (reader.GetTagValue<Double[]>(ExifTags.GPSLatitude, out latitude))
                    {
                        string adjustedlatitude = ToolsFactory.DMStoDegrees(latitude).ToString();

                        int length = adjustedlatitude.Length;
                        int index = adjustedlatitude.IndexOf(',');
                        if (length - index > 6)
                            adjustedlatitude = adjustedlatitude.Substring(0, index + 6);

                        Form1.Textbox3 = adjustedlatitude;
                    }
                    else
                        Form1.Textbox3 = "";


                    //Längengrad
                    Double[] longitude;
                    if (reader.GetTagValue<Double[]>(ExifTags.GPSLongitude, out longitude))
                    {

                        string longitudeRef;
                        if (reader.GetTagValue<string>(ExifTags.GPSLongitudeRef, out longitudeRef))
                        {
                        }

                        string adjustedlongitude;
                        adjustedlongitude = ToolsFactory.DMStoDegrees(longitude).ToString();

                        int length = adjustedlongitude.Length;
                        int index = adjustedlongitude.IndexOf(',');
                        if (length - index > 6)
                            adjustedlongitude = adjustedlongitude.Substring(0, index + 6);

                        if (longitudeRef == "W")
                            adjustedlongitude = "-" + adjustedlongitude;

                        Form1.Textbox4 = adjustedlongitude;
                    }
                    else
                        Form1.Textbox4 = "";


                    //Blendenzahl  0x9202
                    Double apertureValue;
                    if (reader.GetTagValue<Double>(ExifTags.FNumber, out apertureValue))
                        Form1.Textbox5 = "f/" + apertureValue.ToString();
                    else
                        Form1.Textbox5 = "";


                    //Belichtungszeit
                    Double exposureTime;
                    if (reader.GetTagValue<Double>(ExifTags.ExposureTime, out exposureTime))
                    {
                        exposureTime = 1 / exposureTime;
                        Form1.Textbox6 = "1/" + exposureTime.ToString() + " Sek";
                    }
                    else
                        Form1.Textbox6 = "";


                    //Filmempfindlichkeit (ISO)
                    UInt16 isoSpeed;
                    if (reader.GetTagValue<UInt16>(ExifTags.ISOSpeedRatings, out isoSpeed))
                        Form1.Textbox7 = isoSpeed.ToString();
                    else
                        Form1.Textbox7 = "";


                    //Brennweite
                    Double focalLength;
                    if (reader.GetTagValue<Double>(ExifTags.FocalLength, out focalLength))
                        Form1.Textbox8 = focalLength + " mm";
                    else
                        Form1.Textbox8 = "";


                    //Belichtungskorrektur
                    Double exposureBiasValue;
                    if (reader.GetTagValue<Double>(ExifTags.ExposureBiasValue, out exposureBiasValue))
                        Form1.Textbox9 = exposureBiasValue.ToString("0.0") + " Schritt(e)";
                    else
                        Form1.Textbox9 = "";

                    //Messmethode
                    UInt16 meteringMode;
                    if (reader.GetTagValue<UInt16>(ExifTags.MeteringMode, out meteringMode))
                        Form1.Textbox12 = GetNamesMessmethode(meteringMode);
                    else
                        Form1.Textbox12 = "";


                    //Belichtungsprogramm
                    UInt16 exposureProgram;
                    if (reader.GetTagValue<UInt16>(ExifTags.ExposureProgram, out exposureProgram))
                        Form1.Textbox13 = GetNamesBelichtungsprogramm(exposureProgram);
                    else
                        Form1.Textbox11 = "";

                    //Objektiv
                    string lensModel;
                    if (reader.GetTagValue<string>(ExifTags.LensModel, out lensModel))
                        Form1.Textbox20 = lensModel;
                    else
                        Form1.Textbox20 = "";

                    // Orientierung
                    reader.GetTagValue<UInt16>(ExifTags.Orientation, out orientation);

                    //Makernote
                    reader.GetTagValue<Byte[]>(ExifTags.MakerNote, out bytesMakernote);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Fehler beim Einlesen der Exif-Tags." + Environment.NewLine + e);          
            }


            try
            {
                List<Makernote> makernotes = MakernoteFactory.GetMakernotes(bytesMakernote);

                //-------------------------------------------------------
                //Canon CameraSettings Tags
                Makernote cameraSettings = makernotes[0];

                //Aufnahmemodus
                string aufnamemodus = GetNamesAufnamemodus(cameraSettings.Values[11]);
                if (aufnamemodus != "")
                    Form1.Textbox13 = aufnamemodus;

                if (aufnamemodus == "HDR")
                {
                    MessageBox.Show("HDR");
                }

                //AF-Betriebsart
                Form1.Textbox10 = GetNamesAFBetriebsart(Convert.ToInt32(cameraSettings.Values[7]));

                //AF-Bereich-Auswahlmodus
                Form1.Textbox11 = GetNamesAFBereichAuswahlmodus(Convert.ToInt32(cameraSettings.Values[32]));

                //Kontrast
                Form1.Textbox16_3 = cameraSettings.Values[13].Substring(3);

                //Farbsättigung
                Form1.Textbox16_2 = cameraSettings.Values[14].Substring(3);

                //Farbton
                Form1.Textbox16_4 = cameraSettings.Values[42].Substring(3);


                //-------------------------------------------------------
                //Canon ShotInfo Tags
                Makernote shotInfoTags = makernotes[2];

                //auto ISO
                if (shotInfoTags.Values[1] != "0000")
                    Form1.Checkbox2 = true;
                else
                    Form1.Checkbox2 = false;


                //-------------------------------------------------------
                //CanonCameraInfo10D 
                Makernote canonCameraInfo10D= makernotes[32];

                //Bildstil
                int valInt = Convert.ToInt32(canonCameraInfo10D.Values[10]);
                Form1.Textbox15 = GetNamesBildstil(valInt);

                //Schärfe
                Form1.Textbox16_1 = canonCameraInfo10D.Values[2].Substring(3);

            }
            catch (Exception e)
            {
                MessageBox.Show("Fehler beim Einlesen der Exif-Tags." + Environment.NewLine + e);
            }

        }
        #endregion


        #region ResetForm
        public static void ResetForm()
        {
            Form1.Textbox0 = null;
            Form1.Textbox1 = null;
            Form1.Textbox2 = null;
            Form1.Textbox3 = null;
            Form1.Textbox4 = null;
            Form1.Textbox5 = null;
            Form1.Textbox6 = null;
            Form1.Textbox7 = null;
            Form1.Textbox8 = null;
            Form1.Textbox9 = null;
            Form1.Textbox10 = null;
            Form1.Textbox11 = null;
            Form1.Textbox12 = null;
            Form1.Textbox13 = null;
            //Form1.Textbox14 = null;
            Form1.Textbox15 = null;
            Form1.Textbox16_1 = null;
            Form1.Textbox16_2 = null;
            Form1.Textbox16_3 = null;
            Form1.Textbox16_4 = null;
            Form1.Textbox17 = null;
            Form1.Textbox18 = null;
            Form1.Textbox19 = null;
            Form1.Textbox20 = null;

            Form1.Label14 = null;
            Form1.Label16 = null;

            Form1.Datetime = new DateTime();

            //Form1.Filename = null;

            Form1.Picturebox1.Image = null;

        }
        #endregion


        #region NextImage
        public static void NextImage()
        {
            int index = Form1.Filenindex + 1;
            List<string> filenames = Form1.Filenames;
            if (index > (filenames.Count - 1))
            {
                Form1.Filenindex = index - 1;
                return;
            }

            ushort orientation;
            string filename = filenames[index];

            FillForm(filename, out orientation);
            SetImage(filename, orientation, index + 1);

            Form1.Filenindex = index;
            Form1.Filename = filename;

            if (Form1.Histogram)
            {
                Image histogram = (Image)HistogramFactory.CreateHistogram(filename);
                histogram = ImageFactory.ResizeImage(histogram, 215, 275);
                Form1.Picturebox2.Image = histogram;
            }
        }
        #endregion


        #region PreviousImage
        public static void PreviousImage()
        {
            int index = Form1.Filenindex - 1;
            List<string> filenames = Form1.Filenames;
            if (index < 0)
            {
                Form1.Filenindex = index + 1;
                return;
            }

            ushort orientation;
            string filename = filenames[index];

            FillForm(filename, out orientation);
            SetImage(filename, orientation, index + 1);

            Form1.Filenindex = index;
            Form1.Filename = filename;

            if (Form1.Histogram)
            {
                Image histogram = (Image)HistogramFactory.CreateHistogram(filename);
                histogram = ImageFactory.ResizeImage(histogram, 215, 275);
                Form1.Picturebox2.Image = histogram;
            }
        }
        #endregion
        




        #region SetImage
        public static void SetImage(string imageFilename, UInt16 orientation, int index)
        {
            Image orgImage = ImageFactory.OpenImage(imageFilename);
            Form1.Image = orgImage;

            Image image;
            if (orientation == 6)
            {
                image = ImageFactory.ResizeImage(orgImage, 900, 675);
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);

                Form1.Picturebox1.Visible = false;
                Size size = new Size(675, 900);
                Form1.Picturebox1.Size = size;
                Form1.Picturebox1.Location = new Point(537, 27);

            }
            else
            {
                image = ImageFactory.ResizeImage(orgImage, 1200, 900);

                Form1.Picturebox1.Visible = false;
                Size size = new Size(1200, 900);
                Form1.Picturebox1.Size = size;
                Form1.Picturebox1.Location = new Point(12, 27);
            }

            Form1.Picturebox1.Image = image;
            Form1.Picturebox1.Visible = true;

            Form1.Label14 = index.ToString();
//            Form1.Label16 = s_ImageFilenames.Count.ToString();
        }
        #endregion


        //-----------------------------------------------------------------------------------------------------------
        // Wertetabellen

        #region GetNamesBildstil
        private static string GetNamesBildstil(int index)
        {
            string valueName = null;

            switch (index)
            {
                case 81:
                    valueName = "Standard";
                    break;
                case 82:
                    valueName = "Porträt";
                    break;
                case 83:
                    valueName = "Landschaft";
                    break;
                case 84:
                    valueName = "Neutral";
                    break;
                case 85:
                    valueName = "Natürlich";
                    break;
                case 86:
                    valueName = "Monochrom";
                    break;
                case 87:
                    valueName = "Automatisch";
                    break;
                case 21:
                    valueName = "Anw. Def. 1";
                    break;
                case 22:
                    valueName = "Anw. Def. 2";
                    break;
                case 23:
                    valueName = "Anw. Def. 3";
                    break;

                    // Portät
                    // Natürlich
                    // Monochrom
                    // Anw. Def. 1
                    // Anw. Def. 2
                    // Anw. Def. 3
            }

            return valueName;

        }
        #endregion


        #region GetNamesAufnamemodus
        public static string GetNamesAufnamemodus(string index)
        {
            string valueName = null;
            switch (index)
            {
                case "0001":
                    valueName = "";
                    break;
                case "0002":
                    valueName = "Panorama";
                    break;
                case "0008":
                    valueName = "Hochformat";
                    break;
                case "0009":
                    valueName = "Sport";
                    break;
                case "000a":
                    valueName = "Makro (Nahaufname)";
                    break;
                case "0033":    //51
                    valueName = "HDR";
                    break;
                case "0044":
                    valueName = "Speisen";
                    break;
                case "0050":
                    valueName = "Körnigkeit S/ W";
                    break;
                case "004e":
                    valueName = "Keativassistent";
                    break;
                case "004f":
                    valueName = "Selbstporträt";
                    break;
                case "0034":
                    valueName = "Nachtaufnahme";
                    break;
                case "002f":
                    valueName = "Fischaugeneffekt";
                    break;
                case "0051":
                    valueName = "Ölgemälde-Effekt";
                    break;
                case "0052":
                    valueName = "Aquarell-Effekt";
                    break;
                case "0030":
                    valueName = "Miniatureffekt";
                    break;
                case "003a":
                    valueName = "Spielzeugkamera-Effekt";
                    break;
                case "003e":
                    valueName = "Weichzeichner";
                    break;
            }

            return valueName;

        }
        #endregion


        #region GetNamesAFBetriebsart
        public static string GetNamesAFBetriebsart(int index)
        {
            string valueName = null;

            switch (index)
            {
                case 100:
                    valueName = "One-Shot AF";
                    break;
                case 101:
                    valueName = "Servo";
                    break;
            }

            return valueName;

        }
        #endregion


        #region GetNamesAFBereichAuswahlmodus
        public static string GetNamesAFBereichAuswahlmodus(int index)
        {
            string valueName = null;

            switch (index)
            {
                case 0:
                    valueName = "Einzelfeld AF";
                    break;
                case 1:
                    valueName = "Verfolgung";
                    break;
            }

            return valueName;

        }
        #endregion


        #region GetNamesMessmethode
        private static string GetNamesMessmethode(int index)
        {
            string valueName = null;

            switch (index)
            {
                case 2:
                    valueName = "Mittenbetonte Messung";
                    break;
                case 3:
                    valueName = "Spotmessung";
                    break;
                case 5:
                    valueName = "Mehrfeldmessung";
                    break;
                case 6:
                    valueName = "Selektivmessung";
                    break;                 
            }

            return valueName;

        }
        #endregion


        #region GetNamesBelichtungsprogramm
        private static string GetNamesBelichtungsprogramm(int index)
        {
            string valueName = null;

            switch (index)
            {
                case 1:
                    valueName = "Manuelle Belichtung";
                    break;
                case 2:
                    valueName = "Programmautomatik";
                    break;
                case 3:
                    valueName = "Zeitautomatik";
                    break;
                case 4:
                    valueName = "Blendenautomatik";
                    break;                       
            }

            return valueName;

        }
        #endregion

        //--------------------------------------------------------------
    }
}

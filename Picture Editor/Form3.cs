using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExifLib;

namespace Picture_Editor
{
    public partial class Form3 : Form
    {

        public Form3()
        {
            InitializeComponent();
        }


        public void LoadShotInfo()
        {
            this.Text = "Canon ShotInfo Tags";

            string filename = Form1.Filename;

            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Kein Bild geladen");
                return;
            }

            listView1.View = View.Details;

            Image image = Form1.Image;
            PropertyItem[] prop = image.PropertyItems;


            Byte[] bytesMakernote = null;

            try
            {
                using (ExifReader reader = new ExifReader(filename))
                {
                    reader.GetTagValue<Byte[]>(ExifTags.MakerNote, out bytesMakernote);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Fehler beim Einlesen der Exif-Tags." + Environment.NewLine + err);
            }

            List<Makernote> makernotes = MakernoteFactory.GetMakernotes(bytesMakernote);

            //-------------------------------------------------------
            //Canon CameraSettings Tags
            Makernote shotInfo = makernotes[2];

            int index = 0;
            string tagName = null;
            string value;

            // AutoISO - 1
            index = index + 1;
            tagName = "AutoISO";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // BaseISO - 2 
            index = index + 1;
            tagName = "BaseISO";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // MeasuredEV - 3
            index = index + 1;
            tagName = "MeasuredEV";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // TargetAperture - 4
            index = index + 1;
            tagName = "TargetAperture";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // TargetExposureTime - 5
            index = index + 1;
            tagName = "TargetExposureTime";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // ExposureCompensation - 6
            index = index + 1;
            tagName = "ExposureCompensation";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // WhiteBalance - 7
            index = index + 1;
            tagName = "WhiteBalance";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // SlowShutter - 8
            index = index + 1;
            tagName = "SlowShutter";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // SequenceNumber - 9
            index = index + 1;
            tagName = "SequenceNumber";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // OpticalZoomCode - 10
            index = index + 1;
            tagName = "OpticalZoomCode";
            value = shotInfo.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // unknown - 11
            index = index + 1;
            tagName = "unknown";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknown");

            // CameraTemperature - 12
            index = index + 1;
            tagName = "CameraTemperature";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // FlashGuideNumber - 13
            index = index + 1;
            tagName = "FlashGuideNumber";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // AFPointsInFocus - 14
            index = index + 1;
            tagName = "AFPointsInFocus";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // FlashExposureComp - 15
            index = index + 1;
            tagName = "FlashExposureComp";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // AutoExposureBracketing - 16
            index = index + 1;
            tagName = "AutoExposureBracketing";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // AEBBracketValue - 17
            index = index + 1;
            tagName = "AEBBracketValue";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // ControlMode - 18
            index = index + 1;
            tagName = "ControlMode";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	FocusDistanceUpper - 19
            index = index + 1;
            tagName = "	FocusDistanceUpper";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	FocusDistanceLower - 20
            index = index + 1;
            tagName = "	FocusDistanceLower";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	FNumber - 21
            index = index + 1;
            tagName = "	FNumber";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	ExposureTime - 22
            index = index + 1;
            tagName = "	ExposureTime";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	MeasuredEV2 - 23
            index = index + 1;
            tagName = "	MeasuredEV2";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	BulbDuration - 24
            index = index + 1;
            tagName = "	BulbDuration";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	unknown - 25
            index = index + 1;
            tagName = "	unknown";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknown");

            // 	CameraType - 26
            index = index + 1;
            tagName = "	CameraType";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	AutoRotate - 27
            index = index + 1;
            tagName = "	AutoRotate";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	NDFilter - 28
            index = index + 1;
            tagName = "	NDFilter";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	SelfTimer2 - 29
            index = index + 1;
            tagName = "	SelfTimer2";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	unknown - 30
            index = index + 1;
            tagName = "	unknown";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknown");

            // 	unknown - 31
            index = index + 1;
            tagName = "	unknown";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknown");

            // 	unknown - 32
            index = index + 1;
            tagName = "	unknown";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknown");

            // 	FlashOutput - 33
            index = index + 1;
            tagName = "	FlashOutput";
            value = shotInfo.Values[index].ToString();
            value = ConverterFactory.HexstringToInt32(value).ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }


        public void LoadCameraSettings()
        {
            this.Text = "Canon CameraSettings Tags";

            string filename = Form1.Filename;

            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Kein Bild geladen");
                return;
            }

            listView1.View = View.Details;

            Image image = Form1.Image;
            PropertyItem[] prop = image.PropertyItems;


            Byte[] bytesMakernote = null;

            try
            {
                using (ExifReader reader = new ExifReader(filename))
                {  
                    reader.GetTagValue<Byte[]>(ExifTags.MakerNote, out bytesMakernote);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Fehler beim Einlesen der Exif-Tags." + Environment.NewLine + err);
            }

            List<Makernote> makernotes = MakernoteFactory.GetMakernotes(bytesMakernote);

            //-------------------------------------------------------
            //Canon CameraSettings Tags
            Makernote cameraSettings = makernotes[0];

            int index = 0;
            string tagName = null;
            string value;

            // MacroMode - 1
            index = index + 1;
            tagName = "MacroMode";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // SelfTimer - 2 
            index = index + 1;
            tagName = "SelfTimer";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // Quality - 3
            index = index + 1;
            tagName = "Quality";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // CanonFlashMode - 4
            index = index + 1;
            tagName = "CanonFlashMode";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // ContinuousDrive - 5
            index = index + 1;
            tagName = "ContinuousDrive";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // unknown - 6
            index = index + 1;
            tagName = "unknown";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknown");

            // FocusMode - 7
            index = index + 1;
            tagName = "FocusMode";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // unknown - 8
            index = index + 1;
            tagName = "unknown";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknwon");

            // RecordMode - 9
            index = index + 1;
            tagName = "RecordMode";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // CanonImageSize - 10
            index = index + 1;
            tagName = "CanonImageSize";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // EasyMode - 11
            index = index + 1;
            tagName = "EasyMode";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // DigitalZoom - 12
            index = index + 1;
            tagName = "DigitalZoom";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // Contrast - 13
            index = index + 1;
            tagName = "Contrast";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // Saturation - 14
            index = index + 1;
            tagName = "Saturation";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // Sharpness - 15
            index = index + 1;
            tagName = "Sharpness";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // CameraISO - 16
            index = index + 1;
            tagName = "CameraISO";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // MeteringMode - 17
            index = index + 1;
            tagName = "MeteringMode";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // FocusRange - 18
            index = index + 1;
            tagName = "FocusRange";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	AFPoint - 19
            index = index + 1;
            tagName = "	AFPoint";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	CanonExposureMode - 20
            index = index + 1;
            tagName = "	CanonExposureMode";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	unknown - 21
            index = index + 1;
            tagName = "	unknown";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknown");

            // 	LensType - 22
            index = index + 1;
            tagName = "	LensType";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16u");

            // 	MaxFocalLength - 23
            index = index + 1;
            tagName = "	MaxFocalLength";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16u");

            // 	MinFocalLength - 24
            index = index + 1;
            tagName = "	MinFocalLength";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16u");

            // 	FocalUnits - 25
            index = index + 1;
            tagName = "	FocalUnits";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	MaxAperture - 26
            index = index + 1;
            tagName = "	MaxAperture";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	MinAperture - 27
            index = index + 1;
            tagName = "	MinAperture";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	FlashActivity - 28
            index = index + 1;
            tagName = "	FlashActivity";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	FlashBits - 29
            index = index + 1;
            tagName = "	FlashBits";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	unknown - 30
            index = index + 1;
            tagName = "	unknown";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknown");
        
            // 	unknown - 31
            index = index + 1;
            tagName = "	unknown";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknown");

            // 	FocusContinuous - 32
            index = index + 1;
            tagName = "	FocusContinuous";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	AESetting - 33
            index = index + 1;
            tagName = "	AESetting";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	ImageStabilization - 34
            index = index + 1;
            tagName = "	ImageStabilization";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	DisplayAperture - 35
            index = index + 1;
            tagName = "	DisplayAperture";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	ZoomSourceWidth - 36
            index = index + 1;
            tagName = "	ZoomSourceWidth";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	ZoomTargetWidth - 37
            index = index + 1;
            tagName = "ZoomTargetWidth";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	unknown - 38
            index = index + 1;
            tagName = "unknown";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknown");

            // 	SpotMeteringMode - 39
            index = index + 1;
            tagName = "SpotMeteringMode";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	PhotoEffect - 40
            index = index + 1;
            tagName = "PhotoEffect";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	ManualFlashOutput - 41
            index = index + 1;
            tagName = "ManualFlashOutput";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	ColorTone - 42
            index = index + 1;
            tagName = "ColorTone";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            // 	unknown - 43
            index = index + 1;
            tagName = "unknown";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknown");

            // 	unknown - 44
            index = index + 1;
            tagName = "unknown";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknown");

            // 	unknown - 45
            index = index + 1;
            tagName = "unknown";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "unknown");

            // 	SRAWQuality - 46
            index = index + 1;
            tagName = "SRAWQuality";
            value = cameraSettings.Values[index].ToString();
            ListViewFactory.AddItem(listView1, index.ToString(), "", tagName, value, "int16s");

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            ////Aufnahmemodus
            //string aufnamemodus = FormFactory.GetNamesAufnamemodus(cameraSettings.Values[11]);
            //if (aufnamemodus != "")
            //    Form1.Textbox13 = aufnamemodus;

            ////AF-Betriebsart
            //string afBetriebsart = FormFactory.GetNamesAFBetriebsart(Convert.ToInt32(cameraSettings.Values[7]));

            ////AF-Bereich-Auswahlmodus
            //string afBereichAuswahlmodus = FormFactory.GetNamesAFBereichAuswahlmodus(Convert.ToInt32(cameraSettings.Values[32]));

            //////Kontrast
            //Form1.Textbox16_3 = cameraSettings.Values[13].Substring(3);

            ////Farbsättigung
            //Form1.Textbox16_2 = cameraSettings.Values[14].Substring(3);

            ////Farbton
            //Form1.Textbox16_4 = cameraSettings.Values[42].Substring(3);
        }



        #region Form3_Load
        private void Form3_Load(object sender, EventArgs e)
        {
            //string filename = Form1.Filename;

            //if (string.IsNullOrEmpty(filename))
            //{
            //    MessageBox.Show("Kein Bild geladen;");
            //    return;
            //}

            //listView1.View = View.Details;

            ////Image image = Image.FromFile(filename);


            //Image image = Form1.Image;
            //PropertyItem[] prop = image.PropertyItems;
            //List<Makernote> makernotes = null;

            //using (ExifReader reader = new ExifReader(filename))
            //{

            //    listView1.Items.Add(new ListViewItem(new string[]
            //    {
            //        "--------",
            //        "------",
            //        "--------------Exif-Tags---------------",      
            //        "----------------------------------------",
            //        "----------------------------------------",
            //        "------"
            //    }));

            //    int i = 0;
            //    foreach (var item in prop)
            //    {
            //        string id = string.Format("{0:x}", item.Id);
            //        string tagName;
            //        string type;
            //        ExifTags tag = ExifFactory.GetTag(id, out tagName, out type);

            //        string value;

            //        if (id == "927c")
            //        {
            //            Byte[] bytesMAkernote;

            //            if (reader.GetTagValue<Byte[]>(ExifTags.MakerNote, out bytesMAkernote)) ;
            //            {
            //                makernotes = MakernoteFactory.GetMakernotes(bytesMAkernote);


            //                string messageout = "";
            //                foreach (byte b in bytesMAkernote)
            //                {
            //                    messageout = messageout + b + Environment.NewLine;
            //                }

            //                File.WriteAllText(@"c:\temp\bytes.txt", messageout);
            //            }




            //        }
            //        // PropertyItem propItem = image.GetPropertyItem(37500);





            //        if (tag != null)
            //        {
            //            value = ExifFactory.GetValue(tag, reader, type);

            //            if (tagName == null)
            //                tagName = "<unbekannt>";


            //            if (value != null)
            //            {
            //                listView1.Items.Add(new ListViewItem(new string[]
            //                {
            //                    item.Id.ToString(),
            //                    id,
            //                    tagName,
            //                    value,
            //                    type
            //                }));
            //            }
            //            else
            //            {
            //                listView1.Items.Add(new ListViewItem(new string[]
            //                {
            //                    item.Id.ToString(),
            //                    id,
            //                    tagName,
            //                    "",
            //                    ""
            //                }));
            //            }
            //        }

            //    }
            //    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            //}


            //listView1.Items.Add(new ListViewItem(new string[]
            //{
            //    "--------",
            //    "------",
            //    "-----------Canon Makernotes-----------",      
            //    "----------------------------------------",
            //    "----------------------------------------",
            //    "------"
            //}));


            ////1 = BYTE An 8-bit unsigned integer.,
            ////2 = ASCII An 8-bit byte containing one 7-bit ASCII code. The final byte is terminated with NULL.,
            ////3 = SHORT A 16-bit (2-byte) unsigned integer,
            ////4 = LONG A 32-bit (4-byte) unsigned integer,
            ////5 = RATIONAL Two LONGs. The first LONG is the numerator and the second LONG expresses the
            ////denominator.,
            ////7 = UNDEFINED An 8-bit byte that can take any value depending on the field definition,
            ////9 = SLONG A 32-bit (4-byte) signed integer (2's complement notation),
            ////10 = SRATIONAL Two SLONGs. The first SLONG is the numerator and the second SLONG is the




            //if (makernotes != null && makernotes.Count > 0)
            //{
            //    foreach (Makernote makernote in makernotes)
            //    {
            //        Dictionary<int, string> dicValues = makernote.Values;
            //        string value = null;
            //        for (int i = 0; i < dicValues.Count; i++)
            //        {
            //            if (i == 4)
            //            {
            //                if (dicValues.Count > 4)
            //                    value = value + ", ...";
            //            }
            //            else if (i > 4)
            //            {
            //                break;
            //            }
            //            else
            //            {
            //                if (value == null)
            //                    value = dicValues[i].ToString();
            //                else
            //                    value = value + ", " + dicValues[i];
            //            }
            //        }

            //        string id = makernote.Id;
            //        bool isHex;
            //        bool bigEndian;
            //        listView1.Items.Add(new ListViewItem(new string[]
            //        {
            //            "",
            //            id,
            //            MakernoteFactory.GetNameMakernote(id, out isHex, out bigEndian),
            //            value,
            //            makernote.Type.ToString()
            //        }));


            //    }
            //}
        }
        #endregion


        public static ListView Listview1 { get; set; }

    }
}

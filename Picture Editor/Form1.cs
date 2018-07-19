using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AForge.Imaging;
using ExifLib;
using ExifLibrary;
using Image = System.Drawing.Image;


namespace Picture_Editor
{
    public partial class Form1 : Form
    {
        private static List<string> s_ImageFilenames;
        private static  Image s_Image;
        private static string s_Imagefilename;
        private static int s_IndexImage;
        private static Form1 s_MainForm;
        private static DateTime s_DateTime;
        private PropertyItem[] m_PictureProperties;
        private UInt16 m_Orientation;


        public Form1()
        {
            InitializeComponent();
            pictureBox1.AllowDrop = true;
            s_MainForm = this;
        }


        #region Form1_Load
        private void Form1_Load(object sender, EventArgs e)
        {
            BringToFront();
            Focus();
            KeyPreview = true;
            KeyDown += new KeyEventHandler(Form1_KeyDown);
        }
        #endregion


        #region Form1_KeyDown
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {             
                DialogResult result = MessageBox.Show("Bild " + Path.GetFileName(s_Imagefilename) + " löschen ? ", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                { 
                    File.Delete(s_Imagefilename);
                    s_ImageFilenames.RemoveAt(s_IndexImage);
                    FormFactory.NextImage();
                    FormFactory.ResetForm();
                }
            }
            else if ((e.KeyCode == Keys.S))
                FormFactory.PreviousImage();
            else if ((e.KeyCode == Keys.D))
                FormFactory.NextImage();
        }
        #endregion


        #region pictureBox
        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            s_ImageFilenames = FileFactory.GetFilenames(e);
            s_ImageFilenames.Sort();

            if (s_ImageFilenames == null || s_ImageFilenames.Count == 0)
            {
                MessageBox.Show("Keine Fotos gefunden.");
                return;
            }

            s_IndexImage = 0;
            s_Imagefilename = s_ImageFilenames[s_IndexImage];

            FormFactory.FillForm(s_Imagefilename, out m_Orientation);
            FormFactory.SetImage(s_Imagefilename, m_Orientation, s_IndexImage + 1);

            label16.Text = s_ImageFilenames.Count.ToString();

            if (checkBox1.Checked)
            {
                Image histogram = (Image) HistogramFactory.CreateHistogram(s_Imagefilename);
                histogram = ImageFactory.ResizeImage(histogram, 215, 275);
                pictureBox2.Image = histogram;
            }
        }


        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        #endregion


        #region navigation
        private void button7_Click(object sender, EventArgs e)
        {
            FormFactory.NextImage();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormFactory.PreviousImage();
        }
        #endregion


        #region AllTags
        private void button3_Click(object sender, EventArgs e)
        {
            Form3 tagsForm = new Form3();
            tagsForm.Show();
        }
        #endregion


        #region Bilder umbennen
        private void bilderUmbennenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 filenameForm = new Form2();
            filenameForm.Show();
            filenameForm.VisibleChanged += nameFormVisibleChanged;
        }

        private void nameFormVisibleChanged(object sender, EventArgs e)
        {
            Form2 filenameForm = (Form2) sender;
            if (!filenameForm.Visible)
            {
                string newFilename = filenameForm.Filnename;
                filenameForm.Dispose();

                if (!string.IsNullOrEmpty(newFilename))
                    s_ImageFilenames = FileFactory.RenameFiles(s_ImageFilenames, newFilename);

            }
        }
        #endregion


        #region Date/Time
        private void datumZeitÄndernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 dateTimeForm = new Form4();
            dateTimeForm.Show();
            dateTimeForm.VisibleChanged += dateTimeFormVisibleChanged;
        }

        private void dateTimeFormVisibleChanged(object sender, EventArgs e)
        {
            Form4 dateTimeForm = (Form4)sender;
            if (!dateTimeForm.Visible)
            {
                Bitmap bitmap = ImageFactory.GetBitmapWithProperties(s_Imagefilename);
                ImageFactory.ChangePropertyDateTime(bitmap);
                ImageFactory.SaveBitmap(bitmap, s_Imagefilename);
            }
        }
        #endregion


        #region GPS
        private void button4_Click_1(object sender, EventArgs e)
        {
            Form5 coordDialog = new Form5();
            coordDialog.Show();
            coordDialog.VisibleChanged += formVisibleChanged;
        }

        private void formVisibleChanged(object sender, EventArgs e)
        {
            Form5 coordDialog = (Form5)sender;
            if (!coordDialog.Visible)
            {
                string coordinates = coordDialog.Coordinates;
                if (coordinates != "" & coordinates != null)
                {

                    GPSFactory.GPSTagSaved(coordinates, s_Imagefilename);
                }

                coordDialog.Dispose();
            }

        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            string lon = textBox4.Text;
            string lonPoint = lon.Replace(',', '.');

            string lat = textBox3.Text;
            string latPoint = lat.Replace(',', '.');


            string link = "http://www.google.com/maps/place/" + latPoint + ", " + lonPoint;

            System.Diagnostics.Process.Start(link);
        }
        #endregion



        #region Properties
        // Dateiname
        public static String Textbox0
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox0.Text = value; }
        }

        // Datum
        public static String Textbox1
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox1.Text = value; }
        }

        // Uhrzeit
        public static String Textbox2
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox2.Text = value; }
        }

        // Breitengrad
        public static String Textbox3
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox3.Text = value; }
        }

        // Längengrad
        public static String Textbox4
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox4.Text = value; }
        }

        // Blendenzahl
        public static String Textbox5
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox5.Text = value; }
        }

        // Belichtungszeit
        public static String Textbox6
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox6.Text = value; }
        }

        // Filmempfindlichkeit
        public static String Textbox7
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox7.Text = value; }
        }

        // Filmempfindlichkeit
        public static bool Checkbox2
        {
            //get { return Textbox1; }
            set { s_MainForm.checkBox2.Checked = value; }
        }

        // Brennweite
        public static String Textbox8
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox8.Text = value; }
        }

        // Belichtungskorrektur
        public static String Textbox9
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox9.Text = value; }
        }

        // AF-Betriebsart
        public static String Textbox10
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox10.Text = value; }
        }

        // AF-Bereich-Auswahlmodus
        public static String Textbox11
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox11.Text = value; }
        }

        // Messmodus
        public static String Textbox12
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox12.Text = value; }
        }

        // Belichtungsprogramm
        public static String Textbox13
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox13.Text = value; }
        }

        //// Aufnahmemodus
        //public static String Textbox14
        //{
        //    //get { return Textbox1; }
        //    set { s_mainForm.textBox14.Text = value; }
        //}

        // Bildstil:
        public static String Textbox15
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox15.Text = value; }
        }

        // Schärfe:
        public static String Textbox16_1
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox16_1.Text = value; }
        }

        // Farbsättigung
        public static String Textbox16_2
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox16_2.Text = value; }
        }

        // Kontrast
        public static String Textbox16_3
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox16_3.Text = value; }
        }

        // Farbton
        public static String Textbox16_4
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox16_4.Text = value; }
        }


        // Belichtuingsoptimierung:
        public static String Textbox17
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox17.Text = value; }
        }


        // Abmaße
        public static String Textbox18
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox18.Text = value; }
        }


        // Seitenverhältnis:
        public static String Textbox19
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox19.Text = value; }
        }


        // Objektiv:
        public static String Textbox20
        {
            //get { return Textbox1; }
            set { s_MainForm.textBox20.Text = value; }
        }



        //Bild
        public static Image Image
        {
            get { return s_Image; }
            set { s_Image = value; }
        }


        //Dateiindex
        public static int Filenindex
        {
            get { return s_IndexImage; }
            set { s_IndexImage = value; }
        }
        
        // Dateiname
        public static String Filename
        {
            get { return s_Imagefilename; }
            set { s_Imagefilename = value; }
        }

        // alle Dateinamen
        public static List<string> Filenames
        {
            get { return s_ImageFilenames; }
            set { s_ImageFilenames = value; }
        }

        // Datum
        public static DateTime Datetime
        {
            get { return s_DateTime; }
            set { s_DateTime = value; }
        }


        //Histogram
        public static bool Histogram
        {
            get { return s_MainForm.checkBox1.Checked; }

        }


        public static PictureBox Picturebox1
        {
            get { return s_MainForm.pictureBox1; }
            set { s_MainForm.pictureBox1 = value; }
        }


        public static PictureBox Picturebox2
        {
            get { return s_MainForm.pictureBox2; }
            set { s_MainForm.pictureBox2 = value; }
        }

        public static String Label14
        {
            get { return s_MainForm.label14.Text; }
            set { s_MainForm.label14.Text = value; }
        }

        public static String Label16
        {
            get { return s_MainForm.label16.Text; }
            set { s_MainForm.label16.Text = value; }
        }
        #endregion

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Version: 0.1");
        }

    }
}




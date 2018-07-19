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

        #region Form3_Load
        private void Form3_Load(object sender, EventArgs e)
        {
            string filename = Form1.Filename;

            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Kein Bild geladen;");
                return;
            }

            listView1.View = View.Details;

            //Image image = Image.FromFile(filename);


            Image image = Form1.Image;
            PropertyItem[] prop = image.PropertyItems;
            List<Makernote> makernotes = null;

            using (ExifReader reader = new ExifReader(filename))
            {

                listView1.Items.Add(new ListViewItem(new string[]
                {
                    "--------",
                    "------",
                    "--------------Exif-Tags---------------",      
                    "----------------------------------------",
                    "----------------------------------------",
                    "------"
                }));

                int i = 0;
                foreach (var item in prop)
                {
                    string id = string.Format("{0:x}", item.Id);
                    string tagName;
                    string type;
                    ExifTags tag = ExifFactory.GetTag(id, out tagName, out type);

                    string value;

                    if (id == "927c")
                    {
                        Byte[] bytesMAkernote;

                        if (reader.GetTagValue<Byte[]>(ExifTags.MakerNote, out bytesMAkernote)) ;
                        {
                            makernotes = MakernoteFactory.GetMakernotes(bytesMAkernote);


                            string messageout = "";
                            foreach (byte b in bytesMAkernote)
                            {
                                messageout = messageout + b + Environment.NewLine;
                            }

                            File.WriteAllText(@"c:\temp\bytes.txt", messageout);
                        }




                    }
                    // PropertyItem propItem = image.GetPropertyItem(37500);





                    if (tag != null)
                    {
                        value = ExifFactory.GetValue(tag, reader, type);

                        if (tagName == null)
                            tagName = "<unbekannt>";


                        if (value != null)
                        {
                            listView1.Items.Add(new ListViewItem(new string[]
                            {
                                item.Id.ToString(),
                                id,
                                tagName,
                                value,
                                type
                            }));
                        }
                        else
                        {
                            listView1.Items.Add(new ListViewItem(new string[]
                            {
                                item.Id.ToString(),
                                id,
                                tagName,
                                "",
                                ""
                            }));
                        }
                    }

                }
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }


            listView1.Items.Add(new ListViewItem(new string[]
            {
                "--------",
                "------",
                "-----------Canon Makernotes-----------",      
                "----------------------------------------",
                "----------------------------------------",
                "------"
            }));


            //1 = BYTE An 8-bit unsigned integer.,
            //2 = ASCII An 8-bit byte containing one 7-bit ASCII code. The final byte is terminated with NULL.,
            //3 = SHORT A 16-bit (2-byte) unsigned integer,
            //4 = LONG A 32-bit (4-byte) unsigned integer,
            //5 = RATIONAL Two LONGs. The first LONG is the numerator and the second LONG expresses the
            //denominator.,
            //7 = UNDEFINED An 8-bit byte that can take any value depending on the field definition,
            //9 = SLONG A 32-bit (4-byte) signed integer (2's complement notation),
            //10 = SRATIONAL Two SLONGs. The first SLONG is the numerator and the second SLONG is the




            if (makernotes != null && makernotes.Count > 0)
            {
                foreach (Makernote makernote in makernotes)
                {
                    Dictionary<int, string> dicValues = makernote.Values;
                    string value = null;
                    for (int i = 0; i < dicValues.Count; i++)
                    {
                        if (i == 4)
                        {
                            if (dicValues.Count > 4)
                                value = value + ", ...";
                        }
                        else if (i > 4)
                        {
                            break;
                        }
                        else
                        {
                            if (value == null)
                                value = dicValues[i].ToString();
                            else
                                value = value + ", " + dicValues[i];
                        }
                    }

                    string id = makernote.Id;
                    bool isHex;
                    bool bigEndian;
                    listView1.Items.Add(new ListViewItem(new string[]
                    {
                        "",
                        id,
                        MakernoteFactory.GetNameMakernote(id, out isHex, out bigEndian),
                        value,
                        makernote.Type.ToString()
                    }));


                }
            }
        }
        #endregion


        public static ListView Listview1 { get; set; }

    }
}

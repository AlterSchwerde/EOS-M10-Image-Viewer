using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Picture_Editor
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }


        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.Text = Form1.Datetime.ToShortDateString();
            textBox2.Text = Form1.Datetime.ToShortTimeString();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            double days = Convert.ToDouble(textBox6.Text);
            double hours = Convert.ToDouble(comboBox7.Text);
            double minutes = Convert.ToDouble(comboBox8.Text);

            bool add = false;

            if (radioButton1.Checked)
            {
                add = true;
            }
            else if (radioButton2.Checked)
            {
                add = false;
            }

            if (days != 0)
            {
                if (add)
                    Form1.Datetime = Form1.Datetime.AddDays(days);
                else
                    Form1.Datetime = Form1.Datetime.AddDays(-days);

            }
            if (hours != 0)
            {
                if (add)
                    Form1.Datetime = Form1.Datetime.AddHours(hours);
                else
                    Form1.Datetime = Form1.Datetime.AddHours(-hours);
            }
            if (minutes != 0)
            {
                if (add)
                    Form1.Datetime = Form1.Datetime.AddMinutes(minutes);
                else
                    Form1.Datetime = Form1.Datetime.AddMinutes(-minutes);
            }

            Form1.Textbox1 = Form1.Datetime.ToShortDateString();
            Form1.Textbox2 = Form1.Datetime.ToShortTimeString();

            Visible = false;
        }

        public DateTime Datetime { get; set; }

       
    }
}

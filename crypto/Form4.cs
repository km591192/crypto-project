using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace crypto
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        HMAC hmac = new HMAC();
        string key,shmac;
        Controller control = new Controller();
        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = hmac.GenHMAC(textBox1.Text ,textBox2/*.Text*/);
            textBox4.Text = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox5.Text = hmac.Verify(textBox4.Text, textBox3.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            control.openfile(textBox1, "*.txt");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox3.Text = hmac.GenHMAC(textBox1.Text, textBox2/*.Text*/);
            string path = control.save("*.txt");
            string writetofile = File.ReadAllBytes("Key") + "\n" + textBox3.Text.ToString() + "\n" + textBox1.Text.ToString();
            if (path != null)
            {
                File.WriteAllText(path, writetofile);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string of = control.openfile(textBox4, "*.txt");
            key = control.getkeyfromfile(of);
            shmac = control.gethmacfromfile(of);
            string text = textBox4.Text;
            string restext = text.Substring(47);
            textBox4.Text = restext;
            textBox5.Text = hmac.Verify(restext, shmac);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox6.Text = hmac.CBCMAC(textBox1.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox7.Text = hmac.CBCMAC(textBox4.Text);
            hmac.comparerstring(textBox1.Text, textBox4.Text);
        }

       
       

        
    }
}

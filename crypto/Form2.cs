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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        Hash hash = new Hash();
        HashCert hc = new HashCert();
        byte[] signature;
        Controller control = new Controller();
        private string String_Print(byte[] message)
        {
            string val = " ";
            foreach (char c in message)
                val += c.ToString();
            return val;
        }
        private void button1_Click(object sender, EventArgs e)
        {
             signature = hc.Sign(textBox1.Text.Trim().ToString());
             textBox3.Text = textBox1.Text.Trim().ToString();
             textBox2.Text = String_Print(signature);
        }

          private void button2_Click(object sender, EventArgs e)
        { 
              if (hc.Verify(textBox3.Text.Trim().ToString(), signature))
                {  MessageBox.Show("Same"); }
                else
                { MessageBox.Show("Not same");}

          }

          private void button3_Click(object sender, EventArgs e)
          {
               control.openfile(textBox1, "*.txt");
         }

          private void button5_Click(object sender, EventArgs e)
          {
               signature = hc.Sign(textBox1.Text.Trim().ToString());
             textBox2.Text = String_Print(signature);
            string path = control.save("*.txt");
            File.WriteAllBytes("Sign", signature);
            string writetofile = signature + "\n"+ textBox1.Text.ToString();
            if (path != null)
            {
                File.WriteAllText(path, writetofile);
            }
        }


          private void button4_Click(object sender, EventArgs e)
          {
            string of = control.openfile(textBox3, "*.txt");
          // byte[] hash = control.getkeyfromfileb(of);
            byte[] hash = control.getkeyfromfilebb(of);
            string text = textBox3.Text;
            string restext = text.Substring(14);
            textBox3.Text = restext;

            if (hc.Verify(restext, signature))
                {  MessageBox.Show("Same"); }
                else
                { MessageBox.Show("Not same");}
          }
          }
        /*
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = hash.E(textBox1.Text.Trim().ToString());
          //  textBox2.Text = hash.Enc(textBox1.Text.Trim().ToString());
            textBox3.Text = textBox1.Text.Trim().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox4.Text = hash.D(textBox3.Text.Trim().ToString(), textBox2.Text.Trim().ToString());
           // textBox4.Text = hash.Verifyhashes(textBox3.Text.Trim().ToString(),textBox2.Text.Trim().ToString());
        }*/
    }



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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Controller control = new Controller();
        Crypto crypto = new Crypto();
        RC2 rc = new RC2();
        //Cryptos cr = new Cryptos();
        CryptAES cr = new CryptAES();
        Getcert getcert = new Getcert();
        string key, IV;

        private void button1_Click(object sender, EventArgs e)
        {
            getcert.FindCerts();

        }
        private void btnchoose_Click(object sender, EventArgs e)
        {
            control.openfile(textBox1, "*.txt");
        }

        private void btnencrypt_Click(object sender, EventArgs e)
        {
            if (cbciphermode.SelectedItem.ToString() == "DES")
            textBox3.Text = crypto.Encrypt(textBox1.Text.Trim().ToString(), control.getcmode(cbcmode), control.getpmode(cbpmode));
            if (cbciphermode.SelectedItem.ToString() == "RC2")
                textBox3.Text = rc.Encrypt(textBox1.Text.Trim().ToString(), control.getcmode(cbcmode), control.getpmode(cbpmode));
            if (cbciphermode.SelectedItem.ToString() == "AES")
            textBox3.Text = cr.Encrypt(textBox1.Text.Trim().ToString(), control.getcmode(cbcmode), control.getpmode(cbpmode), cbkz.SelectedItem.ToString());
            key = control.getkey();
            string path = control.save("*.crypto");
            if (path != null)
            {
                //File.WriteAllText(path, string.Format("{0} \n {1}", key, textBox3.Text.Trim().ToString()));
                File.WriteAllText(path,  textBox3.Text.Trim().ToString());
            }
        }

        private void btndecrypt_Click(object sender, EventArgs e)
        {
            string of = control.openfile(textBox3, "*.crypto");
            key = control.getkeyfromfile(of);
            //control.setkey(key);
            if (cbciphermode.SelectedItem.ToString() == "AES")
                textBox2.Text = cr.Decrypt(control.gettextfromfile(of), control.getcmode(cbcmode), control.getpmode(cbpmode), cbkz.SelectedItem.ToString()); 
            if (cbciphermode.SelectedItem.ToString() == "RC2")
                textBox2.Text = rc.Decrypt(control.gettextfromfile(of), control.getcmode(cbcmode), control.getpmode(cbpmode));            
            if (cbciphermode.SelectedItem.ToString() == "DES")
            textBox2.Text = crypto.Decrypt(control.gettextfromfile(of), control.getcmode(cbcmode), control.getpmode(cbpmode));
            string path = control.save("*.txt");
            if (path != null)
                 File.WriteAllText(path, textBox2.Text.Trim().ToString());
        }

        


        
    }
}




/*
private void btnencrypt_Click(object sender, EventArgs e)
        {
            textBox3.Text = crypto.Encrypt(textBox1.Text.Trim().ToString(), control.getcmode(cbcmode), control.getpmode(cbpmode));
            key = control.getkey();
            IV = control.getIV();
            string path = control.save("*.crypto");
            if (path != null)
            {
                if (cbcmode.SelectedItem.Equals("ECB"))
                File.WriteAllText(path, string.Format("{0} \n {1}", key, textBox3.Text.Trim().ToString()));
                if (cbcmode.SelectedItem.Equals("CBC"))
                   File.WriteAllText(path, string.Format("{0} \n {1} \n {2}", key, IV, textBox3.Text.Trim().ToString()));
            }
        }

        private void btndecrypt_Click(object sender, EventArgs e)
        {
            string of = control.openfile(textBox3, "*.crypto");
            key = control.getkeyfromfile(of);
            IV = control.getIVfromfile(of);
            control.setkey(key);
            control.setIV(IV);
            if (cbcmode.SelectedItem.Equals("ECB"))
                textBox2.Text = crypto.Decrypt(control.gettextfromfile(of), control.getcmode(cbcmode), control.getpmode(cbpmode));
            if (cbcmode.SelectedItem.Equals("CBC"))
                textBox2.Text = crypto.Decrypt(control.gettextfromfileIV(of), control.getcmode(cbcmode), control.getpmode(cbpmode));
            string path = control.save("*.txt");
            if (path != null)
                 File.WriteAllText(path, textBox2.Text.Trim().ToString());
        }
*/
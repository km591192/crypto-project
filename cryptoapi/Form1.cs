using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cryptoapi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Crypt crypt = new Crypt();
        private void btnencrypt_Click(object sender, EventArgs e)
        {
            tbcipher.Text = Cryptotwo.Encrypt(tbinput.Text.Trim().ToString(), true);
        }

        private void btndecrypt_Click(object sender, EventArgs e)
        {
            tboutput.Text = Cryptotwo.Decrypt(tbcipher.Text.Trim().ToString(), true);
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            tbcipher.Clear();
            tbinput.Clear();
            tboutput.Clear();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace crypto
{
    class Controller
    {
        public string openfile(TextBox tb, string file)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "File (" + file + ")|" + file;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tb.Text = File.ReadAllText(ofd.FileName);
            }
            return ofd.FileName;
        }
        string ofdfilename;
        public string of( string file)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "File (" + file + ")|" + file;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ofdfilename = ofd.FileName;
            }
            return ofdfilename;
        }
        string sfdfilename;
        public string save(string file)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "File (" + file + ")|" + file; ;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                RichTextBox richtextBox1 = new RichTextBox();
                richtextBox1.SaveFile(sfd.FileName);
                sfdfilename = sfd.FileName;
            }
            return sfdfilename;
        }

        public string getIVfromfile(string file)
        {
            FileStream infile = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader sread = new StreamReader(infile);
            int line = 0; string iv = "";
            line = System.IO.File.ReadAllLines(file).Length;
            for (int i = 1; i <= 2; i++)
                iv = sread.ReadLine();
            infile.Close();
            sread.Close();
            return iv;
        }
        public string getkeyfromfile(string file)
        {
            FileStream infile = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader sread = new StreamReader(infile);
            string keys = sread.ReadLine();
            infile.Close();
            sread.Close();
            return keys;
        }
        public byte[] getkeyfromfileb(string file)
        {

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            FileStream infile = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader sread = new StreamReader(infile);
            string key = sread.ReadLine();
            byte[] keys = File.ReadAllBytes("Hash");
            infile.Close();
            sread.Close();
            return keys;
        }
        public byte[] getkeyfromfilebb(string file)
        {

            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            FileStream infile = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader sread = new StreamReader(infile);
            byte[] key = File.ReadAllBytes("Sign");
            infile.Close();
            sread.Close();
            return key;
        }

        public string gethmacfromfile(string file)
        {
            FileStream infile = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader sread = new StreamReader(infile);
            int line = 0; string text = "";
            line = System.IO.File.ReadAllLines(file).Length;
            for (int i = 1; i <= 2; i++)
                text = sread.ReadLine();
            infile.Close();
            sread.Close();
            return text;
        }
        public string gettextfromfile(string file)
        {
            FileStream infile = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader sread = new StreamReader(infile);
            int line = 0; string text="";
            line = System.IO.File.ReadAllLines(file).Length;
            for (int i = 1; i <= line; i++)
            text = sread.ReadLine();
            infile.Close();
            sread.Close();
            return text;
        }
        public string gettextfromfilehmac(string file)
        {
            FileStream infile = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader sread = new StreamReader(infile);
            int line = 0; string text = "", res = "";
            line = System.IO.File.ReadAllLines(file).Length;
            for (int i = 2; i <= line; i++)
            {
                text = sread.ReadLine();
                res += text;
            }
            infile.Close();
            sread.Close();
            return res;
        }
        public string gettextfromfileIV(string file)
        {
            FileStream infile = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader sread = new StreamReader(infile);
            int line = 0; string text = "";
            line = System.IO.File.ReadAllLines(file).Length;
            for (int i = 2; i <= line; i++)
                text = sread.ReadLine();
            infile.Close();
            sread.Close();
            return text;
        }

        public string getIV()
        {
            FileStream file = new FileStream("IV", FileMode.Open, FileAccess.Read);
            StreamReader sreader = new StreamReader(file);
            string IV = sreader.ReadToEnd();
            file.Close();
            sreader.Close();
            return IV;
        }

        public void setIV(string IV)
        {
            FileStream file = new FileStream("IV", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter swr = new StreamWriter(file);
            swr.Write(IV);
            swr.Close();
            file.Close();
        }
        public string getkey()
        {
            FileStream file = new FileStream("SecurityKey",FileMode.Open,FileAccess.Read);
            StreamReader sreader = new StreamReader(file);
            string key = sreader.ReadToEnd();
            file.Close();
            sreader.Close();
            return key;
        }
        public string getkeyhmac()
        {
            FileStream file = new FileStream("Key", FileMode.Open, FileAccess.Read);
            StreamReader sreader = new StreamReader(file);
            string key = sreader.ReadToEnd();
            file.Close();
            sreader.Close();
            return key;
        }

        public void setkey(string key)
        {
            FileStream file = new FileStream("SecurityKey", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter swr = new StreamWriter(file);
            swr.Write(key);
            swr.Close();
            file.Close();
        }

        public string getcmode(ComboBox cb)
        {
            string result="";
            if (cb.SelectedItem.Equals("ECB"))
                result = "ECB";
            else
            if (cb.SelectedItem.Equals("CBC"))
                result = "CBC";
            else
            if (cb.SelectedItem.Equals("CFB"))
                result = "CFB";
            else
            if (cb.SelectedItem.Equals("OFB"))
                result = "OFB";
            else
            { System.Windows.Forms.MessageBox.Show("Error. Select cipher mode."); result = "ECB"; }
            return result;
        }


        public string getpmode(ComboBox cb)
        {
            string result="";
            if (cb.SelectedItem.Equals("PKCS7"))
                result = "PKCS7";
            else
            if (cb.SelectedItem.Equals("Zeros"))
                result = "Zeros";
            else
            if (cb.SelectedItem.Equals("ANSIX923"))
                result = "ANSIX923";
            else
            if (cb.SelectedItem.Equals("ISO10126"))
                result = "ISO10126";
            else
            { System.Windows.Forms.MessageBox.Show("Error. Select cipher mode."); result = "ECB"; }
            return result;
        }

        public string getciphermode(ComboBox cb)
        {
            string result = "";
            if (cb.SelectedItem.Equals("DES"))
                result = "DES";
            else
                if (cb.SelectedItem.Equals("AES"))
                    result = "AES";
                else
                    if (cb.SelectedItem.Equals("RC2"))
                        result = "RC2";
                else
                        { System.Windows.Forms.MessageBox.Show("Error. Select cipher mode."); result = "DES"; }
            return result;
        }
        public int getkeysizeaes(ComboBox cb1 ,ComboBox cb)
        {
            int result = 64;
            if (cb1.SelectedItem.Equals("AES"))
            {
                if (cb.SelectedItem.Equals("128"))
                    result = 128;
                else
                    if (cb.SelectedItem.Equals("192"))
                        result = 192;
                if (cb.SelectedItem.Equals("256"))
                    result = 256;
                else
                { System.Windows.Forms.MessageBox.Show("Error. Select key size."); result = 128; }
            }
            if (cb1.SelectedItem.Equals("DES")) { cb1.Items.Add("64"); cb1.SelectedItem.Equals("64"); }
            return result;
        }
    }
}

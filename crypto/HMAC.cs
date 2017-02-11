using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.IO;

namespace crypto
{
    class HMAC
    { 
        System.Security.Cryptography.RC2CryptoServiceProvider rc2 = new System.Security.Cryptography.RC2CryptoServiceProvider();

        public string CBCMAC(string message)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            rc2.GenerateIV();
            rc2.GenerateKey();
            byte[] keyByte = rc2.Key;
            byte[] IVbyte = rc2.IV;
            File.WriteAllBytes("Key", rc2.Key);
            File.WriteAllBytes("IV", rc2.IV);
            byte[] messageBytes = encoding.GetBytes(message);
            ICryptoTransform cryptoTransform = rc2.CreateEncryptor(keyByte, IVbyte);
            var results = new byte[16];
            cryptoTransform.TransformBlock(messageBytes, 0, 16, results, 0);
            var lastEightBytes = new byte[8];
            Array.Copy(results, 8, lastEightBytes, 0, 8);
            var hexResult = string.Empty;
            foreach (byte ascii in lastEightBytes)
            {
                int n = (int)ascii;
                hexResult += n.ToString("X").PadLeft(2, '0');
            }
            return hexResult;
        }
        public string GenHMAC(string message, TextBox tb/*,string key*/)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
           // byte[] keyByte = encoding.GetBytes(key);
            rc2.GenerateKey();
            byte[] keyByte = rc2.Key;
            File.WriteAllBytes("Key",rc2.Key);
            tb.Text = ByteToString(keyByte);
            HMACMD5 hmacmd5 = new HMACMD5(keyByte);
            byte[] messageBytes = encoding.GetBytes(message);
            byte[] hashmessage = hmacmd5.ComputeHash(messageBytes);
            return ByteToString(hashmessage);
        }

        public string Verify(string message, string _hmac)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            //byte[] keyByte = encoding.GetBytes(key);
            byte[] keyByte = File.ReadAllBytes("Key");
           // System.Windows.Forms.MessageBox.Show(ByteToString(keyByte));
            HMACMD5 hmacmd5 = new HMACMD5(keyByte);
            byte[] messageBytes = encoding.GetBytes(message);
            byte[] hashmessage = hmacmd5.ComputeHash(messageBytes);
            string hmacmessage = ByteToString(hashmessage);
            if (verifyhash(hmacmessage,_hmac) == true)
                System.Windows.Forms.MessageBox.Show("Same");
            else
                System.Windows.Forms.MessageBox.Show("Not same");
            return hmacmessage;
        }


        public string GenHMACf(string message, byte[] keyByte,string shmac)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            HMACMD5 hmacmd5 = new HMACMD5(keyByte);
            byte[] messageBytes = encoding.GetBytes(message);
            byte[] hashmessage = hmacmd5.ComputeHash(messageBytes);
            Verifyf(ByteToString(hashmessage), shmac);
            return ByteToString(hashmessage);
        }

        public void Verifyf(string hmacmessage, string _hmac)
        {
           if (verifyhash(hmacmessage, _hmac) == true)
                System.Windows.Forms.MessageBox.Show("Same");
            else
                System.Windows.Forms.MessageBox.Show("Not same");
        }

        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2");
            }
            return (sbinary);
        }
         public bool verifyhash(string input, string hash)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(input, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
         public void comparerstring(string input, string hash)
         {
             StringComparer comparer = StringComparer.OrdinalIgnoreCase;
             if (0 == comparer.Compare(input, hash))
             {
                 MessageBox.Show("Correct. Same.");
             }
             else
             {
                 MessageBox.Show("Not same.");
             }
         }
        


       }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace crypto
{
    class Hash
    {
        Getcert cert = new Getcert();

        private string String_Print(byte[] message)
        {
            string val = " ";
            foreach (char c in message) 
                val += c.ToString();
            return val;
        }

        public string Enc(string input)
        {
            byte[] data = Encoding.Default.GetBytes(input);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(data, 0, data.Length);
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
            RSAFormatter.SetHashAlgorithm("MD5");
            byte[] Sign = RSAFormatter.CreateSignature(hash);
           string result = String_Print(Sign);
           File.WriteAllBytes("Hash", Sign);
           return result;

        }

        public string Verifyhashes(string tohash,string oldhash)
        {
            byte[] data = Encoding.Default.GetBytes(tohash);
            MD5CryptoServiceProvider md5=new MD5CryptoServiceProvider(); 
            string x_id=CryptoConfig.MapNameToOID("MD5"); 
            byte[] hash = md5.ComputeHash(data,0,data.Length);
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(); 
            Byte[] Sign=RSA.SignHash(hash,x_id);
           if( RSA.VerifyHash(hash, x_id, Encoding.Default.GetBytes(oldhash)))
               System.Windows.Forms.MessageBox.Show("Same");
           else
               System.Windows.Forms.MessageBox.Show("Not same");
            string result = String_Print(Sign);
            return result;
        }
       
 



        public string getMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(UTF8Encoding.Unicode.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public bool verifyMd5Hash(string input, string hash)
        {
            string hashOfInput = getMd5Hash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool verifymd5Hash(string h, string hash)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (hash.Equals(h) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string Encrypts(string toEncrypt)
        {
            string result = null; string signedData = null;
            byte[] hashes = UTF8Encoding.Unicode.GetBytes(getMd5Hash(toEncrypt));
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(RSA);
            RSAFormatter.SetHashAlgorithm("MD5");
            byte[] Sign = RSAFormatter.CreateSignature(hashes);
            foreach (char c in Sign) 
                signedData += c.ToString();
            result = signedData;
            return result;
        }

        public string Decrypts(string stringtohash, string oldhash)
        {
            string result = null,signedData = null;
            string hashes = oldhash;
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
          //  result = getMd5Hash(stringtohash);
            byte[] hash = UTF8Encoding.Unicode.GetBytes(getMd5Hash(stringtohash));
            string x_id = CryptoConfig.MapNameToOID("MD5");
            Byte[] Sign = RSA.SignHash(hash, x_id);
            foreach (char c in Sign)
                signedData += c.ToString();
            result = signedData;
            if (verifymd5Hash(result,hashes) == true)
                System.Windows.Forms.MessageBox.Show("Same");
            else
                 System.Windows.Forms.MessageBox.Show("Not same");
            return result;

        }


        RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            
        public string Encrypt(string toEncrypt)
        {
               string result = null;
               RSAParameters Key = RSAalg.ExportParameters(true);
                string hashes = getMd5Hash(toEncrypt);
                string signedData = cert.Sign(toEncrypt,Key);
                result = signedData;
              //  result = hashes;
            return result;
        }

        public string Decrypt(string stringtohash,string oldhash)
        {
            RSAParameters Key = RSAalg.ExportParameters(true);
            string result = null;
            //string hashes = oldhash;
            result = getMd5Hash(stringtohash);
            
            if (cert.VerifySignedHash(result, oldhash,Key))
                System.Windows.Forms.MessageBox.Show("Same");
            else
                 System.Windows.Forms.MessageBox.Show("Not same");
            /*if (verifymd5Hash(result,hashes) == true)
                System.Windows.Forms.MessageBox.Show("Same");
            else
                 System.Windows.Forms.MessageBox.Show("Not same");*/
            return result;

        }

        public string E(string toEncrypt)
        {
            string result = null;
            string hashes = getMd5Hash(toEncrypt);
            result = hashes;
            return result;
        }

        public string D(string stringtohash, string oldhash)
        {
            string result = null;
            string hashes = oldhash;
            result = getMd5Hash(stringtohash);

            if (verifymd5Hash(result,hashes) == true)
                System.Windows.Forms.MessageBox.Show("Same");
            else
                 System.Windows.Forms.MessageBox.Show("Not same");
            return result;

        }

    }
    }


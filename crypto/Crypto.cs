using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace crypto
{
    class Crypto
    {

        Getcert getcert = new Getcert();
        System.Security.Cryptography.TripleDESCryptoServiceProvider desc = new System.Security.Cryptography.TripleDESCryptoServiceProvider();

        public string getMd5Hash(string input)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
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

        public string Encrypt(string toEncrypt,string m, string pm)
        {
            byte[] keyArray;
            byte[] IV;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            string result = null;

                desc.GenerateIV();
                desc.GenerateKey();
                string keys = Convert.ToBase64String(desc.Key);
                string IVs = Convert.ToBase64String(desc.IV);
               var key = getcert.GetKey3(keys);

                //File.WriteAllText("SecurityKey", keys);
               File.WriteAllText("SecurityKey", key);
                File.WriteAllText("IV", IVs);

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                // keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(keys));
                //keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(toEncrypt));
                keyArray = hashmd5.ComputeHash(Encoding.Default.GetBytes(keys));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < keyArray.Length; i++)
                {
                    sBuilder.Append(keyArray[i].ToString("x2"));
                }
                File.WriteAllText("Hash", sBuilder.ToString());
                hashmd5.Clear();

                //keyArray = UTF8Encoding.UTF8.GetBytes(key);
                keyArray = Convert.FromBase64String(keys);
                IV = Convert.FromBase64String(IVs);


                desc.Key = keyArray;
                desc.IV = IV;
                if (m.Equals("ECB") == true)
                    desc.Mode = CipherMode.ECB;
                if (m.Equals("CBC") == true)
                    desc.Mode = CipherMode.CBC;
                if (m.Equals("CFB") == true)
                    desc.Mode = CipherMode.CFB;
                if (m.Equals("OFB") == true)
                    desc.Mode = CipherMode.OFB;

                if (pm.Equals("PKCS7") == true)
                    desc.Padding = PaddingMode.PKCS7;
                if (pm.Equals("Zeros") == true)
                    desc.Padding = PaddingMode.Zeros;
                if (pm.Equals("ANSIX923") == true)
                    desc.Padding = PaddingMode.ANSIX923;
                if (pm.Equals("ISO10126") == true)
                    desc.Padding = PaddingMode.ISO10126;

                ICryptoTransform cTransform = desc.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                desc.Clear();

               result = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            
            return result;
        }

        public string Decrypt(string cipherString,string m,string pm)
        {
            byte[] keyArray; byte[] IV;
            string result = null;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

           
            string key = File.ReadAllText("SecurityKey");
            string IVs = File.ReadAllText("IV");
            var keys = getcert.SetKey3(key);

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
               // keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(cipherString));
               // keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(keys));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < keyArray.Length; i++)
                {
                    sBuilder.Append(keyArray[i].ToString("x2"));
                }
                hashmd5.Clear();

              // keyArray = UTF8Encoding.UTF8.GetBytes(key);
               // keyArray = Convert.FromBase64String(key); 
            keyArray = Convert.FromBase64String(keys);
                IV = Convert.FromBase64String(IVs);


                    desc.Key = keyArray;
                    desc.IV = IV;

                    if (m.Equals("ECB") == true)
                        desc.Mode = CipherMode.ECB;
                    if (m.Equals("CBC") == true)
                        desc.Mode = CipherMode.CBC;
                    if (m.Equals("CFB") == true)
                        desc.Mode = CipherMode.CFB;
                    if (m.Equals("OFB") == true)
                        desc.Mode = CipherMode.OFB;

                    if (pm.Equals("PKCS7") == true)
                        desc.Padding = PaddingMode.PKCS7;
                    if (pm.Equals("Zeros") == true)
                        desc.Padding = PaddingMode.Zeros;
                    if (pm.Equals("ANSIX923") == true)
                        desc.Padding = PaddingMode.ANSIX923;
                    if (pm.Equals("ISO10126") == true)
                        desc.Padding = PaddingMode.ISO10126;

                    ICryptoTransform cTransform = desc.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    desc.Clear();
                    result = UTF8Encoding.UTF8.GetString(resultArray);
                
                return result;

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace crypto
{
    class RC2
    {

         Getcert getcert = new Getcert();
        System.Security.Cryptography.RC2CryptoServiceProvider rc2 = new System.Security.Cryptography.RC2CryptoServiceProvider();

        public string Encrypt(string toEncrypt,string m, string pm)
        {
            byte[] keyArray;
            byte[] IV;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            string result = null;

                rc2.GenerateIV();
                 rc2.GenerateKey();
                string keys = Convert.ToBase64String( rc2.Key);
                string IVs = Convert.ToBase64String( rc2.IV);
               var key = getcert.GetKey3(keys);

               File.WriteAllText("SecurityKey", key);
                File.WriteAllText("IV", IVs);

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.Default.GetBytes(keys));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < keyArray.Length; i++)
                {
                    sBuilder.Append(keyArray[i].ToString("x2"));
                }
                File.WriteAllText("Hash", sBuilder.ToString());
                hashmd5.Clear();

                keyArray = Convert.FromBase64String(keys);
                IV = Convert.FromBase64String(IVs);
            
                rc2.Key = keyArray;
                 rc2.IV = IV;
                if (m.Equals("ECB") == true)
                     rc2.Mode = CipherMode.ECB;
                if (m.Equals("CBC") == true)
                     rc2.Mode = CipherMode.CBC;
                if (m.Equals("CFB") == true)
                     rc2.Mode = CipherMode.CFB;
                if (m.Equals("OFB") == true)
                    rc2.Mode = CipherMode.OFB;

                if (pm.Equals("PKCS7") == true)
                    rc2.Padding = PaddingMode.PKCS7;
                if (pm.Equals("Zeros") == true)
                     rc2.Padding = PaddingMode.Zeros;
                if (pm.Equals("ANSIX923") == true)
                    rc2.Padding = PaddingMode.ANSIX923;
                if (pm.Equals("ISO10126") == true)
                     rc2.Padding = PaddingMode.ISO10126;

                ICryptoTransform cTransform =  rc2.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                 rc2.Clear();

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


                     rc2.Key = keyArray;
                     rc2.IV = IV;

                    if (m.Equals("ECB") == true)
                         rc2.Mode = CipherMode.ECB;
                    if (m.Equals("CBC") == true)
                         rc2.Mode = CipherMode.CBC;
                    if (m.Equals("CFB") == true)
                         rc2.Mode = CipherMode.CFB;
                    if (m.Equals("OFB") == true)
                         rc2.Mode = CipherMode.OFB;

                    if (pm.Equals("PKCS7") == true)
                         rc2.Padding = PaddingMode.PKCS7;
                    if (pm.Equals("Zeros") == true)
                         rc2.Padding = PaddingMode.Zeros;
                    if (pm.Equals("ANSIX923") == true)
                         rc2.Padding = PaddingMode.ANSIX923;
                    if (pm.Equals("ISO10126") == true)
                         rc2.Padding = PaddingMode.ISO10126;

                    ICryptoTransform cTransform =  rc2.CreateDecryptor();
                   // byte[] resultArrays = cTransform.TransformFinalBlock(toEncryptArray, toEncryptArray.Length - 64, toEncryptArray.Length);
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                     rc2.Clear();
                    result = UTF8Encoding.UTF8.GetString(resultArray);
                   //string  results = UTF8Encoding.UTF8.GetString(resultArrays);
                   //System.Windows.Forms.MessageBox.Show(results);
                return result;

        }

        }
    }


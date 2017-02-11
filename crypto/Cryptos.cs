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
    class Cryptos
    {
        Getcert getcert = new Getcert();
        public string Encrypt(string toEncrypt, string m, string pm, string keysize)
        {
            byte[] keyArray;
            byte[] IV;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Security.Cryptography.AesCryptoServiceProvider desc = new System.Security.Cryptography.AesCryptoServiceProvider();

            desc.KeySize = Convert.ToInt32(keysize);
            desc.GenerateIV();
            desc.GenerateKey();
            string keys = Convert.ToBase64String(desc.Key);
            string IVs = Convert.ToBase64String(desc.IV);
           // string keys = UTF8Encoding.UTF8.GetString(desc.Key);
            //string IVs = UTF8Encoding.UTF8.GetString(desc.IV);
            var key = getcert.GetKey6(keys);

            File.WriteAllText("SecurityKey", keys);
            File.WriteAllText("IV", IVs);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            // keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(keys));
            //keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(toEncrypt));
            keyArray = hashmd5.ComputeHash(Encoding.Default.GetBytes(key));
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
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
          // return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public string Decrypt(string cipherString, string m, string pm, string keysize)
        {
            byte[] keyArray; byte[] IV;
            bool flag = false;
           byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            //byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(cipherString);

            string keys = File.ReadAllText("SecurityKey");
            string IVs = File.ReadAllText("IV");
            var key = getcert.SetKey6(keys);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            // keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(cipherString));
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < keyArray.Length; i++)
            {
                sBuilder.Append(keyArray[i].ToString("x2"));
            }
            hashmd5.Clear();

            //keyArray = UTF8Encoding.UTF8.GetBytes(key);
            keyArray = Convert.FromBase64String(keys);
            IV = Convert.FromBase64String(IVs);
            

            System.Security.Cryptography.AesCryptoServiceProvider desc = new System.Security.Cryptography.AesCryptoServiceProvider();
            desc.KeySize = Convert.ToInt32(keysize);
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

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}

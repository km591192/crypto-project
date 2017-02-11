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
    class CryptAES
    {
        //http://stackoverflow.com/questions/273452/using-aes-encryption-in-c-sharp

        Getcert getcert = new Getcert();


        public byte[] encryptdata(byte[] bytearraytoencrypt, string key, string iv,string m, string pm)
        {
            AesCryptoServiceProvider dataencrypt = new AesCryptoServiceProvider(); 
            dataencrypt.BlockSize = 128; 
            dataencrypt.KeySize = 128; 
            dataencrypt.Key = System.Text.Encoding.UTF8.GetBytes(key); 
            dataencrypt.IV = System.Text.Encoding.UTF8.GetBytes(iv);
            if (m.Equals("ECB") == true)
                dataencrypt.Mode = CipherMode.ECB;
            if (m.Equals("CBC") == true)
                dataencrypt.Mode = CipherMode.CBC;
            if (m.Equals("CFB") == true)
                dataencrypt.Mode = CipherMode.CFB;
            if (m.Equals("OFB") == true)
                dataencrypt.Mode = CipherMode.OFB;

            if (pm.Equals("PKCS7") == true)
                dataencrypt.Padding = PaddingMode.PKCS7;
            if (pm.Equals("Zeros") == true)
                dataencrypt.Padding = PaddingMode.Zeros;
            if (pm.Equals("ANSIX923") == true)
                dataencrypt.Padding = PaddingMode.ANSIX923;
            if (pm.Equals("ISO10126") == true)
                dataencrypt.Padding = PaddingMode.ISO10126;
            ICryptoTransform crypto1 = dataencrypt.CreateEncryptor(dataencrypt.Key, dataencrypt.IV);  
            byte[] encrypteddata = crypto1.TransformFinalBlock(bytearraytoencrypt, 0, bytearraytoencrypt.Length);
            crypto1.Dispose(); 
            return encrypteddata;
        }

        private byte[] decryptdata(byte[] bytearraytodecrypt, string key, string iv,string m, string pm)
        {

            AesCryptoServiceProvider keydecrypt = new AesCryptoServiceProvider();
            keydecrypt.BlockSize = 128;
            keydecrypt.KeySize = 128;
            keydecrypt.Key = System.Text.Encoding.UTF8.GetBytes(key);
            keydecrypt.IV = System.Text.Encoding.UTF8.GetBytes(iv);
            if (m.Equals("ECB") == true)
                keydecrypt.Mode = CipherMode.ECB;
            if (m.Equals("CBC") == true)
                keydecrypt.Mode = CipherMode.CBC;
            if (m.Equals("CFB") == true)
                keydecrypt.Mode = CipherMode.CFB;
            if (m.Equals("OFB") == true)
                keydecrypt.Mode = CipherMode.OFB;

            if (pm.Equals("PKCS7") == true)
                keydecrypt.Padding = PaddingMode.PKCS7;
            if (pm.Equals("Zeros") == true)
                keydecrypt.Padding = PaddingMode.Zeros;
            if (pm.Equals("ANSIX923") == true)
                keydecrypt.Padding = PaddingMode.ANSIX923;
            if (pm.Equals("ISO10126") == true)
                keydecrypt.Padding = PaddingMode.ISO10126;
            ICryptoTransform crypto1 = keydecrypt.CreateDecryptor(keydecrypt.Key, keydecrypt.IV);

            byte[] returnbytearray = crypto1.TransformFinalBlock(bytearraytodecrypt, 0, bytearraytodecrypt.Length);
            crypto1.Dispose();
            return returnbytearray;
        }


        public string Encrypt(string toEncrypt, string m, string pm, string keysize)
        {
            byte[] keyArray;
            byte[] IV;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            System.Security.Cryptography.AesCryptoServiceProvider desc = new System.Security.Cryptography.AesCryptoServiceProvider();
          //  desc.KeySize = Convert.ToInt16(keysize);
           // desc.BlockSize = 128;
            desc.GenerateIV();
            desc.GenerateKey();
            string keys = Encoding.UTF8.GetString(desc.Key);
            string IVs = Encoding.UTF8.GetString(desc.IV);
          //  var key = getcert.GetKey6(keys);

            File.WriteAllText("SecurityKey", keys);
            File.WriteAllText("IV", IVs);

            byte[] data = encryptdata(toEncryptArray, keys, IVs, m, pm);
            string res = Encoding.UTF8.GetString(data);
            return res;

           /* RijndaelManaged myRijndael = new RijndaelManaged();
            myRijndael.GenerateKey();
            myRijndael.GenerateIV();
            string keys = UTF8Encoding.UTF8.GetString(myRijndael.Key);
            string IVs = UTF8Encoding.UTF8.GetString(myRijndael.IV);
           // var key = getcert.GetKey6(keys);

            File.WriteAllText("SecurityKey", keys);
            File.WriteAllText("IV", IVs);

            byte[] encrypted = EncryptStringToBytes(toEncrypt, myRijndael.Key, myRijndael.IV,m,pm);
            StringBuilder s = new StringBuilder();
            foreach (byte item in encrypted)
            {
                s.Append(item.ToString("X2") + " ");
            }
            System.Windows.Forms.MessageBox.Show("Encrypted:   " + s);
            return s.ToString();*/

            /*System.Security.Cryptography.AesCryptoServiceProvider desc = new System.Security.Cryptography.AesCryptoServiceProvider();

            desc.KeySize = Convert.ToInt16(keysize);
            desc.BlockSize = 128;
            desc.GenerateIV();
            desc.GenerateKey();
            //string keys = Convert.ToBase64String(desc.Key);
            //string IVs = Convert.ToBase64String(desc.IV);
            string keys = UTF8Encoding.UTF8.GetString(desc.Key);
            string IVs = UTF8Encoding.UTF8.GetString(desc.IV);
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
           // keyArray = Convert.FromBase64String(keys);
            //IV = Convert.FromBase64String(IVs);
            keyArray = UTF8Encoding.UTF8.GetBytes(keys);
            IV = UTF8Encoding.UTF8.GetBytes(IVs);


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
            //return Convert.ToBase64String(resultArray, 0, resultArray.Length);
           return UTF8Encoding.UTF8.GetString(resultArray);*/
        }
        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV, string m, string pm)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                if (m.Equals("ECB") == true)
                    rijAlg.Mode = CipherMode.ECB;
                if (m.Equals("CBC") == true)
                    rijAlg.Mode = CipherMode.CBC;
                if (m.Equals("CFB") == true)
                    rijAlg.Mode = CipherMode.CFB;
                if (m.Equals("OFB") == true)
                    rijAlg.Mode = CipherMode.OFB;

                if (pm.Equals("PKCS7") == true)
                    rijAlg.Padding = PaddingMode.PKCS7;
                if (pm.Equals("Zeros") == true)
                    rijAlg.Padding = PaddingMode.Zeros;
                if (pm.Equals("ANSIX923") == true)
                    rijAlg.Padding = PaddingMode.ANSIX923;
                if (pm.Equals("ISO10126") == true)
                    rijAlg.Padding = PaddingMode.ISO10126;

                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                           swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        public string Decrypt(string cipherString, string m, string pm, string keysize)
        {
            byte[] keyArray; byte[] IV;
            bool flag = false;
           // byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(cipherString);

            string keys = File.ReadAllText("SecurityKey");
            string IVs = File.ReadAllText("IV");
           // var key = getcert.SetKey6(keys);

            byte[] data = decryptdata(toEncryptArray, keys, IVs, m, pm);
            string res = Encoding.UTF8.GetString(data);
            return res;

            /*string decrypted = DecryptStringFromBytes(toEncryptArray, UTF8Encoding.UTF8.GetBytes(keys), UTF8Encoding.UTF8.GetBytes(IVs),m,pm);
            System.Windows.Forms.MessageBox.Show("Decrypted:    " + decrypted);
            return decrypted;*/

            /*MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            // keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(cipherString));
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < keyArray.Length; i++)
            {
                sBuilder.Append(keyArray[i].ToString("x2"));
            }
            hashmd5.Clear();

            //keyArray = UTF8Encoding.UTF8.GetBytes(key);
          //  keyArray = Convert.FromBase64String(keys);
           // IV = Convert.FromBase64String(IVs);
            keyArray = UTF8Encoding.UTF8.GetBytes(key);
            IV = UTF8Encoding.UTF8.GetBytes(IVs);

            System.Security.Cryptography.AesCryptoServiceProvider desc = new System.Security.Cryptography.AesCryptoServiceProvider();
            desc.KeySize = Convert.ToInt16(keysize);
            desc.BlockSize = 128;
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
            p
            return UTF8Encoding.UTF8.GetString(resultArray);*/
        }

        

    static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV, string m, string pm )
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("Key");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an RijndaelManaged object
        // with the specified key and IV.
        using (RijndaelManaged rijAlg = new RijndaelManaged())
        {
            rijAlg.Key = Key;
            rijAlg.IV = IV;
            if (m.Equals("ECB") == true)
                rijAlg.Mode = CipherMode.ECB;
            if (m.Equals("CBC") == true)
                rijAlg.Mode = CipherMode.CBC;
            if (m.Equals("CFB") == true)
                rijAlg.Mode = CipherMode.CFB;
            if (m.Equals("OFB") == true)
                rijAlg.Mode = CipherMode.OFB;

            if (pm.Equals("PKCS7") == true)
                rijAlg.Padding = PaddingMode.PKCS7;
            if (pm.Equals("Zeros") == true)
                rijAlg.Padding = PaddingMode.Zeros;
            if (pm.Equals("ANSIX923") == true)
                rijAlg.Padding = PaddingMode.ANSIX923;
            if (pm.Equals("ISO10126") == true)
                rijAlg.Padding = PaddingMode.ISO10126;

            // Create a decrytor to perform the stream transform.
            ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

        }

        return plaintext;
     }
   }
    }


    

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Security.Permissions;


namespace crypto
{
    class Getcert
    {
        ASCIIEncoding ByteConverter = new ASCIIEncoding();
        RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

        public string Sign(string dataString, RSAParameters Key)
        {

            Key = RSAalg.ExportParameters(true);
            RSAalg.ImportParameters(Key);
            byte[] originalData = ByteConverter.GetBytes(dataString);
            return RSAalg.SignData(originalData, new MD5CryptoServiceProvider()).ToString();
        }

        public bool VerifySignedHash(string toverify, string signed, RSAParameters Key)
         {
             byte[] DataToVerify = ByteConverter.GetBytes(toverify);
             byte[] SignedData = ByteConverter.GetBytes(signed);
            RSAalg.ImportParameters(Key);
            return RSAalg.VerifyData(DataToVerify, new SHA1CryptoServiceProvider(), SignedData); 

        }

        public string SetKeyhash(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PublicKey.Key;
            byte[] data;
            data = ByteConverter.GetBytes(key);
           // data = Convert.FromBase64String(key);
            //string res = Convert.ToBase64String(rsaprkey.Decrypt(UTF8Encoding.UTF8.GetBytes(key), true));
            // string res = Convert.ToBase64String(rsaprkey.Decrypt(data, false));
            string res = ByteConverter.GetString(rsaprkey.Decrypt(data, false));


            certificatesStore.Close();

            return res;
        }

        public string GetKeyhash(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PrivateKey;
            var keypub = cert.GetPublicKeyString().ToString();

            string res = ByteConverter.GetString(rsaprkey.Encrypt(UTF8Encoding.UTF8.GetBytes(key), false));

            certificatesStore.Close();

            return res;
        }

        public string GetKey6(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PrivateKey;
            byte[] data,encryptkey;
            data = UTF8Encoding.UTF8.GetBytes(key);
            RSAPKCS1KeyExchangeFormatter keyFormatter = new RSAPKCS1KeyExchangeFormatter(rsaprkey);
            encryptkey = keyFormatter.CreateKeyExchange(data);
            string res = Encoding.UTF8.GetString(encryptkey);
            // data = Convert.FromBase64String(key);
            //string res = Convert.ToBase64String(rsaprkey.Decrypt(UTF8Encoding.UTF8.GetBytes(key), true));
            // string res = Convert.ToBase64String(rsaprkey.Decrypt(data, false));
           // string res = Encoding.UTF8.GetString(rsaprkey.Decrypt(data, false));


            certificatesStore.Close();

            return res;
        }

        public string SetKey6(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PublicKey.Key;
            var keypub = cert.GetPublicKeyString().ToString();

            byte[] data,decryptkey;
            data = Encoding.UTF8.GetBytes(key);
            RSAPKCS1KeyExchangeDeformatter keyDeformatter = new RSAPKCS1KeyExchangeDeformatter(rsaprkey);
            decryptkey = keyDeformatter.DecryptKeyExchange(data);
            string res = Encoding.UTF8.GetString(decryptkey);
            // string res = Convert.ToBase64String(rsaprkey.Encrypt(data, false));
            //string res = Encoding.UTF8.GetString(rsaprkey.Encrypt(data, false));

            certificatesStore.Close();

            return res;
        }
        

        public string SetKey5(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PrivateKey;
            byte[] data;
            data = UTF8Encoding.UTF8.GetBytes(key);
           // data = Convert.FromBase64String(key);
            //string res = Convert.ToBase64String(rsaprkey.Decrypt(UTF8Encoding.UTF8.GetBytes(key), true));
            // string res = Convert.ToBase64String(rsaprkey.Decrypt(data, false));
            string res = Encoding.UTF8.GetString(rsaprkey.Decrypt(data, false));


            certificatesStore.Close();

            return res;
        }

        public string GetKey5(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PublicKey.Key;
            var keypub = cert.GetPublicKeyString().ToString();

            byte[] data;
            data = Encoding.UTF8.GetBytes(key);
           // string res = Convert.ToBase64String(rsaprkey.Encrypt(data, false));
            string res = Encoding.UTF8.GetString(rsaprkey.Encrypt(data, false));

            certificatesStore.Close();

            return res;
        }
        

        public string SetKey4(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PrivateKey;
            byte[] data;
            //data = UTF8Encoding.UTF8.GetBytes(key);
            data = Convert.FromBase64String(key);
            //string res = Convert.ToBase64String(rsaprkey.Decrypt(UTF8Encoding.UTF8.GetBytes(key), true));
            // string res = Convert.ToBase64String(rsaprkey.Decrypt(data, false));
            string res = Encoding.UTF8.GetString(rsaprkey.Decrypt(data, /*false*/true));


            certificatesStore.Close();

            return res;
        }

        public string GetKey4(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PublicKey.Key;
            var keypub = cert.GetPublicKeyString().ToString();

            byte[] data;
            data = Encoding.UTF8.GetBytes(key);
            string res = Convert.ToBase64String(rsaprkey.Encrypt(data, false));

            certificatesStore.Close();

            return res;
        }
        

        public string SetKey3(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PrivateKey;
            byte[] data;
            //data = UTF8Encoding.UTF8.GetBytes(key);
            data = Convert.FromBase64String(key);
            //string res = Convert.ToBase64String(rsaprkey.Decrypt(UTF8Encoding.UTF8.GetBytes(key), true));
           // string res = Convert.ToBase64String(rsaprkey.Decrypt(data, false));
            string res = UTF8Encoding.UTF8.GetString(rsaprkey.Decrypt(data, false));
            

            certificatesStore.Close();

            return res;
        }

        public string GetKey3(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PublicKey.Key;
            var keypub = cert.GetPublicKeyString().ToString();

            string res = Convert.ToBase64String(rsaprkey.Encrypt(UTF8Encoding.UTF8.GetBytes(key), false));

            certificatesStore.Close();

            return res;
        }
        

        public string SetKey2(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PrivateKey;

            string res = Convert.ToBase64String(rsaprkey.Decrypt(UTF8Encoding.UTF8.GetBytes(key), true));

            certificatesStore.Close();

            return res;
        }

        public string GetKey2(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PublicKey.Key;
            var keypub = cert.GetPublicKeyString().ToString();

            string res = Convert.ToBase64String(rsaprkey.Encrypt(UTF8Encoding.UTF8.GetBytes(key), true));

            certificatesStore.Close();

            return res;
        }

        public string SetKey(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PrivateKey;
            RSACryptoServiceProvider rsapubkey = (RSACryptoServiceProvider)cert.PublicKey.Key;

            if (cert.HasPrivateKey == true)
            {
                System.Windows.Forms.MessageBox.Show("public key = \n" + cert.GetPublicKeyString().ToString());
            }

            /* byte[] keys = UTF8Encoding.UTF8.GetBytes(key);
             rsapubkey.ExportCspBlob(keys);
            rsapubkey.ImportCspBlob(keys);

              string result;
             result = Convert.ToBase64String(..., 0, keys.Length);
             result = rsapubkey.ToXmlString(false);
             System.Windows.Forms.MessageBox.Show(result);*/


            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSAParameters RSAParams = RSA.ExportParameters(false);
            RSACryptoServiceProvider RSA2 = new RSACryptoServiceProvider();
            RSA2.ImportParameters(RSAParams);

///----!!!!!!!!!
         /* var rsa1 = new RSACryptoServiceProvider();
            string publicPrivateXml = rsa1.ToXmlString(true);
           byte[] toEncryptData = Encoding.ASCII.GetBytes(key);
          // byte[] encryptedRSA = rsa1.Encrypt(toEncryptData, true); //(toEncryptData, false);
           // string EncryptedResult = Encoding.Default.GetString(encryptedRSA);

           var rsa2 = new RSACryptoServiceProvider();
           string res = publicPrivateXml.Substring(22, 172);
            rsa2.FromXmlString(res);
          //  rsa2.ToXmlString(publicPrivateXml);
          //  byte[] decryptedRSA = rsa2.Decrypt(encryptedRSA, false);
            byte[] decryptedRSA = rsa2.Decrypt(toEncryptData, true);
            string originalResult = Encoding.Default.GetString(decryptedRSA);*/

            var rsa = new RSACryptoServiceProvider();
            /*var enc = new ASCIIEncoding();

            byte[] data = enc.GetBytes(key);
           // byte[] encrypted = rsa.Encrypt(data, true);
           // byte[] decrypted = rsa.Decrypt(encrypted, true);
            byte[] decrypted = rsa.Decrypt(data, true);

            string originalResult = enc.GetString(decrypted);*/

            byte[] data = UTF8Encoding.UTF8.GetBytes(key);
            byte[] decrypted = rsa.Decrypt(data, true);

              string originalResult = Convert.ToBase64String(decrypted);

            certificatesStore.Close();

            return originalResult;

        }

        public string GetKey(string key)
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0];
            RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PrivateKey;
            RSACryptoServiceProvider rsapubkey = (RSACryptoServiceProvider)cert.PublicKey.Key;

            if (cert.HasPrivateKey == true)
            {
                System.Windows.Forms.MessageBox.Show("public key = \n" + cert.GetPublicKeyString().ToString());
            }

           /* byte[] keys = UTF8Encoding.UTF8.GetBytes(key);
            rsapubkey.ExportCspBlob(keys);
           rsapubkey.ImportCspBlob(keys);

             string result;
            result = Convert.ToBase64String(..., 0, keys.Length);
            result = rsapubkey.ToXmlString(false);
            System.Windows.Forms.MessageBox.Show(result);*/


            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAParams = RSA.ExportParameters(false);
            RSACryptoServiceProvider RSA2 = new RSACryptoServiceProvider();
             RSA2.ImportParameters(RSAParams);


           /*  var rsa1 = new RSACryptoServiceProvider();
             string publicPrivateXml = rsa1.ToXmlString(true); //false 
             byte[] toEncryptData = Encoding.ASCII.GetBytes(key);
             byte[] encryptedRSA = rsa1.Encrypt(toEncryptData, true);//false
             string EncryptedResult = Encoding.Default.GetString(encryptedRSA);*/

             /*var rsa2 = new RSACryptoServiceProvider();
             rsa2.FromXmlString(publicPrivateXml);
             byte[] decryptedRSA = rsa2.Decrypt(encryptedRSA, false);
             string originalResult = Encoding.Default.GetString(decryptedRSA);*/

             var rsa = new RSACryptoServiceProvider();
             /*var enc = new ASCIIEncoding();

             byte[] data = enc.GetBytes(key);
             byte[] encrypted = rsa.Encrypt(data, true);
             string EncryptedResult = enc.GetString(encrypted);*/

             byte[] data = UTF8Encoding.UTF8.GetBytes(key);
             byte[] encrypted = rsa.Encrypt(data, true);
             string EncryptedResult = Convert.ToBase64String(encrypted);

            certificatesStore.Close();

            return EncryptedResult;

        }


        public X509Certificate2Collection FindCerts()
        {
            string serialNumber = "035f9a2f3114d2e7c2d677935f6cee2e4d095316";
            serialNumber = serialNumber.Replace(" ", string.Empty).ToUpper();
            //-----var searchType = X509FindType.FindBySerialNumber;
            var searchType = X509FindType.FindByThumbprint;
            var storeName = "MY";

            var certificatesStore = new X509Store(storeName, StoreLocation.CurrentUser);
            certificatesStore.Open(OpenFlags.OpenExistingOnly);

            var matchingCertificates = certificatesStore.Certificates.Find(searchType,
                                                                            serialNumber.Replace("\u200e", string.Empty).Replace("\u200f", string.Empty).Replace(" ", string.Empty),
                                                                            false);

            if (matchingCertificates.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("no");
            }

            X509Certificate2 cert = matchingCertificates[0]; 
            System.Windows.Forms.MessageBox.Show(matchingCertificates[0].ToString());
           RSACryptoServiceProvider rsaprkey = (RSACryptoServiceProvider)cert.PrivateKey;
           
            if (cert.HasPrivateKey == true)
            {
                System.Windows.Forms.MessageBox.Show("private key = \n" + cert.PrivateKey.ToXmlString(false));
                System.Windows.Forms.MessageBox.Show("private key = " + rsaprkey.ToXmlString(false));
                System.Windows.Forms.MessageBox.Show("public key = \n" + cert.GetPublicKeyString().ToString());
                var resultstr = rsaprkey.ToXmlString(false).Substring(22, 172);
                System.Windows.Forms.MessageBox.Show("private key = \n" + resultstr);  
            }
            

            certificatesStore.Close();

            return matchingCertificates;
        }


        public string EncryptString(string inputString, int dwKeySize,
                             string xmlString)
        {
            // TODO: Add Proper Exception Handlers
            RSACryptoServiceProvider rsaCryptoServiceProvider =
                                          new RSACryptoServiceProvider(dwKeySize);
            rsaCryptoServiceProvider.FromXmlString(xmlString);
            int keySize = dwKeySize / 8;
            byte[] bytes = Encoding.UTF32.GetBytes(inputString);
            // The hash function in use by the .NET RSACryptoServiceProvider here 
            // is SHA1
            // int maxLength = ( keySize ) - 2 - 
            //              ( 2 * SHA1.Create().ComputeHash( rawBytes ).Length );
            int maxLength = keySize - 42;
            int dataLength = bytes.Length;
            int iterations = dataLength / maxLength;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i <= iterations; i++)
            {
                byte[] tempBytes = new byte[
                        (dataLength - maxLength * i > maxLength) ? maxLength :
                                                      dataLength - maxLength * i];
                Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0,
                                  tempBytes.Length);
                byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes,
                                                                          true);
                // Be aware the RSACryptoServiceProvider reverses the order of 
                // encrypted bytes. It does this after encryption and before 
                // decryption. If you do not require compatibility with Microsoft 
                // Cryptographic API (CAPI) and/or other vendors. Comment out the 
                // next line and the corresponding one in the DecryptString function.
                Array.Reverse(encryptedBytes);
                // Why convert to base 64?
                // Because it is the largest power-of-two base printable using only 
                // ASCII characters
                stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
            }
            return stringBuilder.ToString();
        }

        public string DecryptString(string inputString, int dwKeySize,
                                     string xmlString)
        {
            // TODO: Add Proper Exception Handlers
            RSACryptoServiceProvider rsaCryptoServiceProvider
                                     = new RSACryptoServiceProvider(dwKeySize);
            rsaCryptoServiceProvider.FromXmlString(xmlString);
            int base64BlockSize = ((dwKeySize / 8) % 3 != 0) ?
              (((dwKeySize / 8) / 3) * 4) + 4 : ((dwKeySize / 8) / 3) * 4;
            int iterations = inputString.Length / base64BlockSize;
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < iterations; i++)
            {
                byte[] encryptedBytes = Convert.FromBase64String(
                     inputString.Substring(base64BlockSize * i, base64BlockSize));
                // Be aware the RSACryptoServiceProvider reverses the order of 
                // encrypted bytes after encryption and before decryption.
                // If you do not require compatibility with Microsoft Cryptographic 
                // API (CAPI) and/or other vendors.
                // Comment out the next line and the corresponding one in the 
                // EncryptString function.
                Array.Reverse(encryptedBytes);
                arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(
                                    encryptedBytes, true));
            }
            return Encoding.UTF32.GetString(arrayList.ToArray(
                                      Type.GetType("System.Byte")) as byte[]);
        }
    }

}

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
    class HashCert
    {
        public byte[] Sign(string text)
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
           
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            UnicodeEncoding encoding = new UnicodeEncoding();

            byte[] data = encoding.GetBytes(text);

            byte[] hash = md5.ComputeHash(data);

            return rsaprkey.SignHash(hash, CryptoConfig.MapNameToOID("MD5"));

        }


        public bool Verify(string text, byte[] signature)
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

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            UnicodeEncoding encoding = new UnicodeEncoding();

            byte[] data = encoding.GetBytes(text);

            byte[] hash = md5.ComputeHash(data);


            return rsaprkey.VerifyHash(hash, CryptoConfig.MapNameToOID("MD5"), signature);

        }




    }
}

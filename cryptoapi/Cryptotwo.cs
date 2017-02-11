using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace cryptoapi
{
    class Cryptotwo
    {
        static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                foreach (var key in appSettings.AllKeys) { MessageBox.Show(key, appSettings[key]); }
            }

            catch (ConfigurationErrorsException) { }
        }
        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + @"App.config";
                Configuration configFile = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                // var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null) { settings.Add(key, value); MessageBox.Show("Key add"); }
                else { settings[key].Value = value; }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException) { MessageBox.Show("Error"); }
        }

        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            System.Security.Cryptography.TripleDESCryptoServiceProvider desc = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            // desc.GenerateKey();
            //string keys = Convert.ToBase64String(desc.Key);

            // File.WriteAllText("SecurityKey", keys);
            /*
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["SecurityKey"].Value = keys;
            config.Save(ConfigurationSaveMode.Modified);
            MessageBox.Show(ConfigurationManager.AppSettings["SecurityKey"]);*/

            //AddUpdateAppSettings("SecurityKey", keys);
            //ReadAllSettings();

            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            System.Windows.Forms.MessageBox.Show(key);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                //keyArray = UTF8Encoding.UTF8.GetBytes(key);
                keyArray = Convert.FromBase64String(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));


            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                //   keyArray = UTF8Encoding.UTF8.GetBytes(key);
                keyArray = Convert.FromBase64String(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);

            /*string Result;
            try
            {
                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                Result = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            finally
            {
                tdes.Clear();
            }
            
            /*
            byte[] Results;
            byte[] DataToDecrypt = Convert.FromBase64String(cipherString);
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            TDESAlgorithm.IV = new byte[TDESAlgorithm.BlockSize / 8];
            TDESAlgorithm.Key = keyArray;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;


            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
            }

            return UTF8Encoding.UTF8.GetString(Results);*/

        }
    }
}

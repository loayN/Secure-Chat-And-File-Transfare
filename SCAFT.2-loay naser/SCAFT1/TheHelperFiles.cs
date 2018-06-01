using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SCAFT1
{
    class TheHelperFiles
    {

        public static String password;
        public static String hmacSharedKey;
        public static string fileHmac(string filename)
        {
            byte[] hashedfilehmac = new byte[32];
            FileStream fs = new FileStream(filename, FileMode.Open);
            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(hmacSharedKey));
            var vhashedfilehmac = hmac.ComputeHash(fs);
            return Convert.ToBase64String(vhashedfilehmac);
        }
        public static byte[] EncryptMessage(byte[] chunk)
        {
            RijndaelManaged aes = new RijndaelManaged();
            try
            {
                byte[] Key = passwordConverted();
                aes.Key = Key;
                aes.Mode = CipherMode.CBC;
                aes.GenerateIV();
                byte[] iv = aes.IV;
                aes.Padding = PaddingMode.PKCS7;
                using (MemoryStream ms = new MemoryStream())
                {

                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(Key, iv), CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(chunk);
                            sw.Close();
                        }
                        cs.Close();
                    }

                    byte[] encodedMssage = ms.ToArray();
                    byte[] rv = new byte[iv.Length + encodedMssage.Length];
                    System.Buffer.BlockCopy(iv, 0, rv, 0, iv.Length);
                    System.Buffer.BlockCopy(encodedMssage, 0, rv, iv.Length, encodedMssage.Length);
                    aes.Clear();
                    return rv;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
                return null;
            }

        }
        public static byte[] passwordConverted()
        {
            return new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static byte[] DecryptMessage(byte[] chunk)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                Aes aes = new AesManaged();

                byte[] IV = new byte[aes.BlockSize / 8];
                byte[] decrypted = new byte[chunk.Length - IV.Length];

                Array.Copy(chunk, IV, IV.Length);
                Array.Copy(chunk, IV.Length, decrypted, 0, decrypted.Length);

                aes.Key = passwordConverted();
                aes.IV = IV;

                CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);

                BinaryWriter bw = new BinaryWriter(cs);

                bw.Write(decrypted, 0, decrypted.Length);

                bw.Close();
                return (decrypted);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Cryptographic error -> " + e.Message);
                return null;
            }
        }

        public static void encryptFile(string fileName)
        {
            byte[] salt = GenerateRandomSalt();

            RijndaelManaged aes = new RijndaelManaged();
            try
            {
                byte[] Key = passwordConverted();
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                byte[] buffer = new byte[4096];
                var key = new Rfc2898DeriveBytes(Key, salt, 50000);

                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);

                int read;
                FileStream fsIn = new FileStream(fileName, FileMode.Open);
                FileStream fsOut = new FileStream(fileName + ".encrypted", FileMode.Create);
                fsOut.Write(salt, 0, salt.Length);

                CryptoStream cs = new CryptoStream(fsOut, aes.CreateEncryptor(), CryptoStreamMode.Write);



                try
                {
                    while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cs.Write(buffer, 0, read);
                    }

                    fsIn.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                cs.Close();
                fsOut.Close();

                aes.Clear();

            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
        }

        public static void decryptFile(string fileName)
        {

            byte[] passwordBytes = passwordConverted();
            byte[] salt = new byte[32];

            FileStream fsCrypt = new FileStream(fileName, FileMode.Open);
            fsCrypt.Read(salt, 0, salt.Length);

            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CBC;
            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);
            string[] recevedname = fileName.Split('.');
            string savingName = recevedname[recevedname.Length - 3] + "." + recevedname[recevedname.Length - 2];
            FileStream fsOut = new FileStream(savingName, FileMode.Create);

            int read;
            byte[] buffer = new byte[4096];

            try
            {
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fsOut.Write(buffer, 0, read);
                }

            }
            catch (System.Security.Cryptography.CryptographicException ex_CryptographicException)
            {
                //Debug.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine("Error: " + ex.Message);
            }

            try
            {
                cs.Close();
            }
            catch (Exception ex)
            {
                //Debug.WriteLine("Error by closing CryptoStream: " + ex.Message);
            }
            finally
            {
                fsOut.Close();
                fsCrypt.Close();
            }

        }

        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    rng.GetBytes(data);
                }
            }
            return data;
        }
        public static bool fileCheckHmac(string hasMsg, string encryptMsg)
        {
            if (hasMsg.Equals(encryptMsg)) return true;
            else return false;
            throw new NotImplementedException();
        }


    }
}

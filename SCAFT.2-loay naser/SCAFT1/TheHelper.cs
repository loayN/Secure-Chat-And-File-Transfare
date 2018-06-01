using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SCAFT1
{
    class TheHelper
    {
        public static String password, hmacSharedkey;

        public static string EncryptMessageWithoutHmac(string message)
        {
            RijndaelManaged aes = new RijndaelManaged();
            string messageEncrypt = null;
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
                            sw.Write(message);
                            sw.Close();
                        }
                        cs.Close();
                    }
                    byte[] encodedMssage = ms.ToArray();
                    byte[] rv = new byte[iv.Length + encodedMssage.Length];
                    System.Buffer.BlockCopy(iv, 0, rv, 0, iv.Length);
                    System.Buffer.BlockCopy(encodedMssage, 0, rv, iv.Length, encodedMssage.Length);
                    messageEncrypt = Convert.ToBase64String(rv);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            aes.Clear();
            return messageEncrypt;
        }


        public static string EncryptMessage(string message)
        {
            RijndaelManaged aes = new RijndaelManaged();
            string messageEncrypt = null;
            string hmacAndEncrytStr = null;
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
                            sw.Write(message);
                            sw.Close();
                        }
                        cs.Close();
                    }
                    byte[] encodedMssage = ms.ToArray();
                    byte[] rv = new byte[iv.Length + encodedMssage.Length];
                    System.Buffer.BlockCopy(iv, 0, rv, 0, iv.Length);
                    System.Buffer.BlockCopy(encodedMssage, 0, rv, iv.Length, encodedMssage.Length);
                    messageEncrypt = Convert.ToBase64String(rv);

                    byte[] hashedMsg = new byte[32];
                    HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(hmacSharedkey));
                    var vhashedMsg = hmac.ComputeHash(rv);

                    byte[] hmacAndEncryt = new byte[vhashedMsg.Length + rv.Length];
                    Array.Copy(vhashedMsg, 0, hmacAndEncryt, 0, vhashedMsg.Length);
                    Array.Copy(rv, 0, hmacAndEncryt, vhashedMsg.Length, rv.Length);

                    hmacAndEncrytStr = Convert.ToBase64String(hmacAndEncryt);
                    Console.WriteLine(hmacAndEncryt);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            aes.Clear();
            return hmacAndEncrytStr;

        }
        public static byte[] passwordConverted()
        {
            return new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static string DecryptMessage(string encMsg)
        {


            byte[] key = passwordConverted();
            string dycryptedMsg = null;
            byte[] theThing = Convert.FromBase64String(encMsg);



            var iv = new byte[16];
            System.Buffer.BlockCopy(theThing, 0, iv, 0, iv.Length);
            byte[] newMsgArray = theThing.Skip(16).ToArray();

            try
            {
                using (RijndaelManaged aes =
                    new RijndaelManaged { Key = key, IV = iv, Mode = CipherMode.CBC })

                using (MemoryStream ms =
                    new MemoryStream(newMsgArray))

                using (CryptoStream cs =
                    new CryptoStream(ms, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read))

                {
                    dycryptedMsg = new StreamReader(cs).ReadToEnd();
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Cryptographic error -> " + e.Message);
                return null;
            }
            return (dycryptedMsg);
        }

        /// <summary>
        /// separateHmac takes a msg and seperate the hmac from it 
        /// </summary>
        /// <param name="encMsgWithHmac"> the wncrypted msg -> HMAC+IV+encrypted msg </param>
        /// <returns></returns>
        public static string separateHmac(string encMsgWithHmac)
        {

            byte[] theThing = Convert.FromBase64String(encMsgWithHmac);
            byte[] hasMsg = new byte[32];
            byte[] encryptMsg = new byte[theThing.Length - hasMsg.Length];
            Array.Copy(theThing, hasMsg, hasMsg.Length);
            Array.Copy(theThing, hasMsg.Length, encryptMsg, 0, encryptMsg.Length);


            //return theMsgAfterDecryption;
            if (checkHmac(hasMsg, encryptMsg))
            {
                return DecryptMessage(Convert.ToBase64String(encryptMsg)); ;
            }
            else
            {
                string r = DecryptMessage(Convert.ToBase64String(encryptMsg));
                String[] m = r.Split('-');

                if (!m[0].Equals("HELLO"))
                {
                    new Thread(() =>
                    {
                        LogClass.Log("unmatching HMAC in the text message \r\n message details: \r\n " + r);
                    }).Start();
                }

                return (DecryptMessage(Convert.ToBase64String(encryptMsg)) + " - HMAC doesn't match");
            }

        }
        /// <summary>
        /// compare between the sent hash and the new calculated hash 
        /// </summary>
        /// <param name="hasMsg"></param>
        /// <param name="encryptMsg"></param>
        /// <returns>
        /// return true : in case the msg correct, the sent hash and the calculated is the same 
        /// else return false
        /// </returns>
        public static bool checkHmac(byte[] hasMsg, byte[] encryptMsg)
        {

            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(hmacSharedkey));
            var hashedMsg = hmac.ComputeHash(encryptMsg);
            string hasMsgStr, hashedMsgStr;
            hasMsgStr = Convert.ToBase64String(hasMsg);
            hashedMsgStr = Convert.ToBase64String(hashedMsg);
            if (hashedMsgStr.Equals(hasMsgStr)) return true;
            else return false;
            throw new NotImplementedException();
        }
    }
}

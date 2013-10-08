using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web;

namespace IPDetectServer.Lib.Encrypt
{
    public static class AESHelper
    {
        private const string SALT = "A220343JwIjl12";
        
        public static byte[] Encrypt(string plainText, string sharedSecret)
        {
            var salt = Encoding.ASCII.GetBytes(SALT);
            var passwordBytes = new PasswordDeriveBytes(sharedSecret, salt);
            var key = passwordBytes.GetBytes(32);
            var iv = passwordBytes.GetBytes(16);
            var result = EncryptToBytes(plainText, key, iv);
            return result;
        }

        public static byte[] EncryptToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        public static string Decrypt(byte[] cipherBytes, string sharedSecret)
        {
            var salt = Encoding.ASCII.GetBytes(SALT);
            var passwordBytes = new PasswordDeriveBytes(sharedSecret, salt);
            var key = passwordBytes.GetBytes(32);
            var iv = passwordBytes.GetBytes(16);
            var result = DecryptFromBytes(cipherBytes, key, iv);
            return result;
        }

        public static string DecryptFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

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

        private static byte[] GetKey(string sharedSecret)
        {
            var salt = Encoding.ASCII.GetBytes("A220343JwIjl12");
            int iterations = 1100;
            iterations += sharedSecret.Length;
            var tempResult = new Rfc2898DeriveBytes(sharedSecret, salt, iterations);
            var result = tempResult.GetBytes(32);
            return result;
        }
    }
}

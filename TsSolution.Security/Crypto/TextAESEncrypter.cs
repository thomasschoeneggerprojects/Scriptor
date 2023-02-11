using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TsSolution.Security.Crypto
{
    public class TextAESEncrypter
    {
        // 64 BIT Key
        private static readonly byte[] Key = {
            0x23, 0xaa, 0x55, 0x2f, 0x85, 0xb9, 0x3d, 0xad,
            0x22, 0x5a, 0x4a, 0x77, 0x8e, 0x23, 0xd1, 0x2a,
            0x34, 0xbc, 0x8b, 0x04, 0xd9, 0xb7, 0xaa, 0x50,
            0xa4, 0xe6, 0x88, 0x10, 0x7a, 0x99, 0xe2, 0x61 };

        // 32 BIT IV
        private static readonly byte[] IV = {
            0xf2, 0xd3, 0xee, 0xa1, 0x33, 0x45, 0x66, 0xe4,
            0x34, 0xa7, 0x99, 0x88, 0x69, 0x6b, 0xaf, 0xcf };

        /// <summary>
        /// Padding Mode
        /// </summary>
        private static readonly PaddingMode Padding = PaddingMode.PKCS7;

        /// <summary>
        /// Encrypt Data
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static string EncryptData(string original)
        {
            return EncryptStringToBytes(original, Key, IV);
        }

        /// <summary>
        /// Decrypt Data
        /// </summary>
        /// <param name="encrypted"></param>
        /// <returns></returns>
        public static string DecryptData(string encrypted)
        {
            return DecryptStringFromBytes(encrypted, Key, IV);
        }

        /// <summary>
        /// Encrypt String To Bytes
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        private static string EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException($"{nameof(TextAESEncrypter)}.{nameof(EncryptStringToBytes)} " +
                    $"need a \"{nameof(plainText)}\" for encryption.");

            string encrypted = string.Empty;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Padding = Padding;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor();
                byte[] planbytes = Encoding.UTF8.GetBytes(plainText);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        cs.Write(planbytes, 0, planbytes.Length);
                    encrypted = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encrypted;
        }

        /// <summary>
        /// Decrypt String From Bytes
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        private static string DecryptStringFromBytes(string cipherText, byte[] Key, byte[] IV)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException($"{nameof(TextAESEncrypter)}.{nameof(EncryptStringToBytes)} " +
                   $"need a \"{nameof(cipherText)}\" for decryption.");

            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            string plaintext = string.Empty;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Padding = Padding;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor();

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                    plaintext = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            return plaintext;
        }
    }
}
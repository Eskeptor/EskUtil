// ======================================================================================================
// File Name        : CryptoUtil.cs
// Project          : CSUtil
// Last Update      : 2025.05.19 - yc.jeon
// ======================================================================================================

using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace CSUtil
{
    public static class CryptoUtil
    {
        private const string KEY = "@ESK@UTIL&CRYPTOKEY_ENCP@NGPOKEY";
        private readonly static byte[] KEY_SALT = Encoding.ASCII.GetBytes(KEY);
        private const int KEY_SIZE = 256;
        private const int BLOCK_SIZE = 128;

        /// <summary>
        /// AES 암호화
        /// </summary>
        /// <param name="originData">암호화 할 데이터</param>
        /// <returns>암호화 된 데이터</returns>
        public static string Encrypt(string originData)
        {
            string encrypt = string.Empty;
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.KeySize = KEY_SIZE;
                aes.BlockSize = BLOCK_SIZE;
                aes.Key = DeriveKey();
                aes.GenerateIV();
                byte[] iv = aes.IV;

                using (var memoryStream = new MemoryStream())
                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    memoryStream.Write(iv, 0, iv.Length);

                    byte[] origin = Encoding.UTF8.GetBytes(originData);
                    cryptoStream.Write(origin, 0, origin.Length);
                    cryptoStream.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            return encrypt;
        }

        /// <summary>
        /// AES 복호화
        /// </summary>
        /// <param name="encryptData">복호화 할 암호화 데이터</param>
        /// <returns>복호화 된 데이터</returns>
        public static string Decrypt(string encryptData)
        {
            string decrypt = string.Empty;

            byte[] fullCipher = Convert.FromBase64String(encryptData);
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.KeySize = KEY_SIZE;
                aes.BlockSize = BLOCK_SIZE;
                aes.Key = DeriveKey();

                byte[] iv = new byte[aes.BlockSize / 8];
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                aes.IV = iv;

                int cipherStartIndex = iv.Length;
                int cipherLength = fullCipher.Length - cipherStartIndex;

                using (var memoryStream = new MemoryStream(fullCipher, cipherStartIndex, cipherLength))
                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (var reader = new StreamReader(cryptoStream, Encoding.UTF8))
                {
                    decrypt = reader.ReadToEnd();
                }
            }

            return decrypt;
        }

        private static byte[] DeriveKey()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // KEY와 SALT를 합쳐서 해싱
                byte[] keySource = new byte[KEY_SALT.Length + Encoding.UTF8.GetByteCount(KEY)];
                Buffer.BlockCopy(KEY_SALT, 0, keySource, 0, KEY_SALT.Length);
                Buffer.BlockCopy(Encoding.UTF8.GetBytes(KEY), 0, keySource, KEY_SALT.Length, Encoding.UTF8.GetByteCount(KEY));
#pragma warning disable CA1850 // 'ComputeHash'보다 정적 'HashData' 메서드를 선호합니다.
                return sha256.ComputeHash(keySource);
#pragma warning restore CA1850 // 'ComputeHash'보다 정적 'HashData' 메서드를 선호합니다.
            }
        }
    }
}

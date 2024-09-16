// ======================================================================================================
// File Name        : CryptoUtil.cs
// Project          : CSUtil
// Last Update      : 2024.09.16 - yc.jeon
// ======================================================================================================

using System.Security.Cryptography;
using System.Text;

namespace CSUtil
{
    public static class CryptoUtil
    {
        private const int KEY_SIZE = 256;
        private const int BLOCK_SIZE = 128;

        /// <summary>
        /// AES 암호화
        /// </summary>
        /// <param name="key">Key 값</param>
        /// <param name="originData">암호화 할 데이터</param>
        /// <returns>암호화 된 데이터</returns>
        public static string Encrypt(string key, string originData)
        {
            string encrypt = string.Empty;
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.KeySize = KEY_SIZE;
                aes.BlockSize = BLOCK_SIZE;

                byte[] origin = Encoding.UTF8.GetBytes(originData);
                byte[] keySalt = Encoding.ASCII.GetBytes(key);
                using (PasswordDeriveBytes pKey = new PasswordDeriveBytes(key, keySalt))
                using (ICryptoTransform encryptor = aes.CreateEncryptor(pKey.GetBytes(aes.KeySize / 8), pKey.GetBytes(aes.BlockSize / 8)))
                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(origin, 0, origin.Length);
                    cryptoStream.FlushFinalBlock();
                    byte[] cipherBytes = memoryStream.ToArray();
                    encrypt = Convert.ToBase64String(cipherBytes);
                }
            }

            return encrypt;
        }

        /// <summary>
        /// AES 복호화
        /// </summary>
        /// <param name="key">Key 값</param>
        /// <param name="encryptData">복호화 할 암호화 데이터</param>
        /// <returns>복호화 된 데이터</returns>
        public static string Decrypt(string key, string encryptData)
        {
            string decrypt = string.Empty;
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.KeySize = KEY_SIZE;
                aes.BlockSize = BLOCK_SIZE;

                byte[] encrypt = Convert.FromBase64String(encryptData);
                byte[] keySalt = Encoding.ASCII.GetBytes(key);
                int decryptCount = 0;
                using (PasswordDeriveBytes pKey = new PasswordDeriveBytes(key, keySalt))
                using (ICryptoTransform decryptor = aes.CreateDecryptor(pKey.GetBytes(aes.KeySize / 8), pKey.GetBytes(aes.BlockSize / 8)))
                using (MemoryStream memoryStream = new MemoryStream(encrypt))
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    byte[] origin = new byte[encrypt.Length];
                    decryptCount = cryptoStream.Read(origin, 0, origin.Length);
                    decrypt = Encoding.UTF8.GetString(origin, 0, decryptCount);
                }
            }

            return decrypt;
        }
    }
}

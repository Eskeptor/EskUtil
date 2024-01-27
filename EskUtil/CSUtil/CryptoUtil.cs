// ======================================================================================================
// File Name        : CryptoUtil.cs
// Project          : CSUtil
// Last Update      : 2024.01.27 - yc.jeon
// ======================================================================================================

using System.Security.Cryptography;
using System.Text;

namespace CSUtil
{
    public static class CryptoUtil
    {
        public static string Encrypt(string key, string originData)
        {
            byte[] keySalt = Encoding.ASCII.GetBytes(key);
            RijndaelManaged rmCipher = new RijndaelManaged();
            byte[] origin = Encoding.UTF8.GetBytes(originData);
            PasswordDeriveBytes pKey = new PasswordDeriveBytes(key, keySalt);
            ICryptoTransform encryptor = rmCipher.CreateEncryptor(pKey.GetBytes(32), pKey.GetBytes(16));
            string encrypt = string.Empty;

            using (MemoryStream memoryStream = new MemoryStream())
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(origin, 0, origin.Length);
                cryptoStream.FlushFinalBlock();
                encrypt = Convert.ToBase64String(memoryStream.ToArray());
            }

            return encrypt;
        }

        public static string Decrypt(string key, string encryptData)
        {
            byte[] keySalt = Encoding.ASCII.GetBytes(key);
            RijndaelManaged rmCipher = new RijndaelManaged();
            byte[] encrypt = Convert.FromBase64String(encryptData);
            PasswordDeriveBytes pKey = new PasswordDeriveBytes(key, keySalt);
            ICryptoTransform decryptor = rmCipher.CreateDecryptor(pKey.GetBytes(32), pKey.GetBytes(16));
            int decryptCount = 0;
            string decrypt = string.Empty;

            using (MemoryStream memoryStream = new MemoryStream(encrypt))
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            {
                byte[] origin = new byte[encrypt.Length];
                decryptCount = cryptoStream.Read(origin, 0, origin.Length);
                decrypt = Encoding.UTF8.GetString(origin, 0, decryptCount);
            }

            return decrypt;
        }
    }
}

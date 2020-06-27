using System;
using System.Security.Cryptography;
using System.Text;

namespace HelperKit.Security
{
    public static class HashHelper
    {
        /// <summary>
        /// Generic Method
        /// </summary>
        /// <param name="hashAlgorithm"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GenerateHashString(this HashAlgorithm hashAlgorithm, string text)
        {
            _ = text ?? throw new ArgumentNullException(nameof(text));

            hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
            var hash = hashAlgorithm.Hash;
            var strBuilder = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
                strBuilder.Append(hash[i].ToString("x2"));

            return strBuilder.ToString();
        }

        /// <summary>
        /// Computes MD5 Hash
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ComputeMD5Hash(string rawData)
        {
            _ = rawData ?? throw new ArgumentNullException(nameof(rawData));

            var result = string.Empty;
            using (var md5 = new MD5CryptoServiceProvider())
            {
                result = md5.GenerateHashString(rawData);
            }
            return result;
        }

        /// <summary>
        /// Compare the actual value vs the encripted
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encryptedValue"></param>
        /// <returns></returns>
        public static bool AreEqualMD5(string text, string encryptedValue) => ComputeMD5Hash(text) == encryptedValue;

        /// <summary>
        /// dice que hashea pe
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ComputeSha256Hash(string rawData)
        {
            _ = rawData ?? throw new ArgumentNullException(nameof(rawData));

            var result = string.Empty;
            using (var sha256Hash = SHA256.Create())
            {
                result = sha256Hash.GenerateHashString(rawData);
            }
            return result;
        }

        /// <summary>
        /// Compare the actual value vs the encripted
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encriptedValue"></param>
        /// <returns></returns>
        public static bool AreEqualSha256Hash(string text, string encriptedValue) => ComputeSha256Hash(text) == encriptedValue;
    }
}

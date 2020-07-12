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
            var sb = new StringBuilder();
            foreach (var hashItem in hash)
                sb.Append(hashItem.ToString("x2"));

            return sb.ToString();
        }

        /// <summary>
        /// Computes MD5 Hash
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ComputeMd5Hash(string rawData)
        {
            _ = rawData ?? throw new ArgumentNullException(nameof(rawData));

            using var md5 = new MD5CryptoServiceProvider();
            return md5.GenerateHashString(rawData);
        }

        /// <summary>
        /// Compare the actual value vs the encrypted
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encryptedValue"></param>
        /// <returns></returns>
        public static bool AreEqualMd5(string text, string encryptedValue)
        {
            return ComputeMd5Hash(text) == encryptedValue;
        }

        /// <summary>
        /// Computes Sha256
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ComputeSha256Hash(string rawData)
        {
            _ = rawData ?? throw new ArgumentNullException(nameof(rawData));

            using var sha256Hash = SHA256.Create();
            return sha256Hash.GenerateHashString(rawData);
        }

        /// <summary>
        /// Compare the actual value vs the encrypted
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encryptedValue"></param>
        /// <returns></returns>
        public static bool AreEqualSha256Hash(string text, string encryptedValue)
        {
            return ComputeSha256Hash(text) == encryptedValue;
        }
    }
}
using System;
using System.Security.Cryptography;
using HelperKit.Security;
using NUnit.Framework;

namespace HelperKit.Test.TestHash
{
    public class HashHelperUnitTest
    {
        [Test]
        public void TestHashMd5()
        {
            var stringToHash = Guid.NewGuid().ToString();

            var md5HashProvider = new MD5CryptoServiceProvider();
            var hashResult = md5HashProvider.GenerateHashString(stringToHash);
            Console.WriteLine(hashResult);
            Assert.IsNotEmpty(hashResult);

            var hashExt = HashHelper.ComputeMd5Hash(stringToHash);
            Console.WriteLine(hashExt);
            Assert.IsNotEmpty(hashExt);
            Assert.AreEqual(hashResult, hashExt);
            Assert.IsTrue(md5HashProvider.Matches(stringToHash, hashResult));
        }


        [Test]
        public void TestHashSha256()
        {
            var stringToHash = Guid.NewGuid().ToString();

            var sha256Provider = SHA256.Create();
            var hashResult = sha256Provider.GenerateHashString(stringToHash);
            Console.WriteLine(hashResult);
            Assert.IsNotEmpty(hashResult);

            var hashExt = HashHelper.ComputeSha256Hash(stringToHash);
            Console.WriteLine(hashExt);
            Assert.IsNotEmpty(hashExt);
            Assert.AreEqual(hashResult, hashExt);
            Assert.IsTrue(sha256Provider.Matches(stringToHash, hashResult));
        }

        [Test]
        public void TestHashSha1()
        {
            var stringToHash = Guid.NewGuid().ToString();

            var sha1 = SHA1.Create();
            var hashResult = sha1.GenerateHashString(stringToHash);
            Console.WriteLine(hashResult);
            Assert.IsNotEmpty(hashResult);

            Assert.IsTrue(sha1.Matches(stringToHash, hashResult));
        }

        [Test]
        public void TestHashSha348()
        {
            var stringToHash = Guid.NewGuid().ToString();

            var sha348 = SHA384.Create();
            var hashResult = sha348.GenerateHashString(stringToHash);
            Console.WriteLine(hashResult);
            Assert.IsNotEmpty(hashResult);

            Assert.IsTrue(sha348.Matches(stringToHash, hashResult));
        }

        [Test]
        public void TestHashSha512()
        {
            var stringToHash = Guid.NewGuid().ToString();

            var sha512 = SHA512.Create();
            var hashResult = sha512.GenerateHashString(stringToHash);
            Console.WriteLine(hashResult);
            Assert.IsNotEmpty(hashResult);

            Assert.IsTrue(sha512.Matches(stringToHash, hashResult));
        }
    }
}
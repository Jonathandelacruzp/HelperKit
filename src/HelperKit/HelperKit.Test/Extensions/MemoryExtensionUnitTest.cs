using NUnit.Framework;
using System;
using System.IO;

namespace HelperKit.Test.Extensions
{
    public class MemoryExtensionUnitTest
    {
        [Test]
        public void MemoryExtension_ReturnTrue()
        {
            string solution_dir = Directory.GetCurrentDirectory();
            var stream = new StreamReader(solution_dir + "\\Files\\text2.txt");

            Assert.IsInstanceOf<byte[]>(stream.BaseStream.ToBytes());

            Assert.IsNotNull(stream.BaseStream.ToBytes());
        }

        [Test]
        public void MemoryExtension_ThrowError_WithNullParameter()
        {
            Stream stream = null;

            var exception = Assert.Throws<ArgumentNullException>(() => stream.ToBytes());

            Assert.AreEqual(exception.Message, $"Value cannot be null.{Environment.NewLine}Parameter name: {nameof(stream)}");
        }
    }
}
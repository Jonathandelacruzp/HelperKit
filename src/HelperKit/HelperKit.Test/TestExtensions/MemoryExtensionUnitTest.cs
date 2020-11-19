using System;
using System.IO;
using NUnit.Framework;

namespace HelperKit.Test.TestExtensions
{
    public class MemoryExtensionUnitTest
    {
        [Test]
        public void MemoryExtension_ReturnTrue()
        {
            var solutionDir = Directory.GetCurrentDirectory();
            var stream = new StreamReader(solutionDir + "\\Files\\text2.txt");

            Assert.IsInstanceOf<byte[]>(stream.BaseStream.ToBytes());

            Assert.IsNotNull(stream.BaseStream.ToBytes());
        }

        [Test]
        public void MemoryExtension_ThrowError_WithNullParameter()
        {
            Assert.Throws<ArgumentNullException>(() => ((Stream) null).ToBytes());
        }
    }
}
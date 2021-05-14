using HelperKit.Security;
using NUnit.Framework;

namespace HelperKit.Test.Hash
{
    public class Crc64UnitTest
    {
        [Test]
        public void TestCrc64()
        {
            var result = HashHelper.ComputeCrc64IsoHash("paradaso");

            Assert.AreEqual("53d38bec9bf23bf1", result);
        }
    }
}

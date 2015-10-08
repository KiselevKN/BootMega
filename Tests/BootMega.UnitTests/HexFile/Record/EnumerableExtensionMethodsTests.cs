using Service.HexFile.Record;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BootMega.UnitTests
{
    [TestClass]
    public class EnumerableExtensionMethodsTests
    {
        [TestMethod]
        [TestCategory("EnumerableExtensionMethods")]
        public void ToHexString()
        {
            byte[] array = new byte[5] { 0x11, 0x22, 0x33, 0x44, 0x55 };
            Assert.AreEqual("1122334455", array.ToHexString());
        }

        [TestMethod]
        [TestCategory("EnumerableExtensionMethods")]
        public void Checksum()
        {
            byte[] array = new byte[] { 0x02, 0x00, 0x00, 0x02, 0x00, 0x00 };
            Assert.AreEqual<byte>(0xFC, array.Checksum());

            array = new byte[] { 0x10, 0x00, 0x00, 0x00, 0x0C, 0x94, 
                0x3E, 0x01, 0x0C, 0x94, 0x30, 0x35, 0x0C, 0x94, 0x31, 
                0x35, 0x0C, 0x94, 0x32, 0x35 };
            Assert.AreEqual<byte>(0xFF, array.Checksum());
        }
    }
}

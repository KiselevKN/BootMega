using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Settings.Enums;
using Service.Settings.Extensions;

namespace BootMega.UnitTests
{
    [TestClass]
    public class MemoryTypeExtensionsTests
    {
        #region public void MemoryTypeToInt32()

        [TestMethod]
        [TestCategory("MemoryTypeExtensions")]
        public void MemoryTypeToInt32()
        {
            MemoryType mt = MemoryType.FLASH;  
            Assert.AreEqual<int>(0, mt.ToInt32());
        }

        #endregion

        #region public void MemoryTypeToString()

        [TestMethod]
        [TestCategory("MemoryTypeExtensions")]
        public void MemoryTypeToString()
        {
            MemoryType mt = MemoryType.EEPROM;
            Assert.AreEqual<string>("1", mt.ToString(true));
            Assert.AreEqual<string>("EEPROM", mt.ToString(false));
        }

        #endregion

        #region public void Int32ToMemoryType()

        [TestMethod]
        [TestCategory("MemoryTypeExtensions")]
        public void Int32ToMemoryType()
        {
            Assert.AreEqual<MemoryType>(MemoryType.FLASH, 0.ToMemoryType());
        }

        #endregion

        #region public void StringToMemoryType()

        [TestMethod]
        [TestCategory("MemoryTypeExtensions")]
        public void StringToMemoryType()
        {
            Assert.AreEqual<MemoryType>(MemoryType.FLASH, "0".ToMemoryType(true));
            Assert.AreEqual<MemoryType>(MemoryType.FLASH, "FLASH".ToMemoryType(false));
        }

        #endregion
    }
}

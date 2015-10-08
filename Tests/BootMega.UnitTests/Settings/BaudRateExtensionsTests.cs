using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Settings.Enums;
using Service.Settings.Extensions;

namespace BootMega.UnitTests
{
    [TestClass]
    public class BaudRateExtensionsTests
    {
        #region public void BaudRateToInt32()

        [TestMethod]
        [TestCategory("BaudRateExtensions")]
        public void BaudRateToInt32()
        {
            BaudRate br = BaudRate.BR_57600;  
            Assert.AreEqual<int>(57600, br.ToInt32());
        }

        #endregion

        #region public void BaudRateToString()

        [TestMethod]
        [TestCategory("BaudRateExtensions")]
        public void BaudRateToString()
        {
            BaudRate br = BaudRate.BR_57600;
            Assert.AreEqual<string>("57600", br.ToString(true));
            Assert.AreEqual<string>("BR_57600", br.ToString(false));
        }

        #endregion

        #region public void Int32ToBaudRate()

        [TestMethod]
        [TestCategory("BaudRateExtensions")]
        public void Int32ToBaudRate()
        {
            Assert.AreEqual<BaudRate>(BaudRate.BR_115200, 115200.ToBaudRate());
        }

        #endregion

        #region public void StringToBaudRate()

        [TestMethod]
        [TestCategory("BaudRateExtensions")]
        public void StringToBaudRate()
        {
            Assert.AreEqual<BaudRate>(BaudRate.BR_115200, "115200".ToBaudRate(true));
            Assert.AreEqual<BaudRate>(BaudRate.BR_115200, "BR_115200".ToBaudRate(false));
        }

        #endregion
    }
}

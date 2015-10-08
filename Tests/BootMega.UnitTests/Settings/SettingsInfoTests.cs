using System;
using System.IO.Ports;
using Service.Settings.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Settings.Enums;

namespace BootMega.UnitTests
{
    [TestClass]
    public class SettingsInfoTests
    {
        #region public void Create()

        [TestMethod]
        [TestCategory("SettingsInfo")]
        public void Create()
        {
            SettingsInfo s1 = new SettingsInfo(0, "Name",
                new Processor(0, "Name", 0x1000, 0x40000, 0x3E000, 0x3FFFF),
                new SerialPortSettings(BaudRate.BR_57600, Parity.Even, StopBits.One, 0xA0, 0xA5));

            SettingsInfo s2 = new SettingsInfo(0, "Name",
                new Processor(0, "Name", 0x1000, 0x40000, 0x3E000, 0x3FFFF),
                new SerialPortSettings(BaudRate.BR_57600, Parity.Even, StopBits.One, 0xA0, 0xA5));

            Assert.AreEqual<string>("Name", s1.ToString());
            Assert.AreEqual<string>("Name", s2.ToString());
            Assert.IsTrue(s1.Equals(s2));

            s2.Name = "NewName";

            Assert.AreEqual<string>("NewName", s2.ToString());
            Assert.IsFalse(s1.Equals(s2));
        }

        #endregion

        #region public void CreateIdException()

        [TestMethod]
        [TestCategory("SettingsInfo")]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateIdException()
        {
            SettingsInfo s = new SettingsInfo(-1, "Name",
                new Processor(0, "Name", 0x1000, 0x40000, 0x3E000, 0x3FFFF),
                new SerialPortSettings(BaudRate.BR_57600, Parity.Even, StopBits.One, 0xA0, 0xA5));
        }

        #endregion
        
        #region public void CreateNameException()

        [TestMethod]
        [TestCategory("SettingsInfo")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNameException()
        {
            SettingsInfo s = new SettingsInfo(0, null,
                new Processor(0, "Name", 0x1000, 0x40000, 0x3E000, 0x3FFFF),
                new SerialPortSettings(BaudRate.BR_57600, Parity.Even, StopBits.One, 0xA0, 0xA5));
        }

        #endregion
        
        #region public void CreateProcessorException()

        [TestMethod]
        [TestCategory("SettingsInfo")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateProcessorException()
        {
            SettingsInfo s = new SettingsInfo(0, "Name", null,
                new SerialPortSettings(BaudRate.BR_57600, Parity.Even, StopBits.One, 0xA0, 0xA5));
        }

        #endregion

        #region public void CreateSerialPortSettingsException()

        [TestMethod]
        [TestCategory("SettingsInfo")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateSerialPortSettingsException()
        {
            SettingsInfo s = new SettingsInfo(0, "Name",
                new Processor(0, "Name", 0x1000, 0x40000, 0x3E000, 0x3FFFF), null);
        }

        #endregion

        #region public void SettingsInfoToString()

        [TestMethod]
        [TestCategory("SettingsInfo")]
        public void SettingsInfoToString()
        {
            SettingsInfo si = new SettingsInfo(0, "Name",
                new Processor(0, "Name", 0x1000, 0x40000, 0x3E000, 0x3FFFF),
                new SerialPortSettings(BaudRate.BR_57600, Parity.Even, StopBits.One, 0xA0, 0xA5));

            Console.WriteLine(si.ToString());
            Console.WriteLine(si.ToString("LOG", null));

            SelectedSettings ss = new SelectedSettings(MemoryType.FLASH, si);

            Console.WriteLine(ss.ToString());
            Console.WriteLine(ss.ToString("LOG", null));
        }

        #endregion
    }
}

using System;
using System.IO.Ports;
using Service.Settings.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Settings.Enums;

namespace BootMega.UnitTests
{
    [TestClass]
    public class SerialPortSettingsTests
    {
        #region public void Create()

        [TestMethod]
        [TestCategory("SerialPortSettings")]
        public void Create()
        {
            SerialPortSettings s1 = new SerialPortSettings(BaudRate.BR_57600, 
                Parity.Even, StopBits.One, 0xA0, 0xA5);
            SerialPortSettings s2 = new SerialPortSettings(BaudRate.BR_57600,
                Parity.Even, StopBits.One, 0xA0, 0xA5);

            Assert.IsTrue(s1.Equals(s2));

            foreach (var h in SerialPortSettings.AllowableHeadersList)
            {
                if (h % 0x10 == 0 && h != SerialPortSettings.MinAllowableHeaderValue)
                    Console.WriteLine();
                Console.Write("0x{0:X2} ", h);
            }
        }

        #endregion

        #region public void CreateHeaderTXException()

        [TestMethod]
        [TestCategory("SerialPortSettings")]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateHeaderTXException()
        {
            SerialPortSettings s = new SerialPortSettings(BaudRate.BR_57600,
                Parity.Even, StopBits.One, 0x7F, 0xA5);
        }

        #endregion

        #region public void CreateHeaderRXException()

        [TestMethod]
        [TestCategory("SerialPortSettings")]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateHeaderRXException()
        {
            SerialPortSettings s = new SerialPortSettings(BaudRate.BR_57600,
                Parity.Even, StopBits.One, 0xA0, 0x00);
        }
        #endregion

        #region public void SerialPortSettingsToString()

        [TestMethod]
        [TestCategory("SerialPortSettings")]
        public void SerialPortSettingsToString()
        {
            SerialPortSettings s = new SerialPortSettings(BaudRate.BR_57600,
                Parity.Even, StopBits.One, 0xA0, 0xA1);

            Console.WriteLine(s.ToString());
            Console.WriteLine(s.ToString("LOG", null));
        }

        #endregion

        #region public void SerialPortSettingsToStringException()

        [TestMethod]
        [TestCategory("SerialPortSettings")]
        [ExpectedException(typeof(FormatException))]
        public void SerialPortSettingsToStringException()
        {
            SerialPortSettings s = new SerialPortSettings(BaudRate.BR_57600,
                Parity.Even, StopBits.One, 0xA0, 0xA1);

            Console.WriteLine(s.ToString("F", null));
        }

        #endregion
    }
}

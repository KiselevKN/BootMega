using System.Xml.Linq;
using Service.Settings.Models;
using Service.Settings.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Ports;
using Service.Settings.Enums;

namespace BootMega.UnitTests
{
    [TestClass]
    public class SerialPortSettingsExtensionsTests
    {
        #region public void ToSerialPortSettings()

        [TestMethod]
        [TestCategory("SerialPortSettingsExtensions")]
        public void ToSerialPortSettings()
        {
            string xElementSerialPortSettings = 
                "<serialPortSettings>" + "\r\n  " +
                "<baudrate>57600</baudrate>" + "\r\n  " +
                "<parity>1</parity>" + "\r\n  " +
                "<stopbit>1</stopbit>" + "\r\n  " +
                "<headerTx>0xAA</headerTx>" + "\r\n" +
                "<headerRx>0xA5</headerRx>" + "\r\n" +
                "</serialPortSettings>";

            XElement xElement = XElement.Parse(xElementSerialPortSettings);
            SerialPortSettings serialPortSettings = xElement.ToSerialPortSettings(XNamespace.None);
            SerialPortSettings expectedSerialPortSettings = new SerialPortSettings(BaudRate.BR_57600,
                Parity.Odd, StopBits.One, 0xAA, 0xA5);

            Assert.IsTrue(expectedSerialPortSettings.Equals(serialPortSettings));
        }

        #endregion

        #region public void ToXElement()

        [TestMethod]
        [TestCategory("SerialPortSettingsExtensions")]
        public void ToXElement()
        {
            SerialPortSettings serialPortSettings = new SerialPortSettings(BaudRate.BR_57600,
                Parity.Odd, StopBits.One, 0xAA, 0xA5);

            XElement xElement = serialPortSettings.ToXElement("namespace");

            string xElementSerialPortSettings = 
                "<serialPortSettings xmlns=\"namespace\">" + "\r\n  " +
                "<baudrate>57600</baudrate>" + "\r\n  " +
                "<parity>1</parity>" + "\r\n  " +
                "<stopbit>1</stopbit>" + "\r\n  " +
                "<headerTx>0xAA</headerTx>" + "\r\n  " +
                "<headerRx>0xA5</headerRx>" + "\r\n" +
                "</serialPortSettings>";

            Assert.AreEqual(xElementSerialPortSettings, xElement.ToString());
        }

        #endregion

        #region public void UpdateXElement()

        [TestMethod]
        [TestCategory("SerialPortSettingsExtensions")]
        public void UpdateXElement()
        {
            SerialPortSettings serialPortSettings = new SerialPortSettings(BaudRate.BR_57600,
                Parity.Odd, StopBits.One, 0xAA, 0xA5);

            string xElementSerialPortSettings = 
                "<serialPortSettings>" + "\r\n  " +
                "<baudrate>9600</baudrate>" + "\r\n  " +
                "<parity>1</parity>" + "\r\n  " +
                "<stopbit>2</stopbit>" + "\r\n  " +
                "<headerTx>0x8A</headerTx>" + "\r\n  " +
                "<headerRx>0x85</headerRx>" + "\r\n" +
                "</serialPortSettings>";

            XElement xElement = XElement.Parse(xElementSerialPortSettings);

            xElement.UpdateXElement(serialPortSettings, XNamespace.None);

            string xExpectedElementSerialPortSettings = 
                "<serialPortSettings>" + "\r\n  " +
                "<baudrate>57600</baudrate>" + "\r\n  " +
                "<parity>1</parity>" + "\r\n  " +
                "<stopbit>1</stopbit>" + "\r\n  " +
                "<headerTx>0xAA</headerTx>" + "\r\n  " +
                "<headerRx>0xA5</headerRx>" + "\r\n" +
                "</serialPortSettings>";

            Assert.AreEqual(xExpectedElementSerialPortSettings, xElement.ToString());
        }

        #endregion
    }
}

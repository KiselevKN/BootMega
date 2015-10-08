using System;
using System.Linq;
using System.Xml.Linq;
using Service.Settings.Models;
using Service.Settings.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.IO.Ports;
using Service.Settings.Enums;

namespace BootMega.UnitTests
{
    [TestClass]
    public class SettingsInfoExtensionsTests
    {      
        #region public void ToSettingsInfo()

        [TestMethod]
        [TestCategory("SettingsInfoExtensions")]
        public void ToSettingsInfo()
        {
            XDocument doc = XDocument.Load(Directory.GetCurrentDirectory() + "\\Settings\\TestSettings.xml");
            var ns = doc.Root.GetDefaultNamespace();
            var settings = doc.Element(ns + "settings").ToSettingsInfo(ns).ToList();
            
            Assert.AreEqual<int>(2, settings.Count);

            SettingsInfo si1 = new SettingsInfo(0, "ОПС24Н-1 КО", 
                new Processor(0, "ATMega2560", 0x1000, 0x40000, 0x3E000, 0x3FFFF),
                new SerialPortSettings(BaudRate.BR_57600, Parity.Odd, StopBits.One, 0xAA, 0xA5));

            SettingsInfo si2 = new SettingsInfo(1, "ОПС24Н-1 УУТПЛ", 
                new Processor(1, "ATMega128", 0x1000, 0x20000, 0x1E000, 0x1FFFF),
                new SerialPortSettings(BaudRate.BR_57600, Parity.Odd, StopBits.One, 0xCA, 0xC5));

            Assert.IsTrue(settings[0].Equals(si1));
            Assert.IsTrue(settings[1].Equals(si2));
        }

        #endregion

        #region public void UpdateXElement()

        [TestMethod]
        [TestCategory("SettingsInfoExtensions")]
        public void UpdateXElement()
        {
            SettingsInfo si = new SettingsInfo(1, "ОПС24Н-1 КО",
                new Processor(1, "ATMega128", 0x1000, 0x20000, 0x1E000, 0x1FFFF),
                new SerialPortSettings(BaudRate.BR_38400, Parity.Odd, StopBits.One, 0x80, 0x85));

            string xElementRecord =
                "<record ID =\"0\" processorRef=\"0\">" + "\r\n  " +
                "<name>ОПС24Н-1 КО</name>" + "\r\n  " +
                "<serialPortSettings>" + "\r\n    " +
                "<baudrate>57600</baudrate>" + "\r\n    " +
                "<parity>1</parity>" + "\r\n    " +
                "<stopbit>1</stopbit>" + "\r\n    " +
                "<headerTx>0xAA</headerTx>" + "\r\n    " +
                "<headerRx>0xA5</headerRx>" + "\r\n  " +
                "</serialPortSettings>" + "\r\n" +
                "</record>";

            XElement xElement = XElement.Parse(xElementRecord);

            xElement.UpdateXElement(si, XNamespace.None);

            string xExpectedElementRecord =
                "<record ID=\"1\" processorRef=\"1\">" + "\r\n  " +
                "<name>ОПС24Н-1 КО</name>" + "\r\n  " +
                "<serialPortSettings>" + "\r\n    " +
                "<baudrate>38400</baudrate>" + "\r\n    " +
                "<parity>1</parity>" + "\r\n    " +
                "<stopbit>1</stopbit>" + "\r\n    " +
                "<headerTx>0x80</headerTx>" + "\r\n    " +
                "<headerRx>0x85</headerRx>" + "\r\n  " +
                "</serialPortSettings>" + "\r\n" +
                "</record>";
            Console.WriteLine(xExpectedElementRecord);
            Console.WriteLine(xElement.ToString());
            Assert.AreEqual<string>(xExpectedElementRecord, xElement.ToString());
        }

        #endregion 
    }
}

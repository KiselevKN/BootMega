using System;
using System.IO.Ports;
using System.Xml.Linq;
using Service.Settings.Enums;
using Service.Settings.Models;

namespace Service.Settings.Extensions
{
    internal static class SerialPortSettingsExtensions
    {
        internal static SerialPortSettings ToSerialPortSettings(this XElement element, XNamespace ns)
        {
            return new SerialPortSettings((BaudRate)Convert.ToInt32(element.Element(ns + "baudrate").Value),
                (Parity)Convert.ToInt32(element.Element(ns + "parity").Value),
                (StopBits)Convert.ToInt32(element.Element(ns + "stopbit").Value),
                Convert.ToByte(element.Element(ns + "headerTx").Value, 16),
                Convert.ToByte(element.Element(ns + "headerRx").Value, 16));
        }

        internal static XElement ToXElement(this SerialPortSettings serialPortSettings, XNamespace ns)
        {
            return new XElement(ns + "serialPortSettings",
                new XElement(ns + "baudrate", (Int32)Enum.Parse(typeof(BaudRate), serialPortSettings.BaudRate.ToString())),
                new XElement(ns + "parity", (Int32)Enum.Parse(typeof(Parity), serialPortSettings.Parity.ToString())),
                new XElement(ns + "stopbit", (Int32)Enum.Parse(typeof(StopBits), serialPortSettings.StopBits.ToString())),
                new XElement(ns + "headerTx", "0x" + serialPortSettings.HeaderTX.ToString("X2")),
                new XElement(ns + "headerRx", "0x" + serialPortSettings.HeaderRX.ToString("X2")));
        }

        internal static void UpdateXElement(this XElement element, SerialPortSettings serialPortSettings, XNamespace ns)
        {
            element.Element(ns + "baudrate").Value = ((Int32)Enum.Parse(typeof(BaudRate), serialPortSettings.BaudRate.ToString())).ToString();
            element.Element(ns + "parity").Value = ((Int32)Enum.Parse(typeof(Parity), serialPortSettings.Parity.ToString())).ToString();
            element.Element(ns + "stopbit").Value = ((Int32)Enum.Parse(typeof(StopBits), serialPortSettings.StopBits.ToString())).ToString();
            element.Element(ns + "headerTx").Value = "0x" + serialPortSettings.HeaderTX.ToString("X2");
            element.Element(ns + "headerRx").Value = "0x" + serialPortSettings.HeaderRX.ToString("X2");
        }
    }
}

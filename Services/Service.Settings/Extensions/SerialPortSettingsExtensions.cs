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
                new XElement(ns + "baudrate", (int)Enum.Parse(typeof(BaudRate), serialPortSettings.BaudRate.ToString())),
                new XElement(ns + "parity", (int)Enum.Parse(typeof(Parity), serialPortSettings.Parity.ToString())),
                new XElement(ns + "stopbit", (int)Enum.Parse(typeof(StopBits), serialPortSettings.StopBits.ToString())),
                new XElement(ns + "headerTx", "0x" + serialPortSettings.HeaderTX.ToString("X2")),
                new XElement(ns + "headerRx", "0x" + serialPortSettings.HeaderRX.ToString("X2")));
        }

        internal static void UpdateXElement(this XElement element, SerialPortSettings serialPortSettings, XNamespace ns)
        {
            element.Element(ns + "baudrate").Value = ((int)Enum.Parse(typeof(BaudRate), serialPortSettings.BaudRate.ToString())).ToString();
            element.Element(ns + "parity").Value = ((int)Enum.Parse(typeof(Parity), serialPortSettings.Parity.ToString())).ToString();
            element.Element(ns + "stopbit").Value = ((int)Enum.Parse(typeof(StopBits), serialPortSettings.StopBits.ToString())).ToString();
            element.Element(ns + "headerTx").Value = "0x" + serialPortSettings.HeaderTX.ToString("X2");
            element.Element(ns + "headerRx").Value = "0x" + serialPortSettings.HeaderRX.ToString("X2");
        }
    }
}

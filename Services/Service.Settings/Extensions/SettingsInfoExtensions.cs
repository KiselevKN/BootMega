using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using Service.Settings.Models;

namespace Service.Settings.Extensions
{
    internal static class SettingsInfoExtensions
    {
        internal static IEnumerable<SettingsInfo> ToSettingsInfo(this XElement element, XNamespace ns)
        {
            return from s in element.Element(ns + "usersRecords").Elements(ns + "record")
                   join p in element.Element(ns + "processors").Elements(ns + "processor")
                   on s.Attribute("processorRef").Value equals p.Attribute("ID").Value
                   select new SettingsInfo(Convert.ToInt32(s.Attribute("ID").Value, 10), 
                       s.Element(ns + "name").Value, p.ToProcessor(ns), 
                       s.Element(ns + "serialPortSettings").ToSerialPortSettings(ns));
        }

        internal static void UpdateXElement(this XElement element, SettingsInfo settings, XNamespace ns)
        {
            element.Attribute("ID").Value = settings.Id.ToString();
            element.Attribute("processorRef").Value = settings.Processor.Id.ToString();
            element.Element(ns + "name").Value = settings.Name;
            element.Element(ns + "serialPortSettings").UpdateXElement(settings.SerialPortSettings, ns);
        }
    }
}

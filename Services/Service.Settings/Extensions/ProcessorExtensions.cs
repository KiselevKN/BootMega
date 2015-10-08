using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using Service.Settings.Models;

namespace Service.Settings.Extensions
{
    internal static class ProcessorExtensions
    {
        internal static Processor ToProcessor(this XElement element, XNamespace ns)
        {
            return new Processor(Convert.ToInt32(element.Attribute("ID").Value, 10),
                element.Attribute("Name").Value,
                Convert.ToInt32(element.Element(ns + "eepromSize").Value, 16),
                Convert.ToInt32(element.Element(ns + "flashSize").Value, 16),
                Convert.ToInt32(element.Element(ns + "bootStartAddress").Value, 16),
                Convert.ToInt32(element.Element(ns + "bootEndAddress").Value, 16));
        }

        internal static IEnumerable<Processor> ToProcessors(this XElement element, XNamespace ns)
        {
            return from p in element.Elements(ns + "processor")
                   select p.ToProcessor(ns);
        }

        internal static XElement ToXElement(this Processor processor, XNamespace ns)
        {
            return new XElement(ns + "processor",
                new XAttribute("ID", processor.Id),
                new XAttribute("Name", processor.Name),
                new XElement(ns + "eepromSize", "0x" + processor.EepromSize.ToString("X")),
                new XElement(ns + "flashSize", "0x" + processor.FlashSize.ToString("X")),
                new XElement(ns + "bootStartAddress", "0x" + processor.BootStartAddress.ToString("X")),
                new XElement(ns + "bootEndAddress", "0x" + processor.BootEndAddress.ToString("X")));
        }

        internal static void UpdateXElement(this XElement element, Processor processor, XNamespace ns)
        {
            element.Attribute("ID").Value = processor.Id.ToString();
            element.Attribute("Name").Value = processor.Name;
            element.Element(ns + "eepromSize").Value = "0x" + processor.EepromSize.ToString("X");
            element.Element(ns + "flashSize").Value = "0x" + processor.FlashSize.ToString("X");
            element.Element(ns + "bootStartAddress").Value = "0x" + processor.BootStartAddress.ToString("X");
            element.Element(ns + "bootEndAddress").Value = "0x" + processor.BootEndAddress.ToString("X");
        }
    }
}

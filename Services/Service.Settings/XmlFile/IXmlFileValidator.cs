using System;
using System.Xml.Linq;

namespace Service.Settings.XmlFile
{
    public interface IXmlFileValidator
    {
        Boolean IsValid(XDocument document);
        Exception Exception { get; }
    }
}

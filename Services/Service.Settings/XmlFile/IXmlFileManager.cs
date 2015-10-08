using System.Xml.Linq;

namespace Service.Settings.XmlFile
{
    public interface IXmlFileManager
    {
        XDocument Open();
        void Save(XDocument doc);
    }
}

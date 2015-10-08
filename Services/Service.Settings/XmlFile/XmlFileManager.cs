using System.Xml.Linq;

namespace Service.Settings.XmlFile
{
    public class XmlFileManager : IXmlFileManager
    {
        #region fields

        private IXmlFileValidator xmlFileValidator;
        private string path;

        #endregion

        #region ctors

        public XmlFileManager(IXmlFileValidator xmlFileValidator, string path)
        {
            this.xmlFileValidator = xmlFileValidator;
            this.path = path;
        }

        #endregion

        #region IXmlFileManager Members

        public XDocument Open()
        {
            XDocument doc = XDocument.Load(path);
            if (!xmlFileValidator.IsValid(doc))
                throw xmlFileValidator.Exception;
            return doc;
        }

        public void Save(XDocument doc)
        {
	        if (!xmlFileValidator.IsValid(doc))
                throw xmlFileValidator.Exception;
            doc.Save(path);
        }

        #endregion
    }
}

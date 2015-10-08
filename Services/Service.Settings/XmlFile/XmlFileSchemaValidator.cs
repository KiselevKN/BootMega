using System;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Service.Settings.XmlFile
{
    public class XmlFileSchemaValidator : IXmlFileValidator
    {
        #region fields

        private XmlSchemaSet schemas = null;     
        private Exception exception = null;

        #endregion

        #region ctors

        private XmlFileSchemaValidator() { }

        public XmlFileSchemaValidator(String path)
        {
            schemas = new XmlSchemaSet();
            schemas.Add(null, path);
            schemas.Compile();
        }

        #endregion

        #region IXmlValidator Members

        public bool IsValid(XDocument doc)
        {
            bool error = true;
            doc.Validate(schemas, (o, e) => {
                exception = e.Exception;
                error = false;
            });
            return error;
        }

        public Exception Exception
        {
            get { return exception; }
        }

        #endregion
    }
}

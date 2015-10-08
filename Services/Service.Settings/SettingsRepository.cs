using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using Service.Settings.Models;
using Service.Settings.XmlFile;
using Service.Settings.Extensions;
using Service.Settings.Properties;
using Service.Settings.Enums;

namespace Service.Settings
{
    public class SettingsRepository : ISettingsRepository
    {
        #region fields

        private IXmlFileManager xmlFileManager;

        #endregion

        #region ctors

        public SettingsRepository(IXmlFileManager xmlFileManager)
        {
            this.xmlFileManager = xmlFileManager;
        }

        #endregion

        #region ISettingsRepository Members

        public IEnumerable<SettingsInfo> Settings
        {
            get 
            {
                try
                {
                    XDocument doc = xmlFileManager.Open();
                    XNamespace ns = doc.Root.GetDefaultNamespace();
                    return doc.Element(ns + "settings").ToSettingsInfo(ns);
                }
                catch (Exception ex)
                {
                    throw new SettingsException(Resources.IncorrectFileWithSettings, ex);
                }  
            }
        }

        public void AddNewSettings(SettingsInfo settingsInfo)
        {
            try
            {
                XDocument doc = xmlFileManager.Open();
                XNamespace ns = doc.Root.GetDefaultNamespace();

                if (settingsInfo == null)
                    throw new ArgumentNullException("settingsInfo");

                settingsInfo.Id = Settings.Select(n => n.Id).Max() + 1;
                
                doc.Element(ns + "settings").Element(ns + "usersRecords").Add(new XElement(ns + "record",
                    new XAttribute("ID", settingsInfo.Id),
                    new XAttribute("processorRef", settingsInfo.Processor.Id),
                    new XElement(ns + "name", settingsInfo.Name),
                    settingsInfo.SerialPortSettings.ToXElement(ns)));
                
                xmlFileManager.Save(doc);
            }
            catch (SettingsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new SettingsException(Resources.IncorrectFileWithSettings, ex);
            }
        }

        public void RemoveSettings(SettingsInfo settingsInfo)
        {
            try
            {
                XDocument doc = xmlFileManager.Open();
                XNamespace ns = doc.Root.GetDefaultNamespace();
                var elements = doc.Element(ns + "settings").Element(ns + "usersRecords").Elements(ns + "record");
                var element = elements.Where(x => Convert.ToInt32(x.Attribute("ID").Value, 10) == settingsInfo.Id).First();
                element.Remove();
                xmlFileManager.Save(doc);
            }
            catch (SettingsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new SettingsException(Resources.IncorrectFileWithSettings, ex);
            }
        }

        public void UpdateSettings(SettingsInfo settingsInfo)
        {
            try
            {
                var p = Settings.First(n => n.Name == settingsInfo.Name);
                if (p != null && p.Id != settingsInfo.Id)
                    throw new SettingsException(Resources.SettingsNameAlreadyExists);

                XDocument doc = xmlFileManager.Open();
                XNamespace ns = doc.Root.GetDefaultNamespace();
                var elements = doc.Element(ns + "settings").Element(ns + "usersRecords").Elements(ns + "record");
                var element = elements.Where(x => Convert.ToInt32(x.Attribute("ID").Value, 10) == settingsInfo.Id).First();
                element.UpdateXElement(settingsInfo, ns);
                xmlFileManager.Save(doc);
            }
            catch (SettingsException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new SettingsException(Resources.IncorrectFileWithSettings, ex);
            }
        }

        public SelectedSettings SelectedSettings
        {
            get
            {
                try
                {
                    XDocument doc = xmlFileManager.Open();
                    XNamespace ns = doc.Root.GetDefaultNamespace();

                    int id = Convert.ToInt32(doc.Element(ns + "settings").Element(ns + "selectedSettings").Attribute("recordRef").Value, 10);
                    MemoryType t = (MemoryType)(Convert.ToInt32(doc.Element(ns + "settings").Element(ns + "selectedSettings").Element(ns + "memoryType").Value, 10));

                    SettingsInfo s = Settings.First(n => n.Id == id);
                    return new SelectedSettings(t, s);
                }
                catch (SettingsException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new SettingsException(Resources.IncorrectFileWithSettings, ex);
                }
            }
            set
            {
                try
                {
                    XDocument doc = xmlFileManager.Open();
                    XNamespace ns = doc.Root.GetDefaultNamespace();
                    doc.Element(ns + "settings").Element(ns + "selectedSettings").Attribute("recordRef").Value =
                        value.SettingsInfo.Id.ToString();
                    doc.Element(ns + "settings").
                        Element(ns + "selectedSettings").Element(ns + "memoryType").Value = ((int)Enum.Parse(typeof(MemoryType), value.MemoryType.ToString())).ToString();
                    xmlFileManager.Save(doc);
                }
                catch (SettingsException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new SettingsException(Resources.IncorrectFileWithSettings, ex);
                }
            }
        }

        public IEnumerable<Processor> Processors
        {
            get
            {
                try
                {
                    XDocument doc = xmlFileManager.Open();
                    XNamespace ns = doc.Root.GetDefaultNamespace();
                    return doc.Element(ns + "settings").Element(ns + "processors").ToProcessors(ns);
                }
                catch (Exception ex)
                {
                    throw new SettingsException(Resources.IncorrectFileWithSettings, ex);
                }
            }
        }

        #endregion
    }
}

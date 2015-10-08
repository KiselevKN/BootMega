using System.Linq;
using System.Xml.Linq;
using Service.Settings.Models;
using Service.Settings.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BootMega.UnitTests
{
    [TestClass]
    public class ProcessorExtensionsTests
    {      
        #region public void ToProcessor()

        [TestMethod]
        [TestCategory("ProcessorExtensions")]
        public void ToProcessor()
        {
            string xElementProcessor = 
                "<processor ID=\"1\" Name=\"ATMega128\">" + "\r\n  " +
                "<eepromSize>0x1000</eepromSize>" + "\r\n  " +
                "<flashSize>0x20000</flashSize>" + "\r\n  " +
                "<bootStartAddress>0x1E000</bootStartAddress>" + "\r\n  " +
                "<bootEndAddress>0x1FFFF</bootEndAddress>" + "\r\n" +  
                "</processor>";
            
            XElement xElement = XElement.Parse(xElementProcessor);
            Processor processor = xElement.ToProcessor(XNamespace.None);

            Processor expectedProcessor = new Processor(1, "ATMega128", 
                0x1000, 0x20000, 0x1E000, 0x1FFFF);

            Assert.IsTrue(processor.Equals(expectedProcessor));
        }

        #endregion

        #region public void ToProcessors()

        [TestMethod]
        [TestCategory("ProcessorExtensions")]
        public void ToProcessors()
        {
            string xElementProcessors = 
                "<processors>" + "\r\n  " +
                "<processor ID=\"0\" Name=\"ATMega128\">" + "\r\n    " +
                "<eepromSize>0x1000</eepromSize>" + "\r\n    " +
                "<flashSize>0x20000</flashSize>" + "\r\n    " +
                "<bootStartAddress>0x1E000</bootStartAddress>" + "\r\n    " +
                "<bootEndAddress>0x1FFFF</bootEndAddress>" + "\r\n  " +
                "</processor>" + "\r\n  " +
                "<processor ID=\"1\" Name=\"ATMega2560\">" + "\r\n    " +
                "<eepromSize>0x1000</eepromSize>" + "\r\n    " +
                "<flashSize>0x40000</flashSize>" + "\r\n    " +
                "<bootStartAddress>0x3E000</bootStartAddress>" + "\r\n    " +
                "<bootEndAddress>0x3FFFF</bootEndAddress>" + "\r\n  " +
                "</processor>" + "\r\n" +
                "</processors>";

            XElement xElement = XElement.Parse(xElementProcessors);
            var processors = xElement.ToProcessors(XNamespace.None).ToList();

            Assert.AreEqual<int>(2, processors.Count);

            Processor expectedProcessor1 = new Processor(0, "ATMega128", 
                0x1000, 0x20000, 0x1E000, 0x1FFFF);
            Processor expectedProcessor2 = new Processor(1, "ATMega2560", 
                0x1000, 0x40000, 0x3E000, 0x3FFFF);

            Assert.IsTrue(expectedProcessor1.Equals(processors[0]));
            Assert.IsTrue(expectedProcessor2.Equals(processors[1]));
        }

        #endregion

        #region public void ToXElement()

        [TestMethod]
        [TestCategory("ProcessorExtensions")]
        public void ToXElement()
        {
            Processor processor = 
                new Processor(1, "ATMega128", 0x1000, 0x20000 ,0x1E000 ,0x1FFFF);

            XElement xElement = processor.ToXElement("namespace");

            string xElementProcessor =  
                "<processor ID=\"1\" Name=\"ATMega128\" xmlns=\"namespace\">" + "\r\n  " +
                "<eepromSize>0x1000</eepromSize>" + "\r\n  " +
                "<flashSize>0x20000</flashSize>" + "\r\n  " +
                "<bootStartAddress>0x1E000</bootStartAddress>" + "\r\n  " +
                "<bootEndAddress>0x1FFFF</bootEndAddress>" + "\r\n" +
                "</processor>";
         
            Assert.AreEqual<string>(xElementProcessor, xElement.ToString());
        }

        #endregion

        #region public void UpdateXElement()

        [TestMethod]
        [TestCategory("ProcessorExtensions")]
        public void UpdateXElement()
        {
            Processor processor = new Processor(1, "ATMega2560",
                0x1000, 0x40000, 0x3E000, 0x3FFFF);

            string xElementProcessor = 
                "<processor ID=\"1\" Name=\"ATMega128\">" + "\r\n  " +
                "<eepromSize>0x1000</eepromSize>" + "\r\n  " +
                "<flashSize>0x20000</flashSize>" + "\r\n  " +
                "<bootStartAddress>0x1E000</bootStartAddress>" + "\r\n  " +
                "<bootEndAddress>0x1FFFF</bootEndAddress>" + "\r\n" +
                "</processor>";

            XElement xElement = XElement.Parse(xElementProcessor);

            xElement.UpdateXElement(processor, XNamespace.None);

            string xExpectedElementProcessor = 
                "<processor ID=\"1\" Name=\"ATMega2560\">" + "\r\n  " +
                "<eepromSize>0x1000</eepromSize>" + "\r\n  " +
                "<flashSize>0x40000</flashSize>" + "\r\n  " +
                "<bootStartAddress>0x3E000</bootStartAddress>" + "\r\n  " +
                "<bootEndAddress>0x3FFFF</bootEndAddress>" + "\r\n" +
                "</processor>";

            Assert.AreEqual<string>(xExpectedElementProcessor, xElement.ToString());
        }

        #endregion
    }
}

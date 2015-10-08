using System;
using Service.Settings.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BootMega.UnitTests
{
    [TestClass]
    public class ProcessorTests
    {
        #region public void Create()

        [TestMethod]
        [TestCategory("Processor")]
        public void Create()
        {
            Processor p1 = new Processor(0, "Name", 0x1000, 0x40000, 0x3E000, 0x3FFFF);
            Processor p2 = new Processor(0, "Name", 0x1000, 0x40000, 0x3E000, 0x3FFFF);
            
            Assert.AreEqual<string>("Name", p1.ToString());
            Assert.AreEqual<string>("Name", p2.ToString());
            Assert.IsTrue(p1.Equals(p2));

            p2.Name = "NewName";

            Assert.AreEqual<string>("NewName", p2.ToString());
            Assert.IsFalse(p1.Equals(p2));            
        }

        #endregion

        #region public void CreateIdException()

        [TestMethod]
        [TestCategory("Processor")]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateIdException()
        {
            Processor p1 = new Processor(-1, "Name", 0x1000, 0x40000, 0x3E000, 0x3FFFF);
        }

        #endregion

        #region public void CreateNameException()

        [TestMethod]
        [TestCategory("Processor")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateNameException()
        {
            Processor p1 = new Processor(0, String.Empty, 0x1000, 0x40000, 0x3E000, 0x3FFFF);
        }

        #endregion

        #region public void CreateEepromSizeException()

        [TestMethod]
        [TestCategory("Processor")]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateEepromSizeException()
        {
            Processor p1 = new Processor(0, "Name", -1, 0x40000, 0x3E000, 0x3FFFF);
        }

        #endregion

        #region public void CreateFlashSizeException()

        [TestMethod]
        [TestCategory("Processor")]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateFlashSizeException()
        {
            Processor p1 = new Processor(0, "Name", 0x1000, -1, 0x3E000, 0x3FFFF);
        }

        #endregion

        #region public void CreateBootStartAddressException()

        [TestMethod]
        [TestCategory("Processor")]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBootStartAddressException()
        {
            Processor p1 = new Processor(0, "Name", 0x1000, 0x40000, -1, 0x3FFFF);
        }

        #endregion

        #region public void CreateBootEndAddressException()

        [TestMethod]
        [TestCategory("Processor")]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBootEndAddressException()
        {
            Processor p1 = new Processor(0, "Name", 0x1000, 0x40000, 0x3E000, -1);
        }

        #endregion   

        #region public void ProcessorToString()

        [TestMethod]
        [TestCategory("Processor")]
        public void ProcessorToString()
        {
            Processor p = new Processor(1, "Name", 0x1000, 0x40000, 0x3E000, 0x3FFFF);

            Console.WriteLine(p.ToString());
            Console.WriteLine(p.ToString("LOG", null));
        }

        #endregion
    }
}

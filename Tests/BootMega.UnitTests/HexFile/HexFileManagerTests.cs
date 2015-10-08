using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.HexFile;
using Service.HexFile.MemoryMapping;

namespace BootMega.UnitTests
{
    [TestClass]
    public class HexFileManagerTests
    {
        [TestMethod]
        [TestCategory("HexFileManager")]
        public void HexFile()
        {
            IHexFileManager fileManager = new HexFileManager();
            IMemory memory = new Memory(0x40000);
            fileManager.OpenFile(Directory.GetCurrentDirectory() + "\\HexFile\\file1.hex", memory);
            fileManager.SaveFile(Directory.GetCurrentDirectory() + "\\HexFile\\saved_file1.hex", memory);

            IMemory memory1 = new Memory(0x40000);
            fileManager.OpenFile(Directory.GetCurrentDirectory() + "\\HexFile\\saved_file1.hex", memory1);

            Assert.IsTrue(memory.Equals(memory1));

            memory = new Memory(0x40000);
            fileManager.OpenFile(Directory.GetCurrentDirectory() + "\\HexFile\\file2.hex", memory);
            fileManager.SaveFile(Directory.GetCurrentDirectory() + "\\HexFile\\saved_file2.hex", memory);

            memory1 = new Memory(0x40000);
            fileManager.OpenFile(Directory.GetCurrentDirectory() + "\\HexFile\\saved_file2.hex", memory1);

            Assert.IsTrue(memory.Equals(memory1));
        }
    }
}

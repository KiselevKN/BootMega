using System;
using Service.HexFile.MemoryMapping;

namespace Service.HexFile
{
    public interface IHexFileManager
    {
        void OpenFile(String path, IMemory memory);
        void SaveFile(String path, IMemory memory);
    }
}

using Service.HexFile.MemoryMapping;

namespace Service.HexFile
{
    public interface IHexFileManager
    {
        void OpenFile(string path, IMemory memory);
        void SaveFile(string path, IMemory memory);
    }
}

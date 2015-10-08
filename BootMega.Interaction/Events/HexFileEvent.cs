using Microsoft.Practices.Prism.PubSubEvents;
using Service.HexFile.MemoryMapping;

namespace BootMega.Interaction.Events
{
    public class AddNewFileEvent : PubSubEvent<IMemory> { }
    public class FilesChangedEvent : PubSubEvent<object> { }
}

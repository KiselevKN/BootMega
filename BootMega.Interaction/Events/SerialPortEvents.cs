using Microsoft.Practices.Prism.PubSubEvents;

namespace BootMega.Interaction.Events
{
    public class SelectedComPortEvent : PubSubEvent<string> { }
    public class ComPortsNotFoundEvent : PubSubEvent<object> { }
}

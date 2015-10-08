using System;
using Microsoft.Practices.Prism.PubSubEvents;
using Service.Settings.Models;

namespace BootMega.Interaction.Events
{
    public class SelectedSettingsEvent : PubSubEvent<SelectedSettings> { }
    public class SettingsExceptionEvent : PubSubEvent<Exception> { }
}

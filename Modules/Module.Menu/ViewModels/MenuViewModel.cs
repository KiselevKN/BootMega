using BootMega.Interaction.Events;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Service.Settings.Models;

namespace Module.Menu.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        #region fields

        private IEventAggregator eventAggregator;
        private bool memoryType;

        #endregion

        #region ctors

        public MenuViewModel (IEventAggregator eventAggregator)
	    {
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<SelectedSettingsEvent>().Subscribe(OnChangeSelectedSettings);
	    }

        #endregion

        #region methods

        private void OnChangeSelectedSettings(SelectedSettings obj)
        {
            MemoryType = obj.MemoryType == Service.Settings.Enums.MemoryType.FLASH;
        }

        #endregion

        #region properties

        public bool MemoryType
        {
            get { return memoryType; }
            set { SetProperty(ref memoryType, value); }
        }

        #endregion
    }
}

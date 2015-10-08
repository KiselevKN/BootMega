using BootMega.Theme;
using Module.Settings.ViewModels.Base;

namespace Module.Settings.Views
{
    public partial class SettingsView : DialogWindow
    {
        public SettingsView(BaseViewModel vm)
        {
            InitializeComponent();
            vm.Dlg = this;
            DataContext = vm;
        }
    }
}

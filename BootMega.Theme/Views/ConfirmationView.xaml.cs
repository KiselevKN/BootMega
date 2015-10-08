using BootMega.Theme.ViewModels;

namespace BootMega.Theme.Views
{
    public partial class ConfirmationView : DialogWindow
    {
        public ConfirmationView(ConfirmationViewModel vm)
        {
            InitializeComponent();
            vm.Dlg = this;
            DataContext = vm;
        }
    }
}

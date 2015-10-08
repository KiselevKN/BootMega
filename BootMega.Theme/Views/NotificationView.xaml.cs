using BootMega.Theme.ViewModels;

namespace BootMega.Theme.Views
{
    public partial class NotificationView : DialogWindow
    {
        public NotificationView(NotificationViewModel vm)
        {
            InitializeComponent();
            vm.Dlg = this;
            DataContext = vm;
        }
    }
}

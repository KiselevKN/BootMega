using BootMega.Theme;
using Module.IO.ViewModels;

namespace Module.IO.Views
{
    public partial class IOView : DialogWindow
    {
        public IOView(BaseIOViewModel vm)
        {
            InitializeComponent();
            vm.Dlg = this;
            DataContext = vm;
        }
    }
}

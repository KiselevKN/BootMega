using BootMega.Theme;
using Module.IO.ViewModels;

namespace Module.IO.Views
{

    public partial class SelectionFileForDownloadingView : DialogWindow
    {
        public SelectionFileForDownloadingView(SelectionFileForDownloadingViewModel vm)
        {
            InitializeComponent();
            vm.Dlg = this;
            DataContext = vm;
        }
    }
}

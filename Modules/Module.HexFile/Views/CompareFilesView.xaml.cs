using BootMega.Theme;
using Module.HexFile.ViewModels;

namespace Module.HexFile.Views
{
    /// <summary>
    /// Interaction logic for CompareFilesView.xaml
    /// </summary>
    public partial class CompareFilesView : DialogWindow
    {
        public CompareFilesView(CompareFilesViewModel vm)
        {
            InitializeComponent();
            vm.Dlg = this;
            DataContext = vm;
        }
    }
}

using System.Windows.Controls;
using Module.HexFile.ViewModels;

namespace Module.HexFile.Views
{
    /// <summary>
    /// Interaction logic for HexFileManagerView.xaml
    /// </summary>
    public partial class HexFileManagerView : UserControl
    {
        public HexFileManagerView(HexFileManagerViewModel hexFileManagerViewModel)
        {
            InitializeComponent();
            DataContext = hexFileManagerViewModel;
        }
    }
}

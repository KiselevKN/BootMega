using System.Windows.Controls;
using Module.Menu.ViewModels;

namespace Module.Menu.Views
{
    public partial class ToolBarView : UserControl
    {
        public ToolBarView(ToolBarViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}

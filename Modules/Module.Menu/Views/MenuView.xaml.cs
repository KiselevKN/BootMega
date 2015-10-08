using System.Windows.Controls;
using Module.Menu.ViewModels;

namespace Module.Menu.Views
{
    public partial class MenuView : UserControl
    {
        public MenuView(MenuViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}

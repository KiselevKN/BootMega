using System.Windows.Controls;
using Module.StatusBar.ViewModels;

namespace Module.StatusBar.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class StatusBarView : UserControl
    {
        public StatusBarView(StatusBarViewModel statusBarViewModel)
        {
            InitializeComponent();
            DataContext = statusBarViewModel;
        }
    }
}

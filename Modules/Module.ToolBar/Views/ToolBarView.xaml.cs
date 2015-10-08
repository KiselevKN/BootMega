using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Module.ToolBar.ViewModels;

namespace Module.ToolBar.Views
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

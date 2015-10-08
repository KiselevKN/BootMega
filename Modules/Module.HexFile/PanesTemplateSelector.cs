using System.Windows.Controls;
using System.Windows;
using Xceed.Wpf.AvalonDock.Layout;
using Module.HexFile.ViewModels;

namespace Module.HexFile
{
    public class PanesTemplateSelector : DataTemplateSelector
    {
        public PanesTemplateSelector() { }

        public DataTemplate FileViewTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;

            if (item is HexFileViewModel) return FileViewTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}

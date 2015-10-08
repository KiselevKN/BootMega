using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace BootMega.Theme.ViewModels
{
    public abstract class NotificationViewModel : BindableBase
    {
        #region fields

        private DialogWindow dlg;

        #endregion

        #region properties

        public DialogWindow Dlg
        {
            get { return dlg; }
            set { dlg = value; }
        }

        public ICommand CloseCommand { get; private set; }

        public abstract string Text { get; }
        public abstract string Title { get; }
        public abstract string Close { get; } 

        #endregion

        #region ctors

        public NotificationViewModel()
        {
            CloseCommand = new DelegateCommand(OnClose);
        }

        #endregion

        #region methods

        private void OnClose()
        {
            dlg.Result = false;
            SystemCommands.CloseWindow(dlg);
        }

        #endregion
    }
}

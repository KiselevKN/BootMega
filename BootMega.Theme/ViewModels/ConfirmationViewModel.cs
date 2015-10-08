using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace BootMega.Theme.ViewModels
{
    public abstract class ConfirmationViewModel
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

        public ICommand CancelCommand { get; private set; }
        public ICommand OkCommand { get; private set; }

        public abstract string Text { get; }
        public abstract string Title { get; }
        public abstract string Ok { get; }
        public abstract string Cancel { get; }

        #endregion

        #region ctors

        public ConfirmationViewModel()
        {
            CancelCommand = new DelegateCommand(OnCancel);
            OkCommand = new DelegateCommand(OnOk);
        }

        #endregion

        #region methods

        private void OnCancel()
        {
            dlg.Result = false;
            SystemCommands.CloseWindow(dlg);
        }

        private void OnOk()
        {
            dlg.Result = true;
            SystemCommands.CloseWindow(dlg);
        }

        #endregion
    }
}

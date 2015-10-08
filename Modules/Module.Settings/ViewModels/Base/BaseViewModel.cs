using System.Windows.Input;
using System.Windows.Media;
using BootMega.Mvvm;
using BootMega.Theme;
using Microsoft.Practices.Prism.Commands;
using Service.Settings;

namespace Module.Settings.ViewModels.Base
{
    public abstract class BaseViewModel : ValidatableBindableBase
    {
        #region fields

        protected ISettingsRepository settingsRepository;
        private DialogWindow dlg;

        #endregion

        #region ctors

        public BaseViewModel(ISettingsRepository settingsRepository)
        {
            this.settingsRepository = settingsRepository;
            OkCommand = new DelegateCommand(OnOk, OnCanExecutOk);
            CloseCommand = new DelegateCommand(OnClose);
        }

        #endregion

        #region commands

        #region ICommand CloseCommand

        public ICommand CloseCommand { get; private set; }
        
        protected virtual void OnClose()
        {
            Dlg.Result = false;
            Dlg.Close();
        }

        #endregion

        #region ICommand OkCommand
        
        public ICommand OkCommand { get; private set; }

        protected virtual bool OnCanExecutOk()
        {
            return !HasErrors;
        }

        protected virtual void OnOk()
        {
            Dlg.Result = true;
            Dlg.Close();
        }

        #endregion

        #endregion

        #region properties

        public DialogWindow Dlg 
        { 
            get { return dlg; } 
            set { dlg = value; }
        }

        public abstract DrawingImage Icon { get; }

        public abstract string Title { get; }

        #endregion
    }
}

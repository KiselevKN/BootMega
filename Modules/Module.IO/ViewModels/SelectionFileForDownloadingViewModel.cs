using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BootMega.Theme;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Module.HexFile.ViewModels;

namespace Module.IO.ViewModels
{
    public class SelectionFileForDownloadingViewModel : BindableBase
    {
        #region fields

        private ReadOnlyObservableCollection<HexFileViewModel> files;
        private int indexOfTheSelectedFile;
        private DialogWindow dlg;

        #endregion

        #region ctors

        public SelectionFileForDownloadingViewModel(ReadOnlyObservableCollection<HexFileViewModel> files)
        {
            this.files = files;
            IndexOfTheSelectedFile = 0;
            OkCommand = new DelegateCommand(OnOk);
            CloseCommand = new DelegateCommand(OnClose);
        }

        #endregion

        #region properties

        public IEnumerable<string> FileNames
        {
            get { return files.Select(n=>n.FileName); }
        }

        public int IndexOfTheSelectedFile
        {
            get { return indexOfTheSelectedFile; }
            set { SetProperty(ref indexOfTheSelectedFile, value); }
        }
        
        public DialogWindow Dlg 
        { 
            get { return dlg; } 
            set { dlg = value; }
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

        protected virtual void OnOk()
        {
            Dlg.Result = true;
            Dlg.Close();
        }

        #endregion

        #endregion  
    }
}

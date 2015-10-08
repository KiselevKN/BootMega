using System;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Service.HexFile.MemoryMapping;

namespace Module.HexFile.ViewModels
{
    public class HexFileViewModel : BindableBase
    {
        #region fields

        private MemoryViewModel memoryViewModel;
        private HexFileManagerViewModel hexFileManagerViewModel;
        public string path;
        public string fileName;
        public bool isDirty;

        public IMemory memory;

        #endregion

        #region ctors

        public HexFileViewModel(IMemory memory, HexFileManagerViewModel hexFileManagerViewModel)
        {
            this.hexFileManagerViewModel = hexFileManagerViewModel;
            this.memory = memory;
            memoryViewModel = new MemoryViewModel(memory);
            path = "Noname " + DateTime.Now.ToLongTimeString() + "*";
            fileName = "Noname " + DateTime.Now.ToLongTimeString() + "*";
            isDirty = true;
            SaveCommand = new DelegateCommand<object>(OnSave, CanSave);
            CloseCommand = new DelegateCommand<object>(OnClose, CanClose);
            Debug.WriteLine(string.Format("Open File: {0}", path));
        }

        public HexFileViewModel(IMemory memory, string path, HexFileManagerViewModel hexFileManagerViewModel)
        {          
            this.hexFileManagerViewModel = hexFileManagerViewModel;
            memoryViewModel = new MemoryViewModel(memory);
            this.memory = memory;
            this.path = path;
            fileName = System.IO.Path.GetFileName(path);
            isDirty = false;
            SaveCommand = new DelegateCommand<object>(OnSave, CanSave);
            CloseCommand = new DelegateCommand<object>(OnClose, CanClose);
            Debug.WriteLine(string.Format("Open File: {0}", path));
        }

        ~HexFileViewModel()
        {
            Debug.WriteLine(string.Format("Delete File: {0}", path));
        }

        #endregion

        #region properties

        public MemoryViewModel MemoryViewModel
        {
            get { return memoryViewModel; }
        }

        public string Path
        {
            get { return path; }
            set {  SetProperty(ref path, value); }
        }

        
        public string FileName
        {
            get { return fileName; }
            set { SetProperty(ref fileName, value); }
        }

        
        public bool IsDirty
        {
            get { return isDirty; }
            set { SetProperty(ref isDirty, value); }
        }

        #endregion

        #region commands

        #region SaveCommand

        public ICommand SaveCommand { get; private set; }
        private void OnSave(object arg) { hexFileManagerViewModel.Save(this); }
        private bool CanSave(object arg) { return IsDirty; }

        #endregion

        #region CloseCommand

        public ICommand CloseCommand { get; private set; }
        private void OnClose(object arg) { hexFileManagerViewModel.Close(this); }
        private bool CanClose(object arg) { return true; }

        #endregion

        #endregion
    }
}

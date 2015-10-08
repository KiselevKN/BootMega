using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using BootMega.Interaction.Commands;
using BootMega.Interaction.Events;
using BootMega.Theme.Views;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Win32;
using Module.HexFile.Views;
using Service.HexFile;
using Service.HexFile.MemoryMapping;
using Service.Settings.Enums;
using Service.Settings.Models;

namespace Module.HexFile.ViewModels
{
    public class HexFileManagerViewModel : BindableBase
    {
        #region fields

        private IHexFileManager hexFileManager;
        private IEventAggregator eventAggregator;
        private ILoggerFacade logger;
        private SelectedSettings settings;
        private bool firstTimeSettings = false;
        private ObservableCollection<HexFileViewModel> _files = new ObservableCollection<HexFileViewModel>();
        private ReadOnlyObservableCollection<HexFileViewModel> _readonyFiles = null;
        private HexFileViewModel _activeDocument = null;

        private ICommand openFileCommand;
        private ICommand saveFileCommand;
        private ICommand saveAllFilesCommand;
        private ICommand closeFileCommand;
        private ICommand closeAllFilesCommand;
        private ICommand compareFilesCommand;
        private ICommand closeAppCommand;

        #endregion

        #region ctors

        public HexFileManagerViewModel(IHexFileManager hexFileManager, IEventAggregator eventAggregator, ILoggerFacade logger)
        {
            this.hexFileManager = hexFileManager;
            this.eventAggregator = eventAggregator;
            this.logger = logger;

            this.eventAggregator.GetEvent<SelectedSettingsEvent>().Subscribe(OnChangeSelectedSettings);

            closeAppCommand = new DelegateCommand(OnCloseApp);
            GlobalCommands.CloseAppCommand.RegisterCommand(closeAppCommand);

            _files.CollectionChanged += _files_CollectionChanged;
        }

        private void OnCloseApp()
        {
            OnCloseAllFiles();
            Application.Current.MainWindow.Close();
        }

        void _files_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            eventAggregator.GetEvent<FilesChangedEvent>().Publish(Files);
        }

        #endregion

        #region properties

        public ReadOnlyObservableCollection<HexFileViewModel> Files
        {
            get
            {
                if (_readonyFiles == null)
                    _readonyFiles = new ReadOnlyObservableCollection<HexFileViewModel>(_files);
                return _readonyFiles;
            }
        }

        public HexFileViewModel ActiveDocument
        {
            get { return _activeDocument; }
            set 
            {
                if(SetProperty(ref _activeDocument, value))
                    RaiseCanExecuteChanged();      
            }
        }

        #endregion

        #region methods

        private void OnChangeSelectedSettings(SelectedSettings obj)
        {
            settings = obj;

            if (!firstTimeSettings)
            {
                firstTimeSettings = true;

                openFileCommand = new DelegateCommand(OnOpenFile, OnCanOpenFile);
                
                GlobalCommands.OpenHexFileCommand.RegisterCommand(openFileCommand);

                saveFileCommand = new DelegateCommand(OnSaveFile, OnCanSaveFile);
                GlobalCommands.SaveHexFileCommand.RegisterCommand(saveFileCommand);

                saveAllFilesCommand = new DelegateCommand(OnSaveAllFiles, OnCanSaveAllFiles);
                GlobalCommands.SaveAllHexFilesCommand.RegisterCommand(saveAllFilesCommand);

                closeFileCommand = new DelegateCommand(OnCloseFile, OnCanCloseFile);
                GlobalCommands.CloseHexFileCommand.RegisterCommand(closeFileCommand);

                closeAllFilesCommand = new DelegateCommand(OnCloseAllFiles, OnCanCloseAllFiles);
                GlobalCommands.CloseAllHexFilesCommand.RegisterCommand(closeAllFilesCommand);

                compareFilesCommand = new DelegateCommand(OnCompareFiles, OnCanCompareFiles);
                GlobalCommands.CompareHexFilesCommand.RegisterCommand(compareFilesCommand);

                eventAggregator.GetEvent<AddNewFileEvent>().Subscribe(OnAddNewFile);
            }
            else
            {
                while (Files.Count != 0)
                {
                    Close(ActiveDocument);
                }
            }
        }

        private bool OnCanCompareFiles()
        {
            return ((Files != null) && (Files.Count > 1)) ? true : false;
        }

        private void OnCompareFiles()
        {
            CompareFilesViewModel vm = new CompareFilesViewModel(Files);
            CompareFilesView v = new CompareFilesView(vm);
            v.Owner = Application.Current.MainWindow;
            v.ShowDialog();
        }

        private bool OnCanOpenFile()
        {
            return true;
        }

        private void OnOpenFile()
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Hex files (*.hex) | *.hex";
            if (dlg.ShowDialog().GetValueOrDefault())
            {
                try 
                { 
                    long size = (settings.MemoryType == MemoryType.FLASH) 
                        ? settings.SettingsInfo.Processor.FlashSize
                        : settings.SettingsInfo.Processor.EepromSize;
                    IMemory memory = new Memory(size);
                    hexFileManager.OpenFile(dlg.FileName, memory);
                    var hfvm = new HexFileViewModel(memory, dlg.FileName, this);
                    _files.Add(hfvm);
                    ActiveDocument = hfvm;
                }
                catch (Exception ex)
                {
                    NotificationOpenViewModel vm = new NotificationOpenViewModel(ex.Message);
                    NotificationView v = new NotificationView(vm);
                    v.Owner = Application.Current.MainWindow;
                    v.ShowDialog();
                }
                RaiseCanExecuteChanged();
            }
        }

        private bool OnCanCloseFile()
        {
            return ((Files != null) && (Files.Count > 0)) ? true : false;
        }

        private void OnCloseFile()
        {
            Close(ActiveDocument);        
        }

        private bool OnCanCloseAllFiles()
        {
            return ((Files != null) && (Files.Count > 0)) ? true : false;
        }

        private void OnCloseAllFiles()
        {
            while(Files.Count != 0)
            {
                Close(ActiveDocument);  
            }
        }

        private bool OnCanSaveFile()
        {
            return ((Files != null) && (Files.Count > 0) && ActiveDocument.IsDirty) ? true : false;
        }

        private void OnSaveFile()
        {
            Save(ActiveDocument); 
        }
        
        private bool OnCanSaveAllFiles()
        {
            if ((Files != null) && (Files.Count > 0))
            {
                for(int i = 0; i < Files.Count; i++)
                {
                    if (Files[i].IsDirty)
                        return true;
                }
            }
            return false;
        }

        private void OnSaveAllFiles()
        {
            for (int i = 0; i < Files.Count; i++)
            {
                if (Files[i].IsDirty)
                {
                    Save(Files[i]);
                }      
            }
        }
      
        private void OnAddNewFile(IMemory obj)
        {
            var hfvm = new HexFileViewModel(obj, this);
            _files.Add(hfvm);
            ActiveDocument = hfvm;
            RaiseCanExecuteChanged();
        }
        
        internal void Close(HexFileViewModel fileToClose)
        {
            if (fileToClose.IsDirty)
            {
                ConfirmationSaveViewModel vm = new ConfirmationSaveViewModel(fileToClose.FileName);
                ConfirmationView v = new ConfirmationView(vm);
                v.Owner = Application.Current.MainWindow;
                if((bool)v.ShowDialog())
                {
                    Save(fileToClose);
                }                   
            }
                
            _files.Remove(fileToClose);
            GC.Collect();
            RaiseCanExecuteChanged();
        }

        internal void Save(HexFileViewModel fileToSave)
        {
            var dlg = new SaveFileDialog();
            dlg.Filter = "Hex files (*.hex) | *.hex";
            dlg.FileName = fileToSave.FileName;
            if (dlg.ShowDialog().GetValueOrDefault())
            {
                fileToSave.Path = dlg.FileName;
                fileToSave.IsDirty = false;
                fileToSave.FileName = System.IO.Path.GetFileName(dlg.FileName);

                hexFileManager.SaveFile(dlg.FileName, fileToSave.memory);
                RaiseCanExecuteChanged();
            }         
        }

        private void RaiseCanExecuteChanged()
        {
            ((DelegateCommand)closeFileCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)closeAllFilesCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)saveFileCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)saveAllFilesCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)compareFilesCommand).RaiseCanExecuteChanged();
        }

        #endregion
    }
}

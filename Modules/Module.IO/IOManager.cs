using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BootMega.Interaction.Commands;
using BootMega.Interaction.Events;
using BootMega.Theme.ViewModels;
using BootMega.Theme.Views;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.PubSubEvents;
using Module.HexFile.ViewModels;
using Module.IO.ViewModels;
using Module.IO.Views;
using Service.HexFile.MemoryMapping;
using Service.Settings;
using Service.Settings.Models;

namespace Module.IO
{
    public class IOManager
    {
        #region fields

        private ISettingsRepository settingsRepository;
        private readonly IEventAggregator eventAggregator;
        private readonly ILoggerFacade logger;
        private SelectedSettings settings;
        private string port;
        private ReadOnlyObservableCollection<HexFileViewModel> files;
        private bool isConnected = false;

        #endregion

        #region ctors

        public IOManager(ISettingsRepository settingsRepository, IEventAggregator eventAggregator, ILoggerFacade logger)
        {
            this.settingsRepository = settingsRepository;
            this.eventAggregator = eventAggregator;
            this.logger = logger;
            settings = null;
            port = null;
            files = null;

            Subscribe();
            RegisterCommand(); 
        }

        #endregion

        #region methods

        private void RegisterCommand()
        {
            connectionCommand = new DelegateCommand(OnConnection, OnCanExecutConnection);
            GlobalCommands.ConnectionCommand.RegisterCommand(connectionCommand);

            downloadCommand = new DelegateCommand(OnDownload, OnCanExecutDownload);
            GlobalCommands.DownloadCommand.RegisterCommand(downloadCommand);

            uploadCommand = new DelegateCommand(OnUpload, OnCanExecutUpload);
            GlobalCommands.UploadCommand.RegisterCommand(uploadCommand);

            eraseCommand = new DelegateCommand(OnErase, OnCanExecutErase);
            GlobalCommands.EraseCommand.RegisterCommand(eraseCommand);

            readFuseCommand = new DelegateCommand(OnReadFuse, OnCanExecutReadFuse);
            GlobalCommands.ReadFuseCommand.RegisterCommand(readFuseCommand);

            readLockCommand = new DelegateCommand(OnReadLock, OnCanExecutReadLock);
            GlobalCommands.ReadLockCommand.RegisterCommand(readLockCommand);

            resetCommand = new DelegateCommand(OnReset, OnCanExecutReset);
            GlobalCommands.ResetCommand.RegisterCommand(resetCommand);
        }

        private void Subscribe()
        {
            eventAggregator.GetEvent<SelectedSettingsEvent>().Subscribe(OnChangeSelectedSettings);
            eventAggregator.GetEvent<SelectedComPortEvent>().Subscribe(OnChangeSelectedComPort);
            eventAggregator.GetEvent<FilesChangedEvent>().Subscribe(OnFilesChanged);
        }

        private void OnFilesChanged(object obj)
        {
            files = (ReadOnlyObservableCollection<HexFileViewModel>)obj;
            RaiseCanExecuteChanged();
        }

        private void OnChangeSelectedComPort(string obj)
        {
            port = obj;
            RaiseCanExecuteChanged();
        }

        private void OnChangeSelectedSettings(SelectedSettings obj)
        {
            settings = obj;
            RaiseCanExecuteChanged();
        }

        private void RaiseCanExecuteChanged()
        {
            ((DelegateCommand)connectionCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)downloadCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)uploadCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)eraseCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)readFuseCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)readLockCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)resetCommand).RaiseCanExecuteChanged();
        }

        private bool ShowDialog(BaseIOViewModel vm)
        {
            var v = new IOView(vm);
            v.Owner = Application.Current.MainWindow;
            return (bool)v.ShowDialog();
        }

        private bool ShowDialog(NotificationViewModel vm)
        {
            var v = new NotificationView(vm);
            v.Owner = Application.Current.MainWindow;
            return (bool)v.ShowDialog();
        }

        private IMemory SelectFileForDownloading()
        {
            IMemory memory = null;

            if (files.Count == 1)
                memory = files[0].memory;
            else
            {
                var vm = new SelectionFileForDownloadingViewModel(files);
                var v = new SelectionFileForDownloadingView(vm);
                v.Owner = Application.Current.MainWindow;
                var result = (bool)v.ShowDialog();
                if (result)
                {
                    memory = files[vm.IndexOfTheSelectedFile].memory;
                }
            }

            return memory;
        }

        #endregion

        #region commands

        #region ICommand connectionCommand

        private ICommand connectionCommand;

        private bool OnCanExecutConnection()
        {
            return ((port != null) && (settings != null));
        }

        private void OnConnection()
        {
            var vm = new ConnectionViewModel(settings, port, logger);
            isConnected = ShowDialog(vm);

            if (isConnected)
                ShowDialog(new NotificationConnectionViewModel(vm.Version, vm.Date));

            if (!isConnected && vm.Exception != null)
                ShowDialog(new NotificationErrorViewModel((vm.Exception.InnerException != null) ? 
                    vm.Exception.InnerException.Message : 
                    vm.Exception.Message));

            RaiseCanExecuteChanged();
        }

        #endregion

        #region ICommand downloadCommand

        private ICommand downloadCommand;

        private bool OnCanExecutDownload()
        {
            return ((port != null) && (settings != null) && (files != null) && (files.Count > 0) && isConnected);
        }

        private void OnDownload()
        {
            IMemory memory = SelectFileForDownloading();

            if (memory != null)
            {
                var vm = new WriteViewModel(settings, port, logger, memory);
                var result = ShowDialog(vm);

                if (result)
                    ShowDialog(new NotificationWriteViewModel());

                if (!result && vm.Exception != null)
                    ShowDialog(new NotificationErrorViewModel((vm.Exception.InnerException != null) ?
                        vm.Exception.InnerException.Message :
                        vm.Exception.Message));

                RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region ICommand uploadCommand

        private ICommand uploadCommand;

        private bool OnCanExecutUpload()
        {
            return ((port != null) && (settings != null) && isConnected);
        }

        private void OnUpload()
        {
            var vm = new ReadViewModel(settings, port, logger);
            var result = ShowDialog(vm);

            if (result)
            {
                eventAggregator.GetEvent<AddNewFileEvent>().Publish(vm.Memory);
                ShowDialog(new NotificationReadViewModel());
            }

            if (!result && vm.Exception != null)
                ShowDialog(new NotificationErrorViewModel((vm.Exception.InnerException != null) ?
                    vm.Exception.InnerException.Message :
                    vm.Exception.Message));

            RaiseCanExecuteChanged();
        }

        #endregion

        #region ICommand eraseCommand

        private ICommand eraseCommand;

        private bool OnCanExecutErase()
        {
            return ((port != null) && (settings != null) && isConnected);
        }

        private void OnErase()
        {
            var vm = new EraseViewModel(settings, port, logger);
            var result = ShowDialog(vm);

            if (result)
                ShowDialog(new NotificationEraseViewModel());

            if (!result && vm.Exception != null)
                ShowDialog(new NotificationErrorViewModel((vm.Exception.InnerException != null) ?
                    vm.Exception.InnerException.Message :
                    vm.Exception.Message));

            RaiseCanExecuteChanged();
        }

        #endregion

        #region ICommand readFuseCommand

        private ICommand readFuseCommand;

        private bool OnCanExecutReadFuse()
        {
            return ((port != null) && (settings != null) && isConnected);
        }

        private void OnReadFuse()
        {
            var vm = new ReadFuseViewModel(settings, port, logger);
            var result = ShowDialog(vm);

            if (result)
                ShowDialog(new NotificationFuseViewModel(vm.Fuses));

            if (!result && vm.Exception != null)
                ShowDialog(new NotificationErrorViewModel((vm.Exception.InnerException != null) ?
                    vm.Exception.InnerException.Message :
                    vm.Exception.Message));

            RaiseCanExecuteChanged();
        }

        #endregion

        #region ICommand readLockCommand

        private ICommand readLockCommand;

        private bool OnCanExecutReadLock()
        {
            return ((port != null) && (settings != null) && isConnected);
        }

        private void OnReadLock()
        {
            var vm = new ReadLockViewModel(settings, port, logger);
            var result = ShowDialog(vm);

            if (result)
                ShowDialog(new NotificationLockViewModel(vm.Locks));

            if (!result && vm.Exception != null)
                ShowDialog(new NotificationErrorViewModel((vm.Exception.InnerException != null) ?
                    vm.Exception.InnerException.Message :
                    vm.Exception.Message));

            RaiseCanExecuteChanged();
        }

        #endregion

        #region ICommand resetCommand

        private ICommand resetCommand;

        private bool OnCanExecutReset()
        {
            return ((port != null) && (settings != null) && isConnected);
        }

        private void OnReset()
        {
            var vm = new ResetViewModel(settings, port, logger);
            var result = ShowDialog(vm);

            if (result)
                ShowDialog(new NotificationResetViewModel());

            if (!result && vm.Exception != null)
                ShowDialog(new NotificationErrorViewModel((vm.Exception.InnerException != null) ?
                    vm.Exception.InnerException.Message :
                    vm.Exception.Message));
            
            RaiseCanExecuteChanged();
        }

        #endregion

        #endregion
    }
}

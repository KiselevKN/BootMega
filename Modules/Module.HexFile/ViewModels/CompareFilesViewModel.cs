using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BootMega.Theme;
using System.Windows.Input;
using Microsoft.Windows.Shell;
using Microsoft.Practices.Prism.Commands;
using Module.HexFile.Properties;
using BootMega.Mvvm;
using Service.HexFile.MemoryMapping;
using Module.HexFile.Models;
using System.ComponentModel;

namespace Module.HexFile.ViewModels
{
    public class CompareFilesViewModel : ValidatableBindableBase
    {
        #region fields

        private ReadOnlyObservableCollection<HexFileViewModel> files;
        private int indexOfTheSelectedLeftFile;
        private int indexOfTheSelectedRightFile;
        private DialogWindow dlg;
        private IMemory leftMemory;
        private IMemory rightMemory;
        private Address address;
        private MemoryPage leftPage;
        private MemoryPage rightPage;
        private Addresses addresses;
        private string status;
        private string differences;
        private bool filesAreEqual;
        private int indexOfTheSelectedAddress;
        private bool init = false;
        private SortedSet<long> addressesOfDiffPages;

        #endregion

        #region ctors

        public CompareFilesViewModel(ReadOnlyObservableCollection<HexFileViewModel> files)
        {
            this.files = files;
            CloseCommand = new DelegateCommand(OnClose);
            IndexOfTheSelectedLeftFile = 0;
            IndexOfTheSelectedRightFile = 1;
        }

        #endregion

        #region properties

        public Address Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        public Addresses Addresses
        {
            get { return addresses; }
        }

        public MemoryPage RightPage
        {
            get { return rightPage; }
        }

        public MemoryPage LeftPage
        {
            get { return leftPage; }
        }

        public IEnumerable<HexFileViewModel> Files
        {
            get { return files; }
        }

        public int IndexOfTheSelectedLeftFile
        {
            get { return indexOfTheSelectedLeftFile; }
            set
            {
                SetProperty(ref indexOfTheSelectedLeftFile, value);
                _Update();
            }
        }

        public int IndexOfTheSelectedRightFile
        {
            get { return indexOfTheSelectedRightFile; }
            set
            {
                SetProperty(ref indexOfTheSelectedRightFile, value);
                _Update();
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                SetProperty(ref status, value);
            }
        }

        public string Differences
        {
            get { return differences; }
            set
            {
                SetProperty(ref differences, value);
            }
        }

        public bool FilesAreEqual
        {
            get { return filesAreEqual; }
            set
            {
                SetProperty(ref filesAreEqual, value);
            }
        }

        public int IndexOfTheSelectedAddress
        {
            get { return indexOfTheSelectedAddress; }
            set
            {
                SetProperty(ref indexOfTheSelectedAddress, value);

                if (addressesOfDiffPages.Count > 0)
                Address.Value = addressesOfDiffPages.ToArray()[value];
            }
        }

        public DialogWindow Dlg 
        { 
            get { return dlg; } 
            set { dlg = value; }
        }

        public ObservableCollection<string> AddressesOfDiffPages { get; set; }

        #endregion

        #region commands

        public ICommand CloseCommand { get; private set; }
        
        protected virtual void OnClose()
        {
            Dlg.Result = false;
            SystemCommands.CloseWindow(dlg);
        }

        #endregion

        #region methods

        private void _Update()
        {

            leftMemory = files[IndexOfTheSelectedLeftFile].memory;
            rightMemory = files[IndexOfTheSelectedRightFile].memory;

            List<long> addressesOfDifferences;
            if (Memory.Equals(leftMemory,rightMemory, out addressesOfDifferences))
            {
                Status = Resources.FilesAreEquals;
                FilesAreEqual = true;
                Differences = "";
            }
            else
            {
                Status = Resources.FilesAreNotEquals;
                FilesAreEqual = false;
                Differences = String.Format(Resources.Difference, addressesOfDifferences.Count);
            }


            addressesOfDiffPages = new SortedSet<long>();

            foreach (var address in addressesOfDifferences)
            {
                addressesOfDiffPages.Add((address/Page.Size) * Page.Size);
            }


            if (!init)
            {
                init = true;
                leftPage = new MemoryPage();
                rightPage = new MemoryPage();
                addresses = new Addresses((long)(leftMemory.Size - 0x100));
                Address = new Address();
                Address.PropertyChanged += Address_PropertyChanged;
                Address.Maximum = (long)(leftMemory.Size - 0x100);
                AddressesOfDiffPages = new ObservableCollection<string>();
            }
            AddressesOfDiffPages.Clear();
            foreach (var address in addressesOfDiffPages)
            {
                var lenght = Address.Maximum.ToString("X").Length;
                AddressesOfDiffPages.Add(address.ToString("X" + lenght));
            }
            IndexOfTheSelectedAddress = 0;
            Update();
        }

        void Address_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            for (int i = 0; i < 16; i++)
                Addresses.Values[i].Value = Address.Value + (long)(i * 16);

            for (int i = 0; i < 256; i++)
            {
                LeftPage.Cells[i].Value = leftMemory[Address.Value + i];
                RightPage.Cells[i].Value = rightMemory[Address.Value + i];

                LeftPage.Cells[i].InCompareMode = true;
                RightPage.Cells[i].InCompareMode = true;

                var condition = (LeftPage.Cells[i].Value != RightPage.Cells[i].Value);

                LeftPage.Cells[i].NotEqual = condition;
                RightPage.Cells[i].NotEqual = condition;
            }
        }

        #endregion
    }
}

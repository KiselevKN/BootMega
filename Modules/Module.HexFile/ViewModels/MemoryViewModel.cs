using System.ComponentModel;
using Module.HexFile.Models;
using Microsoft.Practices.Prism.Mvvm;
using Service.HexFile.MemoryMapping;

namespace Module.HexFile.ViewModels
{
    public class MemoryViewModel : BindableBase
    {
        #region fields

        private IMemory memory;
        private Address address;
        private MemoryPage page;
        private Addresses addresses;
        private int size;
        private long fillingFactor;
        private double fillingFactorPercents;

        #endregion

        #region ctors

        public MemoryViewModel(IMemory memory)
        {
            this.memory = memory;
            page = new MemoryPage();
            addresses = new Addresses(memory.Size - 0x100);
            Address = new Address();
            Address.PropertyChanged += Address_PropertyChanged;
            Address.Maximum = memory.Size - 0x100;
            size = (int)memory.Size / 1024;
            fillingFactor = memory.FillingFactor;
            fillingFactorPercents = fillingFactor / (double)memory.Size * 100.0;
        }

        ~MemoryViewModel()
        {
            Address.PropertyChanged -= Address_PropertyChanged;
        }

        #endregion

        #region methods

        void Address_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            for (int i = 0; i < 16; i++)
                Addresses.Values[i].Value = Address.Value + i * 16;

            for (int i = 0; i < 256; i++)
                Page.Cells[i].Value = memory[Address.Value + i];
        }

        #endregion

        #region properties

        public Address Address
        {
            get { return address; }
            set {  SetProperty(ref address, value); }
        }

        public Addresses Addresses
        {
            get { return addresses; }
        }

        public MemoryPage Page
        {
            get { return page; }
        }

        public long FillingFactor
        {
            get { return fillingFactor; }
        }

        public double FillingFactorPercents
        {
            get { return fillingFactorPercents; }
        }

        public int Size
        {
            get { return size; }
        }

        #endregion
    }
}

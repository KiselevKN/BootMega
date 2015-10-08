using System;
using System.IO.Ports;
using System.Windows;
using BootMega.IO.Imitation.ViewModels;
using Microsoft.Practices.Prism.Logging;
using Service.HexFile;
using Service.HexFile.MemoryMapping;
using Service.Settings.Enums;
using Service.Settings.Models;

namespace BootMega.IO.Imitation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPortSettings serialPortSettings = new SerialPortSettings(BaudRate.BR_57600,
                    Parity.Odd, StopBits.One, 0xAA, 0xA5);
        string portName = "COM2";
        ILoggerFacade logger = new Logger();
        public MainWindow()
        {
            InitializeComponent();

            ConnectionViewModel vmc = new ConnectionViewModel(portName, serialPortSettings, logger, 12, new DateTime(2015, 06, 14)); 
            connection.DataContext = vmc;

            ResetViewModel vmr = new ResetViewModel(portName, serialPortSettings, logger);
            reset.DataContext = vmr;

            ReadLockViewModel vmrl = new ReadLockViewModel(portName, serialPortSettings, logger, 0x67);
            readLock.DataContext = vmrl;

            ReadFuseViewModel vmrf = new ReadFuseViewModel(portName, serialPortSettings, logger, new byte[] {0x23, 0x56, 0xAF});
            readFuse.DataContext = vmrf;

            

            HexFileManager hfm = new HexFileManager();
            var memory = new Memory(0x40000);
            hfm.OpenFile("Flash.hex", memory);
            var memoryEeprom = new Memory(0x1000);
            hfm.OpenFile("Eeprom.hex", memoryEeprom);

            EraseViewModel vmref = new EraseViewModel(portName, serialPortSettings, logger, MemoryType.FLASH, memory);
            eraseFlash.DataContext = vmref;

            EraseViewModel vmree = new EraseViewModel(portName, serialPortSettings, logger, MemoryType.EEPROM, memoryEeprom);
            eraseEeprom.DataContext = vmree;
            ReadViewModel vmrrf = new ReadViewModel(portName, serialPortSettings, logger, MemoryType.FLASH, memory);
            readFlash.DataContext = vmrrf;

            ReadViewModel _vmrrf = new ReadViewModel(portName, serialPortSettings, logger, MemoryType.EEPROM, memoryEeprom);
            readEeprom.DataContext = _vmrrf;

            IsEmptyViewModel _vmref = new IsEmptyViewModel(portName, serialPortSettings, logger, MemoryType.FLASH, memory);
            isEmptyFlash.DataContext = _vmref;

            IsEmptyViewModel _vmrre = new IsEmptyViewModel(portName, serialPortSettings, logger, MemoryType.EEPROM, memoryEeprom);
            isEmptyEeprom.DataContext = _vmrre;


            WriteViewModel __vmref = new WriteViewModel(portName, serialPortSettings, logger, MemoryType.FLASH, memory);
            writeFlash.DataContext = __vmref;

            WriteViewModel __vmrre = new WriteViewModel(portName, serialPortSettings, logger, MemoryType.EEPROM, memoryEeprom);
            writeEeprom.DataContext = __vmrre;
        }
    }
}

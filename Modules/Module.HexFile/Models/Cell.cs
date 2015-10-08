using Microsoft.Practices.Prism.Mvvm;

namespace Module.HexFile.Models
{
    public class Cell : BindableBase
    {
        #region fields

        private byte value;
        private bool inCompareMode;
        private bool notEqual;

        #endregion

        #region consts

        public const byte Empty = 0xFF;

        #endregion

        #region ctors

        public Cell()
        {
            Value = Empty;
            InCompareMode = false;
            NotEqual = false;
        }

        #endregion

        #region properties

        public byte Value
        {
            get { return value; }
            set { SetProperty(ref this.value, value); }
        }

        public bool InCompareMode
        {
            get { return inCompareMode; }
            set { SetProperty(ref inCompareMode, value); }
        }     
        
        public bool NotEqual
        {
            get { return notEqual; }
            set { SetProperty(ref notEqual, value); }
        }

        #endregion
    }
}

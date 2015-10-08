using Microsoft.Practices.Prism.Mvvm;

namespace Module.HexFile.Models
{
    public class Address : BindableBase
    {
        #region properties

        #region long Value

        private long value;
        public long Value
        {
            get { return value; }
            set { SetProperty(ref this.value, value); }
        }

        #endregion

        #region long Maximum

        private long maximum;
        public long Maximum
        {
            get { return maximum; }
            set { SetProperty(ref maximum, value); }
        }

        #endregion

        #region long Minimum
        
        private long minimum;
        public long Minimum
        {
            get { return minimum; }
            set { SetProperty(ref minimum, value); }
        }
        #endregion

        #endregion
    }
}

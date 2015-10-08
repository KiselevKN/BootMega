using Microsoft.Practices.Prism.Mvvm;

namespace Module.HexFile.Models
{
    public class Address : BindableBase
    {
        #region fields

        private long value;
        private long maximum;
        private long minimum;

        #endregion

        #region properties

        public long Value
        {
            get { return value; }
            set { SetProperty(ref this.value, value); }
        }

        public long Maximum
        {
            get { return maximum; }
            set { SetProperty(ref maximum, value); }
        }

        public long Minimum
        {
            get { return minimum; }
            set { SetProperty(ref minimum, value); }
        }

        #endregion
    }
}

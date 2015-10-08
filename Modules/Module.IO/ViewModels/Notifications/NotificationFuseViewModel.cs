using BootMega.Theme.ViewModels;
using Module.IO.Properties;

namespace Module.IO.ViewModels
{
    public class NotificationFuseViewModel : NotificationViewModel
    {
        #region fields

        private byte[] fuses;

        #endregion

        #region ctors

        public NotificationFuseViewModel(byte[] fuses)
        {
            this.fuses = fuses;
        }

        #endregion

        #region properties

        public override string Text
        {
            get { return string.Format(Resources.NotificationFuse, fuses[0], fuses[1], fuses[2]); }
        }

        public override string Title
        {
            get { return Resources.Notification; }
        }

        public override string Close
        {
            get { return Resources.Close; }
        }

        #endregion
    }
}

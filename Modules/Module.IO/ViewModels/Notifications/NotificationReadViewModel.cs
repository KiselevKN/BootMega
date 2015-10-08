using BootMega.Theme.ViewModels;
using Module.IO.Properties;

namespace Module.IO.ViewModels
{
    public class NotificationReadViewModel : NotificationViewModel
    {
        #region properties

        public override string Text
        {
            get { return Resources.NotificationRead; }
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

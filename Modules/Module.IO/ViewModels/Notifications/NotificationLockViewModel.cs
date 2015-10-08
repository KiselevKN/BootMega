using System;
using BootMega.Theme.ViewModels;
using Module.IO.Properties;

namespace Module.IO.ViewModels
{
    public class NotificationLockViewModel : NotificationViewModel
    {
        #region fields

        private byte locks;

        #endregion

        #region ctors

        public NotificationLockViewModel(byte locks)
        {
            this.locks = locks;
        }

        #endregion

        #region properties

        public override string Text
        {
            get { return String.Format(Resources.NotificationLock, locks); }
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

using System;
using BootMega.Theme.ViewModels;
using Module.IO.Properties;

namespace Module.IO.ViewModels
{
    public class NotificationConnectionViewModel : NotificationViewModel
    {
        #region fields

        private int version;
        private DateTime date;

        #endregion

        #region ctors

        public NotificationConnectionViewModel(int version, DateTime date)
        {
            this.version = version;
            this.date = date;
        }

        #endregion

        #region properties

        public override string Text
        {
            get { return string.Format(Resources.NotificationConnection, version, date.ToShortDateString()); }
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

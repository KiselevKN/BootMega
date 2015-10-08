using BootMega.Theme.ViewModels;
using Module.IO.Properties;

namespace Module.IO.ViewModels
{
    public class NotificationErrorViewModel : NotificationViewModel
    {
        #region fields
         
        private string error;

        #endregion

        #region ctors

        public NotificationErrorViewModel(string error)
        {
            this.error = error;
        }

        #endregion

        #region properties

        public override string Text
        {
            get { return error; }
        }

        public override string Title
        {
            get { return Resources.Error; }
        }

        public override string Close
        {
            get { return Resources.Close; }
        }

        #endregion
    }
}

using BootMega.Theme.ViewModels;
using Module.HexFile.Properties;

namespace Module.HexFile.ViewModels
{
    public class NotificationOpenViewModel : NotificationViewModel
    {
        #region fields
         
        private string error;

        #endregion

        #region ctors

        public NotificationOpenViewModel(string error)
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

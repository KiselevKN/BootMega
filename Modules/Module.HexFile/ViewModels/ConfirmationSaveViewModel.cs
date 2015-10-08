using System;
using BootMega.Theme.ViewModels;
using Module.HexFile.Properties;

namespace Module.HexFile.ViewModels
{
    public class ConfirmationSaveViewModel : ConfirmationViewModel
    {
        #region fields
         
        private string fileName;

        #endregion

        #region ctors

        public ConfirmationSaveViewModel(string fileName)
        {
            this.fileName = fileName;
        }

        #endregion

        #region properties

        public override string Text
        {
            get { return String.Format(Resources.SaveFile, fileName); }
        }

        public override string Title
        {
            get { return Resources.Сonfirmation; }
        }

        public override string Ok
        {
            get { return Resources.Ok; }
        }

        public override string Cancel
        {
            get { return Resources.Cancel; }
        }

        #endregion
    }
}

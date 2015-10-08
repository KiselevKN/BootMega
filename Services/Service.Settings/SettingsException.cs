using System;

namespace Service.Settings
{
    [Serializable]
    public class SettingsException : ApplicationException
    {
        #region ctors

        public SettingsException() { }
        public SettingsException(string message) : base(message) { }
        public SettingsException(string message, Exception inner) : base(message, inner) { }
        protected SettingsException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext contex) : base(info, contex) { }

        #endregion
    }
}

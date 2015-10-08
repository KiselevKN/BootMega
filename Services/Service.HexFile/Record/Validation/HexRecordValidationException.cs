using System;

namespace Service.HexFile.Record.Validation
{
    [Serializable]
    public class HexRecordValidationException : ApplicationException
    {
        #region ctors

        public HexRecordValidationException() { }
        public HexRecordValidationException(string message) : base(message) { }
        public HexRecordValidationException(string message, Exception inner) : base(message, inner) { }
        protected HexRecordValidationException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext contex) : base(info, contex) { }

        #endregion
    }
}

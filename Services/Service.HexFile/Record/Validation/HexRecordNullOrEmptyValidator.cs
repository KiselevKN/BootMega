using System;

namespace Service.HexFile.Record.Validation
{
    /// <summary>
    /// Null or empty record validator
    /// </summary>
    internal class HexRecordNullOrEmptyValidator : IHexRecordValidator
    {
        #region ctors

        internal HexRecordNullOrEmptyValidator() { }

        #endregion

        #region IHexRecordValidator Members

        public void Validate(string record)
        {
            if (string.IsNullOrEmpty(record))
                throw new ArgumentNullException("record");
        }

        #endregion
    }
}

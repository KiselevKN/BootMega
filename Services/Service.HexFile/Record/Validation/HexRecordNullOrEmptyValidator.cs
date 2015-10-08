namespace Service.HexFile.Record.Validation
{
    using System;

    /// <summary>
    /// Null or empty record validator
    /// </summary>
    internal class HexRecordNullOrEmptyValidator : IHexRecordValidator
    {
        #region ctors

        internal HexRecordNullOrEmptyValidator() { }

        #endregion

        #region IHexRecordValidator Members

        public void Validate(String record)
        {
            if (String.IsNullOrEmpty(record))
                throw new ArgumentNullException("record");
        }

        #endregion
    }
}

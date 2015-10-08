namespace Service.HexFile.Record.Validation
{
    using System;
    using Service.HexFile.Properties;

    /// <summary>
    /// CheckSum validator
    /// </summary>
    internal class HexRecordСheckSumValidator : IHexRecordValidator
    {
        #region ctors

        internal HexRecordСheckSumValidator() { }

        #endregion

        #region IHexRecordValidator Members

        public void Validate(String record)
        {
            if (record.GetCheckSum() != record.GetCheckSumInFact())
                throw new HexRecordValidationException(String.Format(Resources.InvalidRecordChecksum, record));
        }

        #endregion
    }
}

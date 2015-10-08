using Service.HexFile.Properties;

namespace Service.HexFile.Record.Validation
{
    /// <summary>
    /// CheckSum validator
    /// </summary>
    internal class HexRecordСheckSumValidator : IHexRecordValidator
    {
        #region ctors

        internal HexRecordСheckSumValidator() { }

        #endregion

        #region IHexRecordValidator Members

        public void Validate(string record)
        {
            if (record.GetCheckSum() != record.GetCheckSumInFact())
                throw new HexRecordValidationException(string.Format(Resources.InvalidRecordChecksum, record));
        }

        #endregion
    }
}

using Service.HexFile.Properties;

namespace Service.HexFile.Record.Validation
{
    /// <summary>
    /// Record length validator
    /// </summary>
    internal class HexRecordLengthValidator : IHexRecordValidator
    {
        #region ctors

        internal HexRecordLengthValidator() { }

        #endregion

        #region IHexRecordValidator Members

        public void Validate(string record)
        {
            if (record.GetNumberOfDataBytes() != record.GetNumberOfDataBytesInFact())
                throw new HexRecordValidationException(string.Format(Resources.InvalidRecordLength, record));
        }

        #endregion
    }
}

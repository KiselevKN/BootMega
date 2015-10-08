using System.Text.RegularExpressions;
using Service.HexFile.Properties;

namespace Service.HexFile.Record.Validation
{
    /// <summary>
    /// Pattern validator
    /// </summary>
    internal class HexRecordPatternValidator : IHexRecordValidator
    {
        #region consts

        private const string pattern = "^:([a-fA-F0-9]{2}){3}(0[0-5]){1}([a-fA-F0-9]{2}){1,129}$";

        #endregion

        #region ctors

        internal HexRecordPatternValidator()
        {

        }

        #endregion

        #region IHexRecordValidator Members

        public void Validate(string record)
        {
            Regex rgx = new Regex(pattern);
            if (!rgx.IsMatch(record))
                throw new HexRecordValidationException(string.Format(Resources.RecordDoesNotMatchThePattern, record, pattern));
        }

        #endregion
    }
}

namespace Service.HexFile.Record.Validation
{
    using System;
    using System.Collections.Generic;
    using Service.HexFile.Properties;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Pattern validator
    /// </summary>
    internal class HexRecordTypeValidator : IHexRecordValidator
    {
        #region fields

        internal Dictionary<HexRecordType, String> patterns;

        #endregion

        #region ctors
        
        internal HexRecordTypeValidator()
        {
            patterns = new Dictionary<HexRecordType, string>(6);
            patterns.Add(HexRecordType.Data, @"^:([a-fA-F0-9]{2}){3}(00){1}([a-fA-F0-9]{2}){1,129}$");
            patterns.Add(HexRecordType.EOF, @"^:00000001FF$");
            patterns.Add(HexRecordType.ExtendedSegmentAddress, @"^:(02000002){1}([a-fA-F0-9]{2}){3}$");
            patterns.Add(HexRecordType.StartSegmentAddress, @"^:(04000003){1}([a-fA-F0-9]{2}){5}$");
            patterns.Add(HexRecordType.ExtendedLinearAddress, @"^:(02000004){1}([a-fA-F0-9]{2}){3}$");
            patterns.Add(HexRecordType.StartLinearAddress, @"^:(04000005){1}([a-fA-F0-9]{2}){5}$");
        }

        #endregion

        #region IHexRecordValidator Members

        public void Validate(String record)
        {
            HexRecordType type = record.GetRecordType();
            Regex rgx = new Regex(patterns[type]);
            if (!rgx.IsMatch(record))
                throw new HexRecordValidationException(String.Format(Resources.RecordDoesNotMatchThePattern, record, patterns[type]));
        }

        #endregion
    }
}

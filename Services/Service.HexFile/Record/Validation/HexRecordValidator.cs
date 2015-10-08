using System.Collections.Generic;

namespace Service.HexFile.Record.Validation
{
    /// <summary>
    /// Main validator
    /// </summary>
    internal class HexRecordValidator : IHexRecordValidator
    {
        #region fields
        
        /// <summary>
        /// List of validators
        /// </summary>
        private List<IHexRecordValidator> validators;

        #endregion

        #region ctors

        internal HexRecordValidator()
        {
            validators = new List<IHexRecordValidator>();
            validators.Add(new HexRecordNullOrEmptyValidator());
            validators.Add(new HexRecordPatternValidator());
            validators.Add(new HexRecordTypeValidator());
            validators.Add(new HexRecordLengthValidator());
            validators.Add(new HexRecordСheckSumValidator());
        }

        #endregion

        #region IHexRecordValidator Members

        public void Validate(string record)
        {
            foreach (var validator in validators)
                validator.Validate(record);
        }

        #endregion
    }
}

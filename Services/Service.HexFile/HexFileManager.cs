using System;
using System.Collections.Generic;
using System.IO;
using Service.HexFile.MemoryMapping;
using Service.HexFile.Record;
using Service.HexFile.Record.Parsing;
using Service.HexFile.Record.Validation;

namespace Service.HexFile
{
    public class HexFileManager : IHexFileManager
    {
        #region fields

        private IHexRecordValidator recordValidator;
        private IHexRecordParser recordParser;

        #endregion

        #region ctors

        public HexFileManager()
        {
            recordParser = new HexRecordParser();
            recordValidator = new HexRecordValidator();
        }

        #endregion

        #region IHexFileManager Members

        public void OpenFile(string path, IMemory memory)
        {
            try
            {
                ValidationAndParsing(path).FillMemory(memory);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void SaveFile(string path, IMemory memory)
        {
            try
            {
                using (FileStream fs = File.Open(path, FileMode.Create, FileAccess.Write))
                {
                    memory.ToHexRecords().Parsing(fs, recordParser);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region methods

        internal List<HexRecord> ValidationAndParsing(string path)
        {
            List<HexRecord> records;

            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                fs.Validation(recordValidator);
                fs.Seek(0, SeekOrigin.Begin);
                records = fs.Parsing(recordParser);
            }

            return records;
        }

        #endregion
    }
}

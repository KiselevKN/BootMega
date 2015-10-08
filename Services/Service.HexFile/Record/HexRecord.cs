using System.Collections.Generic;
using System.Text;

namespace Service.HexFile.Record
{
    /// <summary>
    /// Base class of hex record
    /// </summary>
    internal abstract class HexRecord
    {
        internal const string EOF = ":00000001FF"; 

        internal abstract HexRecordType RecordType { get; }
        internal abstract long Address { get; }
        internal abstract IEnumerable<byte> AsBytes();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(":");
            sb.Append(AsBytes().ToHexString());
            return sb.ToString();
        }
    }
}

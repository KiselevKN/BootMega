using System.Collections.Generic;

namespace Module.HexFile.Models
{
    public class Addresses
    {
        #region consts

        public const int Size = 16;

        #endregion
        
        #region fields

        private readonly List<Address> values;

        #endregion

        #region ctors

        public Addresses(long maxAddress)
        {
            values = new List<Address>(Size);
            for (int i = 0; i < Size; i++)
                values.Add(new Address() { Maximum = maxAddress });
        }

        #endregion

        #region properties

        public List<Address> Values
        {
            get { return values; }
        }

        #endregion
    }
}

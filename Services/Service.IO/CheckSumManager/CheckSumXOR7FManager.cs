using Service.IO.Buffers;

namespace Service.IO.CheckSumManager
{
    public class CheckSumXOR7FManager : ICheckSumManager
    {
        #region ICheckSumManager Members

        public void Calculate(IOBuffer b)
        {
            b[b.Size - 1] = _Calculate(b);
        }

        public bool IsValid(IOBuffer b)
        {
            return (_Calculate(b) == b[b.Size - 1]);
        }

        #endregion

        #region methods

        private byte _Calculate(IOBuffer b)
        {
            byte checkSum = 0;

            for (int k = 0; k < b.Size - 1; k++)
                checkSum ^= b[k];

            checkSum &= 0x7F;

            return checkSum;
        }

        #endregion
    }
}

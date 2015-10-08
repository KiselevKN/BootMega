using System;
using System.Text;

namespace Service.IO.Buffers
{
    /// <summary>
    /// Base class Tx/Rx buffers
    /// </summary>
    public abstract class IOBuffer
    {
        #region consts

        public const int ConnectionBufferTxSize = 18;
        public const int ConnectionBufferRxSize = 24;

        public const int WritePageBufferTxSize = 28;
        public const int WritePageBufferRxSize = 9;

        public const int ReadPageBufferTxSize = 9;
        public const int ReadPageBufferRxSize = 28;

        public const int ErasePageBufferTxSize = 8;
        public const int ErasePageBufferRxSize = 8;

        public const int ReadFuseBufferTxSize = 4;
        public const int ReadFuseBufferRxSize = 8;

        public const int ReadLockBufferTxSize = 4;
        public const int ReadLockBufferRxSize = 6;

        public const int ResetBufferTxSize = 4;
        public const int ResetBufferRxSize = 5;

        public const int IsEmptyPageBufferTxSize = 8;
        public const int IsEmptyPageBufferRxSize = 9;

        #endregion

        #region fields

        private byte[] array;

        #endregion

        #region ctors

        public IOBuffer(int size)
        {
            array = new byte[size];
        }

        #endregion

        #region indexer

        public byte this[int index]
        {
            get 
            {
                if (index < 0 || index > Size)
                    throw new IndexOutOfRangeException("index");

                return array[index]; 
            }
            set 
            {
                if (index < 0 || index > Size)
                    throw new IndexOutOfRangeException("index");
                
                array[index] = value; 
            }
        }

        #endregion

        #region properties

        public int Size { get { return array.Length; } }

        #endregion

        #region operators

        public static implicit operator byte[](IOBuffer b)
        {
            return b.array;
        }

        #endregion

        #region overrides

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

           foreach(var element in array)
                sb.AppendFormat("{0:X2} ", element);

            return sb.ToString().Trim();
        }

        #endregion
    }
}

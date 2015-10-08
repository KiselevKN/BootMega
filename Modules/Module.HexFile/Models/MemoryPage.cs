using System.Collections.Generic;

namespace Module.HexFile.Models
{
    public class MemoryPage
    {
        #region fields

        private List<Cell> cells;

        #endregion

        #region consts

        public const int Size = 256;

        #endregion

        #region ctors

        public MemoryPage()
        {
            cells = new List<Cell>(Size);
            for (int i = 0; i < Size; i++)
                cells.Add(new Cell());
        }

        #endregion

        #region properties

        public List<Cell> Cells
        {
            get { return cells; }
        }

        #endregion
    }
}

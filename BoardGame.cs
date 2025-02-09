using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxSudoku
{
    internal abstract class BoardGame
    {
        protected int size;
        public int Size => size;

        protected BoardGame(int size)
        {
            this.size = size;
        }

        public abstract void FillBoard(string boardData);
        public virtual bool IsFull() => false;
        public virtual int GetCell(int row, int col) => 0;
        public virtual void SetCell(int row, int col, int value) { }
    }
}

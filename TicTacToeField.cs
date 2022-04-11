using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe5
{
    public class TicTacToeField
    {

        private readonly int size;
        private readonly int rowLength;
        public int Size { get => size; }
        public int RowLength { get => rowLength; }

        public TicTacToeField(int size = 5, int rowLength = 5)
        {
            if (rowLength > size) throw new ArgumentOutOfRangeException(nameof(rowLength));
            this.rowLength = rowLength;
            this.size = size;
            Cells = new Cell[size, size];
            for(int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    Cells[i, j] = new Cell();
                }
            }
        }

        public TicTacToe5.Cell[,] Cells { get; }
        public CellState this[int x, int y]
        {
            get => Cells[x, y].State;
            set => SetCellState(x, y, value);
        }
        public void SetCellState(int x, int y, CellState newState)
        {
            Cells[x, y].State = newState;
        }

        public CellState GetCellState(int x, int y)
        {
            return Cells[x, y].State;
        }

        public bool IsFull
        {
            get
            {
                for(int i = 0; i < Size; ++i)
                {
                    for (int j = 0; j < Size; ++j)
                    {
                        if(Cells[i, j].State == CellState.Empty)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }

        public CellState WinnerSide
        {
            get
            {
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j < size; ++j) {
                        bool cellWinning = false;
                        for(int k = 0; k < 4; ++k)
                        {
                            cellWinning = cellWinning || CheckPattern(i, j, k);
                        }
                        if(cellWinning)
                        {
                            return this[i, j];
                        }
                    }
                }
                return CellState.Empty;
            }
        }
        private bool CheckPattern(int x, int y, int pattern, CellState previous = CellState.Empty, int depth = 1)
        {
            if (this[x, y] == CellState.Empty) return false;
            if (depth > 1 && previous != this[x, y]) return false;
            if (depth < rowLength)
            {
                switch(pattern)
                {
                    case 0: //right
                        if (size - x == 1) return false;
                        return CheckPattern(x + 1, y, pattern, this[x, y], depth + 1);
                    case 1: //right down
                        if (size - x == 1 || size - y == 1) return false;
                        return CheckPattern(x + 1, y + 1, pattern, this[x, y], depth + 1);
                    case 2: //down
                        if (size - y == 1) return false;
                        return CheckPattern(x, y + 1, pattern, this[x, y], depth + 1);
                    case 3: //left down
                        if (x == 0|| size - y == 1) return false;
                        return CheckPattern(x - 1, y + 1, pattern, this[x, y], depth + 1);
                    default:
                        return false;
                }
            }
            return true;
        }
    }
}



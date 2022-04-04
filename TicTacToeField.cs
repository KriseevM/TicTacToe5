using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe5
{
    public class TicTacToeField
    {

        private readonly int size;
        public int Size { get => size; }

        public TicTacToeField(int size = 5)
        {
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
            throw new System.NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe5
{
    public class GameFinishedEventArgs : EventArgs
    {
        public string WinnerName { get; set; }
        public CellState WinnerSide { get; set; } = CellState.Empty;

        public GameFinishedEventArgs(string winnerName, CellState winnerSide = CellState.Empty)
        {
            WinnerName = winnerName;
            WinnerSide = winnerSide;
        }
    }
}
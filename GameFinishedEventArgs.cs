using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe5
{
    public class GameFinishedEventArgs : EventArgs
    {
        public string WinnerName { get; set; }

        public GameFinishedEventArgs(string winnerName)
        {
            WinnerName = winnerName;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe5
{
    public interface IGameProcessor
    {
        public event EventHandler GameTick;
        public event System.EventHandler<GameFinishedEventArgs> GameFinished;

        public TicTacToeField GameField { get; }
        public double Time { get; }

        public void SetCellState(int x, int y, CellState newState);
        public CellState GetCellState(int x, int y);
        public void SetReady();
    }
}
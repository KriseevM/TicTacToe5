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

        public bool IsCrossesTurn { get; }

        public TicTacToeField GameField { get; }
        public double Time { get; }

        public void SetCell(int x, int y);
        public void UnsetCell(int x, int y);

        public CellState GetCellState(int x, int y);
        public void SetReady();

        public void Interrupt();
    }
}
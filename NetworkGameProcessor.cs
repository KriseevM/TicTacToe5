using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe5
{
    public class NetworkGameProcessor : IGameProcessor
    {
        private TicTacToeField field = new TicTacToeField();
        public TicTacToeField GameField { get { return field; } set { field = value; } }
        public double Time { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsCrossesTurn => throw new NotImplementedException();

        public event EventHandler GameTick;
        public event EventHandler<GameFinishedEventArgs> GameFinished;

        public CellState GetCellState(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void Interrupt()
        {
            throw new NotImplementedException();
        }

        public void SetCell(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void SetReady()
        {
            throw new NotImplementedException();
        }

        public void UnsetCell(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
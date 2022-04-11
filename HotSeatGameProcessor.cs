using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace TicTacToe5
{
    public class HotSeatGameProcessor : IGameProcessor
    {
        TicTacToeField field;
        Timer timer;

        public HotSeatGameProcessor(int fieldSize, int rowLength = 5)
        {
            
            if (fieldSize < 3) throw new ArgumentOutOfRangeException(nameof(fieldSize));

            field = new TicTacToeField(fieldSize, rowLength);
            timer = new Timer()
            {
                Interval = 100
            };
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Time += timer.Interval / 1000;
            GameTick.Invoke(this, EventArgs.Empty);
        }

        public TicTacToeField GameField => field;

        public double Time { get; set; } = 0.0;

        bool isCrossesTurn = true;
        public bool IsCrossesTurn => isCrossesTurn;

        public event EventHandler GameTick;
        public event EventHandler<GameFinishedEventArgs> GameFinished;

        public CellState GetCellState(int x, int y)
        {
            return field[x, y];
        }

        public void SetCell(int x, int y)
        {
            if (GameField[x, y] != CellState.Empty) return;
            GameField[x, y] = IsCrossesTurn ? CellState.Cross : CellState.Nought;
            GameTick.Invoke(this, EventArgs.Empty);
            var winner = field.WinnerSide;
            if (winner == CellState.Empty)
            {
                if(GameField.IsFull)
                {
                    GameFinished.Invoke(this, new GameFinishedEventArgs("", CellState.Empty));
                    timer.Stop();
                }
                isCrossesTurn = !isCrossesTurn;
            }
            else
            {
                timer.Stop();
                GameFinished.Invoke(this, new GameFinishedEventArgs(winner == CellState.Cross ? "крестики" : "нолики", winner));
            }

        }

        public void SetReady()
        {
            timer.Start();
        }

        public void UnsetCell(int x, int y)
        {
            field[x, y] = CellState.Empty;
            GameTick.Invoke(this, EventArgs.Empty);
        }

        public void Interrupt()
        {
            timer.Stop();
        }

        ~HotSeatGameProcessor()
        {
            timer.Stop();
            timer.Dispose();
        }
    }
}
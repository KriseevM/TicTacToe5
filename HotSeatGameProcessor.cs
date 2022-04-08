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

        public HotSeatGameProcessor(int fieldSize)
        {
            if (fieldSize < 2) throw new ArgumentOutOfRangeException(nameof(fieldSize));
            field = new TicTacToeField(fieldSize);
            timer = new Timer()
            {
                Interval = 100
            };
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Time += 0.1;
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
            var winner = CheckWinning();
            if (winner == CellState.Empty)
            {
                if(GameField.IsFull)
                {
                    GameFinished.Invoke(this, new GameFinishedEventArgs("", CellState.Empty));
                }
                isCrossesTurn = !isCrossesTurn;
            }
            else
            {
                GameFinished.Invoke(this, new GameFinishedEventArgs(winner == CellState.Cross ? "крестики" : "нолики", winner));
                timer.Stop();
            }

        }

        public CellState CheckWinning()
        {
            
            bool win1 = true, win2 = true;
            for(int i = 1; i < field.Size; ++i)
            {
                if(field[i - 1, i - 1] == CellState.Empty || field[i, i] != field[i - 1, i - 1])
                {
                    win1 = false;
                }
                if(field[field.Size - i, i - 1] == CellState.Empty || field[field.Size - 1 - i, i] != field[field.Size - i, i - 1])
                {
                    win2 = false;
                }
                if (win1 == false && win2 == false) break;
            }
            if(win1 || win2) return field[2, 2];
            CellState winner = CellState.Empty;
            for (int i = 0; i < field.Size; ++i)
            {
                bool rowWins = true;
                bool colWins = true;
                for (int j = 1; j < field.Size; ++j)
                {
                    colWins = colWins && field[i, j - 1] != CellState.Empty && field[i, j] == field[i, j - 1];
                }
                if (colWins)
                {
                    winner = field[i, 0];
                    break;
                }
                for (int j = 1; j < field.Size; ++j)
                {
                    rowWins = rowWins && field[j - 1, i] != CellState.Empty && field[j, i] == field[j - 1, i];
                }
                if (rowWins)
                {
                    winner = field[0, i];
                    break;
                }
            }
            return winner;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;

namespace TicTacToe5
{
    public class LocalGameProcessor : IGameProcessor
    {
        protected TicTacToeField field;
        protected Timer timer;

        protected LocalGameProcessor(int fieldSize, int rowLength = 5, string player1name = "игрок 1", string player2name = "игрок 2")
        {
            
            if (fieldSize < 3) throw new ArgumentOutOfRangeException(nameof(fieldSize));
            field = new TicTacToeField(fieldSize, rowLength);
            timer = new Timer()
            {
                Interval = 100
            };
            timer.Elapsed += Timer_Elapsed;
            this.player1Name = player1name;
            this.player2Name = player2name;
            currentPlayer = player1Name;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Time += timer.Interval / 1000;
            Application.Current.Dispatcher.Invoke(() => GameTick.Invoke(this, EventArgs.Empty));
        }

        public TicTacToeField GameField => field;

        public double Time { get; set; } = 0.0;

        protected bool isCrossesTurn = true;
        public bool IsCrossesTurn => isCrossesTurn;

        protected string currentPlayer = "";
        public string CurrentPlayer => currentPlayer;
        protected string player1Name, player2Name;
        public virtual event EventHandler GameTick;
        public virtual event EventHandler<GameFinishedEventArgs> GameFinished;

        public CellState GetCellState(int x, int y)
        {
            return field[x, y];
        }

        public virtual void SetCell(int x, int y)
        {
            if (GameField[x, y] != CellState.Empty) return;
            GameField[x, y] = IsCrossesTurn ? CellState.Cross : CellState.Nought;
            currentPlayer = isCrossesTurn ? player2Name : player1Name;
            GameTick.Invoke(this, EventArgs.Empty);
            CheckIfGameFinished();
        }

        protected virtual void CheckIfGameFinished()
        {
            var winner = field.WinnerSide;
            if (winner == CellState.Empty)
            {
                if (GameField.IsFull)
                {
                    GameFinished.Invoke(this, new GameFinishedEventArgs("", CellState.Empty));
                    timer.Stop();
                }
                isCrossesTurn = !isCrossesTurn;
            }
            else
            {
                timer.Stop();
                GameFinished.Invoke(this, new GameFinishedEventArgs(winner == CellState.Cross ? player1Name : player2Name, winner));
            }
        }

        public virtual void SetReady()
        {
            timer.Start();
        }

        public virtual void UnsetCell(int x, int y)
        {
            field[x, y] = CellState.Empty;
            GameTick.Invoke(this, EventArgs.Empty);
        }

        public virtual void Interrupt()
        {
            timer.Stop();
        }

        ~LocalGameProcessor()
        {
            timer.Stop();
            timer.Dispose();
        }
    }
}
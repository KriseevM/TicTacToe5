using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

namespace TicTacToe5
{
    class PlainAnimationGameProcessor : IGameProcessor
    {
        private TicTacToeField field;
        public TicTacToeField GameField { get { return field; } private set { field = value; } }
        public double Time { get; set; } = 0.0;

        public event EventHandler GameTick;
        public event EventHandler<GameFinishedEventArgs> GameFinished;
        private bool isRising = true;
        private int counter = 0;

        private (int, int)[] points = new (int, int)[25]
        {
            (0, 0),(1, 1),(2, 2),(3, 3),(4, 4),
            (4, 3),(3, 2),(2, 1),(1, 0),(0, 1),
            (1, 2),(2, 3),(3, 4),(4, 2),(3, 1),
            (2, 0),(0, 2),(1, 3),(2, 4),(4, 1),
            (3, 0),(0, 3),(1, 4),(4, 0),(0, 4)
        };
        private Timer timer;
        public PlainAnimationGameProcessor(double interval)
        {
            field = new TicTacToeField(5);
            timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += Timer_Elapsed;
        }
        ~PlainAnimationGameProcessor()
        {
            timer.Stop();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var p = GetCellCoords(counter);

            field[p.Item1, p.Item2] = isRising ? CellState.Cross : CellState.Nought;
            counter++;
            if (counter == points.Length)
            {
                counter = 0;
                isRising = !isRising;
            }

            GameTick.Invoke(this, EventArgs.Empty);
        }

        private (int, int) GetCellCoords(int counter)
        {
            return points[counter];
        }

        public CellState GetCellState(int x, int y)
        {
            return GameField[x, y];
        }

        public void SetCellState(int x, int y, CellState newState)
        {
            //GameField[x, y] = newState;
        }

        public void SetReady()
        {
            timer.Start();
        }
    }
}

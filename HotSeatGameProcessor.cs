using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe5
{
    public class HotSeatGameProcessor : LocalGameProcessor
    {
        public HotSeatGameProcessor(int fieldSize, int rowLength = 5, string player1name = "игрок 1", string player2name = "игрок 2") : base(fieldSize, rowLength, player1name, player2name)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe5
{
    // UPD 2.08.2022:
    // Этот класс идентичен его базовому классу. Единственное, для чего он нужен -
    //  чтобы получить красивую диаграмму классов. Теоретически, сюда можно было бы запихнуть изменённое поведение,
    //  но на текущем этапе это просто класс-пустышка, который полностью повторяет поведение родительского
    public class HotSeatGameProcessor : LocalGameProcessor
    {
        public HotSeatGameProcessor(int fieldSize, int rowLength = 5, string player1name = "игрок 1", string player2name = "игрок 2") : base(fieldSize, rowLength, player1name, player2name)
        {
        }
    }
}

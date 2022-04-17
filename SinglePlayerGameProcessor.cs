using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe5
{
    public class SinglePlayerGameProcessor : LocalGameProcessor
    {

        public class ByValue : IComparer<(int, int)>
        {
            public ByValue()
            {
            }
            public int Compare((int, int) x, (int, int) y)
            {
                if (x.Item1 == y.Item1 && x.Item2 == y.Item2) return 0;
                if (x.Item2 >= y.Item2) return -1;
                return 1;
            }
        }
        public SinglePlayerGameProcessor(int fieldSize) : base(fieldSize, 5, "игрок", "компьютер") {
            occupiedCells = new SortedSet<(int, int)>();
        }

        public override void SetCell(int x, int y)
        {
            base.SetCell(x, y);
            if (!isCrossesTurn && !field.IsFull && field.WinnerSide == CellState.Empty)
            {
                var step = BotStep();
                occupiedCells.Add(step);
                base.SetCell(step.Item1, step.Item2);
            }

        }
        
        private SortedSet<(int, int)> occupiedCells;
        private (int, int) BotStep()
        {
            // (int, int) - (index, value)
            // index - index of pattern with max depth
            // value - depth of that pattern
            SortedSet<(int, int)>[,] patternsMap = new SortedSet<(int, int)>[field.Size, field.Size];
            for (int i = 0; i < field.Size; ++i)
            {
                for (int j = 0; j < field.Size; j++)
                {
                    patternsMap[i, j] = new SortedSet<(int, int)>(new ByValue());
                    for(int k = 0; k < 8; ++k)
                    {
                        patternsMap[i, j].Add((k, GetPatternDepth(i, j, k)));
                    }
                }
            }
            int maxCellPatternDepth = -1;
            (int, int) approxCoord = (-1, -1);
            for (int i = 0; i < field.Size; ++i)
            {
                for (int j = 0; j < field.Size; ++j)
                {
                    if (field[i, j] != CellState.Cross) continue;
                    if (occupiedCells.Contains((i, j))) continue;
                    (int, int) precalc = (-1, -1);
                    int k = 0;
                    do
                    {
                        precalc = CalculateApproxCoords((i, j), patternsMap[i, j].Skip(k).First().Item1, patternsMap[i, j].Skip(k).First().Item2);
                        k++;
                    }
                    while (occupiedCells.Contains(precalc) && k < 8);
                    if (occupiedCells.Contains(precalc))
                    {
                        continue;
                    }
                    else
                    {
                        if (Math.Abs(precalc.Item1 - i) >= maxCellPatternDepth || Math.Abs(precalc.Item2 - j) >= maxCellPatternDepth)
                        {
                            approxCoord = precalc;
                            maxCellPatternDepth = Math.Max(Math.Abs(precalc.Item1 - i), Math.Abs(precalc.Item2 - j));
                        }
                    }
                }
            }
            return GetNearestFreeCell(approxCoord.Item1, approxCoord.Item2);
        }

        private (int, int) CalculateApproxCoords((int, int) maxCellCoords, int maxCellPattern, int maxCellPatternDepth)
        {
            (int, int) approxCoord = (-1, -1);
            switch (maxCellPattern)
            {
                case 0:
                    approxCoord = (Math.Min(field.Size - 1, maxCellCoords.Item1 + maxCellPatternDepth), maxCellCoords.Item2);
                    break;
                case 1:
                    approxCoord = (Math.Min(field.Size - 1, maxCellCoords.Item1 + maxCellPatternDepth), Math.Min(field.Size - 1, maxCellCoords.Item2 + maxCellPatternDepth));
                    break;
                case 2:
                    approxCoord = (maxCellCoords.Item1, Math.Min(field.Size - 1, maxCellCoords.Item2 + maxCellPatternDepth));
                    break;
                case 3:
                    approxCoord = (Math.Max(0, maxCellCoords.Item1 - maxCellPatternDepth), Math.Min(field.Size - 1, maxCellCoords.Item2 + maxCellPatternDepth));
                    break;
                case 4:
                    approxCoord = (Math.Max(0, maxCellCoords.Item1 - maxCellPatternDepth), maxCellCoords.Item2);
                    break;
                case 5:
                    approxCoord = (Math.Max(0, maxCellCoords.Item1 - maxCellPatternDepth), Math.Max(0, maxCellCoords.Item2 - maxCellPatternDepth));
                    break;
                case 6:
                    approxCoord = (maxCellCoords.Item1, Math.Max(0, maxCellCoords.Item2 - maxCellPatternDepth));
                    break;
                case 7:
                    approxCoord = (Math.Min(field.Size - 1, maxCellCoords.Item1 + maxCellPatternDepth), Math.Max(0, maxCellCoords.Item2 - maxCellPatternDepth));
                    break;
            }

            return approxCoord;
        }
        int GetPatternDepth(int x, int y, int pattern = 0)
        {
            if(x >= 0 && x < field.Size && y >= 0 && y < field.Size && field[x, y] == CellState.Cross)
            {
                switch(pattern)
                {
                    case 0:
                        return GetPatternDepth(x + 1, y, pattern) + 1;
                    case 1:
                        return GetPatternDepth(x + 1, y+1, pattern) + 1;
                    case 2:
                        return GetPatternDepth(x, y+1, pattern) + 1;
                    case 3:
                        return GetPatternDepth(x-1, y+1, pattern) + 1;
                    case 4:
                        return GetPatternDepth(x - 1, y, pattern) + 1;
                    case 5:
                        return GetPatternDepth(x - 1, y - 1, pattern) + 1;
                    case 6:
                        return GetPatternDepth(x, y - 1, pattern) + 1;
                    case 7:
                        return GetPatternDepth(x + 1, y - 1, pattern) + 1;
                    default:
                        return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public (int, int) GetNearestFreeCell(int x, int y, SortedSet<(int, int)> takenPoints = null)
        {
            if (takenPoints == null) takenPoints = new SortedSet<(int, int)>();
            takenPoints.Add((x, y));
            if (field[x, y] == CellState.Empty) return (x, y);
            for (int i = Math.Max(x - 1, 0); i < Math.Min(x + 2, field.Size); ++i)
            {
                for (int j = Math.Max(y - 1, 0); j < Math.Min(y + 2, field.Size); ++j)
                {
                    if (takenPoints.Contains((i, j))) continue;
                    if (field[i, j] == CellState.Empty) return (i, j);
                }
            }
            for (int i = Math.Max(x - 1, 0); i < Math.Min(x + 2, field.Size); ++i)
            {
                for (int j = Math.Max(y - 1, 0); j < Math.Min(y + 2, field.Size); ++j)
                {
                    if (takenPoints.Contains((i, j))) continue;
                    var result = GetNearestFreeCell(i, j, takenPoints);
                    if (result != (-1, -1)) return result;
                }
            }
            return (-1, -1);
        }
    }
}

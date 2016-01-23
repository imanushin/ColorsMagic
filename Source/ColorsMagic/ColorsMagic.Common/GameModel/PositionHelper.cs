using System;

namespace ColorsMagic.Common.GameModel
{
    public static class PositionHelper
    {
        public static int GetCellsCount(int triangleSize)
        {
            return ((triangleSize + 1) * triangleSize) / 2;
        }

        public static int GetMaxTriangleSize(int colorsCount)
        {
            var result = 0;

            while (GetCellsCount(result) <= colorsCount)
            {
                result++;
            }

            return result - 1;
        }

        public static int GetPosition(int colorsCount, GamePosition position)
        {
            var size = GetMaxTriangleSize(colorsCount);

            switch (position)
            {
                case GamePosition.Top:
                    return 0;
                case GamePosition.CenterLeft:
                    return GetCellsCount(size / 2) - 1;
                case GamePosition.CenterRight:
                    return GetCellsCount(size / 2 + 1);
                case GamePosition.BottomLeft:
                    return GetCellsCount(size - 1);
                case GamePosition.BottomRight:
                    return colorsCount - 1;
                case GamePosition.BottomCenter:
                    return GetCellsCount(size - 1) + size / 2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
        }

        public static TrianglePosition GetTrianglePosition(int initialIndex, int triangleSize)
        {
            var index = initialIndex;
            var row = 0;

            while (index - (triangleSize - row) >= 0)
            {
                index = index - (triangleSize - row);
                row++;
            }

            var column = index;

            return new TrianglePosition(row, column, triangleSize);
        }
    }
}

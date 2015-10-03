using System;

namespace ColorsMagic.Common.GameModel
{
    public static class PositionHelper
    {
        public static int GetCellsCount(int triangleSize)
        {
            return 2 * triangleSize * (triangleSize - 1);
        }

        public static int GetTriangleSize(int colorsCount)
        {
            return (int)Math.Sqrt(colorsCount / 2) + 1;
        }

        public static int GetPosition(int colorsCount, GamePosition position)
        {
            var size = GetTriangleSize(colorsCount);

            switch (position)
            {
                case GamePosition.Top:
                    return 0;
                case GamePosition.CenterLeft:
                    return GetCellsCount(size / 2) + 1;
                case GamePosition.CenterRight:
                    return GetCellsCount(size / 2 + 1) - 1;
                case GamePosition.BottomLeft:
                    return GetCellsCount(size - 1) + 1;
                case GamePosition.BottomRight:
                    return colorsCount - 1;
                case GamePosition.BottomCenter:
                    return GetCellsCount(size - 1) + size / 2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
        }

        public static TrianglePosition GetTrianglePosition(int colorsCount, int index)
        {
            var size = GetTriangleSize(colorsCount);

            var row = GetTriangleSize(index) - 1;
            var column = index - GetCellsCount(row);

            return new TrianglePosition(row, column);
        }
    }
}

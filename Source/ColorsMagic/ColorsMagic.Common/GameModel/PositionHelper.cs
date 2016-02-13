using System;
using System.Collections.Immutable;

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

        public static TrianglePosition GetPosition(int colorsCount, GamePosition position)
        {
            var size = GetMaxTriangleSize(colorsCount);

            switch (position)
            {
                case GamePosition.Top:
                    return new TrianglePosition(size - 1, 0, size);
                case GamePosition.CenterLeft:
                    return new TrianglePosition(size / 2, 0, size);
                case GamePosition.CenterRight:
                    return new TrianglePosition(size / 2 - 1, size / 2, size);
                case GamePosition.BottomLeft:
                    return new TrianglePosition(0, 0, size);
                case GamePosition.BottomRight:
                    return new TrianglePosition(0, size - 1, size);
                case GamePosition.BottomCenter:
                    return new TrianglePosition(0, size / 2, size);
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, null);
            }
        }

        public static ImmutableList<TrianglePosition> GetNearest(TrianglePosition current, int triangleSize)
        {
            var hasLeft = current.Column > 0;
            var hasRight = current.Row + current.Column < triangleSize;
            var hasBottom = current.Row > 0;

            var builder = ImmutableList<TrianglePosition>.Empty.ToBuilder();

            if (hasLeft)
            {
                builder.Add(new TrianglePosition(current.Row, current.Column - 1, triangleSize));
                builder.Add(new TrianglePosition(current.Row - 1, current.Column - 1, triangleSize));
            }

            if (hasRight)
            {
                builder.Add(new TrianglePosition(current.Row, current.Column + 1, triangleSize));
                builder.Add(new TrianglePosition(current.Row + 1, current.Column, triangleSize));
            }

            if (hasBottom)
            {
                builder.Add(new TrianglePosition(current.Row - 1, current.Column + 1, triangleSize));
                builder.Add(new TrianglePosition(current.Row - 1, current.Column, triangleSize));
            }

            return builder.ToImmutable();
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

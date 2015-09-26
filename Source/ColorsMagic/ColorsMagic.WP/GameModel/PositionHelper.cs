using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorsMagic.WP.Settings;

namespace ColorsMagic.WP.GameModel
{
    internal static class PositionHelper
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
    }
}

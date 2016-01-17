using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ColorsMagic.Common.GameModel
{
    public sealed class GridGenerator
    {
        private static readonly double r = 1;
        private static readonly double t = 2 * r / Math.Sqrt(3);
        private static readonly double h = 4 * r / Math.Sqrt(3);

        private readonly int _cellsCount;
        private int _edgeCellsCount;

        public GridGenerator(int cellsCount)
        {
            _cellsCount = cellsCount;
            _edgeCellsCount = PositionHelper.GetMaxTriangleSize(cellsCount);
        }

        public ImmutableArray<PortablePoint> ExternalGrid
        {
            get
            {
                var result = new List<PortablePoint>();

                var y0 = 0;
                var y1 = t / 2;
                result.Add(new PortablePoint(0, y1));

                for (var i = 0; i < _edgeCellsCount; i++)
                {
                    result.Add(new PortablePoint(i * r * 2 + r, y0));
                    result.Add(new PortablePoint(i * r * 2 + 2 * r, y1));
                }

                for (var i = 0; i < _edgeCellsCount; i++)
                {
                    result.Add(new PortablePoint((_edgeCellsCount * 2 - i) * r, t * 3 / 2 + i * 2 * t));
                    result.Add(new PortablePoint((_edgeCellsCount * 2 - i) * r, t * 3 / 2 + i * 2 * t));
                }

                for (var i = 0; i < _edgeCellsCount; i++)
                {
                    result.Add(new PortablePoint((_edgeCellsCount - i) * r, (_edgeCellsCount - i) * (3 * t / 2)));
                    result.Add(new PortablePoint((_edgeCellsCount - i) * r, (_edgeCellsCount - i) * (3 * t / 2) - t));
                }

                return result.ToImmutableArray();
            }
        }

        public double Width => _edgeCellsCount * 2 * r;

        public double Height => (_edgeCellsCount * 3 * t + 1) / 2;
    }
}
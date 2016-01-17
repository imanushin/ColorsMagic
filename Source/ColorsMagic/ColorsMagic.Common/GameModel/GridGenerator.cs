using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ColorsMagic.Common.GameModel
{
    public sealed class GridGenerator
    {
        private readonly double r;
        private readonly double t;

        private readonly int _cellsCount;
        private readonly int _edgeCellsCount;

        public GridGenerator(int cellsCount, PortableSize maxSize)
        {
            _cellsCount = cellsCount;
            _edgeCellsCount = PositionHelper.GetMaxTriangleSize(cellsCount);

            var rawSize = GetSize(1, _edgeCellsCount);

            var multiplier = Math.Min(maxSize.Height / rawSize.Height, maxSize.Width / rawSize.Width);

            r = multiplier;
            t = GetT(r);

            Size = GetSize(r, _edgeCellsCount);
        }

        private static double GetT(double r)
        {
            return 2 * r / Math.Sqrt(3);
        }

        private static PortableSize GetSize(double r, int edgeCount)
        {
            var t = GetT(r);

            var width = edgeCount * 2 * r;
            var height = edgeCount * 3 * t / 2 + t / 2;

            return new PortableSize(width, height);
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
                    result.Add(new PortablePoint(Size.Width - i * r, (i + 1) * 3 * t / 2));
                    result.Add(new PortablePoint(Size.Width - i * r - r, (i + 1) * 3 * t / 2 + t / 2));
                }

                for (var i = 0; i < _edgeCellsCount; i++)
                {
                    result.Add(new PortablePoint(Size.Width / 2 - (i + 1) * r, (_edgeCellsCount - i) * (3 * t / 2)));
                    result.Add(new PortablePoint(Size.Width / 2 - (i + 1) * r, (_edgeCellsCount - i) * (3 * t / 2) - t));
                }

                return result.Select(p => new PortablePoint(p.X, Size.Height - p.Y)).ToImmutableArray();
            }
        }

        public PortableSize Size { get; private set; }
    }
}
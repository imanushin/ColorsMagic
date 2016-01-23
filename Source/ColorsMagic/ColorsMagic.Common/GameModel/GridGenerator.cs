using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CheckContracts;

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
            EllipseSize = new PortableSize(r, r);
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

                var y0 = Size.Height;
                var y1 = Size.Height - t / 2;
                result.Add(new PortablePoint(0, y1));

                for (var i = 0; i < _edgeCellsCount; i++)
                {
                    result.Add(new PortablePoint(i * r * 2 + r, y0));
                    result.Add(new PortablePoint(i * r * 2 + 2 * r, y1));
                }

                result.AddRange(GetLeftToBottomPath(_edgeCellsCount));

                result.AddRange(GetRightToBottomPath(_edgeCellsCount));

                return result.ToImmutableArray();
            }
        }

        private ImmutableArray<PortablePoint> Reverse(IEnumerable<PortablePoint> result)
        {
            return result.Select(p => new PortablePoint(p.X, Size.Height - p.Y)).ToImmutableArray();
        }

        private ImmutableArray<PortablePoint> GetRightToBottomPath(int itemsCount)
        {
            var width = Size.Width;

            var result = new List<PortablePoint>();

            for (var i = 0; i < itemsCount; i++)
            {
                result.Add(new PortablePoint(width / 2 - (i + 1) * r, (itemsCount - i) * (3 * t / 2)));
                result.Add(new PortablePoint(width / 2 - (i + 1) * r, (itemsCount - i) * (3 * t / 2) - t));
            }

            return Reverse(result);
        }

        private ImmutableArray<PortablePoint> GetLeftToBottomPath(int itemsCount)
        {
            var width = GetSize(r, itemsCount).Width;

            var result = new List<PortablePoint>();

            for (var i = 0; i < itemsCount; i++)
            {
                result.Add(new PortablePoint(width - i * r, (i + 1) * 3 * t / 2));
                result.Add(new PortablePoint(width - i * r - r, (i + 1) * 3 * t / 2 + t / 2));
            }

            return Reverse(result);
        }

        public ImmutableArray<ImmutableArray<PortablePoint>> InternalPathes
        {
            get
            {
                var result = new List<ImmutableArray<PortablePoint>>();

                for (var i = 1; i < _edgeCellsCount; i++)
                {
                    var bottomToLeftPath = new List<PortablePoint>();
                    var bottomToRightPath = new List<PortablePoint>();

                    for (var row = 0; row < _edgeCellsCount - i + 1; row++)
                    {
                        var y1 = t / 2 + row * 3 * t / 2;
                        var y2 = y1 + t;

                        var lx = i * r * 2 + r * row;
                        var rx = Size.Width - lx;

                        bottomToLeftPath.Add(new PortablePoint(lx, y1));
                        bottomToLeftPath.Add(new PortablePoint(lx, y2));

                        bottomToRightPath.Add(new PortablePoint(rx, y1));
                        bottomToRightPath.Add(new PortablePoint(rx, y2));
                    }

                    result.Add(Reverse(bottomToLeftPath));
                    result.Add(Reverse(bottomToRightPath));
                }

                return result.ToImmutableArray();
            }
        }

        public PortablePoint GetCenterOfCell(TrianglePosition position)
        {
            var x = (position.Row + 2 * position.Column + 1) * r;
            var y = (1 + 2 * position.Row) * t;

            Validate.GreaterThan(x, 0);
            Validate.GreaterThan(y, 0);
            Validate.LessOrEqualThan(y, Size.Height);
            Validate.LessOrEqualThan(x, Size.Width);

            return new PortablePoint(x, Size.Height - y);
        }

        public PortableSize Size { get; }

        public PortableSize EllipseSize { get; }
    }
}
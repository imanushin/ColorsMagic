using JetBrains.Annotations;
using System.Collections.Immutable;
using System.Linq;
using CheckContracts;

namespace ColorsMagic.Common.GameModel
{
    public sealed class CellsGraph
    {
        private readonly GameModel _initialModel;
        private ImmutableList<CellData> _datas;
        private int _cellsCount;
        private readonly int _triangleSize;

        public CellsGraph([NotNull] GameModel initialModel)
        {
            Validate.ArgumentIsNotNull(initialModel, nameof(initialModel));

            _initialModel = initialModel;

            _cellsCount = initialModel.Data.Colors.Length;
            _triangleSize = PositionHelper.GetMaxTriangleSize(_cellsCount);

            _datas = Enumerable.Range(0, _cellsCount).Select(GetCellData).ToImmutableList();
        }

        [NotNull]
        private CellData GetCellData(int index)
        {
            var position = PositionHelper.GetTrianglePosition(index, _triangleSize);
            var nearest = PositionHelper.GetNearest(position, _triangleSize);
            var nearestIndexes = nearest.Select(p => p.Index).ToImmutableList();

            return new CellData(index, position, nearestIndexes);
        }

        private sealed class CellData
        {
            public CellData(int index, TrianglePosition position, [NotNull] ImmutableList<int> adjacentCells)
            {
                Validate.ArgumentIsNotNull(adjacentCells, nameof(adjacentCells));

                Index = index;
                Position = position;
                AdjacentCells = adjacentCells;
            }

            public int Index { get; }

            public TrianglePosition Position { get; }

            [NotNull]
            public ImmutableList<int> AdjacentCells { get; }
        }
    }
}
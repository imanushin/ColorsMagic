using CheckContracts;

namespace ColorsMagic.Common.GameModel
{
    public struct TrianglePosition
    {
        private readonly int _triangleSize;

        public readonly int Row;
        public readonly int Column;

        public TrianglePosition(int row, int column, int triangleSize)
        {
            Validate.ArgumentGreaterOrEqualThan(row, 0, nameof(row));
            Validate.ArgumentGreaterOrEqualThan(column, 0, nameof(column));
            Validate.ArgumentLessOrEqualThan(row, triangleSize, nameof(row));
            Validate.ArgumentLessOrEqualThan(column, triangleSize, nameof(column));

            Row = row;
            Column = column;
            _triangleSize = triangleSize;
        }

        public int Index => (1 + _triangleSize) * Row - (Row + 1) * Row / 2 + Column;

        public override string ToString()
        {
            return $"Row: {Row}, Column: {Column}";
        }
    }
}
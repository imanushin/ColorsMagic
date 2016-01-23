using System.Collections.Immutable;
using System.Linq;
using ColorsMagic.Common.GameModel;
using NUnit.Framework;
using Shouldly;

namespace ColorsMagic.Tests
{
    [TestFixture]
    internal sealed class GridGeneratorTest
    {

        [Test]
        public void FirstItemFromExternalGridShouldEqualWithLast([Values(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)]int bottomRowSize)
        {
            // Given
            var cellsCount = PositionHelper.GetCellsCount(bottomRowSize);

            var generator = new GridGenerator(cellsCount, new PortableSize(1, 1));

            // When
            var externalGrid = generator.ExternalGrid;

            // Then
            externalGrid.First().ShouldBe(externalGrid.Last());
        }

        [Test]
        public void CheckBallCenterX([Values(1, 3, 5)]int triangleSize)
        {
            // Given
            var cellsCount = PositionHelper.GetCellsCount(triangleSize);

            var generator = new GridGenerator(cellsCount, new PortableSize(1, 10));
            var column = triangleSize / 2;
            var trianglePosition = new TrianglePosition(0, column, triangleSize);

            // When
            var centerOfMiddle = generator.GetCenterOfCell(trianglePosition);

            // Then
            centerOfMiddle.X.ShouldBe(0.5);
        }

        [Test]
        public void ShouldGetCenterOfAllCells([Values(1, 3, 5, 8, 9, 10)]int triangleSize)
        {
            // Given
            var cellsCount = PositionHelper.GetCellsCount(triangleSize);

            var generator = new GridGenerator(cellsCount, new PortableSize(1, 1));

            var positions = Enumerable.Range(0, cellsCount).Select(i => PositionHelper.GetTrianglePosition(i, triangleSize)).ToImmutableArray();

            // When
            var centres = positions.Select(generator.GetCenterOfCell).ToImmutableArray();

            // Then
            centres.Length.ShouldBe(positions.Length);
        }
    }
}
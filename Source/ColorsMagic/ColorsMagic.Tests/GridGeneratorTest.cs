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
    }
}
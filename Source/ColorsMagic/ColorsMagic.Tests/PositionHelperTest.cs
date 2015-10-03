using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorsMagic.Common.GameModel;
using NUnit.Framework;
using Shouldly;

namespace ColorsMagic.Tests
{
    [TestFixture]
    internal sealed class PositionHelperTest
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 3)]
        [TestCase(3, 6)]
        [TestCase(4, 10)]
        public void CellsCountShouldBeCorrect(int triangleSize, int expectedCount)
        {
            PositionHelper.GetCellsCount(triangleSize).ShouldBe(expectedCount);
        }

        [Test]
        public void TriangleSizeShouldReturnTheSameValueWithCellsCount([Values(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10)] int initialTriangleSize)
        {
            var cellsCount = PositionHelper.GetCellsCount(initialTriangleSize);

            PositionHelper.GetMaxTriangleSize(cellsCount).ShouldBe(initialTriangleSize);
        }

        [TestCase(0, 0)]
        [TestCase(2, 3)]
        [TestCase(2, 4)]
        [TestCase(2, 5)]
        [TestCase(4, 11)]
        [TestCase(4, 14)]
        public void CheckTriangleSizeForNearValues(int expectedTriangleSize, int cellsCount)
        {
            PositionHelper.GetMaxTriangleSize(cellsCount).ShouldBe(expectedTriangleSize);
        }
    }
}

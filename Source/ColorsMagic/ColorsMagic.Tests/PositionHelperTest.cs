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
    }
}

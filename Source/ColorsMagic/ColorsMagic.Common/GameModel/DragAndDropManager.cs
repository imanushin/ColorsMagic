using JetBrains.Annotations;
using System.Collections.Immutable;

namespace ColorsMagic.Common.GameModel
{
    public sealed class DragAndDropManager
    {
        private readonly GridGenerator _gridGenerator;
        private readonly GameColorViewModel _colorsModel;

        public DragAndDropManager([NotNull] GridGenerator gridGenerator, [NotNull] GameColorViewModel colorsModel)
        {
            _gridGenerator = gridGenerator;
            _colorsModel = colorsModel;
        }

        public ImmutableArray<PortablePoint> MovePath { get; }

        public void StartDrag(TrianglePosition initialBall)
        {

        }

        public void ContinueDrag([NotNull] PortablePoint currentPoint)
        {

        }

        public void FinishDrag()
        {

        }
    }
}
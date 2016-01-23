using System.Collections.Immutable;

namespace ColorsMagic.Common.GameModel
{
    public sealed class DragAndDropManager
    {
        private readonly GridGenerator _gridGenerator;

        public DragAndDropManager(GridGenerator gridGenerator)
        {
            _gridGenerator = gridGenerator;
        }

        public ImmutableArray<PortablePoint> MovePath { get; }

        public void StartDrag(TrianglePosition initialBall)
        {
            
        }

        public void ContinueDrag(PortablePoint currentPoint)
        {
            
        }

        public void FinishDrag()
        {
            
        }
    }
}
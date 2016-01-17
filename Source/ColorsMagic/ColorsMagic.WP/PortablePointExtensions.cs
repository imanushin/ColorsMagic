using Windows.Foundation;
using ColorsMagic.Common.GameModel;
using JetBrains.Annotations;

namespace ColorsMagic.WP
{
    public static class PortablePointExtensions
    {
        public static Point ToPoint([NotNull] this PortablePoint portablePoint)
        {
            return new Point(portablePoint.X, portablePoint.Y);
        }
    }
}
using Windows.Foundation;
using ColorsMagic.Common.GameModel;
using JetBrains.Annotations;

namespace ColorsMagic.WP
{
    public static class PortableSizeExtensions
    {
        public static Size ToSize([NotNull] this PortableSize portableSize)
        {
            return new Size(portableSize.Width, portableSize.Height);
        }

        [NotNull]
        public static PortableSize ToSize(this Size size)
        {
            return new PortableSize(size.Width, size.Height);
        }
    }
}
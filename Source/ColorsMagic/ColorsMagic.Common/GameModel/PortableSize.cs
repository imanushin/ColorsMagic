namespace ColorsMagic.Common.GameModel
{
    public sealed class PortableSize
    {
        public PortableSize(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Width { get; private set; }

        public double Height { get; private set; }
    }
}
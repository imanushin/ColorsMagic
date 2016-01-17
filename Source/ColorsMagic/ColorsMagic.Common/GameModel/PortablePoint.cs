namespace ColorsMagic.Common.GameModel
{
    public sealed class PortablePoint
    {

        public PortablePoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Y { get; private set; }

        public double X { get; private set; }
    }
}
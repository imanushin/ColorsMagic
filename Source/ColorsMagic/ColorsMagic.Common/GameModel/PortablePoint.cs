using System;

namespace ColorsMagic.Common.GameModel
{
    public sealed class PortablePoint
    {
        private const double Epsilon = 1e-15;

        public PortablePoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Y { get; }

        public double X { get; }

        private bool Equals(PortablePoint other)
        {
            return Math.Abs(Y - other.Y) <= Epsilon && Math.Abs(X - other.X) <= Epsilon;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is PortablePoint && Equals((PortablePoint)obj);
        }

        public override int GetHashCode()
        {
            return 1;
        }

        public override string ToString()
        {
            return $"Y: {Y}, X: {X}";
        }
    }
}
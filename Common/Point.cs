namespace Common
{
    using System;

    public class Point
    {
        public int Y { get; private set; }
        public int X { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            var otherPoint = obj as Point;

            if (otherPoint == null)
            {
                return false;
            }

            return otherPoint.X == X && otherPoint.Y == Y;
        }

        public double DistanceTo(Point point)
        {
            var dX = point.X - X;
            var dY = point.Y - Y;
            return Math.Sqrt(dY*dY + dX*dX);
        }
    }
}
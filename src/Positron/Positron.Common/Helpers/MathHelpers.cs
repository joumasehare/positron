namespace Positron.Common.Helpers
{
    public static class MathHelpers
    {
        public const int LatitudeMax = 180;
        public const int LongitudeMax = 360;

        //https://stackoverflow.com/a/51839058/3736063
        public static double CalculateDistance(Coordinate positionOne, Coordinate positionTwo)
        {
            var d1 = positionOne.Latitude * (Math.PI / 180.0);
            var num1 = positionOne.Longitude * (Math.PI / 180.0);
            var d2 = positionTwo.Latitude * (Math.PI / 180.0);
            var num2 = positionTwo.Longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        public static double CalculateDistanceBetweenPoints(Point pointOne, Point pointTwo)
        {
            int distX = pointTwo.X - pointOne.X;
            int distY = pointTwo.Y - pointOne.Y;

            return Math.Sqrt(distX * distX + distY * distY);
        }
    }
}

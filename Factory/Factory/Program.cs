using System;

namespace Factory
{
    class Program
    {
        public enum CoorinateSystem
        {
            Cartesian,
            Polar
        }
        public class Point
        {
            private double x, y;
            /// <summary>
            /// Initializes a point from EITHER cartesian or polar
            /// </summary>
            /// <param name="a"></param> x if cartesian, rho if polar
            /// <param name="b"></param>
            /// <param name="system"></param>
            /// 
            //factory method
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
            private Point(double x, double y)
            {
                this.x = x;
                this.y = y;
                /*switch (system)
                {
                    case CoorinateSystem.Cartesian:
                        x = a;
                        y = b;
                        break;
                    case CoorinateSystem.Polar:
                        x = a * Math.Cos(b);
                        y = a * Math.Sin(b);
                        break;
                    default:
                        break;
                }*/
            }

            public override string ToString()
            {
                return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
            }
        }
        static void Main(string[] args)
        {
            var point = Point.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
        }
    }
}

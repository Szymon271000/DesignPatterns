using System;

namespace Lesson3Open_ClosesPrinciple
{
    class Program
    {
        public class Rectangle
        {
            public Rectangle(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public Rectangle()
            {

            }
            public virtual int Width { get; set; }
            public virtual int Height { get; set; }

            public override string ToString()
            {
                return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
            }
        }
        public class Square : Rectangle
        {
            public override int Width
            {
                set
                {
                    base.Width = base.Height = value;
                }
            }

            public override int Height
            {
                set
                {
                    base.Width = base.Height = value;
                }
            }
        }
        static void Main(string[] args)
        {
            static int Area(Rectangle r) 
            {
                int area= r.Width* r.Height;
                return area;
            }

            Rectangle rc = new Rectangle(2,3);
            Console.WriteLine($"{rc} has area {Area(rc)}");

            Square sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");
        }
    }
}

namespace SAPR_ART
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public class Rectangle
    {
        public string Color { get; set; }
        public Point BotLeft { get; set; }
        public Point TopRight { get; set; }

        public Rectangle(string color, Point botLeft, Point topRight)
        {
            Color = color;
            BotLeft = botLeft;
            TopRight = topRight;
        }
    }
}

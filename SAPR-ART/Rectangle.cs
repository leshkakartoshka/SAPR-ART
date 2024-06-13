using Autodesk.AutoCAD.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskSAPR_ART
{
    internal class Rectangle
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

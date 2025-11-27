using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPETRIS
{
    public class Food : GameEntity
    {
        public Food(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override void Draw(Graphics canvas, int width, int height)
        {
            canvas.FillEllipse(Brushes.DarkRed, new Rectangle(X * width, Y * height, width, height));
        }
    }
}

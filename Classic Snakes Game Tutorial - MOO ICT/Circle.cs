using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPETRIS
{
    public class Circle : GameEntity
    {
        public Circle()
        {
            X = 0;
            Y = 0;
        }

        // Implement the abstract Draw method
        public override void Draw(Graphics canvas, int width, int height)
        {
            Brush brush = Brushes.Gray;
            canvas.FillEllipse(brush, new Rectangle(X * width, Y * height, width, height));
        }
    }
}

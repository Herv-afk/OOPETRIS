using System.Drawing;

namespace OOPETRIS
{
    public abstract class GameEntity
    {
        // Encapsulated properties
        public int X { get; set; }
        public int Y { get; set; }

        // Abstract method for drawing the entity
        public abstract void Draw(Graphics canvas, int width, int height);
    }
}

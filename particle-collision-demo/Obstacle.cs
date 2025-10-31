using System;
using System.Numerics;

namespace MohawkGame2D;
public class Obstacle
{
    public Vector2 position;
    public Vector2 size;
    Color color;
    public Obstacle(Vector2 position, Vector2 size)
    {
        this.position = position;
        this.size = size;
        this.color = Color.DarkGray;
    }
    public void Update()
    {
        // Draw obstacle
        Draw.LineSize = 0;
        Draw.LineColor = Color.Black;
        Draw.FillColor = color;
        Draw.Rectangle(position, size);
    }
}
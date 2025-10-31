using System;
using System.ComponentModel;
using System.Numerics;

namespace MohawkGame2D;
public class Particle
{
    Vector2 position;
    Vector2 velocity;
    int size;
    Color color;

    public void Setup()
    {
        // Give random position inside of screen from (0,0) to Window Size
        position = Random.Vector2(Window.Size);

        // Create vector to move with
        Vector2 direction = Random.Direction();
        float speed = Random.Float(25, 75); // 25 - 75 pixels per second
        velocity = direction * speed;

        // Hard coded for now
        size = 3; // 3x3 pixels
        color = Color.Magenta;
    }
    public void Update(Obstacle obstacle)
    {
        // Move particle
        position += velocity * Time.DeltaTime;

        // Constrain to bounds of screen
        ConstrainToScreenBounds();

        // Collide with obstacles
        CollideWithObstacle(obstacle);

        // Draw particle
        Draw.LineSize = 0;
        Draw.LineColor = Color.Black;
        Draw.FillColor = color;
        Draw.Square(position, size);
    }
    void ConstrainToScreenBounds()
    {
        float particleL = position.X;
        float particleR = position.X + size;
        float particleT = position.Y;
        float particleB = position.Y + size;

        // Left edge check; 0 is window left edge
        if (particleL <= 0)
        {
            // Move particle to left edge of screen
            position.X = 0;

            // Invert velocity to move back into screen
            velocity.X = -velocity.X;
        }

        // Right edge check
        if (particleR >= Window.Width)
        {
            // Move particle to right edge of screen
            position.X = Window.Width - size;

            // Invert velocity to move back into screen
            velocity.X = -velocity.X;
        }

        // Top edge check; 0 is window top edge
        if (particleT <= 0)
        {
            // Move particle to top edge of screen
            position.Y = 0;

            // Invert velocity to move back into screen
            velocity.Y = -velocity.Y;
        }

        // Bottom edge check
        if (particleB >= Window.Height)
        {
            // Move particle to bottom edge of screen
            position.Y = Window.Height - size;

            // Invert velocity to move back into screen
            velocity.Y = -velocity.Y;
        }
    }
    void CollideWithObstacle(Obstacle obstacle)
    {
        // Particle edges
        float particleL = position.X;
        float particleR = position.X + size;
        float particleT = position.Y;
        float particleB = position.Y + size;

        // Obstacle edges
        float obstacleL = obstacle.position.X;
        float obstacleR = obstacle.position.X + obstacle.size.X;
        float obstacleT = obstacle.position.Y;
        float obstacleB = obstacle.position.Y + obstacle.size.Y;

        // Check if particle is within all bounds of obstacle
        bool isParticleWithinR = particleL <= obstacleR;
        bool isParticleWithinL = particleR >= obstacleL;
        bool isParticleWithinT = particleB >= obstacleT;
        bool isParticleWithinB = particleT <= obstacleB;

        // If colliding at all
        if (isParticleWithinL && isParticleWithinR && isParticleWithinT && isParticleWithinB)
        {
            // If within top and bottom

            if (isParticleWithinT && isParticleWithinB)
            {
                velocity.X = -velocity.X;
            }
            // If within left and right
            if (isParticleWithinL && isParticleWithinR)
            {
                velocity.Y = -velocity.Y;
            }
        }
    }
}
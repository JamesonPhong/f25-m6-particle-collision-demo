using System;
using System.ComponentModel;
using System.Numerics;

namespace MohawkGame2D;
public class Particle
{
    Vector2 position;
    Vector2 previousPosition;
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
    public void Update(Obstacle[] obstacles)
    {
        // Record where we are
        previousPosition = position;

        // Move particle
        position += velocity * Time.DeltaTime;

        // Constrain to bounds of screen
        ConstrainToScreenBounds();

        // Collide with obstacles
        for (int i = 0; i < obstacles.Length; i++)
            CollideWithObstacle(obstacles[i]);

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
            // Compare to centre of obstacle, snap to closest edge
            float obstacleCentreX = obstacle.position.X + obstacle.size.X / 2;
            float obstacleCentreY = obstacle.position.Y + obstacle.size.Y / 2;

            // If previous position is left of left edge and current position is below centre
            if (previousPosition.X + size < obstacleB && particleR < obstacleCentreX)
                position.X = obstacleL - size - 1;

            // If previous position is right of right edge and current position is below centre 
            if (previousPosition.X > obstacleT && particleL > obstacleCentreX)
                position.X = obstacleR + 1;

            // If previous position is below bottom and current position is below centre
            if (previousPosition.Y > obstacleB && particleT > obstacleCentreY)
                position.Y = obstacleB + 1;

            // If previous position is above top and current position is below centre 
            if (previousPosition.Y + size < obstacleT && particleB < obstacleCentreY)
                position.Y = obstacleT - size - 1;

            // Bottom and Top
            if (particleB <= obstacleB && particleT >= obstacleT)
            {
                velocity.X = -velocity.X;
            }
            // Left and Right
            if (particleL >= obstacleL && particleR <= obstacleR)
            {
                velocity.Y = -velocity.Y;
            }
        }
    }
}
using System;
using System.Numerics;

namespace MohawkGame2D
{
    public class Game
    {
        Particle[] particles = new Particle[100];
        Obstacle[] obstacles = [
            new Obstacle(new Vector2(150, 150), new Vector2(100)),
            new Obstacle(new Vector2(50, 50), new Vector2(25)),
            new Obstacle(new Vector2(325, 325), new Vector2(25)),
            ];
        public void Setup()
        {
            Window.SetTitle("Particle Collision");
            Window.SetSize(400, 400);
            
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i] = new Particle();
                particles[i].Setup();
            }
        }
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].Update(obstacles);
            }

            for (int i = 0; i < obstacles.Length; i++)
            {
                obstacles[i].Update();
            }
        }
    }
}

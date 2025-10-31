using System;
using System.Numerics;

namespace MohawkGame2D
{
    public class Game
    {
        Particle[] particles = new Particle[100];
        Obstacle obstacle = new Obstacle(new Vector2(150, 150), new Vector2(100));
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

            obstacle.Update();

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].Update(obstacle);
            }
        }
    }
}

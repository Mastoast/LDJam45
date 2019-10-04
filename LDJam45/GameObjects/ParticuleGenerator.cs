using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LDJam45
{
    class ParticleGenerator
    {
        private static ParticleGenerator instance;

        private static GraphicsDeviceManager _graphicsDevice;
        private static Texture2D particleTexture;
        private static Vector2 origin;

        private static List<Particle> particles;

        private ParticleGenerator(GraphicsDeviceManager graphicsDeviceManager)
        {
            // init
            _graphicsDevice = graphicsDeviceManager;

            // Create texture
            particleTexture = new Texture2D(_graphicsDevice.GraphicsDevice, 1, 1);
            particleTexture.SetData(new[] { Color.White });
            origin = new Vector2(particleTexture.Height / 2f, particleTexture.Width / 2f);

            // Initialize list
            particles = new List<Particle>();
        }

        public static ParticleGenerator GetInstance(GraphicsDeviceManager graphicsDeviceManager)
        {
            if (instance == null)
            {
                instance = new ParticleGenerator(graphicsDeviceManager);
            }
            return instance;
        }

        public void UnloadContent()
        {
            particleTexture.Dispose();
        }

        // define the particle struct
        public class Particle
        {
            public int x, y, size;
            public float speed, lifetime;
            public Vector2 direction;

            public Particle(int x, int y, int size, float speed, Vector2 direction)
            {
                this.x = x;
                this.y = y;
                this.size = size;
                this.speed = speed;
                this.direction = direction;
                this.lifetime = 0;
            }
        }

        public void SpawnParticles(int x, int y, int size, int number, float speed)
        {
            Random rd = new Random();
            for (int i = 0; i < number; i++)
            {
                double angle = rd.NextDouble() * 2f * MathHelper.Pi;
                Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                direction.Normalize();
                Particle newParticle = new Particle(x, y, size, speed, direction);
                particles.Add(newParticle);
            }
        }

        public bool IsActive()
        {
            return particles.Count != 0;
        }

        public void Update(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].lifetime > 0.6f)
                {
                    particles.RemoveAt(i);
                    i--;
                }
                else
                {
                    particles[i].x += (int)(particles[i].speed * particles[i].direction.X * delta);
                    particles[i].y += (int)(particles[i].speed * particles[i].direction.Y * delta);
                    particles[i].lifetime += (float)delta;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in particles)
            {
                // draw texture
                spriteBatch.Draw(particleTexture, new Rectangle(item.x, item.y, item.size, item.size), null,
                    Color.Black, 0f, origin, SpriteEffects.None, 0f);
            }
        }
    }
}



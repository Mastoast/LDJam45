using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace LDJam45
{
    class GameState : GameObject
    {
        protected List<GameObject> actors;
        protected SpriteFont font;
        //protected Ball ball;
        protected ParticleGenerator pg;

        public GameState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Initialize()
        {
            actors = new List<GameObject>();

            /* ball
            ball = new Ball(_graphicsDevice, 100f);
            ball.Initialize();*/

            // Particle generator
            pg = ParticleGenerator.GetInstance(_graphicsDevice);
        }

        public override void LoadContent(ContentManager content)
        {
            // ball
            //ball.LoadContent(content);

            // Font
            font = content.Load<SpriteFont>("Fonts/Joystix");

        }

        public override void UnloadContent()
        {
            pg.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;

            // pg
            pg.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Font
            string text = "test";
            Vector2 middlePoint = font.MeasureString(text)/2;
            Vector2 position = new Vector2(_graphicsDevice.PreferredBackBufferWidth / 2, _graphicsDevice.PreferredBackBufferHeight / 2);
            spriteBatch.DrawString(font, text, position, Color.White, 0, middlePoint, 1.0f, SpriteEffects.None, 1f);

            // Pg
            pg.Draw(spriteBatch);
        }
    }
}

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

        Letter letter;

        public GameState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Initialize()
        {
            actors = new List<GameObject>();

            // Particle generator
            pg = ParticleGenerator.GetInstance(_graphicsDevice);
        }

        public override void LoadContent(ContentManager content)
        {
            // Font
            font = content.Load<SpriteFont>("Fonts/Joystix_32");
            // CHECK COLOR CHANGE
            //font.Texture.GetData<>();

            // TEST letter
            //letter = new Letter(_graphicsDevice, 'A', font);
            //letter.LoadContent(content);

        }

        public override void UnloadContent()
        {
            pg.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;

            // TEST letter
            //letter.Update(gameTime);

            // pg
            pg.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // TEST letter
            //letter.Draw(spriteBatch);

            // Pg
            pg.Draw(spriteBatch);
        }
    }
}

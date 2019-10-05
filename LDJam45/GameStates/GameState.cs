using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace LDJam45
{
    class GameState : GameObject
    {
        protected List<GameObject> numbers;
        protected Word currentWord;

        protected SpriteFont font;
        protected ParticleGenerator pg;

        Number number;

        // Levels
        string level1 = "Levels/level1.txt";

        public GameState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Initialize()
        {
            // Level Parser
            //LevelParser.ReadFile(level1);
            
            // Numbers list
            numbers = new List<GameObject>();

            // Particle generator
            pg = ParticleGenerator.GetInstance(_graphicsDevice);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            // Font
            font = contentManager.Load<SpriteFont>("Fonts/Joystix_32");

            //Test Number
            number = new Number(_graphicsDevice, 152, 8,
                new Vector2(_graphicsDevice.PreferredBackBufferWidth, 100), 100, font);
            numbers.Add(number);

            //Test Word
            currentWord = new Word(_graphicsDevice, "NOTHING", font);
            currentWord.LoadContent(contentManager);

        }

        public override void UnloadContent()
        {
            currentWord.UnloadContent();
            pg.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;

            // Current Word
            currentWord.Update(gameTime);

            // Numbers
            foreach (var item in numbers)
            {
                item.Update(gameTime);
            }

            // pg
            pg.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Current word
            currentWord.Draw(spriteBatch);

            // Numbers
            foreach (var item in numbers)
            {
                item.Draw(spriteBatch);
            }

            // Pg
            pg.Draw(spriteBatch);
        }
    }
}

// Debug
/*
var debugText = timeBeforeShot;
spriteBatch.DrawString(font, debugText.ToString(), new Vector2(500,500),
Color.Black);
var kstate = Keyboard.GetState();
if (kstate.IsKeyDown(Keys.Up))
{

}
*/

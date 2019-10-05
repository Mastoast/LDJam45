using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace LDJam45
{
    public class GameState : State
    {
        public static List<Bullet> bullets;

        public int health = 100;

        protected Word currentWord;
        protected Level currentLevel;
        protected List<Number> numbers;

        protected SpriteFont font;
        protected ParticleGenerator pg;

        public GameState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Initialize()
        {
            // Numbers list
            numbers = new List<Number>();
            // Bullets list
            bullets = new List<Bullet>();

            // Particle generator
            pg = ParticleGenerator.GetInstance(_graphicsDevice);

            // Get levels
            LevelStorage.GenerateLevels();
            currentLevel = LevelStorage.GetNextLevel();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            // Font
            font = contentManager.Load<SpriteFont>("Fonts/Joystix_32");

            //Test Word
            currentWord = new Word(_graphicsDevice, "IMPOSSIBLE", font);
            currentWord.LoadContent(contentManager);

            //Test Number
            SpawnNumber(152, 8, 100, 1);
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

            // Wait for init
            if (!currentWord.initialized)
                return;

            // Bullets
            // list of bullets
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                // Check out of screen
                if (bullets[i].position.X >= _graphicsDevice.PreferredBackBufferWidth)
                {
                    bullets.RemoveAt(i);
                }
                else
                {
                    bool alive = true;
                    // collision with numbers
                    for (int j = numbers.Count - 1; j >= 0; j--)
                    {
                        if (bullets[i].GetRectangle().Intersects(numbers[j].GetRectangle()))
                        {
                            int decim = numbers[j].Hit();
                            if (decim != 0)
                            {
                                // TODO split decimal
                            }
                            numbers.RemoveAt(j);
                            bullets.RemoveAt(i);
                            alive = false;
                            break;
                        }
                    }

                    // Update number
                    if (alive)
                        bullets[i].Update(gameTime);
                }
            }

            // Numbers
            // list of numbers
            for (int i = numbers.Count - 1; i >= 0; i--)
            {
                    // Check collisions
                    if (numbers[i].position.X <= (currentWord.leftPosition + Letter.squareSize))
                    {
                        Hurt(numbers[i].damage);
                        numbers.RemoveAt(i);
                    }
                    else
                    {
                        // Update number
                        numbers[i].Update(gameTime);
                    }
            }

            // pg
            pg.Update(gameTime);
        }

        public void Hurt(int amount)
        {
            health -= amount;
            // Game Over
            if (amount <= 0)
            {
                // TODO Game over state
            }
        }

        public void SpawnNumber(int number, int decim, int speed, int line)
        {
            float lineHeight = currentWord.GetLineHeight(line);
            Vector2 spawnPosition = new Vector2(_graphicsDevice.PreferredBackBufferWidth, lineHeight);
            numbers.Add(new Number(_graphicsDevice, number, decim, spawnPosition, speed, font));
        }

        public void SpawnNumber(Event @event)
        {
            if (@event.line != 0)
            {
                // spawn event
                float lineHeight = currentWord.GetLineHeight(@event.line);
                Vector2 spawnPosition = new Vector2(_graphicsDevice.PreferredBackBufferWidth, lineHeight);
                numbers.Add(new Number(_graphicsDevice, @event.number, @event.decim, spawnPosition, @event.speed, font));

            }

            if (@event.text != "")
            {
                // print event
                Print(@event.text);
            }
        }

        public void Print(string text)
        {
            // TODO Make prints
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Current word
            currentWord.Draw(spriteBatch);

            // Bullets
            foreach (var item in bullets)
            {
                item.Draw(spriteBatch);
            }

            // Numbers
            foreach (var item in numbers)
            {
                item.Draw(spriteBatch);
            }

            // Health
            // TODO Draw health

            // Pg
            pg.Draw(spriteBatch);
        }
    }
}

// Debug
/*
//DEBUG
var debugText = bullets.Count;
spriteBatch.DrawString(font, debugText.ToString(), new Vector2(500, 500),
Color.Black);
//DEBUG

var kstate = Keyboard.GetState();
if (kstate.IsKeyDown(Keys.Up))
{

}
*/

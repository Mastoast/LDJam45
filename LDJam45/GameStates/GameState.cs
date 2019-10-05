using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace LDJam45
{
    public class GameState : GameObject
    {
        public static List<Bullet> bullets;

        public int health = 100;

        protected Game game;
        protected Word currentWord;
        protected List<Number> numbers;

        protected SpriteFont font;
        protected ParticleGenerator pg;

        // Levels
        string level1 = "Levels/level1.txt";

        public GameState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
        }

        public void SetGame(Game game)
        {
            this.game = game;
        }

        public override void Initialize()
        {
            // Level Parser
            //LevelParser.ReadFile(level1);
            
            // Numbers list
            numbers = new List<Number>();
            // Bullets list
            bullets = new List<Bullet>();

            // Particle generator
            pg = ParticleGenerator.GetInstance(_graphicsDevice);

            // Get levels
            LevelsStorage.GenerateLevels();
            // TODO Load levels
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

            // Bullets
            // list of bullets
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                // Check out of screen
                if (bullets[i].position.X >= _graphicsDevice.PreferredBackBufferWidth)
                {
                    // TODO loose score
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
                            // TODO ADD SCORE
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
                // TODO
            }
        }

        public void SpawnNumber(int number, int decim, int speed, int line)
        {
            float lineHeight = currentWord.GetLineHeight(line);
            Vector2 spawnPosition = new Vector2(_graphicsDevice.PreferredBackBufferWidth, lineHeight);
            numbers.Add(new Number(_graphicsDevice, number, decim, spawnPosition, speed, font));
        }

        public void SpawnNumber(Spawn spawn)
        {
            float lineHeight = currentWord.GetLineHeight(spawn.line);
            Vector2 spawnPosition = new Vector2(_graphicsDevice.PreferredBackBufferWidth, lineHeight);
            numbers.Add(new Number(_graphicsDevice, spawn.number, spawn.decim, spawnPosition, spawn.speed, font));
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
            // TODO

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

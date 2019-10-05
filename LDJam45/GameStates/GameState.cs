﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace LDJam45
{
    class GameState : GameObject
    {
        public static List<Bullet> bullets;

        public int health = 100;

        protected List<Number> numbers;
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
            numbers = new List<Number>();
            // Bullets list
            bullets = new List<Bullet>();

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

            // Bullets
            // list of bullets
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                // check if still exists
                if (bullets[i] == null)
                    bullets.RemoveAt(i);
                else
                {
                    // Check out of screen
                    if (bullets[i].position.X >= _graphicsDevice.PreferredBackBufferWidth)
                    {
                        // TODO loose score
                        bullets.RemoveAt(i);
                    }
                    else
                    {
                        // collision with numbers
                        for (int j = numbers.Count - 1; j >= 0; j--)
                        {
                            if (bullets[i].GetRectangle().Intersects(numbers[j].GetRectangle()))
                            {
                                int decim = numbers[j].Hit();
                                // TODO
                            }
                        }

                        // Update number
                        bullets[i].Update(gameTime);
                    }
                }
            }

            // Numbers
            // list of bullets
            for (int i = numbers.Count - 1; i >= 0; i--)
            {
                // check if still exists
                if (numbers[i] == null)
                    numbers.RemoveAt(i);
                else
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
var debugText = timeBeforeShot;
spriteBatch.DrawString(font, debugText.ToString(), new Vector2(500,500),
Color.Black);
var kstate = Keyboard.GetState();
if (kstate.IsKeyDown(Keys.Up))
{

}
*/

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
        protected Event nextEvent;
        protected TextBalloon balloon;

        protected SpriteFont font;
        protected ParticleGenerator pg;
        private ContentManager contentManager;

        private double freezeTime;
        private bool frozen = false;

        public GameState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Initialize()
        {
            // cooldown
            Letter.cooldown = 0.5;
            // Numbers list
            numbers = new List<Number>();
            // Bullets list
            bullets = new List<Bullet>();

            // Particle generator
            pg = ParticleGenerator.GetInstance(_graphicsDevice);

            // Load levels only if not already started
            if (!LevelStorage.generated)
                LevelStorage.GenerateLevels();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            // Font
            font = contentManager.Load<SpriteFont>("Fonts/Joystix_32");

            // Create text balloon
            balloon = new TextBalloon(_graphicsDevice, font);
            balloon.LoadContent(contentManager);

            // Save content manager for level initialization
            this.contentManager = contentManager;

            //  Start the first Word
            if (LevelStorage.currentLevel == -1)
                currentLevel = LevelStorage.GetNextLevel();
            else
                currentLevel = LevelStorage.GetCurrentLevel();

            StartLevel(null);
        }

        public void StartLevel(GameTime gameTime)
        {
            // Unload the old word
            if (currentWord != null)
                currentWord.UnloadContent();
            // Create the new word
            currentWord = new Word(_graphicsDevice, currentLevel.word, font);
            currentWord.Initialize();
            currentWord.LoadContent(contentManager);
            // Reset level timer
            if (gameTime != null)
                gameTime.TotalGameTime = TimeSpan.Zero;
            // Start first event
            nextEvent = currentLevel.GetNextEvent();
        }

        public override void UnloadContent()
        {
            currentWord.UnloadContent();
            balloon.UnloadContent();
            pg.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            //Balloon
            balloon.Update(gameTime);

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

            // Handle events
            HandleEvent(gameTime);

            // Particle Generator
            pg.Update(gameTime);
        }

        public void HandleEvent(GameTime gameTime)
        {
            // Time frozen with texts
            if (frozen)
            {
                if (balloon.text == "")
                {
                    gameTime.TotalGameTime = TimeSpan.FromSeconds(freezeTime);
                    frozen = false;
                    nextEvent = currentLevel.GetNextEvent();
                }
                return;
            }

            // End of the level
            if (nextEvent.time == 0 && nextEvent.text == "")
            {
                if (numbers.Count != 0)
                    return; // wait for all numbers to disapear
                LevelStorage.GetNextLevel();
                // End of the game
                if (currentLevel.word.Equals(""))
                {
                    SetWin();
                    return;
                }
                StartLevel(gameTime);
                return;
            }
            // Handle next event
            if (nextEvent.time <= gameTime.TotalGameTime.TotalSeconds)
            {
                if (nextEvent.text != "")
                {
                    // Print event
                    balloon.SetText(nextEvent.text);
                    frozen = true;
                    freezeTime = gameTime.TotalGameTime.TotalSeconds;
                }
                else if (nextEvent.line != 0)
                {
                    // spawn event
                    SpawnNumber(nextEvent.number, nextEvent.decim, nextEvent.speed, nextEvent.line);
                    nextEvent = currentLevel.GetNextEvent();
                }
            }
        }

        public void Hurt(int amount)
        {
            health -= amount;
            // Game Over
            if (health <= 0)
            {
                this.game.SetState(new GameOverState(_graphicsDevice));
            }
        }

        public void SetWin()
        {
            game.SetState(new WinState(_graphicsDevice));
        }

        public void SpawnNumber(int number, int decim, int speed, int line)
        {
            float lineHeight = currentWord.GetLineHeight(line);
            Vector2 spawnPosition = new Vector2(_graphicsDevice.PreferredBackBufferWidth, lineHeight);
            numbers.Add(new Number(_graphicsDevice, number, decim, spawnPosition, speed, font));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //DEBUG
            var debugText = health.ToString();
            spriteBatch.DrawString(font, debugText.ToString(), new Vector2(0, 650),
            Color.Black);
            //DEBUG

            // Current word
            currentWord.Draw(spriteBatch);

            //Balloon
            balloon.Draw(spriteBatch);

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

            // Particle Generator
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Audio;

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

        protected SoundEffect hitSfx;
        protected SoundEffectInstance hitSfxInst;

        protected SpriteFont font;
        protected ParticleGenerator pg;
        private ContentManager contentManager;

        private Texture2D rectTexture;
        private Vector2 rectOrigin;

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

            // create rect texture
            rectTexture = new Texture2D(_graphicsDevice.GraphicsDevice, 1, 1);
            rectTexture.SetData(new[] { Color.White });
            rectOrigin = new Vector2(rectTexture.Height / 2f, rectTexture.Width / 2f);

            // Create text balloon
            balloon = new TextBalloon(_graphicsDevice, font);
            balloon.LoadContent(contentManager);

            // Save content manager for in game initialization
            this.contentManager = contentManager;

            // load sfx
            hitSfx = contentManager.Load<SoundEffect>("Sounds/hit");
            hitSfxInst = hitSfx.CreateInstance();

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
            rectTexture.Dispose();
            currentWord.UnloadContent();
            balloon.UnloadContent();
            pg.UnloadContent();
        }

        /*
         * UPDATE METHODS
        */
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
                            if (decim != -1)
                            {
                                // Split decimal
                                SpawnDecimal(decim, numbers[j].speed, numbers[j].position.X);
                                numbers.RemoveAt(j);
                            }
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
                currentLevel = LevelStorage.GetNextLevel();
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
            // sfx
            hitSfxInst.Stop();
            hitSfxInst.Play();
            // Game Over
            if (health <= 0)
            {
                game.SetState(new GameOverState(_graphicsDevice));
            }
        }

        public void SetWin()
        {
            game.SetState(new WinState(_graphicsDevice));
        }

        /*
         * SPAWN METHODS
        */
        public void SpawnDecimal(int decim, int speed, float xPos)
        {
            if (decim <= 0)
                return;
            Random rand = new Random();
            int line = rand.Next(1, currentWord.length);
            SpawnNumber(decim, 0, speed, line, xPos);
        }

        public void SpawnNumber(int number, int decim, int speed, int line)
        {
            SpawnNumber(number, decim, speed, line, _graphicsDevice.PreferredBackBufferWidth);
        }

        public void SpawnNumber(int number, int decim, int speed, int line, float xPos)
        {
            float lineHeight = currentWord.GetLineHeight(line);
            Vector2 spawnPosition = new Vector2(xPos, lineHeight);

            Number newNumber = new Number(_graphicsDevice, number, decim, spawnPosition, speed, font);
            newNumber.LoadContent(contentManager);
            numbers.Add(newNumber);
        }

        /*
         * DRAW METHODS
        */
        public override void Draw(SpriteBatch spriteBatch)
        {
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
            DrawHealthBar(spriteBatch);

            // Particle Generator
            pg.Draw(spriteBatch);
        }

        public void DrawHealthBar(SpriteBatch spriteBatch)
        {
            int x = currentWord.leftPosition;
            int y = _graphicsDevice.PreferredBackBufferHeight - 60;
            int width = (int)(health * 11);
            int height = 50;
            Color color = new Color(0.4f, 0.1f, 0.1f);
            Rectangle position = new Rectangle(x, y, width, height);
            spriteBatch.Draw(rectTexture, position, null, color,
                0f, Vector2.Zero, SpriteEffects.None, 0f);
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

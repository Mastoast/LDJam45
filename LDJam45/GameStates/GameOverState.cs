using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LDJam45
{
    public class GameOverState : MenuState
    {

        public GameOverState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            centralWord = "GAME OVER";
            Letter.cooldown = 0.5f;
        }

        public override void AddLetters(ContentManager contentManager)
        {
            // Place word on screen
            PlaceWord(centralWord, 0);
            PlaceWord("TRY AGAIN", 200);

            // Load Letters
            foreach (var item in letters)
            {
                item.LoadContent(contentManager);
            }
        }

        public override void Update(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;

            // Is SpaceBar pressed
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Space))
            {
                // Only if new press
                if (!lastPressed)
                {
                    // reset game time
                    gameTime.TotalGameTime = TimeSpan.Zero;
                    // Retry level
                    NextState(gameTime);
                }
                lastPressed = true;
            }
            else
            {
                lastPressed = false;
            }

            // Update letters
            bool allPressed = true;
            foreach (var item in letters)
            {
                item.Update(gameTime);
                if (item.timeBeforeShot != 0)
                    allPressed = false;
            }

        }

        public override void NextState(GameTime gameTime)
        {
            this.game.SetState(new GameState(_graphicsDevice));
        }

        public override void AdditionnalDraw(SpriteBatch spriteBatch)
        {
            // Message
            string text = "Press SPACE Key to";
            Vector2 mPosition = new Vector2(_graphicsDevice.PreferredBackBufferWidth / 2,
                _graphicsDevice.PreferredBackBufferHeight / 2 + 100);
            Vector2 middlePoint = font.MeasureString(text) / 2;
            spriteBatch.DrawString(font, text, mPosition, Color.Black,
                0, middlePoint, 1.0f, SpriteEffects.None, 1f);

            // Score
            if (LevelStorage.inBonus)
            {
                // score
                text = "SCORE :" + LevelStorage.score.ToString();
                mPosition = new Vector2(_graphicsDevice.PreferredBackBufferWidth / 2,
                    _graphicsDevice.PreferredBackBufferHeight / 2 - 300);
                middlePoint = font.MeasureString(text) / 2;
                spriteBatch.DrawString(font, text,
                    mPosition, Color.Black, 0, middlePoint, 1.0f, SpriteEffects.None, 1f);
                // max score
                text = "BEST SCORE :" + LevelStorage.maxScore.ToString();
                mPosition = new Vector2(_graphicsDevice.PreferredBackBufferWidth / 2,
                    _graphicsDevice.PreferredBackBufferHeight / 2 - 250);
                middlePoint = font.MeasureString(text) / 2;
                spriteBatch.DrawString(font, text,
                    mPosition, Color.Black, 0, middlePoint, 1.0f, SpriteEffects.None, 1f);
            }
        }
    }
}

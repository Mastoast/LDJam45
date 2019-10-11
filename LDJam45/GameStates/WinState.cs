using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDJam45
{
    public class WinState : MenuState
    {
        public WinState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            centralWord = "THANKS FOR PLAYING";
        }

        public override void NextState(GameTime gameTime)
        {
            GameState bonusState = new BonusGameState(_graphicsDevice);
            bonusState.SwitchtoBonus(gameTime);
            this.game.SetState(bonusState);
        }

        public override void AddLetters(ContentManager contentManager)
        {
            // Place word on screen
            PlaceWord(centralWord, 0);

            // Load Letters
            foreach (var item in letters)
            {
                item.LoadContent(contentManager);
            }
        }

        public override void AdditionnalDraw(SpriteBatch spriteBatch)
        {
            // Message
            string text = "Endless mode unlocked";
            Vector2 mPosition = new Vector2(_graphicsDevice.PreferredBackBufferWidth / 2,
                _graphicsDevice.PreferredBackBufferHeight / 2 + 200);
            Vector2 middlePoint = font.MeasureString(text) / 2;
            spriteBatch.DrawString(font, text, mPosition, Color.Black,
                0, middlePoint, 1.0f, SpriteEffects.None, 1f);
        }
    }
}

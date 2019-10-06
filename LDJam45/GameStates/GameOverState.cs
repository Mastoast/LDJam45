using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDJam45
{
    public class GameOverState : Menustate
    {

        public GameOverState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            centralWord = "GAME OVER";
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

        public override void NextState()
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
        }
    }
}

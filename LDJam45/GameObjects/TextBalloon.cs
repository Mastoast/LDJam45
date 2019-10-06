using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LDJam45
{
    public class TextBalloon : GameObject
    {
        public string text;
        public Rectangle position;

        private Texture2D rectTexture;
        private Vector2 rectOrigin;

        private bool lastPressed = true;

        private SpriteFont font;
        private Color textColor;
        private Color marginColor;
        private Color backColor;

        private Vector2 marginOffset;
        private int marginSize;
        private Vector2 backOffset;
        private Vector2 textOffset;

        public TextBalloon(GraphicsDeviceManager graphicsDevice, SpriteFont font) : base(graphicsDevice)
        {
            this.font = font;
            Initialize();
        }

        public override void Initialize()
        {
            textColor = Color.Black;
            marginColor = Color.Black;
            backColor = new Color(0.9f, 0.9f, 0.9f);

            text = "TEST";
            position = new Rectangle(_graphicsDevice.PreferredBackBufferWidth / 2, 100, 800, 100);

            marginOffset = backOffset = textOffset = Vector2.Zero;
            marginSize = 8;
        }

        public override void LoadContent(ContentManager content)
        {
            // create rect texture
            rectTexture = new Texture2D(_graphicsDevice.GraphicsDevice, 1, 1);
            rectTexture.SetData(new[] { Color.White });
            rectOrigin = new Vector2(rectTexture.Height / 2f, rectTexture.Width / 2f);
        }

        public override void UnloadContent()
        {
            rectTexture.Dispose();
        }

        /*
         * UPDATE FUNCTIONS
        */
        public override void Update(GameTime gameTime)
        {
            // Skip if non active
            if (text == "")
                return;

            // Is SpaceBar pressed
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Space))
            {
                // Only if new press
                if (!lastPressed)
                {
                    text = "";
                }
                lastPressed = true;
            }
            else
            {
                lastPressed = false;
            }

            double delta = gameTime.ElapsedGameTime.TotalSeconds;
        }

        /*
         * PRINT A NEW TEXT
        */
        public void SetText(string text)
        {
            this.text = text;
        }

        /*
         * DRAW FUNCTIONS
        */
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (text == "")
                return;
            Rectangle marginPosition = position;
            marginPosition.Width += marginSize;
            marginPosition.Height += marginSize;
            Rectangle backPosition = position;
            Vector2 textPosition = position.Location.ToVector2();

            DrawMargin(spriteBatch, marginPosition);
            DrawBack(spriteBatch, backPosition);
            DrawText(spriteBatch, textPosition);
        }

        public void DrawMargin(SpriteBatch spriteBatch, Rectangle position)
        {
            // Square Texture
            // Margin
            spriteBatch.Draw(rectTexture, position, null, marginColor,
                0f, rectOrigin, SpriteEffects.None, 0f);
        }

        public void DrawBack(SpriteBatch spriteBatch, Rectangle position)
        {
            // Back
            spriteBatch.Draw(rectTexture, position, null, backColor,
                0f, rectOrigin, SpriteEffects.None, 0f);
        }

        public void DrawText(SpriteBatch spriteBatch, Vector2 position)
        {
            // Text
            Vector2 middlePoint = font.MeasureString(text) / 2;
            spriteBatch.DrawString(font, text, position, textColor,
                0, middlePoint, 0.7f, SpriteEffects.None, 1f);

            // Text Shadow
            float shadowOffset = 1f;
            Color shadowColor = Color.Multiply(textColor, 0.7f);
            Vector2 shadowPos = new Vector2(position.X - shadowOffset, position.Y - shadowOffset);
            spriteBatch.DrawString(font, text, shadowPos, shadowColor,
                0, middlePoint, 0.7f, SpriteEffects.None, 1f);
        }
    }
}

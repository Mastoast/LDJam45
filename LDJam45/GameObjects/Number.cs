using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDJam45
{
    class Number : GameObject
    {
        public int number { get; }
        private int decim { get; }
        private int speed { get; }
        public Vector2 position;

        private SpriteFont font;
        private Color numberColor;
        private Color decimalColor;

        public Number(GraphicsDeviceManager graphicsDevice, int number, int decim, Vector2 position, SpriteFont font) : base(graphicsDevice)
        {
            this.font = font;
            this.number = number;
            this.decim = decim;
            this.position = position;
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            this.position.X -= (float)delta * speed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Font
            // number
            spriteBatch.DrawString(font, number.ToString(), position, numberColor);

            // decimal
            if (decim != 0)
            {
                spriteBatch.DrawString(font, decim.ToString(), position, decimalColor);
            }
        }
    }
}

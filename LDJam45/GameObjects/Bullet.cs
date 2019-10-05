using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDJam45
{
    class Bullet : GameObject
    {
        public static int speed = 1500;
        public string letter;
        public Vector2 position;

        private SpriteFont font;
        private Color bulletColor;

        public Bullet(GraphicsDeviceManager graphicsDevice, Vector2 position, string letter, SpriteFont font) : base(graphicsDevice)
        {
            this.letter = letter;
            this.position = position;
            this.font = font;
            //bulletColor = Color.Lerp(Color.White, Color.Black, 0.3f);
            bulletColor = Color.Multiply(Color.Black, 0.9f);
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
            position.X += (float)delta * speed;
        }

        // Return rectangle hitbox
        public Rectangle GetRectangle()
        {
            Vector2 measure = font.MeasureString(letter.ToString()) / 2; //print is scaled at 0.5
            return new Rectangle((int)position.X, (int)position.Y, (int)measure.X, (int)measure.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Bullet head
            Vector2 middlePoint = font.MeasureString(letter) / 2;
            spriteBatch.DrawString(font, letter, this.position, bulletColor, 0, middlePoint, 0.5f, SpriteEffects.None, 1f);
        }
    }
}

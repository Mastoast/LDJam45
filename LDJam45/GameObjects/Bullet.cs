using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDJam45
{
    public class Bullet : GameObject
    {
        public static int speed = 1300;
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
            float scale = 0.6f;
            spriteBatch.DrawString(font, letter, this.position, bulletColor, 0,
                middlePoint, scale, SpriteEffects.None, 1f);

            // Draw bullet trace
            int nbTail = 10;
            int offset = 15;

            for (int i = 1; i <= nbTail; i++)
            {
                // decrease position
                Vector2 tailPos = new Vector2(this.position.X - (i * offset), this.position.Y);
                // reduce alpha channel
                Color tailColor = Color.Multiply(bulletColor, 1 - (i * 0.05f));
                // decrease size
                float tailScale = scale - (scale/2 * (i * 0.1f));
                if (tailPos.X > 75)
                {
                    spriteBatch.DrawString(font, letter, tailPos, tailColor, 0,
                    middlePoint, tailScale, SpriteEffects.None, 0.5f);
                }
            }
        }
    }
}

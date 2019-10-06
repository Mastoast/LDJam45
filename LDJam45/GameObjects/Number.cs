using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace LDJam45
{
    public class Number : GameObject
    {
        public int number;
        public Vector2 position;
        public int damage = 20;

        public int decim;
        public int speed;

        private SpriteFont font;

        private SoundEffect hitSfx;
        private SoundEffectInstance hitSfxInst;

        private Color numberColor = Color.Green;
        private Color decimalColor = Color.Red;
        private Color dotColor = Color.Black;

        public Number(GraphicsDeviceManager graphicsDevice, int number, int decim, Vector2 position, int speed, SpriteFont font) : base(graphicsDevice)
        {
            this.font = font;
            this.number = number;
            this.decim = decim;
            this.position = position;
            this.speed = speed;
            Initialize();
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            hitSfx = content.Load<SoundEffect>("Sounds/numHit");
            hitSfxInst = hitSfx.CreateInstance();
            hitSfxInst.Volume = 0.5f;
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= (float)delta * speed;
        }

        public int Hit()
        {
            // Play hit sound
            hitSfxInst.Stop();
            hitSfxInst.Play();

            // Loose digit
            string sNum = number.ToString();
            if (sNum.Length > 1)
            {
                float removalSpace = font.MeasureString(sNum[0].ToString()).X;
                number = int.Parse(sNum.Remove(0, 1));
                position.X += removalSpace;
                return -1;
            }
            else
            {
                return decim;
            }

        }

        // Return rectangle hitbox
        public Rectangle GetRectangle()
        {
            Vector2 measure = font.MeasureString(number.ToString());
            return new Rectangle((int)position.X, (int)position.Y, (int)measure.X, (int)measure.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Font
            // number
            spriteBatch.DrawString(font, number.ToString(), position, numberColor, 0f,
                Vector2.Zero, 1f, SpriteEffects.None, 0.8f);

            // decimal
            if (decim != 0)
            {
                //number offset
                float numOffset = font.MeasureString(number.ToString()).X;
                //dot offset
                float dotOffset = font.MeasureString(".").X;
                Vector2 dotPosition = new Vector2(position.X + numOffset, position.Y);
                spriteBatch.DrawString(font, ".", dotPosition, dotColor);
                //decimal number
                Vector2 decimalPosition = new Vector2(position.X + numOffset + dotOffset, position.Y);
                spriteBatch.DrawString(font, decim.ToString(), decimalPosition, decimalColor);
            }
        }
    }
}

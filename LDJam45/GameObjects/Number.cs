using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDJam45
{
    private SpriteFont font;

    class Number : GameObject
    {
        private SpriteFont font;
        private int number { get; set; }
        private int decim { get; set; }

        public Number(GraphicsDeviceManager graphicsDevice, int number, int decim, SpriteFont font) : base(graphicsDevice)
        {
            this.font = font;
            this.number = number;
            this.decim = decim;
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

        public override void Update(GameTime delta)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}

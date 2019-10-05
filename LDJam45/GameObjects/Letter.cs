using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDJam45
{
    class Letter : GameObject
    {
        private char letter { get; }
        private float colldown;
        private SpriteFont font;

        public Letter(GraphicsDeviceManager graphicsDevice, char letter, SpriteFont font) : base(graphicsDevice)
        {
            this.letter = letter;
            this.font = font;
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

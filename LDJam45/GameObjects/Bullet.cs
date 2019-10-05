using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDJam45
{
    class Bullet : GameObject
    {
        private SpriteFont font;

        public Bullet(GraphicsDeviceManager graphicsDevice, SpriteFont font) : base(graphicsDevice)
        {
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

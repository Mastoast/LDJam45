using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDJam45
{
    public class State : GameObject
    {
        protected Game game;

        public State(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
        }

        public void SetGame(Game game)
        {
            this.game = game;
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager contentManager)
        {
        }

        public override void UnloadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
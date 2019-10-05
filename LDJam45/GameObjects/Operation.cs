using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LDJam45
{
    class Operation : GameObject
    {
        Number number1;
        Number number2;
        char oper;
        SpriteFont font;

        public Operation(GraphicsDeviceManager graphicsDevice, int number1, int number2, char oper, int speed, SpriteFont font) : base(graphicsDevice)
        {
            this.oper = oper;
            this.font = font;
            this.number1 = new Number(_graphicsDevice, number1, 0, Vector2.Zero, speed, font);
            this.number2 = new Number(_graphicsDevice, number2, 0, Vector2.Zero, speed, font);
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

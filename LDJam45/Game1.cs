using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LDJam45
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameState currentState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 400,
                PreferredBackBufferWidth = 400
            };
            Content.RootDirectory = "Content";

            // First state
            currentState = new GameState(graphics);
        }

        protected override void Initialize()
        {
            currentState.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            currentState.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            currentState.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // begin
            spriteBatch.Begin();

            // state draws
            currentState.Draw(spriteBatch);

            // end
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

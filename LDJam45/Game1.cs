using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace LDJam45
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private State currentState;
        private Color backgroundColor;

        protected SoundEffect looseSfx;
        protected SoundEffectInstance looseSfxInst;

        public Game1()
        {
            // Game title
            Window.Title = "Words against Numbers";

            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };

            Content.RootDirectory = "Content";

            // First state
            currentState = new MenuState(graphics);
            currentState.SetGame(this);
        }

        protected override void Initialize()
        {
            currentState.Initialize();

            backgroundColor = new Color(0.8f, 0.8f, 0.70f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Sfx
            looseSfx = Content.Load<SoundEffect>("Sounds/loose");
            looseSfxInst = looseSfx.CreateInstance();

            currentState.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            //State
            currentState.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentState.Update(gameTime);

            base.Update(gameTime);
        }

        public void SetState(State newState)
        {
            // Unload old scene
            currentState.UnloadContent();
            // Init new scene
            currentState = newState;
            currentState.SetGame(this);
            currentState.Initialize();
            currentState.LoadContent(Content);

            // Play sound
            looseSfx.Play();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

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

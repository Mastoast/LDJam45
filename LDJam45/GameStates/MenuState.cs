using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace LDJam45
{
    public class Menustate : State
    {
        protected List<Letter> letters;
        protected int spaceMargin = 10;
        protected int upPosition = 75;
        protected string centralWord;

        protected SpriteFont font;
        protected bool lastPressed = true;

        public Menustate(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            centralWord = "START";
        }

        public void PlaceWord(string word, int offsetY)
        {
            int availableSpace = _graphicsDevice.PreferredBackBufferWidth;
            int squareSize = Letter.squareSize + Letter.squareMargin;
            int stringCount = word.Length;

            // Set at center of the screen
            upPosition = (_graphicsDevice.PreferredBackBufferHeight / 2) - (squareSize / 2);

            // Offset for first letter
            int wordSize = (stringCount * squareSize) + (stringCount - 1) * spaceMargin;
            int lettersOffset = (availableSpace - wordSize) / 2 + (squareSize / 2);

            for (int i = 0; i < stringCount; i++)
            {
                if ((word[i] >= 'A' && word[i] <= 'Z') || (word[i] >= 'a' && word[i] <= 'z'))
                {
                    Letter newLetter = new Letter(_graphicsDevice, word[i], font);
                    // place the letters at the center of the height
                    newLetter.position = new Vector2(lettersOffset + (squareSize * i) + (spaceMargin * i),
                        upPosition + offsetY);
                    letters.Add(newLetter);
                }
            }
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager contentManager)
        {
            // Font
            font = contentManager.Load<SpriteFont>("Fonts/Joystix_32");

            // Letters list
            letters = new List<Letter>();

            AddLetters(contentManager);

        }

        public virtual void AddLetters(ContentManager contentManager)
        {
            // Place word on screen
            PlaceWord(centralWord, 0);

            // Load Letters
            foreach (var item in letters)
            {
                item.LoadContent(contentManager);
            }
        }

        public override void UnloadContent()
        {
            // Letters
            foreach (var item in letters)
            {
                item.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;

            // Is SpaceBar pressed
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Space))
            {
                // Only if new press
                if (!lastPressed)
                {
                    NextState();
                }
                lastPressed = true;
            }
            else
            {
                lastPressed = false;
            }

            // Update letters
            foreach (var item in letters)
            {
                item.Update(gameTime);
            }
        }

        public virtual void NextState()
        {
            this.game.SetState(new GameState(_graphicsDevice));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Letters
            foreach (var item in letters)
            {
                // Draw Letters
                item.Draw(spriteBatch);
            }
            AdditionnalDraw(spriteBatch);
        }

        public virtual void AdditionnalDraw(SpriteBatch spriteBatch)
        {
            // Message
            string text = "Press SPACE Key to";
            Vector2 mPosition = new Vector2(_graphicsDevice.PreferredBackBufferWidth /2,
                _graphicsDevice.PreferredBackBufferHeight / 2 - 100);
            Vector2 middlePoint = font.MeasureString(text) / 2;
            spriteBatch.DrawString(font, text, mPosition, Color.Black,
                0, middlePoint, 1.0f, SpriteEffects.None, 1f);
        }
    }
}

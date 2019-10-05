using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace LDJam45
{
    public class Menustate : State
    {
        protected List<Letter> letters;
        protected int spaceMargin = 10;
        protected int upPosition = 75;
        protected string centralWord;

        protected SpriteFont font;

        public Menustate(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            centralWord = "START";
        }

        public void PlaceWord(string word)
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
                Letter newLetter = new Letter(_graphicsDevice, word[i], font);
                // place the letters at the center of the height
                newLetter.position = new Vector2(lettersOffset + (squareSize * i) + (spaceMargin * i),
                    upPosition);
                letters.Add(newLetter);
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

            // Place word on screen
            PlaceWord(centralWord);

            // Letters
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

            foreach (var item in letters)
            {
                item.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Letters
            foreach (var item in letters)
            {
                // Draw Letters
                item.Draw(spriteBatch);
            }

        }
    }
}

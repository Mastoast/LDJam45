using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LDJam45
{
    class Word : GameObject
    {
        private int length;
        private List<Letter> letters;
        private int health = 100;

        private SpriteFont font;
        private int leftPosition = 75;
        private int topBottomMargin = 50;
        private int spaceMargin = 32;

        public Word(GraphicsDeviceManager graphicsDevice, string word, SpriteFont font) : base(graphicsDevice)
        {
            this.length = word.Length;
            this.font = font;

            // Place words on screen
            int availableSpace = _graphicsDevice.PreferredBackBufferHeight - (2 * topBottomMargin);
            int stringCount = word.Length;

            this.letters = new List<Letter>();
            for (int i = 0; i < this.length; i++)
            {
                Letter newLetter = new Letter(_graphicsDevice, word[i], font);
                int squareSize = newLetter.squareSize + newLetter.squareMargin;
                // place the letters at the center of the height
                int lettersOffset = topBottomMargin + (availableSpace - (stringCount * squareSize) - ((stringCount - 1) * spaceMargin))/2;
                newLetter.position = new Vector2(leftPosition, topBottomMargin + lettersOffset + (squareSize / 2) * (i + 1) + (spaceMargin * i));
                letters.Add(newLetter);
            }
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            foreach (var item in letters)
            {
                item.LoadContent(content);
            }
        }

        public override void UnloadContent()
        {
            foreach (var item in letters)
            {
                item.UnloadContent();
            }
        }

        public override void Update(GameTime delta)
        {
            foreach (var item in letters)
            {
                item.Update(delta);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in letters)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}

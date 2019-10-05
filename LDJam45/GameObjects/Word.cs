using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LDJam45
{
    public class Word : GameObject
    {
        public int spaceMargin = 10;
        public int leftPosition = 75;

        private int length;
        private List<Letter> letters;
        private SpriteFont font;
        private int lettersOffset;

        public Word(GraphicsDeviceManager graphicsDevice, string word, SpriteFont font) : base(graphicsDevice)
        {
            this.length = word.Length;
            this.font = font;

            // Place word on screen
            int availableSpace = _graphicsDevice.PreferredBackBufferHeight;
            int squareSize = Letter.squareSize + Letter.squareMargin;
            int stringCount = word.Length;
            // Offset for first letter
            int wordSize = (stringCount * squareSize) + (stringCount - 1) * spaceMargin;
            lettersOffset = (availableSpace - wordSize) / 2 + (squareSize / 2);

            // init the list of letters
            this.letters = new List<Letter>();
            for (int i = 0; i < this.length; i++)
            {
                Letter newLetter = new Letter(_graphicsDevice, word[i], font);
                // place the letters at the center of the height
                newLetter.position = new Vector2(leftPosition, lettersOffset + (squareSize * i) + (spaceMargin * i));
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

        public float GetLineHeight(int line)
        {
            int squareSize = Letter.squareSize + Letter.squareMargin;
            return lettersOffset + (squareSize * (line - 1)) + (spaceMargin * (line - 1)) - (squareSize / 2);
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

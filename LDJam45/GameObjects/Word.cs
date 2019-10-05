using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LDJam45
{
    public class Word : GameObject
    {
        public bool initialized = false;
        public int spaceMargin = 10;
        public int leftPosition = 75;

        private int initUpPosition = 0;
        private float initSpeed = 450;

        private int length;
        private List<Letter> letters;
        private SpriteFont font;
        private int lettersOffset;
        private int lineWidth;

        private Texture2D rectText;

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
                newLetter.position = new Vector2(leftPosition, lettersOffset
                    + (squareSize * i) + (spaceMargin * i));
                letters.Add(newLetter);
            }

            // calculate line width
            lineWidth = _graphicsDevice.PreferredBackBufferWidth - leftPosition;
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            // Rectangle texture
            rectText = new Texture2D(_graphicsDevice.GraphicsDevice, 1, 1);
            rectText.SetData(new[] { Color.White });

            // Letters
            foreach (var item in letters)
            {
                item.LoadContent(content);
            }
        }

        public override void UnloadContent()
        {
            //Rectangle texture
            rectText.Dispose();

            // Letters
            foreach (var item in letters)
            {
                item.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;

            if (!initialized)
            {
                this.initUpPosition += (int)(delta * initSpeed);
                return;
            }
            foreach (var item in letters)
            {
                item.Update(gameTime);
            }
        }

        public float GetLineHeight(int line)
        {
            int squareSize = Letter.squareSize + Letter.squareMargin * 2;
            return lettersOffset + (squareSize * (line % length - 1))
                + (spaceMargin * (line % length - 1)) - (squareSize / 2);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Count letter in place
            int nbInitialized = 0;
            // Letters
            foreach (var item in letters)
            {
                if (!initialized)
                {
                    if (item.position.Y > initUpPosition)
                    {
                        // Draw lines
                        DrawLine(spriteBatch, leftPosition, initUpPosition,
                            lineWidth, 2, Color.Gray, 0.9f);
                        // Draw Letters
                        item.DrawAtPosition(spriteBatch, new Vector2(leftPosition, initUpPosition));
                        return;
                    }
                    // increment
                    nbInitialized += 1;
                }
                // Draw lines
                DrawLine(spriteBatch, leftPosition, (int)item.position.Y,
                    lineWidth, 2, Color.Gray, 0.9f);

                // Draw Letters
                item.Draw(spriteBatch);
            }

            if (!initialized)
            {
                if (nbInitialized == length)
                {
                    initialized = true;
                }
            }
        }

        public void DrawLine(SpriteBatch spriteBatch, int x, int y, int width, int height, Color color, float depth)
        {
            spriteBatch.Draw(rectText, new Rectangle(x, y, width, height),
                null, color, 0f, Vector2.Zero, SpriteEffects.None, depth);
        }
    }
}

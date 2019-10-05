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

        public Word(GraphicsDeviceManager graphicsDevice, string word, SpriteFont font) : base(graphicsDevice)
        {
            this.length = word.Length;
            this.font = font;

            this.letters = new List<Letter>();
            for (int i = 0; i < this.length; i++)
            {
                Letter newLetter = new Letter(_graphicsDevice, word[i], font);
                newLetter.position = new Vector2(10 * i, 10 * i);
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

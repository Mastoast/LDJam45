using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LDJam45
{
    class Word : GameObject
    {
        private int length { get; }
        private List<Letter> letters;
        private int health { get; set; }
        private SpriteFont font;

        public Word(GraphicsDeviceManager graphicsDevice, string word, SpriteFont font) : base(graphicsDevice)
        {
            this.length = word.Length;
            this.font = font;

            this.letters = new List<Letter>();
            for (int i = 0; i < this.length; i++)
            {
                letters.Add(new Letter(_graphicsDevice, word[i], font));
            }
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

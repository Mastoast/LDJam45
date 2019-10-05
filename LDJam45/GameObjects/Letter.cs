using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LDJam45
{
    public class Letter : GameObject
    {
        public string letter { get; }
        public Vector2 position;

        public static int squareSize = 40;
        public static int squareMargin = 8;

        private int index;
        private float rotation;
        private float rotationSpeed;
        private double cooldown = 0.0;
        private double timeBeforeShot = 0.0;
        private bool lastPressed = true;

        private Texture2D squareText;
        private Vector2 squareOrigin;

        private SpriteFont font;
        private GameState state;
        private Color letterColor = Color.Black;
        private Color marginColor = Color.Maroon;
        private Color backColor = Color.AntiqueWhite;

        public Letter(GraphicsDeviceManager graphicsDevice, char letter,SpriteFont font) : base(graphicsDevice)
        {
            this.letter = letter.ToString().ToUpper();

            this.font = font;
            this.rotation = 0f;
            this.position = Vector2.Zero;
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            // create square texture
            squareText = new Texture2D(_graphicsDevice.GraphicsDevice, 1, 1);
            squareText.SetData(new[] { Color.White });
            squareOrigin = new Vector2(squareText.Height / 2f, squareText.Width / 2f);
        }

        public override void UnloadContent()
        {
            squareText.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;

            // Reduce colldown
            if (timeBeforeShot != 0.0)
            {
                timeBeforeShot -= delta;
                if (timeBeforeShot < 0.0)
                    timeBeforeShot = 0.0;
            }

            // Get pressed keys
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
            bool pressed = false;
            foreach (var item in pressedKeys)
            {
                if (item.ToString().ToUpper().Equals(letter))
                {
                    pressed = true;
                }
            }

            // Letter is pressed
            if (pressed)
            {
                // Only if new press
                if (!lastPressed)
                {
                    // Shoot
                    if (timeBeforeShot == 0.0)
                    {
                        this.Shoot();
                        timeBeforeShot = cooldown;
                    }
                }
                lastPressed = true;
            }
            else
            {
                this.lastPressed = false;
            }
        }

        private void Shoot()
        {
            Bullet newBullet = new Bullet(_graphicsDevice, position, letter, font);
            if (GameState.bullets != null)
                GameState.bullets.Add(newBullet);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Square Texture
            // margin
            spriteBatch.Draw(squareText, new Rectangle((int)position.X, (int)position.Y, squareSize + squareMargin, squareSize + squareMargin),
                null, marginColor, rotation, squareOrigin, SpriteEffects.None, 0f);
            // back
            spriteBatch.Draw(squareText, new Rectangle((int)position.X, (int)position.Y, squareSize, squareSize),
                null, backColor, rotation, squareOrigin, SpriteEffects.None, 0f);

            // Font
            Vector2 middlePoint = font.MeasureString(letter) / 2;
            spriteBatch.DrawString(font, letter, position, letterColor,
                0, middlePoint, 1.0f, SpriteEffects.None, 1f);

            // Font Shadow
            float shadowOffset = 3f;
            Color shadowColor = Color.Multiply(letterColor, 0.7f);
            Vector2 shadowPos = new Vector2(position.X - shadowOffset, position.Y - shadowOffset);
            spriteBatch.DrawString(font, letter, shadowPos, shadowColor,
                0, middlePoint, 1.0f, SpriteEffects.None, 1f);
        }
    }
}

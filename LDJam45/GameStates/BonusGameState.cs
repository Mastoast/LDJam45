using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LDJam45
{
    public class BonusGameState : GameState
    {
        private double bonusSpawnDelay = 3.5;
        private double bonusSpawnCounter = 0;

        public BonusGameState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
        }

        public override void Initialize()
        {
            // cooldown
            Letter.cooldown = 0.5;
            // Numbers list
            numbers = new List<Number>();
            // Bullets list
            bullets = new List<Bullet>();
        }

        public override void HandleEvent(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            if (bonusSpawnCounter <= 0)
            {
                // spawn event
                SpawnNumber(nextEvent.number, nextEvent.decim, nextEvent.speed, nextEvent.line);
                nextEvent = currentLevel.GetNextEvent();
                // reset counter
                bonusSpawnCounter = bonusSpawnDelay;
            }
            else
            {
                bonusSpawnCounter -= delta;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Score : " + LevelStorage.score.ToString(),
                    new Vector2(500, 50), Color.Black);
            base.Draw(spriteBatch);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace LDJam45
{
    public class GameOverState : Menustate
    {


        public GameOverState(GraphicsDeviceManager graphicsDevice) : base(graphicsDevice)
        {
            centralWord = "GAME OVER";
        }
    }
}

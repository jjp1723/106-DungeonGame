using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Group4GroupProject;
using GDAPS2Group4;

namespace Group4GroupProject
{
    class Descend : MovementButton
    {
        private MapManager manager;
        public Descend(Texture2D image, Rectangle rect, Color hue, Player p, Room[,] r, MapManager manager) : base(image, rect, hue, p, r)
        {
            this.manager = manager;
        }
        
        /// <summary>
        /// Generates a new floor and moves player to the center of the dungeon.
        /// </summary>
        public override void Move()
        {
            active = false;
            player.X = 15;
            player.Y = 15;
            manager.Rebuild();
        }
    }
}

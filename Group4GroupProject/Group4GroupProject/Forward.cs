using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPS2Group4;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/// <summary>
/// Forward move button just overrides 1 method from MovementButton
/// </summary>
namespace Group4GroupProject
{
    class Forward:MovementButton
    {

        public Forward(Texture2D image, Rectangle rect, Color hue, Player p,Room[,] r) : base(image, rect, hue, p,r)
        {
            rooms = r;
        }
        /// <summary>
        /// Moves player forward a room and reassigns direction 
        /// </summary>
        public override void Move()
        {
            if (player.Direction == Direction.North)
            {
                if (rooms[player.X, player.Y - 1] != null)
                {
                    player.Y--;
                    player.Direction = Direction.North;
                }
            }
            else if (player.Direction == Direction.South)
            {
                if (rooms[player.X, player.Y + 1] != null)
                {
                    player.Y++;
                    player.Direction = Direction.South;
                }
            }
            else if (player.Direction == Direction.East)
            {
                if (rooms[player.X+1, player.Y] != null)
                {
                    player.X++;
                    player.Direction = Direction.East;
                }
            }
            else if (player.Direction == Direction.West)
            {
                if (rooms[player.X - 1, player.Y] != null)
                {
                    player.X--;
                    player.Direction = Direction.West;
                }
            }
            rooms[player.X, player.Y].Visited = true;
        }

        public override void Update()
        {
            base.Update();
            if (player.Direction == Direction.North)
            {
                if (rooms[player.X, player.Y - 1] != null)
                {
                    active = true;
                }
                else
                {
                    active = false;
                }
            }
            else if (player.Direction == Direction.South)
            {
                if (rooms[player.X, player.Y + 1] != null)
                {
                    active = true;
                }
                else
                {
                    active = false;
                }
            }
            else if (player.Direction == Direction.East)
            {
                if (rooms[player.X + 1, player.Y] != null)
                {
                    active = true;
                }
                else
                {
                    active = false;
                }
            }
            else if (player.Direction == Direction.West)
            {
                if (rooms[player.X - 1, player.Y] != null)
                {
                    active = true;
                }
                else
                {
                    active = false;
                }
            }
        }
    }
}

using GDAPS2Group4;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4GroupProject
{
    class MovementButton : Button
    {
        // ----- Fields ------
        protected Player player;
        
        protected Room[,] rooms;
        
        // ----- Fields Properties -----
        //Player Property
        public Player Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
            }
        }
        // ----- Constructor -----
        public MovementButton(Texture2D image, Rectangle rect, Color hue,Player p,Room[,] r) : base(image, rect, hue)
        {
            player = p;
            
            rooms = r;
        }

        /// <summary>
        /// Moves player in direction that corresponds to the button
        /// </summary>
        public virtual void Move()
        {
            
        }

        public override void Onclick()
        {
            Move();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}

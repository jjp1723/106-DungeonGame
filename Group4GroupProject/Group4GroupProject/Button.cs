using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Group4GroupProject
{
    class Button
    {
        // ----- Fields -----
        protected Texture2D texture;
        protected Rectangle position;
        protected MouseState previousState;
        protected Color color;
        protected bool clicked;
        protected bool active;



        // ----- Fields Propteries -----
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        //Texture Property
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }

        //Rectangle Property
        public Rectangle Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        //X Property
        public int X
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
            }
        }

        //Y Property
        public int Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
            }
        }

        //Color Property
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        //Clicked Property
        public bool Clicked
        {
            get
            {
                return clicked;
            }
            set
            {
                clicked = value;
            }
        }



        // ----- Constructor -----
        public Button(Texture2D image, Rectangle rect, Color hue)
        {
            texture = image;
            Position = rect;
            color = hue;
            clicked = false;
            previousState = Mouse.GetState();
        }



        // ----- Methods -----

        /// <summary>
        /// This is a temporary placeholder. Will need to be filled out eventually.
        /// </summary>
        public virtual void Onclick()
        {
            clicked = true;
        }


        //Draw Method
        public void Draw(SpriteBatch sb)
        {
            if (active)
            {
                sb.Draw(texture, position, color);
            }
            else
            {
                sb.Draw(texture, position, Color.Gray);
            }
        }

        /// <summary>
        /// Should update on gameTime, if clicked while game is running in Explore mode, it should move player to adjacent room
        /// </summary>
        public virtual void Update()
        {
            MouseState ms = Mouse.GetState();
            // If clicked
            if (ms.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released)
            {
                if (this.Position.Contains(ms.Position))
                {
                    Onclick();
                }
            }
            previousState = ms;
        }
    }
}

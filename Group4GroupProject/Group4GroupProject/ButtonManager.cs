using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPS2Group4;
using Microsoft.Xna.Framework.Graphics;

namespace Group4GroupProject
{
    class ButtonManager
    {
        // ----- Fields -----
        protected Button attack;
        protected Button guard;
        protected Button item;
        protected Button flee;
        protected SpriteBatch batch;

        //ToDo: Add Fields for Movement Buttons



        // ----- Field Properties -----

        //AttackButton Property
        public Button Attack
        {
            get
            {
                return attack;
            }
            set
            {
                attack = value;
            }
        }

        //GuardButton Method
        public Button Guard
        {
            get
            {
                return guard;
            }
            set
            {
                guard = value;
            }
        }

        //ItemButton Method
        public Button Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }

        //FleeButton Method
        public Button Flee
        {
            get
            {
                return flee;
            }
            set
            {
                flee = value;
            }
        }

        //SpriteBatch Property
        public SpriteBatch SpriteBatch
        {
            get
            {
                return batch;
            }
            set
            {
                batch = value;
            }
        }

        //ToDo: Add Field Properties for Movement Button Fields



        // ----- Constructor -----
        public ButtonManager(Button aButton, Button gButton, Button iButton, Button fButton, SpriteBatch sb)
        {
            attack = aButton;
            guard = gButton;
            item = iButton;
            flee = fButton;
            batch = sb;
            attack.Active = true;
            guard.Active = true;
            item.Active = true;
            flee.Active = true;
            //ToDo: Update Constructor for Movement Buttons
        }



        // ----- Methods -----

        //BattleDraw Method
        public void BattleDraw()
        {



            //Drawing the AttackButton
            attack.Draw(batch);


            //Drawing the GuardButton
            guard.Draw(batch);


            //Drawing the ItemButton
            item.Draw(batch);


            //Drawing the FleeButton
            flee.Draw(batch);




        }

        //ExploreDraw Method
        public void ExploreDraw()
        {

            //Beginning the Spritebatch
            batch.Begin();

            //Drawing the ItemButton
            batch.Draw(item.Texture, item.Position, item.Color);

            //ToDo: Add Draw commands for Movement Button Fields

            //Ending the Spritebatch
            batch.End();
        }
    }
}

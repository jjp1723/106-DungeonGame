using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPS2Group4;

namespace Group4GroupProject
{
    class Weapon : Item
    {
        // ----- Feilds -----
        protected int damage;



        // ----- Field Properties -----

        //Damage Property
        public int Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }



        // ----- Constructor -----
        public Weapon(int dmg, string name) : base(1, new Random(), name)
        {
            damage = dmg;
        }
    }
}

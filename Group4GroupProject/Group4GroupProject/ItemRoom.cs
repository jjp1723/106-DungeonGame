using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//This is the item room class - inherits from the room class
namespace GDAPS2Group4 
{
    class ItemRoom : Room
    {
        private bool looted;
        private int lootInt;
        private bool hasLeft;
        public bool Looted
        {
            get { return looted; }
            set { looted = value; }
        }

        public ItemRoom(int x, int y,int lootNum) : base(x, y)
        {
            special = true;
            visited = false;
            looted = false;
            flavorText = "There's a chest here! It could have anything! Even a face eating monster!" + "\r\n" + "(dw it's not implemented... Yet. Anyways, press 'E' to loot the contents.) \r\n";
            switch (lootNum)
            {
                case 0:
                    lootInt = 50;
                    break;
                case 1:
                    lootInt = 10;
                    break;
                case 2:
                    lootInt = 5;
                    break;
                default:
                    break;
            }
            hasLeft = false;
        }

        /// <summary>
        /// Changes flavor text
        /// </summary>
        public void Update(Player p)
        {
            if (looted)
            {
                flavorText = "There was a chest with loot here, but a certain greedy adventurer took the contents >:(";
                if (!hasLeft)
                {
                    if (lootInt == 50)
                    {
                        flavorText += "\r\nYou received a potion of healing - it healed you for 50HP. You now have " + p.Health;
                    }
                    if (lootInt == 10)
                    {
                        flavorText += "\r\nYou found a shinier sword! Shiny stuff is always better! (You deal 10 more damage now)";
                    }
                    if (lootInt == 5)
                    {
                        flavorText += "\r\nYou found a new shield! It gives 5 additional defense when blocking!";
                    }
                }
            }


        }

        /// <summary>
        /// Loots the item. Sometimes will be a trap (not implemented)
        /// </summary>
        public string Loot(Player p)
        {
            if (Looted)
            {
                return "";
            }

            Looted = true;
            if (lootInt == 50)
            {
                p.Health += 50;
                if(p.Health > p.MaxHp)
                {
                    p.Health = p.MaxHp;
                }
                return "You found a potion of healing! You restore 50 HP. You are now at " + p.Health;
            }
            if(lootInt == 10)
            {
                p.Strength += 10;
                return "You found a shinier sword! Shiny stuff is always better! (You deal 10 more damage now)";
            }
            if(lootInt == 5)
            {
                p.Block += 5;
                return "You found a new shield! It gives 5 additional defense when blocking!";
            }
            return "";
        }
    }
}

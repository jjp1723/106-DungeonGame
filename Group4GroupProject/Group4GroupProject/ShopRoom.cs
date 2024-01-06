using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Shop room class for the game, passed in room
/// </summary>
namespace GDAPS2Group4
{
    class ShopRoom : Room
    {

        private List<Item> forSale;


        public ShopRoom(int x,int y,Random rng, int floor) : base(x, y)
        {
            forSale = new List<Item>();
            forSale.Add(new Item(1, rng, 10+floor*5, "Name"));
            forSale.Add(new Item(2, rng, 25+floor*10, "Name"));
            forSale.Add(new Item(3, rng, 50+floor*15, "Name"));
            special = true;
            flavorText = "You see a market stall in the room, surprisingly nice for the utter lack of decor" + "\r\n"  + "in every other room.";
        }

        /// <summary>
        /// Returns true if player can purchase an item
        /// </summary>
        /// <returns>T if player can buy an item, false otherwise</returns>
        public bool CanPurchase(Item i, Player p)
        {
            if(p.Money > i.Cost)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes an item for sale (because its been bought)
        /// and Adds it to the player's inventory
        /// </summary>
        /// <param name="i"></param>
        public void BuyItem(Item i, Player p)
        {
            p.Add(i);
            forSale.Remove(i);
        }

        
    }
}




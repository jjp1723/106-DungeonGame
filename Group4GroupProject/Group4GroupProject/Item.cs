using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Item class for all items found in the game or in the shop
/// </summary>
namespace GDAPS2Group4
{
    enum Items
    {
        Consumable = 0,
        Weapon = 1,
        Armor = 2,
        SmokeBomb,
        HealingPotion,
        FireBomb,
        Broadsword,
        Battleaxe,
        Dagger,
        Leather,
        Chainmail,
        Iron
        
    }

    public class Item
    {
        // ----- Fields -----

        // The item type
        private Items itemType;

        // The current item in that type
        private Items item;

        // Cost for item at a shop
        protected int cost;

        // Name of item
        protected string name;

        // Tooltype
        protected string tooltip;
        protected Random rng;



        // ----- Fields Properties -----

        //Cost Property
        public int Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
            }
        }

        //Name Property
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        // Regular constructor for items found in a room
        public Item(int typeItem,  Random rng, string id)
        {
            name = id;

            if (typeItem == 0)
            {
                itemType = Items.Consumable;
                item = (Items) rng.Next((int)Items.SmokeBomb, (int)Items.FireBomb+1);
            }
            if (typeItem == 1)
            {
                itemType = Items.Weapon;
                item = (Items)rng.Next((int)Items.Broadsword,(int)Items.Dagger+ 1);
            }
            if(typeItem == 2)
            {
                itemType = Items.Armor;
                item = (Items)rng.Next((int)Items.Leather,(int)Items.Iron + 1);
            }

        }

        // Constructor for items found in the shop room
        public Item(int typeItem, Random rng , int cost, string id)
        {
            this.cost = cost;
            name = id;

            // Setting what kind of item this item is
            if (typeItem == 0)
            {
                itemType = Items.Consumable;
                item = (Items)rng.Next((int)Items.SmokeBomb, (int)Items.FireBomb + 1);
            }
            if (typeItem == 1)
            {
                itemType = Items.Weapon;
                item = (Items)rng.Next((int)Items.Broadsword, (int)Items.Dagger + 1);
            }
            if (typeItem == 2)
            {
                itemType = Items.Armor;
                item = (Items)rng.Next((int)Items.Leather, (int)Items.Iron + 1);
            }
        }

        public override string ToString()
        {
            return item.ToString() + " it is a " + itemType.ToString();
        }
    }
}

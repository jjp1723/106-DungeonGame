using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Group4GroupProject;
/// <summary>
/// The Character Abstract Class. 
/// Saumil is responsible for this class/ its child classes
/// Refer to Trello or ask questions in discord
/// </summary>
namespace GDAPS2Group4
{
    abstract class Character
    {
        // ----- Fields -----
        protected int health;
        protected int strength;
        protected int money;
        protected bool alive;
        protected Weapon weapon;
        protected List<Item> inventory;



        // ----- Field Properties -----

        //Health Property
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        //Damage Property
        public int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
            }
        }

        //Money Property
        public int Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
            }
        }

        //Weapon Property
        public Weapon Weapon
        {
            get
            {
                return weapon;
            }
            set
            {
                weapon = value;
            }
        }

        //Inventory Property
        public List<Item> Inventory
        {
            get
            {
                return inventory;
            }
            set
            {
                inventory = value;
            }
        }

        //Item Indexer for Inventory Field
        public Item this[int i]
        {
            get
            {
                return inventory[i];
            }
            set
            {
                inventory[i] = value;
            }
        }

        //Alive Property
        public bool Alive
        {
            get
            {
                return Health > 0;
            }
            set
            {
                alive = value;
            }
        }



        // ----- Constructor -----
        public Character(int hp, int str, int wallet, Weapon wp)
        {
            health = hp;
            strength = str;
            money = wallet;
            alive = true;
            weapon = wp;
            inventory = new List<Item>();

            //Adding the weapon to the inventory
            inventory.Add(weapon);
        }



        // ----- Methods -----

        //DisplayInventoryMethod
        public void DisplayInventory()
        {
            //ToDo: Add code that displays the inventory to the player
        }

        //Attack Method
        public void Attack(Character other)
        {
            other.Health -= strength + weapon.Damage;

            //If the other character's health falls below zero, it is set to zero
            if(other.Health < 0)
            {
                other.Health = 0;
            }
        }

        //Add Method
        public void Add(Item item)
        {
            inventory.Add(item);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Group4GroupProject;
using Microsoft.Xna.Framework.Graphics;

/// <summary>
/// The Enemy Class. Inherits from Character
/// Refer to Trello or ask questions in discord
/// </summary>
namespace GDAPS2Group4
{
    //Different Types of Enemies
    enum EnemyType
    {
        Slime,
        Goblin,
        Troll
    }

    class Enemy : Character
    {
        // ----- Fields -----
        protected EnemyType type;
        protected string name;



        // ----- Field Properties -----

        //EnemyType Property
        public EnemyType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
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



        // ----- Constructor -----
        public Enemy(int health, int damage, int wallet, Weapon weapon, EnemyType tp) : base(health, damage, wallet, weapon)
        {
            type = tp;

            //Determining the enemy's name based on its type
            switch(type)
            {
                case EnemyType.Slime:
                    name = "Slime";
                    break;

                case EnemyType.Goblin:
                    name = "Goblin";
                    break;

                case EnemyType.Troll:
                    name = "Troll";
                    break;
            }
        }
        public Enemy(int hp,int dam, int wal, Weapon wep) : base(hp, dam, wal, wep)
        {

        }
    }
}

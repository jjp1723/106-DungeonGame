using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Group4GroupProject;
/// <summary>
/// The Player Class. Inherits from Character.
/// Refer to Trello or ask questions in discord
/// </summary>
namespace GDAPS2Group4
{
    class Player : Character
    {
        // ----- Fields -----
        private int level;
        private int block;
        private int fleechance;
        private BattleState battleState;
        private int maxHP;

        //Local coord vs Global Coord Field
        protected int xPos;
        protected int yPos;
        private Direction facingDirection;

        private int exp;
        private int expToNext;

        // ----- Field Properties -----

        //Experience points
        public int XP
        {
            get { return exp; }
            set { exp = value; }
        }

        //Direction Property
        public Direction Direction
        {
            get
            {
                return facingDirection;
            }
            set
            {
                facingDirection = value;
            }
        }

        //Level Property
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }

        //Guard Property
        public int Block
        {
            get
            {
                return block;
            }
            set
            {
                block = value;
            }
        }

        //FleeChance Property
        public int FleeChance
        {
            get
            {
                return fleechance;
            }
            set
            {
                fleechance = value;
            }
        }

        //X Property
        public int X
        {
            get
            {
                return xPos;
            }
            set
            {
                xPos = value;
            }
        }

        //Y Property
        public int Y
        {
            get
            {
                return yPos;
            }
            set
            {
                yPos = value;
            }
        }

        public int MaxHp
        {
            get { return maxHP; }
        }
        public BattleState CurBattleState {
            get { return battleState; }
            set { battleState = value; }
        }

        // ----- Constructor -----
        public Player(int str, int health, int wallet, Weapon weapon, int x, int y) : base(health, str, wallet, weapon)
        {
            xPos = x;
            yPos = y;
            facingDirection = Direction.North;
            level = 1;
            expToNext = 1;
            exp = 0;
            block = 5;
            maxHP = 100;
        }

        // ----- Methods -----

        //Guard Method
        public void Guard(Enemy enemy)
        {
            if(block >= enemy.Strength)
            {
                return;
            }
            Health -= enemy.Strength - block;
        }

        //Flee Method
        public void Flee()
        {
            switch (Direction)
            {
                case Direction.East:
                    xPos--;
                    Direction = Direction.West;
                    break;
                case Direction.West:
                    xPos++;
                    Direction = Direction.East;
                    break;
                case Direction.North:
                    yPos++;
                    Direction = Direction.South;
                    break;
                case Direction.South:
                    yPos--;
                    Direction = Direction.North;
                    break;
            }
        }

        /// <summary>
        /// Increases player stats if exp is greater than or equal to level requirement.
        /// </summary>
        public string LevelUp()
        {
            if(exp >= expToNext)
            {
                level++;
                exp -= expToNext;
                expToNext = level * 3 + 1;
                strength += 5;
                block+= 5;
                maxHP += (12 * level);
                health = maxHP;
                return "You have leveled up! You are now level " + level + ". Your health has been restored to full. \r\n";
            }
            return "";
        }
    }
}

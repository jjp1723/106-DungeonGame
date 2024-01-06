using GDAPS2Group4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
enum BossType
{
    Slime,
    Ogre,
    Shaman
}
namespace Group4GroupProject
{
    
    class Boss : Enemy
    {
        //Fields
        private int cooldown;
        private string info;

        //Field Properties
        public int Cooldown
        {
            get { return cooldown; }
            set { cooldown = value; }
        }
        public string Info
        {
            get { return info; }
        }
        public Boss(int health, int damage, int wallet, Weapon weapon, EnemyType type) : base(health, damage, wallet, weapon,type)
        {
            cooldown = (int)type + 3;
        }

        /// <summary>
        /// Depending on racial characteristics, the boss will exhibit unique attack patterns
        /// </summary>
        public int SpecialAttack()
        {
            switch (type)
            {
                case EnemyType.Slime:
                    cooldown--;
                    if(cooldown == 0)
                    {
                        cooldown = (int)type + 4;
                        return -2;
                    }
                    else
                    {
                        return strength;
                    }

                case EnemyType.Troll:
                    cooldown--;
                    info = "";
                    if(cooldown == 0)
                    {
                        cooldown = (int)type + 2;
                        info = "The ogre bashes you over the head extra hard.";
                        return strength * 2;

                    }
                    return strength;

                case EnemyType.Goblin:
                    cooldown--;
                    info = "";
                    if (cooldown == 0)
                    {
                        cooldown = (int)type + 2;
                        info = "The goblin shaman regenerates a broken limb. It gains 20HP.";
                        this.Health += 25;
                        return 0;
                    }
                    return strength;

                default:
                    break;
            }
            return -1;
        }
    }
}

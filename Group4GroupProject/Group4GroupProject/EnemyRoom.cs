using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// The EnemyRoom Class. Inherits from Room Class.
/// Refer to Trello or ask questions in discord
/// </summary>
namespace GDAPS2Group4
{
    class EnemyRoom : Room
    {
        private Enemy enemy;
        public Enemy Enemy
        {
            get { return enemy; }
        }
        public EnemyRoom(int x, int y, Enemy enemy) : base(x, y)
        {
            this.enemy = enemy;
            flavorText = "There was an enemy here but its dead now. IT HAD A FAMILY Y'KNOW.";
        }

        
    }
}

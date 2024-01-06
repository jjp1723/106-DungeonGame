using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// The BossRoom Class. Inherits from EnemyRoom.
/// Refer to Trello or ask questions in discord
/// </summary>

namespace GDAPS2Group4
{
    class BossRoom : EnemyRoom
    {
        public bool FloorCleared
        {
            get { return !Enemy.Alive; }
        }
        public BossRoom(int x, int y,Enemy e) : base(x, y,e)
        {
            special = true;
        }

    }
}

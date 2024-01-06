using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// The Room Class. 
/// Zack is responsible for this class/ its child classes
/// Refer to Trello or ask questions in discord
/// </summary>
namespace GDAPS2Group4
{
    public enum FlavorText
    {
        Nothing,
        Bricks,
        Exit,
        Same
    }
    class Room
    {
        // Fields
        private int numExits;
        protected bool visited = false;
        protected bool special;
        private int roomPosX;
        private int roomPosY;
        protected string flavorText;

        // Properties
        public int Exits
        {
            get { return numExits; }
            set { numExits = value; }
        }
        public bool Special { get { return special; } }
        public bool Visited { get { return visited; } set { visited = value; } }
        // Position X
        public int RoomPosX
        {
            get { return roomPosX; }
            set { roomPosX = value; }
        }
        // Position Y
        public int RoomPosY
        {
            get { return roomPosY; }
            set { roomPosY = value; }
        }
        public string FlavorText
        {
            get { return flavorText; }
            set { flavorText = value; }
        }
        // For standard rooms
        public Room(int x, int y,int flavorTextIndex)
        {
            numExits = 0;
            RoomPosX = x;
            RoomPosY = y;
            switch (flavorTextIndex)
            {
                case 0:
                    flavorText = "There is absolutely nothing in this room. FASCINATING.";
                    break;
                case 1:
                    flavorText = "Did something just move? Nah. Gray bricks can't move.";
                    break;
                case 2:
                    flavorText = "Ever get that feeling of Deja Vu?";
                    break;
                case 3:
                    flavorText = "This is like your 5th time here. Who am I kidding, everything looks the same.";
                    break;
                default:
                    flavorText = "How did this even appear? Someone did an oopsies :P";
                    break;
            }
        }
        // For inheritance
        public Room(int x, int y)
        {
            numExits = 0;
            RoomPosX = x;
            RoomPosY = y;
        }
    }
}

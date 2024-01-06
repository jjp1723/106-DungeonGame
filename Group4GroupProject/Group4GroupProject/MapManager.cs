using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Group4GroupProject;
/// <summary>
/// Handles Floor Layout
/// </summary>
/// 
enum Direction
{
    North,
    East,
    South,
    West
}
namespace GDAPS2Group4
{
    
    class MapManager
    {
        private Room[,] rooms;
        private int floor;
        private int numberOfRooms;
        private Random random;
        private List<Room> allRooms;
        private bool bossRoomBuilt;
        private bool itemRoomBuilt;
        private bool shopBuilt;
        private int numOfEnemyRoom;
        private BossRoom bossRoom;
        public Room[,] Rooms
        {
            get
            {
                return rooms;
            }
        }
        public Room this[int x, int y]
        {
            get
            {
                return rooms[x, y];
            }
        }

        public BossRoom BRoom
        {
            get { return bossRoom; }
        }

        public MapManager(Random rng)
        {
            allRooms = new List<Room>();
            rooms = new Room[30,30];
            random = rng;
            Clear();
            floor = 3;
            numberOfRooms = floor * 5 + 3;
            BuildRooms(rooms.GetLength(0) / 2, rooms.GetLength(1) / 2);
            bossRoomBuilt = true;
            itemRoomBuilt = true;
            shopBuilt = true;
            numberOfRooms = 0;
            numOfEnemyRoom = 7;
            SetRoomTypes();
            
            
        }

        /// <summary>
        /// Builds a random room then spirals outwards by continuously building more rooms
        /// 
        /// Known Bugs:
        /// May not fully build an entire floor. (Done in order to prevent stackOverflow error - Fix later)
        /// 
        /// </summary>
        public void BuildRooms(int xPos,int yPos)
        {
            if (numberOfRooms > 0)
            {
                // Creates a new room and decreases the amount of rooms left to build
                numberOfRooms--;
                Room r = new Room(xPos, yPos,random.Next(4));
                rooms[xPos, yPos] = r;
                allRooms.Add(r);
                // Sets how many adjacent rooms can appear
                r = BoundRoom(r);
                int roomsToGenerate = random.Next(1, r.Exits+1);
                
                for (int i = 0; i < roomsToGenerate; i++)
                {
                    // Picks a random direction and recursively call the method
                    Direction d = (Direction)random.Next(4);
                    switch (d)
                    {
                        case Direction.North:
                            if(xPos == 0)
                            {
                                // Cannot Have a negative index x position
                                break;
                            }
                            if (rooms[xPos - 1,yPos] == null)
                            {
                                BuildRooms(xPos - 1, yPos);

                            }
                            break;

                        case Direction.East:
                            if(yPos == 0)
                            {
                                break;

                            }
                            if (rooms[xPos, yPos - 1] == null)
                            {
                                BuildRooms(xPos, yPos - 1);
                            }
                            break;
                        case Direction.South:
                            if (xPos == rooms.GetLength(0)-1)
                            {
                                // Cannot Have a out of bounds index x position
                                break;
                            }
                            if (rooms[xPos + 1, yPos] == null)
                            {
                                BuildRooms(xPos + 1, yPos);

                            }
                            break;

                        case Direction.West:
                            if (yPos == rooms.GetLength(0)-1)
                            {
                                // Cannot Have a out of bounds index y position
                                break;
                            }
                            if (rooms[xPos, yPos + 1] == null)
                            {
                                BuildRooms(xPos, yPos + 1);

                            }
                            break;
                    }
                }
            }
            
            return;
        }

        /// <summary>
        /// Changes base room into other rooms
        /// </summary>
        public void SetRoomTypes()
        {
            
            if (bossRoomBuilt) {
                //Sets the boss room
                Room toChange = allRooms[allRooms.Count - 1];

                
                // Temporary. Replace with correct constructor once BR class is finished
                BossRoom br = new BossRoom(toChange.RoomPosX, toChange.RoomPosY, new Boss((floor-2) * 75, floor * floor, floor*100, new Weapon(100, "Knife"), (EnemyType) random.Next(3)));

                rooms[toChange.RoomPosX, toChange.RoomPosY] = br;
                allRooms[allRooms.Count - 1] = br;
                bossRoom = br;
                bossRoomBuilt = false;
            }

            // Creates an item room
            int indexToChange = random.Next(allRooms.Count);
            if (itemRoomBuilt)
            {
                
                Room toChange = allRooms[indexToChange];
                while (toChange.Special)
                {
                    indexToChange = random.Next(allRooms.Count);
                    toChange = allRooms[indexToChange];
                }
                ItemRoom ir = new ItemRoom(toChange.RoomPosX, toChange.RoomPosY, random.Next(0,3));
                rooms[toChange.RoomPosX, toChange.RoomPosY] = ir;
                allRooms[indexToChange] = ir;
                itemRoomBuilt = false;
            }

            // Creates a shop room
            indexToChange = random.Next(allRooms.Count);
            if (shopBuilt)
            {
                Room toChange = allRooms[indexToChange];
                while (toChange.Special)
                {
                    indexToChange = random.Next(allRooms.Count);
                    toChange = allRooms[indexToChange];
                }
                ShopRoom shop = new ShopRoom(toChange.RoomPosX, toChange.RoomPosY,random,floor);
                rooms[toChange.RoomPosX, toChange.RoomPosY] = shop;
                allRooms[indexToChange] = shop;
                shopBuilt = false;
            }

            // Converts a portion of the floors non-special rooms into enemy rooms
            
            for (int i = numOfEnemyRoom; i > 0;i--)
            {
                indexToChange = random.Next(allRooms.Count);
                Room toChange = allRooms[indexToChange];
                while (toChange.Special)
                {
                    indexToChange = random.Next(allRooms.Count);
                    toChange = allRooms[indexToChange];
                }
                EnemyRoom ene = new EnemyRoom(toChange.RoomPosX, toChange.RoomPosY, new Enemy((floor+1) * 8, (floor+1) * (floor) -5, floor *random.Next(0,11), new Weapon(10, "Knife"), (EnemyType) random.Next(3)));
                rooms[toChange.RoomPosX, toChange.RoomPosY] = ene;
                allRooms[indexToChange] = ene;

            }
            rooms[rooms.GetLength(0) / 2, rooms.GetLength(1) / 2] = new Room(rooms.GetLength(0) / 2, rooms.GetLength(1) / 2,random.Next(4));
        }

        /// <summary>
        /// Clears the floor
        /// </summary>
        public void Clear()
        {
            allRooms.Clear();
            Array.Clear(rooms, 0, rooms.Length);
            numberOfRooms = floor * 3 + 5;
            bossRoomBuilt = true;
            itemRoomBuilt = true;
            shopBuilt = true;
        }

        /// <summary>
        /// Helper method.
        /// Defines how many exits a room CAN have, not necessarily will have.
        /// 
        /// Should clean up Eventually.
        /// </summary>
        /// <param name="toCheck"> The room to define </param>
        /// <returns> number of possible exits </returns>
        public Room BoundRoom(Room toCheck)
        {
            //Defines Left Bound Rooms
            if (toCheck.RoomPosX == 0)
            {
                if(toCheck.RoomPosY == 0)
                {
                    toCheck.Exits = 2;
                    return toCheck; 
                }
                if(toCheck.RoomPosY == rooms.GetLength(1)-1)
                {
                    toCheck.Exits = 2;
                    return toCheck; 

                }
                toCheck.Exits = 3;
                return toCheck;
            }

            //Defines Right bound rooms
            else if(toCheck.RoomPosX == rooms.GetLength(0)-1)
            {
                if (toCheck.RoomPosY == 0)
                {
                    toCheck.Exits = 2;
                    return toCheck;

                }
                if (toCheck.RoomPosY == rooms.GetLength(1)-1)
                {

                    toCheck.Exits = 2;
                    return toCheck;
                    
                }
                toCheck.Exits = 3;
                return toCheck;
            }
            else
            {
                // Top Bound Rooms (Excluding Corners)
                if(toCheck.RoomPosY == 0)
                {
                    toCheck.Exits = 3;
                    return toCheck;
                }

                //Bottom Bound Rooms(Excluding Corners)
                if (toCheck.RoomPosY == rooms.GetLength(1)-1)
                {
                    toCheck.Exits = 3;
                    return toCheck;
                }
            }
            //Center Rooms
            toCheck.Exits = 4;
            return toCheck;
        }

        /// <summary>
        /// Loads a map from a file
        /// </summary>
        public void LoadMap()
        {

        }

        /// <summary>
        /// Rebuilds a floor after either a clear or a death
        /// </summary>
        public void Rebuild()
        {
            Clear();
            floor++;
            numberOfRooms = floor * 5 + 3;
            BuildRooms(15,15);
            numOfEnemyRoom = floor * 2 + 1;
            SetRoomTypes();
        }
    }
}

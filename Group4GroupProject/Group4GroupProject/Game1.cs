using GDAPS2Group4;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Group4GroupProject
{
    /// <summary>
    /// Untitled Dungeon Game: Group 4
    /// Vincent, Zach, John, Saumil
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D spriteTest;
        private MapManager mapManager;
        private Random rng;

        // States for the game
        enum GameState
        {
            Menu,
            Explore,
            ShopRoom,
            ItemRoom,
            Combat,
            Death
        }

        // Enum varible
        private GameState gameState;

        // Menu and Game over textures
        private Texture2D background;
        private Texture2D mainMenuTitle;
        private Texture2D gameOverTitle;

        // Keyboard state's
        private KeyboardState kbState;
        private KeyboardState previousKbState;

        // Player for the game
        private Player p1;

        // Player and Enemy Rectangle
        private Rectangle p1Rect;

        // What direction should the player move
        private Left left;
        private Right right;
        private Forward forward;
        private Back back;

        //Combat buttons
        private Button attack;
        private Button guard;
        private Button flee;
        private Button itemUse;

        //Descend button. Appears after boss has been killed
        private Button descend;

        // SpriteFont
        private SpriteFont gameFont;

        private ButtonManager buttonManager;
        private BattleManager battler;

        //Room Visuals
        private Texture2D blankRoom;
        private Texture2D centerDoor;
        private Texture2D leftDoor;
        private Texture2D rightDoor;
        private Texture2D shopImg;
        private Texture2D diagBox;
        private Texture2D hatch;
        private Texture2D floorHatch;

        //Enemy Visuals
        private Texture2D slimeSkin;
        private Texture2D goblinSkin;
        private Texture2D trollSkin;

        private bool mapEnabled = false;
        private string currentLine;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            rng = new Random();
            mapManager = new MapManager(rng);

            // Setting the textures
            background = Content.Load<Texture2D>("Main Menu Background");
            mainMenuTitle = Content.Load<Texture2D>("Title");
            gameOverTitle = Content.Load<Texture2D>("Game Over");

            // Making a new player and setting menu to begin
            p1 = new Player(10, 100, 50, new Weapon(0, "Sword"), 100, 50);
            p1.X = mapManager.Rooms.GetLength(0)  /2;
            p1.Y = mapManager.Rooms.GetLength(1) /2;

            this.IsMouseVisible = true;
            gameState = GameState.Menu;
            currentLine = "";

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteTest = Content.Load<Texture2D>("square");

            p1Rect = new Rectangle(p1.X*10, p1.Y*10, 10, 10);

            left = new Left(Content.Load<Texture2D>("Left Button"), new Rectangle(00, 350, 200, 200), Color.White, p1,mapManager.Rooms);
            forward = new Forward(Content.Load<Texture2D>("Forward Button"), new Rectangle(200, 350, 200, 200), Color.White, p1, mapManager.Rooms);
            right = new Right(Content.Load<Texture2D>("Right Button"), new Rectangle(400, 350, 200, 200), Color.White, p1, mapManager.Rooms);
            back = new Back(Content.Load<Texture2D>("Back Button"), new Rectangle(600, 350, 200, 200), Color.White, p1, mapManager.Rooms);

            attack = new Button(Content.Load<Texture2D>("Attack Button"), new Rectangle(000, 350, 200, 150), Color.White);
            guard = new Button(Content.Load<Texture2D>("Guard Button"), new Rectangle(200, 350, 200, 150), Color.White);
            itemUse = new Button(Content.Load<Texture2D>("Item Button"), new Rectangle(400, 350, 200, 150), Color.White);
            itemUse.Active = false;
            flee = new Button(Content.Load<Texture2D>("Flee Button"), new Rectangle(600, 350, 200, 150), Color.White);

            descend = new Descend(Content.Load<Texture2D>("descend"), new Rectangle(350, 430, 100, 50), Color.White,p1,mapManager.Rooms,mapManager);

            buttonManager = new ButtonManager(attack, guard, itemUse, flee, spriteBatch);

            blankRoom = Content.Load<Texture2D>("Room - No Doors");
            centerDoor = Content.Load<Texture2D>("Doorway - Center");
            leftDoor = Content.Load<Texture2D>("Doorway - Left");
            rightDoor = Content.Load<Texture2D>("Doorway - Right");
            shopImg = Content.Load<Texture2D>("Shop Stand");
            diagBox = Content.Load<Texture2D>("box");
            gameFont = Content.Load<SpriteFont>("GameFont");
            hatch = Content.Load<Texture2D>("Trapdoor - Ceiling");
            floorHatch = Content.Load<Texture2D>("Trapdoor - Floor");

            slimeSkin = Content.Load<Texture2D>("Enemy - Slime");
            goblinSkin = Content.Load<Texture2D>("Enemy - Goblin");
            trollSkin = Content.Load<Texture2D>("Enemy - Troll");
            // Loading the files for player stats
            /**
            FileStream inStream = File.OpenRead("game.txt");
            BinaryReader input = new BinaryReader(inStream);
            p1.Level = input.ReadInt32();
            p1.Block = input.ReadInt32();
            p1.FleeChance = input.ReadInt32();
            input.Close();
            **/


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Needs to be fixed in saving
            FileStream outStream = File.OpenWrite("game.txt");
            BinaryWriter output = new BinaryWriter(outStream);
            output.Write(p1.Level);
            output.Write(p1.Block);
            output.Write(p1.FleeChance);
            output.Close();
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            kbState = Keyboard.GetState();
            // Switching game states
            switch (gameState)
            {
                case GameState.Menu:
                    
                    if (SingleKeyPress(Keys.Enter))
                    {
                        mapManager.BuildRooms(15,15);
                        gameState = GameState.Explore;
                    }
                    break;

                // Only Leave when entering enemy encounter
                case GameState.Explore:
                    if (p1.CurBattleState != BattleState.Fled)
                    {
                        currentLine = mapManager.Rooms[p1.X, p1.Y].FlavorText;
                    }
                    if(p1.CurBattleState == BattleState.EnemyDead)
                    {
                        currentLine = battler.Outcome;
                        p1.CurBattleState = BattleState.PlayerTurn;
                    }
                    left.Update();
                    right.Update();
                    forward.Update();
                    back.Update();
                    if (descend.Active)
                    {
                        descend.Update();
                    }
                    // If dies, game over
                    if (p1.Health <= 0)
                    {
                        gameState = GameState.Death;
                    }
                    if (mapManager.Rooms[p1.X, p1.Y] is BossRoom)
                    {
                        battler = new BattleManager(p1, ((EnemyRoom)mapManager.Rooms[p1.X, p1.Y]), buttonManager);
                        if (((EnemyRoom)mapManager.Rooms[p1.X, p1.Y]).Enemy.Alive)
                        {
                            gameState = GameState.Combat;
                            p1.CurBattleState = BattleState.PlayerTurn;
                            currentLine = battler.Battle();
                        }
                    }
                    else if (mapManager.Rooms[p1.X, p1.Y] is EnemyRoom)
                    {
                        battler = new BattleManager(p1, ((EnemyRoom)mapManager.Rooms[p1.X, p1.Y]), buttonManager);
                        if (((EnemyRoom)mapManager.Rooms[p1.X, p1.Y]).Enemy.Alive)
                        {
                            gameState = GameState.Combat;
                            p1.CurBattleState = BattleState.PlayerTurn;
                            currentLine = battler.Battle();
                        }
                    }

                    if(mapManager.Rooms[p1.X,p1.Y] is ItemRoom)
                    {
                        ((ItemRoom)mapManager.Rooms[p1.X, p1.Y]).Update(p1);
                        if (SingleKeyPress(Keys.E))
                        {
                            currentLine = ((ItemRoom)mapManager.Rooms[p1.X, p1.Y]).Loot(p1);
                        }
                    }
                    
                    if (SingleKeyPress(Keys.Tab))
                    {
                        mapEnabled = !mapEnabled;
                    }
                    break;

                case GameState.Combat:
                    // If dies, game over
                    currentLine = battler.Battle();
                    if (p1.Health <= 0)
                    {
                        gameState = GameState.Death;
                    }
                    if (p1.CurBattleState == BattleState.Fled)
                    {
                        gameState = GameState.Explore;
                    }
                    if (p1.CurBattleState == BattleState.PlayerDead)
                    {
                        gameState = GameState.Death;
                    }

                    // If enemy dies, continue to explore
                    if (mapManager.Rooms[p1.X, p1.Y] is EnemyRoom)
                    {
                        if (((EnemyRoom)mapManager.Rooms[p1.X, p1.Y]).Enemy.Health <= 0)
                        {
                            ((EnemyRoom)mapManager.Rooms[p1.X, p1.Y]).Enemy.Alive = false;
                            if (mapManager.Rooms[p1.X, p1.Y] is BossRoom)
                            {
                                descend.Active = true;
                            }
                            gameState = GameState.Explore;
                        }
                    }
                    //Insta kills enemy
                    if (SingleKeyPress(Keys.Escape))
                    {
                        ((EnemyRoom)mapManager.Rooms[p1.X, p1.Y]).Enemy.Health = 0;
                        ((EnemyRoom)mapManager.Rooms[p1.X, p1.Y]).Enemy.Alive = false;
                    }
                    //Commits suicide
                    if (SingleKeyPress(Keys.Back))
                    {
                        p1.Health = 0;
                    }
                    break;


                // If escape is pressed then go back to main menu
                case GameState.Death:
                    if (SingleKeyPress(Keys.A))
                    {
                        gameState = GameState.Menu;
                        mapManager.Clear();
                        mapManager.BuildRooms(15, 15);
                        p1.X = 15;
                        p1.Y = 15;
                        p1.Health = 100;
                        p1.Direction = Direction.North;
                        mapManager.SetRoomTypes();
                    }
                    break;
            }
            previousKbState = kbState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            spriteBatch.Begin();

            // Adding text to menu to make to start up the game
            if(gameState == GameState.Menu)
            {
                spriteBatch.Draw(background, new Rectangle(-100, -100, 1000, 1000), Color.White);
                spriteBatch.Draw(mainMenuTitle, new Rectangle(GraphicsDevice.Viewport.Width / 4 - 50, GraphicsDevice.Viewport.Height / 4 - 100, 500, 500), Color.White);
            }
            if(gameState == GameState.Death)
            {
                spriteBatch.Draw(background, new Rectangle(-100, -100, 1000, 1000), Color.White);
                spriteBatch.Draw(gameOverTitle, new Rectangle(GraphicsDevice.Viewport.Width / 4 - 50, GraphicsDevice.Viewport.Height / 4 - 100, 500, 500), Color.White);
                /* 
                 * spriteBatch.DrawString(gameFont,"Death Recap:", new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2 + 100), Color.Black);
                spriteBatch.DrawString(gameFont, "press A to go back to main menu", new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2 + 200), Color.Black);
                */
            }

            // Draws in the game state
            if (gameState == GameState.Explore || gameState == GameState.Combat)
            {
                spriteBatch.Draw(blankRoom, new Rectangle(0, 0, 800, 500), Color.White);
                if (mapManager[p1.X, p1.Y] is BossRoom)
                {
                    spriteBatch.Draw(floorHatch, new Rectangle(300, 300, 200, 200), Color.White);
                }
                if (p1.X == 15 && p1.Y == 15)
                {
                    spriteBatch.Draw(hatch, new Rectangle(300, 20, 200, 50), Color.White);
                }
                if (left.Active)
                {
                    spriteBatch.Draw(leftDoor, new Rectangle(15, 100, 100, 400), Color.White);
                }
                if (forward.Active)
                {
                    spriteBatch.Draw(centerDoor, new Rectangle(300, 100, 200, 300), Color.White);
                }
                if (right.Active)
                {
                    spriteBatch.Draw(rightDoor, new Rectangle(700, 100, 100, 405), Color.White);
                }
                if (mapManager.Rooms[p1.X, p1.Y] is ShopRoom)
                {
                    spriteBatch.Draw(shopImg, new Rectangle(150, 100, 250, 300), Color.White);
                }
                spriteBatch.Draw(diagBox, new Rectangle(75, 300, 650, 150), Color.White);
                spriteBatch.DrawString(gameFont, currentLine, new Vector2(90, 330), Color.Black);
            }

            if (gameState == GameState.Explore)
            {
                
                left.Draw(spriteBatch);
                right.Draw(spriteBatch);
                forward.Draw(spriteBatch);
                back.Draw(spriteBatch);

                if (descend.Active)
                {
                    descend.Draw(spriteBatch);
                }
                if (mapEnabled)
                {
                    for (int i = 0; i < mapManager.Rooms.GetLength(0); i++)
                    {
                        for (int j = 0; j < mapManager.Rooms.GetLength(1); j++)
                        {
                            if (mapManager[i, j] != null)
                            {
                                if (mapManager[i, j].Visited)
                                {
                                    if (mapManager[i, j] is BossRoom)
                                    {
                                        spriteBatch.Draw(spriteTest, new Rectangle(i * 10, j * 10, 10, 10), Color.Red);
                                    }
                                    else if (mapManager[i, j] is ShopRoom)
                                    {
                                        spriteBatch.Draw(spriteTest, new Rectangle(i * 10, j * 10, 10, 10), Color.Green);
                                    }
                                    else if (mapManager[i, j] is ItemRoom)
                                    {
                                        spriteBatch.Draw(spriteTest, new Rectangle(i * 10, j * 10, 10, 10), Color.Blue);
                                    }
                                    else if (mapManager[i, j] is EnemyRoom)
                                    {
                                        spriteBatch.Draw(spriteTest, new Rectangle(i * 10, j * 10, 10, 10), Color.Orange);
                                    }
                                    else
                                    { 
                                        spriteBatch.Draw(spriteTest, new Rectangle(i * 10, j * 10, 10, 10), Color.White);
                                    }
                                }
                                else
                                {
                                    spriteBatch.Draw(spriteTest, new Rectangle(i * 10, j * 10, 10, 10), Color.Gray);
                                }
                            }
                            
                        }
                    }
                    // Draws player
                    spriteBatch.Draw(spriteTest, new Rectangle(p1.X * 10, p1.Y * 10, 10, 10), Color.Purple);
                }  
            }
            if (gameState == GameState.Combat)
            {
                spriteBatch.End();
                spriteBatch.Begin();

                //Draws the enemy
                switch (battler.Enemy.Type)
                {
                    //Draws a slime if the enemy is a slime
                    case EnemyType.Slime:
                        spriteBatch.Draw(slimeSkin, new Rectangle(300, 130, 200, 200), Color.White);
                        break;

                    //Draws a goblin if the enemy is a goblin
                    case EnemyType.Goblin:
                        spriteBatch.Draw(goblinSkin, new Rectangle(280, 130, 250, 250), Color.White);
                        break;

                    //Draws a troll if the enemy is a troll
                    case EnemyType.Troll:
                        spriteBatch.Draw(trollSkin, new Rectangle(250, 110, 300, 300), Color.White);
                        break;
                }
                spriteBatch.Draw(diagBox, new Rectangle(75, 300, 650, 150), Color.White);
                spriteBatch.DrawString(gameFont, currentLine, new Vector2(90, 330), Color.Black);
                spriteBatch.End();
                spriteBatch.Begin();
                buttonManager.BattleDraw();
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        // Key press method
        public bool SingleKeyPress(Keys key)
        {
            if (kbState.IsKeyDown(key) && previousKbState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        // What moves the player, allows it to encounter stuff
        public void MovePlayer()
        {
            KeyboardState board = Keyboard.GetState();

            if (board.IsKeyDown(Keys.Right))
            {
                p1Rect.X += 2;
            }
            if (board.IsKeyDown(Keys.Left))
            {
                p1Rect.X -= 2;
            }
            if (board.IsKeyDown(Keys.Up))
            {
                p1Rect.Y -= 2;
            }
            if (board.IsKeyDown(Keys.Down))
            {
                p1Rect.Y += 2;
            }
        }
    }
}

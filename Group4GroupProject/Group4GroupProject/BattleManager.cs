using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPS2Group4;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
enum BattleState
{
    PlayerTurn,
    EnemyTurn,
    Fled,
    PlayerDead,
    EnemyDead
}
enum PlayerAction
{
    Attack,
    Guard,
    Item,
    Flee,
    Start
}
namespace Group4GroupProject
{
    class BattleManager
    {
        // ----- Fields -----
        protected Player player;
        protected Enemy enemy;
        protected EnemyRoom room;
        protected BattleState state;
        protected ButtonManager buttons;
        PlayerAction action = PlayerAction.Start;
        private string turnOutcome;
        //ToDo: Add more fields related to player and enemy statistics



        // ----- Field Properties -----

        //Player Property
        public Player Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
            }
        }

        //Enemy Property
        public Enemy Enemy
        {
            get
            {
                return enemy;
            }
            set
            {
                enemy = value;
            }
        }

        //EnemyRoom Property
        public EnemyRoom EnemyRoom
        {
            get
            {
                return room;
            }
            set
            {
                room = value;
            }
        }

        //ButtonManager Property
        public ButtonManager ButtonManager
        {
            get
            {
                return buttons;
            }
            set
            {
                buttons = value;
            }
        }

        //BattleState Property
        public BattleState BattleState
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        //Outcome Property
        public string Outcome
        {
            get
            {
                return turnOutcome;
            }
        }



        // ----- Constructor -----
        public BattleManager(Player user, EnemyRoom er, ButtonManager butt)
        {
            player = user;
            enemy = er.Enemy;
            room = er;
            buttons = butt;
            state = BattleState.PlayerTurn;
            turnOutcome = "";
        }



        // ----- Methods -----

        //Battle Method
        public string Battle()
        {
            //Using a switch to check the state of the battle
            switch (state)
                {
                //When it's the player's turn
                case BattleState.PlayerTurn:
                    //Drawing the buttons related to player actions
                    buttons.Attack.Update();
                    buttons.Guard.Update();
                    buttons.Item.Update();
                    buttons.Flee.Update();
                    //If the Attack Button is clicked, the player's attack method is called, the Attack Button's clicked field is set to false, and it becomes the enemy's turn
                    if (buttons.Attack.Clicked)
                    {
                        player.Attack(enemy);
                        buttons.Attack.Clicked = false;
                        state = BattleState.EnemyTurn;
                        action = PlayerAction.Attack;
                        player.CurBattleState = BattleState.EnemyTurn;
                        turnOutcome = "You attacked for " + player.Strength + " damage\r\n" + "The " + enemy.Name + " has " + enemy.Health + " health remaining";
                        if (Enemy.Health <= 0)
                        {
                            if (enemy is Boss)
                            {
                                player.XP += 3;
                            }
                            else
                            {
                                player.XP++;
                            }
                            string  s= player.LevelUp();
                            if(s != null)
                            {
                                turnOutcome = s;
                            }
                            player.CurBattleState = BattleState.EnemyDead;
                            state = BattleState.EnemyDead;
                        }
                    }
                    //If the Guard Button is clicked, the player's guard method is called and the Guard Button's clicked field is set to false
                    if (buttons.Guard.Clicked)
                    {
                        player.Guard(enemy);
                        buttons.Guard.Clicked = false;
                        player.CurBattleState = BattleState.EnemyTurn;
                        action = PlayerAction.Guard;
                        if (player.Health < 0)
                        {
                            state = BattleState.PlayerDead;
                        }
                        if (enemy.Strength < player.Block)
                        {
                            turnOutcome = "The " + enemy.Name + " attacked! You blocked and took no damage!";
                        }
                        else
                        {
                            turnOutcome = "The " + enemy.Name + " attacked! You blocked and took " + (enemy.Strength - player.Block) + " damage. \r\nYou now have " + player.Health + " health remaining.";
                        }
                    }
                    //If the Item Button is clicked, the button's DisplayInventory Method is called
                    if (buttons.Item.Clicked)
                    {
                         player.DisplayInventory();
                        action = PlayerAction.Item;
                        buttons.Item.Clicked = false;
                           // state = BattleState.EnemyTurn;
                    }
                    //If the Flee Button is clicked, the Flee Button's clicked field is set to false and the player's Flee method is called
                    if (buttons.Flee.Clicked)
                    {
                        buttons.Flee.Clicked = false;
                        player.Health -= enemy.Strength;
                        action = PlayerAction.Flee;
                        turnOutcome = "You have fled! You took " + enemy.Strength + " damage. \r\n You now have " + player.Health + " health remaining.";
                        player.Flee();
                        state = BattleState.Fled;
                        player.CurBattleState = BattleState.Fled;
                    }
                    //Checking if the enemy's health has dropped below zero, and if they have, the battle ends
                    
                    break;

                    //When it's the enemy's turn
                    case BattleState.EnemyTurn:
                    int enemyMove;
                    //The enemy attacks the player, lowering the player's health
                    if (enemy is Boss)
                    {
                        enemyMove = ((Boss)enemy).SpecialAttack();
                        if(((Boss)enemy).Type == EnemyType.Slime)
                        {
                            if(enemyMove < 0)
                            {
                                player.Strength += enemyMove;
                                turnOutcome = ("Slime has permanently damaged your weapon. You now deal 2 less damage.");
                                state = BattleState.PlayerTurn;
                                break;
                            }
                            else
                            {
                                if(action == PlayerAction.Guard)
                                {
                                    if(player.Block < enemy.Strength)
                                    {
                                        player.Health -= (enemy.Strength - player.Block);
                                    }
                                }
                                else
                                {
                                    player.Health -= enemy.Strength;
                                }
                                
                            }
                        }
                        if (((Boss)enemy).Type == EnemyType.Troll)
                        {
                            if(((Boss)enemy).Info != "")
                            {
                                turnOutcome = ((Boss)enemy).Info;
                            }
                            if (action == PlayerAction.Guard)
                            {
                                if (player.Block < enemy.Strength)
                                {
                                    player.Health -= (enemyMove - player.Block);
                                }
                            }
                            else
                            {
                                player.Health -= enemyMove;
                            }
                        }
                        if((((Boss)enemy).Type == EnemyType.Goblin))
                        {
                            if (((Boss)enemy).Info != "")
                            {
                                turnOutcome = ((Boss)enemy).Info;
                            }
                            if(enemyMove == 0)
                            {
                                state = BattleState.PlayerTurn;
                                break;
                            }
                            if (action == PlayerAction.Guard && enemyMove > 0)
                            {
                                if (player.Block < enemy.Strength)
                                {
                                    player.Health -= (enemyMove - player.Block);
                                }
                            }
                            else
                            {
                                player.Health -= enemyMove;
                            }
                        }
                    }
                    else
                    {
                        enemyMove = enemy.Strength;
                        if (action == PlayerAction.Guard)
                        {
                            if (player.Block < enemy.Strength)
                            {
                                player.Health -= (enemyMove - player.Block);
                            }
                        }
                        else
                        {
                            player.Health -= enemyMove;
                        }
                    }
                    state = BattleState.PlayerTurn;
                    player.CurBattleState = BattleState.PlayerTurn;
                    //Checking if the either the player's or enemy's health has dropped below zero, and if they have, the battle ends
                    if (player.Health < 0)
                    {
                        state = BattleState.PlayerDead;
                    }
                    switch (action)
                    {
                        case PlayerAction.Attack:
                            turnOutcome += " \r\nThe " + enemy.Name + " attacked! You took " + (enemyMove) + " damage \r\nYou now have " + player.Health + " health remaining.";
                            break;

                        case PlayerAction.Guard:
                            if (enemy.Strength < player.Block)
                            {
                                turnOutcome = "The " + enemy.Name + " attacked! You blocked and took no damage!";
                            }
                            else
                            {
                                turnOutcome = "The " + enemy.Name + " attacked! You blocked and took " + (enemyMove - player.Block) + " damage \r\n You now have " + player.Health + " health remaining.";
                            }
                            break;

                        case PlayerAction.Flee:
                            turnOutcome = "You have fled! You took " + enemyMove + " damage. \r\n You now have " + player.Health + " health remaining.";
                            break;

                        case PlayerAction.Item:
                            break;

                        default:
                            turnOutcome = "OK something went really wrong if you are seeing this message.";
                            break;
                    }
                    break;
                }
            

            return turnOutcome;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HelloDungeon
{
    internal class Game
    {
        float playerHealth = 10.0f;
        float playerMana = 5.0f;
        float playerMaxMana = 5.0f;
        int playerAttack = 3;
        int playerDefense = 3;
        int playerMagic = 3;
        int playerMagicDefense = 3;
        int playerGold = 3;
        string playerAlignment = "Neutral";
        float playerProximityToNearestLivingSkeleton = 20.0f;
        bool playerAlive = true;
        string playerRole = "";
        int input = 0;
        string playerName = "";

        public void Run()
        {
            Console.WriteLine("What's your name, adventurer?");
            string? playerName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(playerName))
            {
                Console.WriteLine("Sorry, what didya say?");
                playerName = Console.ReadLine();
            }


            


            Console.WriteLine("Hello, " + playerName + "!");
            Console.WriteLine();
            Console.WriteLine("Welcome to the dungeon!");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Are you a Wizard, or a Warrior?");


            int input = PlayerTwoChoices("Are you a warrior or a wizard?", "Warrior", "Wizard");
            if (input == 1)
            {
                playerRole = "Warrior";
                playerAttack += 2;
                playerDefense += 2;
            }
            else if (input == 2)
            {
                playerRole = "Wizard";
                playerMagic += 2;
                playerMagicDefense += 2;
                playerMana += 2;
                playerMaxMana += 2;
            }
            Console.WriteLine("Health: " + playerHealth);
            Console.WriteLine("Mana: " + playerMana);
            Console.WriteLine("Gold: " + playerGold);
            Console.WriteLine("Alignment: " + playerAlignment);
            Console.WriteLine("Alive?: " + playerAlive);
            Console.WriteLine("Proximity to Nearest Living Skeleton: " + playerProximityToNearestLivingSkeleton + " meters");
            Console.WriteLine("Player Role: " + playerRole);

            Console.WriteLine("Gray bricks line the walls of the dungeon,"
                + " and dust and dread permeate the air.");
            Combat(42);

            input = PlayerTwoChoices("There are two doors on opposite walls from each other. Which do you choose?", "Left", "Right");
            if (input == 1)
            {
                Console.WriteLine("You enter the left door.");
                Console.WriteLine("The scent of dust and dread is far stronger beyond this door.");
                Console.WriteLine("Cracked stone bricks continue to line the walls."
                    + " You feel a dreadful presence somewhere in these halls.");
                playerProximityToNearestLivingSkeleton -= 3;
                Console.WriteLine();
                Console.WriteLine("Proximity to Nearest Living Skeleton: " + playerProximityToNearestLivingSkeleton + " meters (-3)");
                Console.WriteLine();
                Console.WriteLine("Nonetheless, you must perservere. You come across a fork in your path.");
                input = PlayerTwoChoices("One path leads to a strange chest, and the other certainly leads you closer to a Living Skeleton." + 
                    "Which way do you choose?", "Straight", "Left");
                    if (input == 1)
                    {
                        Console.WriteLine("You approach the chest with the same caution you'd give a wild animal.");
                        Console.WriteLine("You see a bit of clear fluid around the lid...");
                        input = PlayerTwoChoices("Are you certain you wish to open this chest?", "Yes", "No");
                            if (input == 1)
                            {
                                Console.WriteLine("Alas, the chest was a Mimic!");
                                Console.WriteLine("That was pretty obvious, though.");
                                Console.WriteLine("The Mimic lunges at you!");
                                Console.WriteLine("COMBAT START!");

                                // we're gonna learn how to make our own functions eventually
                                // that's when i figure out the combat

                            }
                            else if (input == 2)
                            {
                                Console.WriteLine("You turn around, then go down the hallway that is now to your right.");
                            }

                    }
                    else if (input == 2)
                    {
                        Console.WriteLine("As you turn the corner, you stumble into a small, green slime.");
                        Console.WriteLine("It squelches with an immense rage that has been stewing for years upon years.");
                        Combat(1);


                        // combat will happen eventually dw about it rn
                    }
                }
            else if (input == 2)
            {
                Console.WriteLine("You enter the right door.");
                Console.WriteLine("Dust and dread are replaced by ash and flame. Magma pours around you, but never on you.");
                Console.WriteLine("You feel a general lack of dreadful presences in this direction.");
                playerProximityToNearestLivingSkeleton += 3;
                Console.WriteLine();
                Console.WriteLine("Proximity to Nearest Living Skeleton: " + playerProximityToNearestLivingSkeleton + " meters (+3)");
                Console.WriteLine();
                Console.WriteLine("Soon enough, you come across a fork in your path.");
                input = PlayerTwoChoices("Straight ahead is a Sphinx, encrusted in magma. To the right is a very tall door." + 
                    "Which way do you go?", "Straight", "Right");
                    if (input == 1)
                    {
                        Console.WriteLine("You approach the molten Sphinx with great caution.");
                        Console.WriteLine("The Sphinx immediately fixes its gaze upon you.");
                        Console.WriteLine("''HUMAN,'' It bellows. ''TO PASS THROUGH YOU MUST ANSWER MY RIDDLES.''");
                        /*
                        -- I walk on two legs in the morning, two legs in the evening, and two legs in the afternoon. What am I?
                        A: Chicken
                        -- I have a bank but no money, and a channel but no television. What am I?
                        A: River
                        -- 2 / x = 1
                        A: 2
                        -- FOUR BAJILLION AND TWO - 1
                        A: FOUR BAJILLION AND ONE
                        --
                        A:
                        --
                        A:
                        */
                    }
                    else if (input == 2)
                    {
                        Console.WriteLine("The moment you touch the doorknob, the door falls over like a domino away from you.");
                        Console.WriteLine("I dont know whats behind this other than a lever code thing");
                        Console.WriteLine("uhhh HEY LOOK AT THIS -> left left right ");
                        Console.WriteLine("wait right a slime");
                    }
           
                    }
                }
        int PlayerTwoChoices(string description, string option1, string option2)
        {
            string input = "";
            int inputRecieved = 0;

            // while loop to prevent invalid input
            while (inputRecieved != 1 && inputRecieved != 2)
            {
                // Print options
                Console.WriteLine(description);
                Console.WriteLine("1: " + option1 + " | 2: " + option2);
                Console.Write("> ");

                // Get input from player cause theyre so cool
                input = Console.ReadLine();

                // if player selected the first option
                if (input == "1" || input == option1)
                {
                    // set input recieved to the first option
                    inputRecieved = 1;
                }
                // if they chose the second option
                else if (input == "2" || input == option2)
                {
                    // set it to the second option :3
                    inputRecieved = 2;
                }
                // if its neither, however
                else
                {
                    // display error
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey();
                }
            }
            Console.Clear();
            return inputRecieved;
        }
        void Combat(int enemyID)
        {
            // Declare the base enemy stats real quick
            string enemyName = "";
            float enemyHealth = 0.0f;
            float enemyMana = 0.0f;
            int enemyAttack = 0;
            int enemyDefense = 0;
            int enemyMagic = 0;
            int enemyMagicDefense = 0;
            float damageDealt = 0.0f;
            bool enemyAlive = true;


            // This function uses the enemyID to find the Enemy's stats
            FindEnemy(enemyID);


            // announce the beginning of combat
            Console.WriteLine("The " + enemyName + " approaches!");
            Console.WriteLine("COMBAT START!");
            Console.ReadKey();
            Console.Clear();


            // combat loop
            while (enemyAlive == true)
            {
                Console.WriteLine("Your turn!");
                Console.WriteLine("Your Health: " + playerHealth);
                Console.WriteLine("Your Mana: " + playerMana);
                input = PlayerTwoChoices("What type of attack will you do?", "Melee", "Magic");
                if (input == 1)
                {
                    DamageRoll(false, playerAttack, enemyDefense, 0, "attack!");
                }
                else if (input == 2)
                {
                    DamageRoll(false, playerMagic, enemyMagicDefense, 1, "cast a spell!");
                }
                if (enemyHealth <= 0)
                {
                    enemyAlive = false;
                    Console.WriteLine("The " + enemyName + " was defeated!");
                }
                else
                {
                    Console.WriteLine("Enemy Health: " + enemyHealth);
                    Console.ReadKey();
                    Console.Clear();

                    // enemy decides what kind of attack to do
                    if (enemyAttack > enemyMagic && enemyMana > 0)
                    {
                        DamageRoll(true, enemyMagic, playerMagicDefense, 1, "casts a spell!");
                    }
                    else
                    {
                        DamageRoll(true, enemyAttack, playerDefense, 0, "attacks!");
                    }

                    if (playerHealth <= 0)
                    {
                        playerAlive = false;
                        Console.WriteLine("You have been defeated!");
                        Console.WriteLine();
                        Console.WriteLine("GAME OVER");
                        Console.WriteLine("Restart the program to try again!");
                        Console.ReadLine();
                        Environment.Exit(1);
                        return;
                    }
                }
            }
            return;



            void DamageRoll(bool isAttackingPlayer, int attackingStat, int defendingStat, float manaCost, string attackDescription)
            {

                // if the attack targets the player, print "The enemyName" amd then the attack description
                // otherwise, print "You" and then the attack description
                if (isAttackingPlayer == true)
                {
                    Console.WriteLine("The " + enemyName + " " + attackDescription);
                }
                else
                {
                    Console.WriteLine("You " + attackDescription);
                }

                if (manaCost > 0.0f)
                {
                    if (isAttackingPlayer == true)
                    {
                        enemyMana -= manaCost;
                        Console.WriteLine("The " + enemyName + " used " + manaCost + " mana.");
                    }
                    else
                    {
                        if (playerMana >= manaCost)
                        {
                            playerMana -= manaCost;
                            Console.WriteLine("You used " + manaCost + " mana.");
                        }
                        else
                        {
                            Console.WriteLine("You tried to cast a spell, but you didn't have enough mana!");
                            return;
                        }
                    }
                }
                else if (playerMana <= playerMaxMana && isAttackingPlayer == false)
                {
                    playerMana += playerMaxMana / 4;
                    Console.WriteLine("You regenerated some mana!");
                }

                // Attack - defense = damage
                damageDealt = attackingStat - defendingStat;

                // if the damage is 0 or less, the damage is set to 1
                if (damageDealt <= 0)
                {
                    damageDealt = 1;
                }

                // print damage dealt
                if (isAttackingPlayer == true)
                {
                    Console.WriteLine("You take " + damageDealt + " damage!");
                    playerHealth -= damageDealt;
                }
                else
                {
                    Console.WriteLine("The " + enemyName + " takes " + damageDealt + " damage!");
                    enemyHealth -= damageDealt;
                }

                if (playerMana > playerMaxMana)
                {
                    playerMana = playerMaxMana;
                }

                //return
                return;
            }

     



            void FindEnemy(int enemyID)
            {
                /* 
                 * Okay youre gonna hate me for making 7 arguments for the set enemy stats function but look.
                 * I have no idea how else to do it. sorry ):
                 * Anyways, the order is as follows:
                 * string setName, float setHealth, float setMana, int setAttack, int setDefense, int setMagic, int setMagicDefense
                 */

                // If enemyID is 1, the enemy is a Slime.
                if (enemyID == 1)
                {
                    SetEnemyStats("Slime", 10, 0, 2, 0, 2000, 0);
                    return;
                }
                else if (enemyID == 2)
                {
                    SetEnemyStats("Mimic", 15, 0, 5, 5, 0, 0);
                    return;
                }
                else
                {
                    SetEnemyStats("CoolTestEnemy", 100, 10, 1, 1, 1, 1);
                    return;
                }
            }



            void SetEnemyStats(string setName, float setHealth, float setMana, int setAttack, int setDefense, int setMagic, int setMagicDefense)
            {
                enemyName = setName;
                enemyHealth = setHealth;
                enemyMana = setMana;
                enemyAttack = setAttack;
                enemyDefense = setDefense;
                enemyMagic = setMagic;
                enemyMagicDefense = setMagicDefense;
                return;
            }


        }
    }

        }


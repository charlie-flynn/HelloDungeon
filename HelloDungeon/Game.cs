using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HelloDungeon
{
    internal class Game
    {
        bool runDebugCombat = false;


        struct Stats
        {
        public string name = "";
        public string role = "";
        public int exp = 0;
        public int expToLevel = 3;
        public int level = 1;
        public float health = 10.0f;
        public float maxHealth = 10.0f;
        public float mana = 5.0f;
        public float maxMana = 5.0f;
        public int attack = 3;
        public int defense = 3;
        public int magic = 3;
        public int magicDefense = 3;
        public int gold = 3;
        public bool alive = true;

            // player stats
            public Stats
                (
                string name,
                string role,
                int exp,
                int expToLevel,
                int level,
                float health,
                float maxHealth,
                float mana,
                float maxMana,
                int attack,
                int defense,
                int magic,
                int magicDefense,
                int gold
                )
            {
                this.name = name;
                this.role = role;
                this.exp = exp;
                this.expToLevel = expToLevel;
                this.level = level;
                this.health = health;
                this.maxHealth = maxHealth;
                this.mana = mana;
                this.maxMana = maxMana;
                this.attack = attack;
                this.defense = defense;
                this.magic = magic;
                this.magicDefense = magicDefense;
                this.gold = gold;
            }


            // enemy stats
            public Stats
                (
                string name,
                int exp,
                float health,
                float maxHealth,
                float mana,
                float maxMana,
                int attack,
                int defense,
                int magic,
                int magicDefense,
                int gold,
                bool alive
                )
            {
                this.name = name;
                this.exp = exp;
                this.health = health;
                this.maxHealth = maxHealth;
                this.mana = mana;
                this.maxMana = maxMana;
                this.attack = attack;
                this.defense = defense;
                this.magic = magic;
                this.magicDefense = magicDefense;
                this.gold = gold;
                this.alive = alive;
            }
        }

        Stats player = new Stats(name: "",role: "", 0, 3, 1, 10, 10, 3, 3, 4, 3, 4, 3, 3);



        /// <summary>
        /// stuff for rooms, including 4 spots for ids, a general id for enemy rooms, event rooms, etc.
        /// </summary>
        struct Room
        {
            public int id = 0;
            public int type = 0;
            public int item1 = 0;
            public int item2 = 0;
            public int item3 = 0;
            public int item4 = 0;
            public Room
                (
                int id,
                int type,
                int item1,
                int item2,
                int item3,
                int item4
                )
            {
                this.id = id;
                this.type = type;
                this.item1 = item1;
                this.item2 = item2;
                this.item3 = item3;
                this.item4 = item4;
            }
        }
        
        int input = 0;

        public void Run()
        {
            Console.WriteLine("What's your name, adventurer?");
            player.name = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(player.name))
            {
                Console.WriteLine("Sorry, what didya say?");
                player.name = Console.ReadLine();
            }



            Console.Clear();
            Console.WriteLine("Hello, " + player.name + "!");
            Console.WriteLine();
            Console.WriteLine("Welcome to the dungeon!");
            Console.WriteLine();

            
            int input = PlayerChoices("Are you a Wizard or a Warrior?", "Wizard", "Warrior");
            if (input == 1)
            {
                player.role = "Wizard";
                player.magic += 3;
                player.magicDefense += 2;
                player.mana += 2;
                player.maxMana += 2;
            }
            else if (input == 2)
            {
                player.role = "Warrior";
                player.attack += 3;
                player.defense += 2;
                player.maxHealth += 2;
                player.health += 2;

            }

            PrintPlayerStats();
            Console.Clear();

            if (runDebugCombat == true)
            {
            Combat(42);
            }
            

            Console.WriteLine("Gray bricks line the walls of the dungeon,"
                + " and dust and dread permeate the air.");
            Console.WriteLine("A slime is stewing on the ground. It seems to be digesting something.");
            Console.WriteLine("It notices you. It seems to me that you must battle it.");
            Combat(1);

            Console.WriteLine("Now that the slime is gone, you can focus on what's important: choosing a door.");
            input = PlayerChoices("There are two doors on opposite walls from each other. Which do you choose?", "Left", "Right");
            if (input == 1)
            {
                Console.WriteLine("You enter the left door.");
                Console.WriteLine("The scent of dust and dread is far stronger beyond this door.");
                Console.WriteLine("Cracked stone bricks continue to line the walls."
                    + " You feel a dreadful presence somewhere in these halls.");
                Console.WriteLine("Nonetheless, you must perservere. You come across a fork in your path.");
                input = PlayerChoices("One path leads to a strange chest, and the other certainly leads you closer to a Living Skeleton." + 
                    "Which way do you choose?", "Straight", "Left");
                    if (input == 1)
                    {
                        Console.WriteLine("You approach the chest with the same caution you'd give a wild animal.");
                        Console.WriteLine("You see a bit of clear fluid around the lid...");
                        input = PlayerChoices("Are you certain you wish to open this chest?", "Yes", "No");
                            if (input == 1)
                            {
                                Console.WriteLine("Alas, the chest was a Mimic!");
                                Console.WriteLine("That was pretty obvious, though.");
                                Combat(2);

                            }
                            else if (input == 2)
                            {
                                Console.WriteLine("You turn around, then go down the hallway that is now to your right.");
                            }

                    }
                    if (input == 2)
                    {
                        Console.WriteLine("As you turn the corner, you stumble into a small, green slime.");
                        Console.WriteLine("It squelches with an immense rage that has been stewing for years upon years.");
                        Combat(1);
                    }
                }
            else if (input == 2)
            {
                Console.WriteLine("You enter the right door.");
                Console.WriteLine("Dust and dread are replaced by ash and flame. Magma pours around you, but never on you.");
                Console.WriteLine("You feel a general lack of dreadful presences in this direction.");
                Console.WriteLine("Soon enough, you come across a fork in your path.");
                input = PlayerChoices("Straight ahead is a Sphinx, encrusted in magma. To the right is a very tall door." + 
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
        int PlayerChoices(string description, string option1, string option2)
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

        int PlayerChoices(string description, string option1, string option2, string option3)
        {
            string input = "";
            int inputRecieved = 0;

            // while loop to prevent invalid input
            while (inputRecieved != 1 && inputRecieved != 2 && inputRecieved != 3)
            {
                // Print options
                Console.WriteLine(description);
                Console.WriteLine("1: " + option1 + " | 2: " + option2 + " | 3:" + option3);
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
                // if its option 3
                else if (input == "3" || input == option3)
                {
                    // set input recieved to 3
                    inputRecieved = 3;
                }
                // if its none of them, however
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

        // ok i think you get the point now
        int PlayerChoices(string description, string option1, string option2, string option3, string option4)
        {
            string input = "";
            int inputRecieved = 0;

            // while loop to prevent invalid input
            while (inputRecieved != 1 && inputRecieved != 2 && inputRecieved != 3)
            {
                // Print options
                Console.WriteLine(description);
                Console.WriteLine("1: " + option1 + " | 2: " + option2 + " | 3:" + option3 + " | 4:" + option4);
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
                // if its option 3
                else if (input == "3" || input == option3)
                {
                    // set input recieved to 3
                    inputRecieved = 3;
                }
                else if (input == "4" || input == option4)
                {
                    inputRecieved = 4;
                }
                // if its none of them, however
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
            Stats enemy = new Stats("", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, true);


            // This function uses the enemyID to find the Enemy's stats
            FindEnemy(enemyID);


            // announce the beginning of combat
            Console.WriteLine("The " + enemy.name + " approaches!");
            Console.WriteLine("COMBAT START!");
            Console.ReadKey();
            Console.Clear();


            // combat loop
            while (enemy.alive == true)
            {
                Console.WriteLine("Your turn!");
                Console.WriteLine("Your Health: " + player.health + "/" + player.maxHealth);
                Console.WriteLine("Your Mana: " + player.mana + "/" + player.maxMana);
                input = PlayerChoices("What will you do?", "Melee", "Magic");

                // if you pick a melee attack, you do an attack using your attack stat against the enemy's defense stat
                if (input == 1)
                {
                    DamageRoll(false, player.attack, enemy.defense, 0, "attack!");
                }

                // otherwise, you do an attack using your magic stat against the enemy's magic stat
                else if (input == 2)
                {
                    DamageRoll(false, player.magic, enemy.magicDefense, 1, "cast a spell!");
                }

                // check if the enemy has died
                if (enemy.health <= 0)
                {
                    // enemy is not alive, print that fact out
                    enemy.alive = false;
                    Console.WriteLine("The " + enemy.name + " was defeated!");

                    //finally, give experience to the player
                    GivePlayerExp(enemy.exp);
                }

                // if they haven't, print the enemy's health and let them attack
                else
                {
                    Console.WriteLine("Enemy Health: " + enemy.health + "/" + enemy.maxHealth);
                    Console.ReadKey();
                    Console.Clear();

                    // enemy decides what kind of attack to do
                    if (enemy.attack < enemy.magic && enemy.mana > 0)
                    {
                        DamageRoll(true, enemy.magic, player.magicDefense, 1, "casts a spell!");
                    }
                    else
                    {
                        DamageRoll(true, enemy.attack, player.defense, 0, "attacks!");
                    }

                    // if the player is dead, print that out and then end the program
                    if (player.health <= 0)
                    {
                        Console.WriteLine("You have been defeated!");
                        Console.WriteLine();
                        Console.WriteLine("GAME OVER");
                        Environment.Exit(13);
                    }
                }
            }

            // at the end of combat, player health and mana are healed to max
            player.health = player.maxHealth;
            player.mana = player.maxMana;

            return;



            void DamageRoll(bool isAttackingPlayer, int attackingStat, int defendingStat, float manaCost, string attackDescription)
            {
                int damageDealt = 0;

                // if the attack targets the player, print "The enemyName" amd then the attack description
                // otherwise, print "You" and then the attack description
                if (isAttackingPlayer == true)
                {
                    Console.WriteLine("The " + enemy.name + " " + attackDescription);
                }
                else
                {
                    Console.WriteLine("You " + attackDescription);
                }

                // if the attack costs mana, deduct the cost from the mana from whoever is using that attack
                if (manaCost > 0.0f)
                {
                    if (isAttackingPlayer == true)
                    {
                        enemy.mana -= manaCost;
                        Console.WriteLine("The " + enemy.name + " used " + manaCost + " mana.");
                    }
                    else
                    {
                        if (player.mana >= manaCost)
                        {
                            player.mana -= manaCost;
                            Console.WriteLine("You used " + manaCost + " mana.");
                        }
                        else
                        {
                            Console.WriteLine("But it failed! You didn't have enough mana!");
                            if (player.mana > player.maxMana)
                            {
                                player.mana = player.maxMana;
                            }
                            if (player.health > player.maxHealth)
                            {
                                player.health = player.maxHealth;
                            }
                            return;
                        }
                    }
                }

                // if it doesn't cost mana and the player is not at max mana, regenerate some of the player's mana
                else if (player.mana < player.maxMana && isAttackingPlayer == false)
                {
                    player.mana += player.maxMana / 4;
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
                    player.health -= damageDealt;
                }
                else
                {
                    Console.WriteLine("The " + enemy.name + " takes " + damageDealt + " damage!");
                    enemy.health -= damageDealt;
                }

                // if the player's mana is more than their max mana, set their mana to their max mana
                if (player.mana > player.maxMana)
                {
                    player.mana = player.maxMana;
                }

                // same thing here but for health
                if (player.health > player.maxHealth)
                {
                    player.health = player.maxHealth;
                }

                //return
                return;
            }

     



            void FindEnemy(int enemyID)
            {
                /* 
                 * Okay youre gonna hate me for making 8 arguments for the set enemy stats function but look.
                 * I have no idea how else to do it aside from copy-pasting code. sorry ):
                 * Anyways, the order is as follows:
                 * string setName, float setHealth, float setMana, int setAttack, int setDefense, int setMagic, int setMagicDefense, int setExpDrop
                 */

                // If enemyID is 1, the enemy is a Slime.
                if (enemyID == 1)
                {
                    SetEnemyStats("Slime", 10, 0, 2, 0, 2000, 0, 3);
                    return;
                }
                // if it's 2, it is a mimic
                else if (enemyID == 2)
                {
                    SetEnemyStats("Mimic", 15, 0, 10, 5, 0, 0, 10);
                    return;
                }
                else if (enemyID == 3)
                {
                    SetEnemyStats("Molten Sphinx", 25, 4, 5, 5, 10, 1, 13);
                }
                else
                {
                    SetEnemyStats("CoolTestEnemy", 10, 1, 1000, 1, 1, 1, 5);
                    return;
                }
            }



            void SetEnemyStats(string setName, float setHealth, float setMana, int setAttack, int setDefense, int setMagic, int setMagicDefense, int setExpDrop)
            {
                enemy.name = setName;
                enemy.health = setHealth;
                enemy.maxHealth = setHealth;
                enemy.mana = setMana;
                enemy.maxMana = setMana;
                enemy.attack = setAttack;
                enemy.defense = setDefense;
                enemy.magic = setMagic;
                enemy.magicDefense = setMagicDefense;
                enemy.exp = setExpDrop;
                return;
            }


        }
        
        void GivePlayerExp(int expGained)
        {
            int levelsGained = 0;


            // print the amount of exp gained amd then give the player the exp
            Console.WriteLine("You gained +" + expGained + " experience!");
            player.exp += expGained;

            // while your exp is more than the amount needed to level up, level up
            while (player.exp >= player.expToLevel)
            {
                // increment the player's level
                // and also keep track of how many times you do that
                player.level++;
                levelsGained++;

                // print a message for every time you level up and also deduct the exp
                Console.WriteLine("You leveled up to level " + player.level + "!");
                player.exp -= player.expToLevel;

                // and finally, add 5 to the needed exp to level
                player.expToLevel += 5;
            }

            // increase the player's stats based on how many levels were gained and the player's class- er, role
            if (levelsGained > 0)
            {
                Console.ReadKey();
                if (player.role == "Wizard")
                {
                    player.maxHealth += 1 * levelsGained;
                    player.magic += 2 * levelsGained;
                    player.magicDefense += 2 * levelsGained;
                    player.maxMana += 2 * levelsGained;
                    player.attack += 1 * levelsGained;
                    player.defense += 1 * levelsGained;
                }
                else
                {
                    player.maxHealth += 2 * levelsGained;
                    player.magic += 1 * levelsGained;
                    player.magicDefense += 1 * levelsGained;
                    player.maxMana += 1 * levelsGained;
                    player.attack += 2 * levelsGained;
                    player.defense += 2 * levelsGained;
                }

                // set player's health and mana to their new maximum
                player.health = player.maxHealth;
                player.mana = player.maxMana;

                // print the player's stats as they are now
                PrintPlayerStats();

                Console.ReadKey();
                Console.Clear();
            }
        }

        void PrintPlayerStats()
        {
            Console.WriteLine(player.name + " the " + player.role + "'s stats!");
            Console.WriteLine("Level: " + player.level);
            Console.WriteLine("Experience: " + player.exp + "/" + player.expToLevel);
            Console.WriteLine("Health: " + player.health + "/" + player.maxHealth);
            Console.WriteLine("Mana: " + player.mana + "/" + player.maxMana);
            Console.WriteLine("Gold: " + player.gold);
            Console.WriteLine("Attack: " + player.attack);
            Console.WriteLine("Defense: " + player.defense);
            Console.WriteLine("Magic: " + player.magic);
            Console.WriteLine("Magic Defense: " + player.magicDefense);
            Console.ReadKey();
            return;
        }
    }

        }


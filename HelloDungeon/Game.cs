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
    /*
     * HelloDungeon should have:
     *  - Use of variables (gl not doing that)
     *  - Use of functions
     *  - Use of a struct
     *  - Use A function overload
     * 
     * You should also have
     * - Follow proper naming convention
     * - Your project should run with no errors (warnings are okay!)
     * - Your code should be commented to a reasonable standard
     */

    internal class Game
    {

        private enum Riddle
        {
            RIVER_RIDDLE,
            PIANO_RIDDLE,
            DEATH_RIDDLE,
            NINETEEN_RIDDLE,
            SEMICOLON_RIDDLE,
            FRIDAY_RIDDLE,
            PURPLE_RIDDLE,
            CORRECT_RIDDLE
        }
        

        /// <summary>
        /// struct for player stats
        /// </summary>
        struct PlayerStats
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

            public PlayerStats
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
        }

        /// <summary>
        /// struct for enemy stats thats so much cooler
        /// </summary>
        struct Enemy
        {
            public string enemyName;
            public int enemyExpDrop;
            public float enemyHealth;
            public float enemyMaxHealth;
            public float enemyMana;
            public float enemyMaxMana;
            public int enemyAttack;
            public int enemyDefense;
            public int enemyMagic;
            public int enemyMagicDefense;
            public bool enemyIsAlive;
            public Enemy
                (
                string enemyName,
                int enemyExpDrop,
                float enemyHealth,
                float enemyMaxHealth,
                float enemyMana,
                float enemyMaxMana,
                int enemyAttack,
                int enemyDefense,
                int enemyMagic,
                int enemyMagicDefense,
                bool enemyIsAlive
                )
            {
                this.enemyName = enemyName;
                this.enemyExpDrop = enemyExpDrop;
                this.enemyHealth = enemyHealth;
                this.enemyMaxHealth = enemyMaxHealth;
                this.enemyMana = enemyMana;
                this.enemyMaxMana = enemyMaxMana;
                this.enemyAttack = enemyAttack;
                this.enemyDefense = enemyDefense;
                this.enemyMagic = enemyMagic;
                this.enemyMagicDefense = enemyMagicDefense;
                this.enemyIsAlive = enemyIsAlive;
            }
        }

        PlayerStats player = new PlayerStats(name: "", role: "", 0, 3, 1, 10, 10, 3, 3, 4, 3, 4, 3, 3);

        /// <summary>
        /// struct for items
        /// </summary>
        struct Item
        {
            public int itemID;
            public bool isConsumable;
            public string itemName;
            public string itemDescription;
            public Item
                (
                int itemID,
                bool isConsumable,
                string itemName,
                string itemDescription
                )
            {
                this.itemID = itemID;
                this.isConsumable = isConsumable;
                this.itemName = itemName;
                this.itemDescription = itemDescription;
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

            // make the player choose their class and increase their base stats based on what they chose
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

            // here's a list of Every Enemy Ever!

            // order of stats is name, experience it drops, health, max health, mana, max mana, attack, defense, magic attack, magic defense, if its dead
            Enemy slime = new Enemy("Slime", 3, 10, 10, 0, 0, 5, 1, 0, 1, true);
            Enemy shamblingZombie = new Enemy("Shambling Zombie", 10, 20, 20, 0, 0, 8, 5, 0, 8, true);
            Enemy mimic = new Enemy("Mimic", 15, 15, 15, 0, 0, 12, 4, 0, 2, true);
            Enemy moltenSphinx = new Enemy("Molten Sphinx", 25, 25, 25, 2, 2, 4, 3, 14, 3, true);


            // initiate combat with a slime
            Console.WriteLine("Gray bricks line the walls of the dungeon,"
                    + " and dust and dread permeate the air.");
            Console.WriteLine("A slime is stewing on the ground. It seems to be digesting something.");
            Console.WriteLine("It notices you. It seems that you must battle it.");
            Combat(slime);

            // after combat, prompt player to choose between two doors
            // the left one leads them to mimic hallway
            // the right one leads them to sphinx riddles
                Console.WriteLine("Now that the slime is gone, you can focus on what's important: choosing a door.");
                input = PlayerChoices("There are two doors on opposite walls from each other. Which do you choose?", "Left", "Right");
                if (input == 1)
                {
                    Console.WriteLine("You enter the left door.");
                    Console.WriteLine("The scent of dust and dread is far stronger beyond this door.");
                    Console.WriteLine("Cracked stone bricks continue to line the walls."
                        + " You feel a dreadful presence somewhere in these halls.");
                    Console.WriteLine("Nonetheless, you must perservere. You come across a fork in your path.");
                    Console.WriteLine("One path leads to a strange chest, and the other certainly leads you closer to a Living Skeleton.");
                    input = PlayerChoices("Which way do you choose?", "Straight", "Left");
                // mimic hallway choice
                // if the player goes straight, lead them to mimic
                // if they go left, lead them to Shambling Zombie (altho they'll always encounter shambling zombie either way)
                    if (input == 1)
                    {
                        Console.WriteLine("You approach the chest with the same caution you'd give a wild animal.");
                        Console.WriteLine("You see a bit of clear fluid around the lid...");
                        input = PlayerChoices("Are you certain you wish to open this chest?", "Yes", "No");
                        if (input == 1)
                        {
                            Console.WriteLine("Alas, the chest was a Mimic!");
                            Console.WriteLine("That was pretty obvious, though.");
                            
                            Combat(mimic);
                            Console.WriteLine("Lucky for you, the Mimic dropped some treasure!");
                            Console.WriteLine("You got +5 Gold!");
                        // if the player beats the mimic, they get treasure! give em 5 gold :)
                        player.gold += 5;
                            

                            // create a item named enchanted blade with that description, then print the name and description and apply its effects
                            Item enchantedSword = new Item(1, false, "Enchanted Blade", "A sword enchanted by a kinda lame ancient deity long ago."
                                + " Gives +3 Attack, +3 Magic.");
                            GivePlayerItem(enchantedSword);
                            Console.WriteLine("Now that the Mimic is gone...");
                        }
                       Console.WriteLine("You turn around, then go down the hallway that was to your left earlier.");
                    }
                    // after mimic go down shambling zombie hallway, or the player can go directly to it
                    // initiate combat with shambling zombie. afterwards go deeper into the dungeon which doesnt exist
                        Console.WriteLine("As you turn the corner, a Shambling Zombie shambles towards you.");
                        Console.WriteLine("It shambles, in a menacing fashion.");
                        Combat(shamblingZombie);
                        Console.WriteLine("Now that the zombie shambles no more, you continue down the hall.");
                }
                else if (input == 2)
                {
                // sphinx choice
                // if you go straight, answer the sphinx's riddles
                // if you take a right, you must fight the Shambling Zombie
                    Console.WriteLine("You enter the right door.");
                    Console.WriteLine("Dust and dread are replaced by ash and flame. Magma pours around you, but never on you.");
                    Console.WriteLine("You feel a general lack of dreadful presences in this direction.");
                    Console.WriteLine("Soon enough, you come across a fork in your path.");
                    input = PlayerChoices("Straight ahead is a Sphinx, encrusted in magma. To the right is a very tall door." +
                        " Which way do you go?", "Straight", "Right");
                    if (input == 1)
                    {
                        Console.WriteLine("You approach the molten Sphinx with great caution.");
                        Console.WriteLine("The Sphinx immediately fixes its gaze upon you.");
                        Console.WriteLine("''HUMAN,'' It bellows. ''TO PASS THROUGH YOU MUST ANSWER MY RIDDLES.''");
                        Console.WriteLine("''IF YOU FAIL, YOU SHALL PERISH.''");
                        Console.ReadKey();
                        Console.Clear();
                        SphinxRiddles();
                        Console.WriteLine("Now that you've taken care of the sphinx, you can move deeper into the dungeon.");
                        
                    }
                    else if (input == 2)
                    {
                    // shambling zombie combat
                        Console.WriteLine("The moment you touch the doorknob, the door falls over like a domino away from you.");
                        Console.WriteLine("Around the corner of the hallway was a Shambling Zombie.");
                        Console.WriteLine("It shambles towards you in a menacing fashion.");

                        Combat(shamblingZombie);

                    // afterwards, initiate sphinx riddles after giving the player clues to a lever puzzle that doesnt exist
                        Console.WriteLine("Now that the Shambling Zombie has been taken care of, you proceed down the hall.");
                        Console.WriteLine("The hallway leads to a dead end, but in that dead end is a message.");
                        Console.WriteLine("''DOWN, UP, DOWN''");
                        Console.WriteLine("Strange... Anyways, you turn around and go back down the hall, to approach the Molten Sphinx.");

                        Console.ReadLine();
                        Console.Clear();

                        Console.WriteLine("The moment you exit the hallway, the Sphinx fixes its gaze upon you.");
                        Console.WriteLine("''HUMAN,'' It bellows. ''TO PASS THROUGH YOU MUST ANSWER MY RIDDLES.''");
                        Console.WriteLine("''IF YOU FAIL, YOU SHALL PERISH.''");
                        SphinxRiddles();
                        Console.WriteLine("Now that you've taken care of the sphinx, you can move deeper into the dungeon.");
                    }

                }
                void SphinxRiddles()
                {
                    // generate a random sequence of riddles
                    // if any of the riddles are the same, try again!
                    Riddle riddle1 = (Riddle)RandomNumberGenerator.GetInt32(1, 9);
                    Riddle riddle2 = (Riddle)RandomNumberGenerator.GetInt32(1, 9);
                    Riddle riddle3 = (Riddle)RandomNumberGenerator.GetInt32(1, 9);
                    int riddleCompletionTracker = 1;
                    bool riddleFailed = false;
                    Riddle riddleID = 0;
                    while (riddle1 == riddle2 || riddle2 == riddle3 || riddle3 == riddle1)
                    {
                        riddle1 = (Riddle)RandomNumberGenerator.GetInt32(1, 9);
                        riddle2 = (Riddle)RandomNumberGenerator.GetInt32(1, 9);
                        riddle3 = (Riddle)RandomNumberGenerator.GetInt32(1, 9);
                    }
                    // while you still have riddles left to complete or until you fail, give the player riddles
                    while (riddleCompletionTracker < 4 && riddleFailed == false)
                    {
                        riddleFailed = PrintRiddle(riddleCompletionTracker);
                    }

                    // if you failed the riddle, you engage in combat with the sphinx. but if you pass, you gain exp for your epic riddle skillz
                    if (riddleFailed == true)
                    {
                        Console.WriteLine("The Molten Sphinx looks at you, its petrifying gaze piercing into your mind.");
                        Console.WriteLine("''YOU HAVE FAILED, HUMAN. FOR THAT, YOU MUST PERISH.''");
                        Console.ReadKey();

                        Combat(moltenSphinx);
                    }
                    else
                    {
                        Console.WriteLine("The Molten Sphinx looks at you, its gaze not one of malice, but of pride.");
                        Console.WriteLine("''YOU HAVE PASSED, HUMAN. FOR THAT, YOU SHALL RECEIVE MY GIFT OF KNOWLEDGE''");
                        Console.ReadKey();
                        GivePlayerExp(25);
                    }



                    
                    bool PrintRiddle(int riddleNumber)
                    {

                        // if riddle number is 1, load up riddle1. if it's 2, load up riddle2. if it's 3, riddle3.
                        if (riddleNumber == 1)
                        {
                            riddleID = riddle1;
                        }
                        else if (riddleNumber == 2)
                        {
                            riddleID = riddle2;
                        }
                        else if (riddleNumber == 3)
                        {
                            riddleID = riddle3;
                        }



                    // checks the riddle id for the riddle that must be given
                    // i.e. if the riddle id was 4 itd load up riddle #4
                    // if the player gets the riddle right, increment their progress
                    switch (riddleID)
                    {
                        case Riddle.RIVER_RIDDLE:
                            input = PlayerChoices("''I HAVE A BANK BUT NO MONEY, AND A CHANNEL BUT NO TELEVISION. WHAT AM I?''",
                                "TV", "Crab", "River", "Tears");
                            if (input == 3)
                            {
                                Console.WriteLine("''CORRECT.''");
                                Console.WriteLine("''THE RIVER HAS A BANK AND A CHANNEL, BUT NO MONEY OR TELEVISION.''");
                                riddleCompletionTracker++;
                                return false;
                            }
                            else
                            {
                                Console.WriteLine("''INCORRECT.''");
                                Console.WriteLine("''THE ANSWER WAS THE RIVER, FOR IT HAS A BANK AND A CHANNEL, BUT NO MONEY OR TELEVISION.''");
                                return true;
                            }
                            break;
                        case Riddle.PIANO_RIDDLE:
                            input = PlayerChoices("''I HAVE DOZENS OF KEYS, BUT I CANNOT OPEN ANY LOCKS. WHAT AM I?''",
                                "Piano", "Tablet", "Keyring", "Ant");
                            if (input == 1)
                            {
                                Console.WriteLine("''CORRECT.''");
                                Console.WriteLine("''THE PIANO HAS MANY, MANY KEYS, BUT CAN NEVER OPEN A LOCK.''");
                                riddleCompletionTracker++;
                                return false;
                            }
                            else
                            {
                                Console.WriteLine("''INCORRECT.''");
                                Console.WriteLine("''THE ANSWER WAS THE PIANO, FOR DESPITE ITS MANY, MANY KEYS, IT CAN NEVER OPEN A LOCK.''");
                                return true;
                            }
                            break;
                        case Riddle.DEATH_RIDDLE:
                            input = PlayerChoices("''ALL MOURN WHEN I HAPPEN TO OTHERS, BUT WHEN I HAPPEN TO YOU, YOU WON'T BAT AN EYE. WHAT AM I?''",
                                "Housefire", "Death", "Wolves Invasion", "Big Ant");
                            if (input == 2)
                            {
                                Console.WriteLine("''CORRECT.''");
                                Console.WriteLine("''THE MOMENT YOU DIE, YOU WILL NEVER BAT AN EYE AGAIN.''");
                                riddleCompletionTracker++;
                                return false;
                            }
                            else
                            {
                                Console.WriteLine("''INCORRECT.''");
                                Console.WriteLine("''THE ANSWER WAS DEATH, FOR THE MOMENT YOU DIE, YOU WILL NEVER BAT AN EYE AGAIN.''");
                                return true;
                            }
                            break;
                        case Riddle.NINETEEN_RIDDLE:
                            input = PlayerChoices("''WHAT'S 9+10?''",
                                "19", "21", "12", "14");
                            if (input == 1)
                            {
                                Console.WriteLine("''CORRECT.''");
                                Console.WriteLine("''NINE PLUS TEN EQUALS NINETEEN.''");
                                riddleCompletionTracker++;
                                return false;
                            }
                            else if (input == 2)
                            {
                                Console.WriteLine("''YOU STUPID.''");
                                return true;
                            }
                            else
                            {
                                Console.WriteLine("''INCORRECT.''");
                                Console.WriteLine("''THE CORRECT ANSWER WAS NINETEEN, FOR NINE PLUS TEN EQUALS NINETEEN.''");
                                return true;
                            }
                            break;
                        case Riddle.SEMICOLON_RIDDLE:
                            input = PlayerChoices("''I AM AN ALL-IMPORTANT PIECE OF THIS WORLD, SYMBOLIZING AN END OF THE LINE. WHAT AM I?''",
                                "Skull", "GAME OVER", "Exclamation Point", "Semicolon");
                            if (input == 4)
                            {
                                Console.WriteLine("''CORRECT.''");
                                Console.WriteLine("''THE SEMICOLON SYMBOLIZES THE END OF A LINE OF CODE.''");
                                riddleCompletionTracker++;
                                return false;
                            }
                            else
                            {
                                Console.WriteLine("''INCORRECT.''");
                                Console.WriteLine("''THE ANSWER WAS THE SEMICOLON, FOR IT SYMBOLIZES THE END OF A LINE OF CODE.''");
                                return true;
                            }
                            break;
                        case Riddle.FRIDAY_RIDDLE:
                            input = PlayerChoices("''A KNIGHT RIDES INTO A TOWN ON FRIDAY. HE STAYS THREE DAYS AND THREE NIGHTS, AND LEAVES ON FRIDAY." +
                                " HOW IS THAT POSSIBLE?''",
                                "Time Paradox", "Horse's Name", "Three Knights", "Knight's Name");
                            if (input == 2)
                            {
                                Console.WriteLine("''CORRECT.''");
                                Console.WriteLine("''THE HORSE'S NAME WAS FRIDAY.''");
                                riddleCompletionTracker++;
                                return false;
                            }
                            else
                            {
                                Console.WriteLine("''INCORRECT.''");
                                Console.WriteLine("''THE ANSWER WAS THE HORSE'S NAME, FOR THE HORSE WAS NAMED FRIDAY''");
                                return true;
                            }
                            break;
                        case Riddle.PURPLE_RIDDLE:
                            input = PlayerChoices("''WHAT IS MY FAVORITE COLOR?''",
                                "Red", "Orange", "Green", "Purple");
                            if (input == 4)
                            {
                                Console.WriteLine("''CORRECT.''");
                                Console.WriteLine("''I REALLY LIKE PURPLE. :)''");
                                riddleCompletionTracker++;
                                return false;
                            }
                            else
                            {
                                Console.WriteLine("''INCORRECT.''");
                                Console.WriteLine("''THE ANSWER WAS PURPLE, BECAUSE I REALLY LIKE PURPLE. :)''");
                                return true;
                            }
                            break;
                        case Riddle.CORRECT_RIDDLE:
                            input = PlayerChoices("''WHICH IS THE CORRECT ANSWER?''", "Correct Answer", "Incorrect Answer", "Wrong Answer", "Not the Right Answer");
                            if (input == 1)
                            {
                                Console.WriteLine("''CORRECT.''");
                                Console.WriteLine("''THAT'S THE RIGHT ANSWER''");
                                riddleCompletionTracker++;
                                return false;
                            }
                            else
                            {
                                Console.WriteLine("''INCORRECT.''");
                                Console.WriteLine("''THE ANSWER WAS THE CORRECT ANSWER, IT IS THE RIGHT ANSWER.''");
                                return true;
                            }
                            break;
                        default:
                            return false;
                            break;
                    }
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
                // Print options and get input from the player
                Console.WriteLine(description);
                Console.WriteLine("1: " + option1 + " | 2: " + option2);
                Console.Write("> ");
                input = Console.ReadLine();

                // if the player inputted a number or the name of an option, set inputrecieved to said option
                if (input == "1" || input == option1)
                {
                    inputRecieved = 1;
                }
                else if (input == "2" || input == option2)
                {
                    inputRecieved = 2;
                }
                // if the input was not a valid option, display an error
                else
                {
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
                // Print options and get input from the player
                Console.WriteLine(description);
                Console.WriteLine("1: " + option1 + " | 2: " + option2 + " | 3: " + option3);
                Console.Write("> ");
                input = Console.ReadLine();

                // if player selected a valid option, set inputrecieved to said option
                if (input == "1" || input == option1)
                {
                    inputRecieved = 1;
                }
                else if (input == "2" || input == option2)
                {
                    inputRecieved = 2;
                }
                else if (input == "3" || input == option3)
                {
                    inputRecieved = 3;
                }
                else
                {
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
            while (inputRecieved != 1 && inputRecieved != 2 && inputRecieved != 3 && inputRecieved != 4)
            {
                // Print options, then get the player's input
                Console.WriteLine(description);
                Console.WriteLine("1: " + option1 + " | 2: " + option2 + " | 3: " + option3 + " | 4: " + option4);
                Console.Write("> ");
                input = Console.ReadLine();

                // if player inputted 1, set inputrecieved to 1. if player inputted 2, set input recieved to 2, etc. etc.
                if (input == "1" || input == option1)
                {
                    inputRecieved = 1;
                }
                else if (input == "2" || input == option2)
                {
                    inputRecieved = 2;
                }
                else if (input == "3" || input == option3)
                {
                    inputRecieved = 3;
                }
                else if (input == "4" || input == option4)
                {
                    inputRecieved = 4;
                }
                else
                {
                    // if they didnt input any of the options, print an error
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey();
                }
            }
            Console.Clear();
            return inputRecieved;
        }



        void Combat(Enemy enemy)
        {
            // announce the beginning of combat
            Console.WriteLine("The " + enemy.enemyName + " approaches!");
            Console.WriteLine("COMBAT START!");
            Console.ReadKey();
            Console.Clear();


            // combat loop
            while (enemy.enemyIsAlive == true)
            {
                Console.WriteLine("Your turn!");
                Console.WriteLine("Your Health: " + player.health + "/" + player.maxHealth);
                Console.WriteLine("Your Mana: " + player.mana + "/" + player.maxMana);
                input = PlayerChoices("What will you do?", "Melee", "Magic");

                // if you pick a melee attack, you do an attack using your attack stat against the enemy's defense stat
                // otherwise, you do an attack using your magic stat against the enemy's magic defense stat
                if (input == 1)
                {
                    DamageRoll(false, player.attack, enemy.enemyDefense, 0, "attack!");
                }
                else if (input == 2)
                {
                    DamageRoll(false, player.magic, enemy.enemyMagicDefense, 1, "cast a spell!");
                }

                // if the enemy's health is less than or equal to 0, print out the fact that they have died and give the player exp
                if (enemy.enemyHealth <= 0)
                {
                    enemy.enemyIsAlive = false;
                    Console.WriteLine("The " + enemy.enemyName + " was defeated!");

                    GivePlayerExp(enemy.enemyExpDrop);
                }

                // if they haven't died, print the enemy's health and let them attack
                else
                {
                    Console.WriteLine("Enemy Health: " + enemy.enemyHealth + "/" + enemy.enemyMaxHealth);
                    Console.ReadKey();
                    Console.Clear();

                    if (enemy.enemyAttack < enemy.enemyMagic && enemy.enemyMana > 0)
                    {
                        DamageRoll(true, enemy.enemyMagic, player.magicDefense, 1, "casts a spell!");
                    }
                    else
                    {
                        DamageRoll(true, enemy.enemyAttack, player.defense, 0, "attacks!");
                    }

                    // if the player is dead, print that out and then end the program
                    if (player.health <= 0)
                    {
                        Console.WriteLine("You have been defeated!");
                        Console.WriteLine();
                        Console.WriteLine("GAME OVER");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
            }

            // at the end of combat, player health and mana are healed to max
            player.health = player.maxHealth;
            player.mana = player.maxMana;

            return;


            
            void DamageRoll(bool isAttackingPlayer, int attackingStat, int defendingStat, float manaCost, string attackDescription)
            {
                float damageCalculated = 0.0f;
                int damageDealt = 0;
                int damageRoll = 0;
                int critRoll = 0;
                float damageMult = 0;

                // if the attack targets the player, print "The enemyName" amd then the attack description
                // otherwise, print "You" and then the attack description
                if (isAttackingPlayer == true)
                {
                    Console.WriteLine("The " + enemy.enemyName + " " + attackDescription);
                }
                else
                {
                    Console.WriteLine("You " + attackDescription);
                }

                // if the attack costs mana, deduct the cost from the mana from whoever is using that attack
                // if the player didnt have enough mana, print that out and dont do that.
                if (manaCost > 0.0f)
                {
                    if (isAttackingPlayer == true)
                    {
                        enemy.enemyMana -= manaCost;
                        Console.WriteLine("The " + enemy.enemyName + " used " + manaCost + " mana.");
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

                // if it doesn't cost mana and the player is not at max mana, regenerate a fourth of the player's mana
                else if (player.mana < player.maxMana && isAttackingPlayer == false)
                {
                    player.mana += player.maxMana / 4;
                    Console.WriteLine("You regenerated some mana!");
                }

                // Attack - defense = damage, with a minimum of 1
                damageCalculated = attackingStat - defendingStat;
                if (damageCalculated <= 0)
                {
                    damageCalculated = 1;
                }


                // if you land a 1 in 12 chance, damage is tripled instead of a different damage roll and also says somethin about it
                critRoll = RandomNumberGenerator.GetInt32(1, 13);
                if (critRoll == 12)
                {
                    Console.WriteLine("A critical hit!");
                    damageCalculated *= 3;
                }
                else
                {
                    // roll a d5, add 2 to the result and then divide it by 5, to get a damage multiplier with a minimum of 0.6, and a max of 1.4 
                    damageRoll = RandomNumberGenerator.GetInt32(1, 5);
                    damageMult = damageRoll;
                    damageMult += 2;
                    damageMult /= 5;
                    damageCalculated *= damageMult;
                }
                

                // truncate the damage by turning it into an integer
                damageDealt = (int)damageCalculated;





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
                    Console.WriteLine("The " + enemy.enemyName + " takes " + damageDealt + " damage!");
                    enemy.enemyHealth -= damageDealt;
                }

                // if the player's mana and/or health is more than their max mana/health, set their mana/health to their max mana/health
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
        
        void GivePlayerExp(int expGained)
        {
            int levelsGained = 0;


            // print the amount of exp gained amd then give the player the exp
            Console.WriteLine("You gained +" + expGained + " experience!");
            player.exp += expGained;

            // while your exp is more than or equal to the amount needed to level up, level up
            // and print out a message for each time you level up
            while (player.exp >= player.expToLevel)
            {
                player.level++;
                levelsGained++;
                Console.WriteLine("You leveled up to level " + player.level + "!");
                player.exp -= player.expToLevel;
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

                // set player's health and mana to their new maximum, then print out their new stats
                player.health = player.maxHealth;
                player.mana = player.maxMana;
                PrintPlayerStats();
                Console.Clear();
            }
        }

        void PrintPlayerStats()
        {
            // yeah this does what you'd expect it to
            // that being printing the player's stats
            Console.WriteLine(player.name + " the " + player.role + "'s stats!");
            Console.WriteLine("Level:         " + player.level);
            Console.WriteLine("Experience:    " + player.exp + "/" + player.expToLevel);
            Console.WriteLine("Health:        " + player.health + "/" + player.maxHealth);
            Console.WriteLine("Mana:          " + player.mana + "/" + player.maxMana);
            Console.WriteLine("Gold:          " + player.gold);
            Console.WriteLine("Attack:        " + player.attack);
            Console.WriteLine("Defense:       " + player.defense);
            Console.WriteLine("Magic:         " + player.magic);
            Console.WriteLine("Magic Defense: " + player.magicDefense);
            Console.ReadKey();
            return;
        }



        void GivePlayerItem(Item item)
        {

            // print the item name, then item description
            Console.WriteLine("You got the " + item.itemName + "!");
            Console.WriteLine("Description: " + item.itemDescription);

            // apply the item's effects
            ApplyItemEffect(item.itemID, item.isConsumable);
            Console.ReadKey();
            Console.Clear();
            
            // if the item is not consumable, print the player's stats cuz they probably got changed
            if (item.isConsumable == false)
            {
                PrintPlayerStats();
                Console.Clear();
            }
            
            // there would be an inventory system but i forgot to actually implement it ngl
            // eh, doesnt matter too much cuz i never added any consumable items

            return;
        }

        void ApplyItemEffect(int itemID, bool isConsumable)
        {
            // otherwise, comb through all the item ids to find the right match for the effect
            // there's only one right now though. but if there was more items there would be more here
            if (itemID == 1)
            {
                player.attack += 3;
                player.magic += 3;
                return;
            }
            return;
        }
    }

        }


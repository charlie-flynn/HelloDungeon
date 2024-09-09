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

        Stats player = new Stats(name: "", role: "", 0, 3, 1, 10, 10, 3, 3, 4, 3, 4, 3, 3);



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
                Console.WriteLine("It notices you. It seems that you must battle it.");
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
                    Console.WriteLine("One path leads to a strange chest, and the other certainly leads you closer to a Living Skeleton.");
                    input = PlayerChoices("Which way do you choose?", "Straight", "Left");
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
                            // change the input to 2 to let the player into the hallway from earlier
                            input = 2;
                            Console.WriteLine("Lucky for you, the Mimic dropped some treasure!");
                            Console.WriteLine("You got +5 Gold!");
                            player.gold += 5;


                            // create a non-consumable item named enchanted blade with that description
                            Item enchantedSword = new Item(1, false, "Enchanted Blade", "A sword enchanted by a kinda lame ancient deity long ago."
                                + " Gives +3 Attack, +3 Magic.");

                            // then print the name and description, and apply its effects.
                            GivePlayerItem(enchantedSword.itemID, enchantedSword.isConsumable, enchantedSword.itemName, enchantedSword.itemDescription);
                        }
                        if (input == 2)
                        {
                            Console.WriteLine("You turn around, then go down the hallway that was to your left earlier.");
                        }

                    }
                    if (input == 2)
                    {
                        Console.WriteLine("As you turn the corner, a Shambling Zombie shambles towards you.");
                        Console.WriteLine("It shambles, in a menacing fashion.");
                        Combat(4);
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
                        Console.WriteLine("''IF YOU FAIL, YOU SHALL PERISH.''");
                        Console.ReadKey();
                        Console.Clear();
                        SphinxRiddles();
                    }
                    else if (input == 2)
                    {
                        Console.WriteLine("The moment you touch the doorknob, the door falls over like a domino away from you.");
                        Console.WriteLine("Around the corner of the hallway was a Shambling Zombie.");
                        Console.WriteLine("It shambles towards you in a menacing fashion.");
                        Combat(4);
                    }

                }
                void SphinxRiddles()
                {

                    // generate a random sequence of riddles
                    int riddle1 = RandomNumberGenerator.GetInt32(1, 9);
                    int riddle2 = RandomNumberGenerator.GetInt32(1, 9);
                    int riddle3 = RandomNumberGenerator.GetInt32(1, 9);
                    int riddleCompletionTracker = 1;
                    bool riddleFailed = false;
                    int riddleID = 0;

                    // if any of the riddles are the same, try again!
                    while (riddle1 == riddle2 || riddle2 == riddle3 || riddle3 == riddle1)
                    {
                        riddle1 = RandomNumberGenerator.GetInt32(1, 9);
                        riddle2 = RandomNumberGenerator.GetInt32(1, 9);
                        riddle3 = RandomNumberGenerator.GetInt32(1, 9);
                    }
                    // while you still have riddles left to complete or until you fail, give the player a riddle
                    while (riddleCompletionTracker < 4 && riddleFailed == false)
                    {
                        riddleFailed = PrintRiddle(riddleCompletionTracker);
                    }

                    // if you failed the riddle, you engage in combat with the sphinx
                    if (riddleFailed == true)
                    {
                        Console.WriteLine("The Molten Sphinx looks at you, its petrifying gaze piercing into your mind.");
                        Console.WriteLine("''YOU HAVE FAILED, HUMAN. FOR THAT, YOU MUST PERISH.''");
                        Console.ReadKey();
                        Combat(3);
                    }
                    // if you passed, you get experience for your swag money riddle skills
                    else
                    {
                        Console.WriteLine("The Molten Sphinx looks at you, its gaze not one of malice, but of pride.");
                        Console.WriteLine("''YOU HAVE PASSED, HUMAN. FOR THAT, YOU SHALL RECEIVE MY GIFT OF KNOWLEDGE''");
                        Console.ReadKey();
                        GivePlayerExp(25);
                    }




                    bool PrintRiddle(int riddleNumber)
                    {

                        // if riddle number is 1, load up riddle1. if it's 2, load up riddle2. if it's 3, riddle3
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



                        // checks the riddle id for the riddle you want to be given
                        // i.e. if the riddle id was 4 itd load up riddle #4
                        if (riddleID == 1)
                        {
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
                        }
                        else if (riddleID == 2)
                        {
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
                        }
                        else if (riddleID == 3)
                        {
                            input = PlayerChoices("''ALL MOURN WHEN IT HAPPENS TO OTHERS, BUT WHEN IT HAPPENS TO YOU, YOU WON'T BAT AN EYE. WHAT AM I?''",
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

                        }
                        else if (riddleID == 4)
                        {
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
                        }
                        else if (riddleID == 5)
                        {
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
                        }
                        else if (riddleID == 6)
                        {
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
                        }
                        else if (riddleID == 7)
                        {
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
                        }
                        else if (riddleID == 8)
                        {
                            input = PlayerChoices("''PLACEHOLDER RIDDLE''", "Correct Answer", "Incorrect Answer", "Wrong Answer", "Not the Right Answer");
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
                        }
                        return false;
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
                Console.WriteLine("1: " + option1 + " | 2: " + option2 + " | 3: " + option3);
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
            while (inputRecieved != 1 && inputRecieved != 2 && inputRecieved != 3 && inputRecieved != 4)
            {
                // Print options
                Console.WriteLine(description);
                Console.WriteLine("1: " + option1 + " | 2: " + option2 + " | 3: " + option3 + " | 4: " + option4);
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
                // if it's option 4
                else if (input == "4" || input == option4)
                {
                    // set input to 4
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



        /// <summary>
        /// The combat loop of the game, which includes multiple functions within it for damage rolls, getting the enemy's stats, etc.
        /// </summary>
        /// <param name="enemyID"></param>
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

                // if they haven't died, print the enemy's health and let them attack
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
                float damageCalculated = 0.0f;
                int damageDealt = 0;
                int damageRoll = 0;
                int critRoll = 0;
                float damageMult = 0;

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
                // roll a d5 to decide the damage multiplier
                damageRoll = RandomNumberGenerator.GetInt32(1, 5);

                // add 2 and then divide it by 5, giving a minimum of 0.6, and a max of 1.4
                damageMult = damageRoll;
                damageMult += 2;
                damageMult /= 5;

                // multiply the damage calculated by the damage roll
                damageCalculated *= damageMult;
                }
                

                // convert that into an integer, thus rounding it down
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
                    SetEnemyStats("Slime", 10, 0, 4, 0, 2000, 0, 3);
                    return;
                }
                // if it's 2, it is a mimic
                else if (enemyID == 2)
                {
                    SetEnemyStats("Mimic", 20, 0, 10, 5, 0, 0, 10);
                    return;
                }
                // so on and so forth
                else if (enemyID == 3)
                {
                    SetEnemyStats("Molten Sphinx", 25, 4, 5, 5, 10, 1, 25);
                }
                else if (enemyID == 4)
                {
                    SetEnemyStats("Shambling Zombie", 25, 0, 10, 4, 0, 3, 8);
                }
                else
                {
                    SetEnemyStats("CoolTestEnemy", 1000, 1, 1, 1, 1, 1, 5);
                    return;
                }
            }



            void SetEnemyStats(string setName, float setHealth, float setMana, int setAttack, int setDefense, int setMagic, int setMagicDefense, int setExpDrop)
            {

                // set alllll of da enemy's stats
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

                // and then return
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
                Console.Clear();
            }
        }

        void PrintPlayerStats()
        {
            // yeah this does what you'd expect it to
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



        void GivePlayerItem(int itemID, bool isConsumable, string itemName, string itemDescription)
        {

            // print the item name, then item description
            Console.WriteLine("You got the " + itemName + "!");
            Console.WriteLine("Description: " + itemDescription);

            // apply the item's effects, if it isn't consumable
            ApplyItemEffect(itemID, isConsumable);
            Console.ReadKey();
            Console.Clear();
            
            // if the item is not consumable, print the player's stats
            if (isConsumable == false)
            {
                PrintPlayerStats();
                Console.Clear();
            }
            


            return;
        }

        void ApplyItemEffect(int itemID, bool isConsumable)
        {

            // if the item is consumable, add it to your inventory
            if (isConsumable == true)
            {

            }

            // otherwise, comb through all the item ids to find the right match for the effect
            else if (itemID == 1)
            {
                player.attack += 3;
                player.magic += 3;
                return;
            }
            return;
        }
    }

        }


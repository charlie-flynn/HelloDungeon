using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HelloDungeon
{
    internal class Game
    {



        public void Run()
        {
            Console.WriteLine("What's your name, adventurer?");
            string? playerName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(playerName))
            {
                Console.WriteLine("Sorry, what didya say?");
                playerName = Console.ReadLine();
            }

            GameCombat gameCombat = new GameCombat();

            float playerHealth = 10.0f;
            float playerMana = 5.0f;
            int playerGold = 3;
            string playerAlignment = "Neutral";
            float playerProximityToNearestLivingSkeleton = 20.0f;
            bool playerAlive = true;
            string playerRole = "";





            Console.WriteLine("Hello, " + playerName + "!");
            Console.WriteLine();
            Console.WriteLine("Welcome to the dungeon!");
            Console.WriteLine();
            Console.WriteLine("Health: " + playerHealth);
            Console.WriteLine("Mana: " + playerMana);
            Console.WriteLine("Gold: " + playerGold);
            Console.WriteLine("Alignment: " + playerAlignment);
            Console.WriteLine("Alive?: " + playerAlive);
            Console.WriteLine("Proximity to Nearest Living Skeleton: " + playerProximityToNearestLivingSkeleton + " meters");
            Console.WriteLine();
            Console.WriteLine("Are you a Wizard, or a Warrior?");


            int input = PlayerTwoChoices("Are you a warrior or a wizard?", "Warrior", "Wizard");
            if (input == 1)
            {
                playerRole = "Warrior";
            }
            else if (input == 2)
            {
                playerRole = "Wizard";
            }
            Console.WriteLine("Player Role: " + playerRole);

            Console.WriteLine("Gray bricks line the walls of the dungeon,"
                + " and dust and dread permeate the air.");

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
                input = PlayerTwoChoices("One path leads to a strange chest, and the other certainly leads you closer to a Living Skeleton. Which way do you choose?", "Straight", "Left");
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
                                gameCombat.RunCombat();

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
                        Console.WriteLine("The Slime leaps toward you!");
                        Console.WriteLine("COMBAT START!");
                        gameCombat.RunCombat();

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
                input = PlayerTwoChoices("Straight ahead is a Sphinx, encrusted in magma. To the right is a very tall door. Which way do you go?", "Straight", "Right");
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
                        Console.WriteLine("The Slime leaps towards you!");
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
    }

        }


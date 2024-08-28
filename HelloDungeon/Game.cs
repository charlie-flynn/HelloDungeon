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
            

            string? playerChoice = "";
            while (playerChoice != "1" && playerChoice != "2")
            {
                Console.WriteLine("1: Wizard | 2: Warrior");
                playerChoice = Console.ReadLine();
                if (playerChoice == "1")
                {
                    playerRole = "Wizard";
                }
                else
                {
                    playerRole = "Warrior";
                }
            }
            Console.WriteLine("Player Role: " + playerRole);

            Console.WriteLine("Gray bricks line the walls of the dungeon,"
                + " and dust and dread permeate the air.");
            Console.WriteLine("Two doors stand side by side on the wall in front of you."
            + " Which do you choose?");
            Console.WriteLine();

            playerChoice = "fun cool placeholder message";
            // While loop to prevent the player from making an invalid choice
            while (playerChoice != "1" && playerChoice != "2")
            {
            Console.WriteLine("1: Left | 2: Right");
            playerChoice = Console.ReadLine();
            }
            if (playerChoice == "1")
            {
                Console.WriteLine("You enter the left door.");
                Console.WriteLine("The scent of dust and dread is far stronger beyond this door.");
                Console.WriteLine("Cracked stone bricks continue to line the walls."
                    + "You feel a dreadful presence wandering these halls.");
                playerProximityToNearestLivingSkeleton -= 6;
                Console.WriteLine();
                Console.WriteLine("Proximity to Nearest Living Skeleton: " + playerProximityToNearestLivingSkeleton + " meters (-6)");
                Console.WriteLine();
                Console.WriteLine("Nonetheless, you must perservere. You come across a fork in your path.");
                Console.WriteLine("Continuing straight will lead you to what appears to be a treasure chest.");
                Console.WriteLine("But taking a left will undoubtedly lead you closer to a Living Skeleton.");
                Console.WriteLine("Which way do you choose?");
                playerChoice = "e";
                while (playerChoice != "1" && playerChoice != "2")
                {
                    Console.WriteLine("1: Straight | 2: Left");
                    playerChoice = Console.ReadLine();
                    if (playerChoice == "1")
                    {
                        Console.WriteLine("You approach the chest with the same caution you'd give a wild animal.");
                        Console.WriteLine("You see a bit of clear fluid around the lid...");
                        Console.WriteLine("Are you certain you wish to open this?");
                        playerChoice = "4";
                        while (playerChoice != "1" && playerChoice != "2")
                        {
                            Console.WriteLine("1: Yes, open it | 2: No, go down the other hall");
                            playerChoice = Console.ReadLine();
                            if (playerChoice == "1")
                            {
                                Console.WriteLine("Alas, the chest was a Mimic!");
                                Console.WriteLine("That was pretty obvious, though.");
                                Console.WriteLine("The Mimic lunges at you!");
                                Console.WriteLine("COMBAT START!");
                                gameCombat.RunCombat();

                                // we're gonna learn how to make our own functions eventually
                                // that's when i figure out the combat

                            }
                            else if (playerChoice == "2")
                            {
                                Console.WriteLine("You turn around, then go down the hallway that is now to your right.");
                            }
                        }

                    }
                    else if (playerChoice == "2")
                    {
                        Console.WriteLine("");
                    }
                }
            }
            else if (playerChoice == "2")
            {
                Console.WriteLine("You enter the right door.");
                Console.WriteLine("Dust and dread are replaced by ash and flame. Magma pours around you, but never on you.");
                Console.WriteLine("You feel a general lack of dreadful presences in this direction.");
                playerProximityToNearestLivingSkeleton += 6;
                Console.WriteLine();
                Console.WriteLine("Proximity to Nearest Living Skeleton: " + playerProximityToNearestLivingSkeleton + " meters (+6)");
                Console.WriteLine();
                Console.WriteLine("Soon enough, you come across a fork in your path.");
                Console.WriteLine("Straight ahead is a Sphinx, encrusted in magma. To the right is a large door.");
                Console.WriteLine("Which way will you go?");
                playerChoice = "not 1 or 2";
                while (playerChoice != "1" && playerChoice != "2")
                {
                    Console.WriteLine("1: Straight | 2: Right");
                    playerChoice = Console.ReadLine();
                    if (playerChoice == "1")
                    {
                        Console.WriteLine("You approach the molten Sphinx with great caution.");
                        Console.WriteLine("The Sphinx immediately fixes its gaze upon you.");
                        Console.WriteLine("''HUMAN,'' It bellows. ''TO PASS THROUGH YOU MUST ANSWER MY RIDDLES.''");
                    }
                    else if (playerChoice == "2")
                    {
                        Console.WriteLine("The moment you touch the doorknob, the door falls over like a domino away from you.");
                    }
                }

            }

        }
    }
}

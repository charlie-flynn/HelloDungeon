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

            float playerHealth = 10.0f;
            float playerMana = 5.0f;
            int playerGold = 3;
            string playerAlignment = "Neutral";
            float playerProximityToNearestLivingSkeleton = 12.0f;
            bool playerAlive = true;


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
            Console.WriteLine("Gray bricks line the walls of the dungeon,"
                + " and dust and dread permeate the air.");
            Console.WriteLine("Two doors stand side by side on the wall in front of you."
            + " Which do you choose?");
            Console.WriteLine();
            Console.WriteLine("1: Left | 2: Right");
            string? playerChoice = Console.ReadLine();

            // While loop to prevent the player from making an invalid choice
            while (playerChoice != "1" && playerChoice != "2")
            {
                Console.WriteLine("Invalid Choice, try again!");
                playerChoice = Console.ReadLine();
            }
            if (playerChoice == "1")
            {
                Console.WriteLine("You enter the left door.");
            }
            else
            {
                Console.WriteLine("You enter the right door.");
            }

        }
    }
}

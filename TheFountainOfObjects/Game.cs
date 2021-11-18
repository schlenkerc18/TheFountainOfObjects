using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFountainOfObjects
{
    public class Game
    {
        // initialize stuff to false
        bool isGameOver = false;
        bool isFountainOn = false;

        public void Run()
        {
            Help(); // instructions for the game

            // creating board and player
            Console.Write("Small, medium, or large game? ");
            string choice = Console.ReadLine();
            Board board = new Board(choice);
            (int row, int column) entrance = board._entrance;
            Player player = new Player(entrance.row, entrance.column);

            // to track time player spent in the game
            DateTime startTime = DateTime.Now;

            while (isGameOver == false)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"You are in the room at (Row = {player._row}, Column = {player._column})");
                Console.WriteLine($"You have {player._bow._arrows} arrow(s)");

                if (IsInEntranceRoom(board, player))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You see light coming from the cave entrance.");
                }

                if (IsInFountainRoom(board, player))
                {
                    if (isFountainOn)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("You hear the rushing waters from the Fountain of Objects. It has been reactivated!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("You hear dripping water in this room. The Fountain of Objects is here!");
                    }
                }

                // check to see if trap is nearby
                Console.ForegroundColor = ConsoleColor.Red;
                player.IsPitNearby(board);
                player.IsAmarokNearby(board);

                bool moved = false;

                do
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("What do you want to do? ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    string input = Console.ReadLine();
                    (moved, isFountainOn) = player.Move(input, isFountainOn, board, player);
                } while (moved == false);

                // for pretty spacing
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------");
                isGameOver = IsGameOver(player, board, startTime);
            }
        }



        private void Help()
        {
            Console.WriteLine("You enter the Cavern of Objects, a maze of rooms filled with dagnerous pits and amaroks \nin search of the Fountain of Objects. ");
            Console.WriteLine("Light is visible only in the entrance, and no other light is seen anywhere in the caverns. ");
            Console.WriteLine("You must navigate the Caverns with your other senses. ");
            Console.WriteLine("Look out for pits. You will feel a breeze if a pit is in an adjacent room. \nIf you enter a room with a pit, you will die.");
            Console.WriteLine("Amaroks roam the caverns. Encountering one means certain death, but they stink and can be smelled from nearby rooms. ");
            Console.WriteLine("You carry with you a bow and a quiver of arrows.  You can use them to shoot monsters in the caverns,\nbut be warned: you have a limited supply.");
            Console.WriteLine("Find the Fountain of Objects, activate it, and return to the entrance without falling into a trap!");
            Console.WriteLine();
        }

        public string GetRoom(Board board, Player player) => board._rooms[player._row, player._column];

        public bool IsInFountainRoom(Board board, Player player) => GetRoom(board, player) == "fountain";

        public bool IsInEntranceRoom(Board board, Player player) => GetRoom(board, player) == "entrance";

        public bool IsGameOver(Player player, Board board, DateTime timeStart)
        {
            bool isGameOver = false;

            if (IsInEntranceRoom(board, player) && isFountainOn == true)
            {
                Console.WriteLine($"You are in the room at (Row = {player._row}, Column = {player._column})");
                Console.WriteLine("The Fountain of Objects has been reactivated, and you have escaped with you life!");
                Console.WriteLine("You win!");
                Console.WriteLine();
                isGameOver = true;
            }

            if (player.IsPlayerInPit(board))
            {
                Console.WriteLine("You have fallen into a pit! You lose!");
                isGameOver = true;
            }

            if (player.IsPlayerInAmarokRoom(board))
            {
                Console.WriteLine("You have been smashed by a mighty Amarok! You lose!");
                isGameOver = true;
            }

            if (isGameOver)
            {
                TimeSpan timePlayed = DateTime.Now - timeStart;
                Console.WriteLine($"Time played: {timePlayed}");
            }

            return isGameOver;
        }
    }
}

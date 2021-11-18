using System;

namespace TheFountainOfObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();

            while (true)
            {
                Console.Write("Would you like to play again? (yes or no) ");
                string response = Console.ReadLine();
                Console.WriteLine();
                if (response == "yes")
                {
                    Game newGame = new Game();
                    newGame.Run();
                }
                else
                {
                    Console.WriteLine("Thanks for playing!");
                    break;

                }

            }
        }

    }

}
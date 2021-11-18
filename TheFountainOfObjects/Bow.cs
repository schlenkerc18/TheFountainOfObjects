using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFountainOfObjects
{
    public class Bow
    {
        public int _arrows { get; private set; }

        public Bow()
        {
            _arrows = 5;
        }

        public void ShootArrow(Player player, Board board, string direction)
        {
            if (!OutOfArrows())
            {
                _arrows--;
                DidArrowHit(player, board, direction);
            }
            else Console.WriteLine("You cannot shoot. You are out of arrows!");
        }

        public bool OutOfArrows() => _arrows == 0;

        public bool DidArrowHit(Player player, Board board, string direction)
        {
            bool hit = false;

            switch (direction)
            {
                case "east":
                    hit = RemoveAmarok(player._row, player._column + 1, board);
                    break;
                case "west":
                    hit = RemoveAmarok(player._row, player._column - 1, board);
                    break;
                case "south":
                    hit = RemoveAmarok(player._row + 1, player._column, board);
                    break;
                case "north":
                    hit = RemoveAmarok(player._row - 1, player._column, board);
                    break;
            }
            if (!hit) Console.WriteLine("You missed!");

            return hit;
        }

        public bool RemoveAmarok(int row, int column, Board board)
        {
            if (board._amaroks.Contains((row, column)))
            {
                board._amaroks.Remove((row, column));
                Console.WriteLine("You killed an amarok!");
                return true;
            }
            return false;
        }
    }
}

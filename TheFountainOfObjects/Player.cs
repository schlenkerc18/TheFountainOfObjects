using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFountainOfObjects
{
    public class Player
    {
        public int _row { get; private set; }
        public int _column { get; private set; }
        public Bow _bow { get; }

        public Player(int row, int column)
        {
            _row = row;
            _column = column;
            _bow = new Bow();
        }

        public bool IsPitNearby(Board board)
        {
            List<(int row, int column)> pits = board._pits;

            foreach (var pit in pits)
            {
                // check north and south directions for pits
                if ((_row + 1 == pit.row || _row - 1 == pit.row) && _column == pit.column)
                {
                    Console.WriteLine("You feel a draft.  There is a pit in a nearby room.");
                    return true;
                }
                // check east and west directions for pits
                else if ((_column - 1 == pit.column || _column + 1 == pit.column) && _row == pit.row)
                {
                    Console.WriteLine("You feel a draft.  There is a pit in a nearby room.");
                    return true;
                }
                // checking northwest, northeast, southwest, and southeast rooms for pits
                else if ((_row + 1 == pit.row && _column + 1 == pit.column)
                      || (_row - 1 == pit.row && _column + 1 == pit.column)
                      || (_row + 1 == pit.row && _column - 1 == pit.column)
                      || (_row - 1 == pit.row && _column - 1 == pit.column))
                {
                    Console.WriteLine("You feel a draft.  There is a pit in a nearby room.");
                    return true;
                }
            }

            return false;
        }



        public bool IsPlayerInPit(Board board)
        {
            List<(int row, int column)> pits = board._pits;

            foreach (var pit in pits)
            {
                if (_row == pit.row && _column == pit.column) return true;
            }

            return false;
        }

        public bool IsAmarokNearby(Board board)
        {
            List<(int row, int column)> amaroks = board._amaroks;

            foreach (var amarok in amaroks)
            {
                // check north and south rows for amaroks
                if ((_row + 1 == amarok.row || _row - 1 == amarok.row) && _column == amarok.column)
                {
                    Console.WriteLine("You can smell the rotten stinch of an amarok in a nearby room.");
                    return true;
                }
                // check east and west columns for amaroks
                else if ((_column + 1 == amarok.column || _column - 1 == amarok.column) && _row == amarok.row)
                {
                    Console.WriteLine("You can smell the rotten stinch of an amarok in a nearby room.");
                    return true;
                }
                // checking northwest, northeast, southwest, and southeast rooms for amaroks
                else if ((_row + 1 == amarok.row && _column + 1 == amarok.column)
                      || (_row - 1 == amarok.row && _column + 1 == amarok.column)
                      || (_row + 1 == amarok.row && _column - 1 == amarok.column)
                      || (_row - 1 == amarok.row && _column - 1 == amarok.column))
                {
                    Console.WriteLine("You can smell the rotten stinch of an amarok in a nearby room.");
                    return true;
                }
            }

            return false;
        }

        public bool IsPlayerInAmarokRoom(Board board)
        {
            List<(int row, int column)> amaroks = board._amaroks;

            foreach (var amarok in amaroks)
            {
                if (_row == amarok.row && _column == amarok.column) return true;
            }

            return false;
        }

        public (bool moved, bool isFountainOn) Move(string move, bool isFountainOn, Board board, Player player)
        {
            bool moved = false;

            switch (move)
            {
                case "move east":
                    if (_column < (board._rooms.GetLength(1) - 1))
                    {
                        _column += 1;
                        moved = true;
                    }
                    else Console.WriteLine("Cannot move east");
                    break;
                case "move west":
                    if (_column > 0)
                    {
                        _column -= 1;
                        moved = true;
                    }
                    else Console.WriteLine("Cannot move west");
                    break;
                case "move north":
                    if (_row > 0)
                    {
                        _row -= 1;
                        moved = true;
                    }
                    else Console.WriteLine("Cannot move north");
                    break;
                case "move south":
                    if (_row < (board._rooms.GetLength(0) - 1))
                    {
                        _row += 1;
                        moved = true;
                    }
                    else Console.WriteLine("Cannot move south");
                    break;
                case "enable fountain":
                    if ((player._row, player._column) == board._fountain && !isFountainOn)
                    {
                        moved = true;
                        isFountainOn = true;
                    }
                    else Console.WriteLine("You cannot enable the fountain.");
                    break;
                case "shoot east":
                    if (!_bow.OutOfArrows())
                    {
                        _bow.ShootArrow(player, board, "east");
                        moved = true;
                    }
                    else Console.WriteLine("You are out of arrows.");
                    break;
                case "shoot west":
                    if (!_bow.OutOfArrows())
                    {
                        _bow.ShootArrow(player, board, "west");
                        moved = true;
                    }
                    else Console.WriteLine("You are out of arrows.");
                    break;
                case "shoot north":
                    if (!_bow.OutOfArrows())
                    {
                        _bow.ShootArrow(player, board, "north");
                        moved = true;
                    }
                    else Console.WriteLine("You are out of arrows.");
                    break;
                case "shoot south":
                    if (!_bow.OutOfArrows())
                    {
                        _bow.ShootArrow(player, board, "south");
                        moved = true;
                    }
                    else Console.WriteLine("You are out of arrows.");
                    break;
            }

            return (moved, isFountainOn);
        }
    }
}

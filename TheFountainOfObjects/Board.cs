using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFountainOfObjects
{
    public class Board
    {
        public string[,] _rooms { get; private set; }
        public (int row, int column) _entrance { get; private set; }
        public (int row, int column) _fountain { get; private set; }
        public List<(int row, int column)> _pits { get; private set; }
        public List<(int row, int column)> _amaroks { get; private set; }

        public Board(string size)
        {
            _rooms = size switch
            {
                "small" => _rooms = new string[4, 4],
                "medium" => _rooms = new string[6, 6],
                "large" => _rooms = new string[8, 8],
                _ => _rooms = new string[4, 4]
            };

            // start out by intitializing all rooms to empty
            InitializeRoomsToEmpty();

            // generate _entrance, fountain, and pits
            _entrance = GenerateEntrance();
            _fountain = GenerateFountain();
            _pits = GeneratePits();
            _amaroks = GenerateAmaroks();
        }

        public void InitializeRoomsToEmpty()
        {
            for (int i = 0; i < _rooms.GetLength(0); i++)
            {
                for (int j = 0; j < _rooms.GetLength(1); j++)
                {
                    _rooms[i, j] = "empty";
                }
            }
        }

        /// <summary>
        /// Generates a random entrance.  Only rule is that the entrance has to be on the border of the board
        /// </summary>
        /// <returns></returns>
        public (int row, int column) GenerateEntrance()
        {
            Random rand = new Random();
            int row = -1;
            int column = -1;

            while ((row != _rooms.GetLength(0) - 1 && row != 0) && (column != _rooms.GetLength(1) - 1 && column != 0))
            {
                row = rand.Next(0, _rooms.GetLength(0));
                column = rand.Next(0, _rooms.GetLength(1));
            }

            _rooms[row, column] = "entrance";

            return (row, column);
        }

        /// <summary>
        /// Generates random fountain room. Only rule is that it cannot be where the entrance room is
        /// </summary>
        /// <returns></returns>
        public (int row, int column) GenerateFountain()
        {
            Random rand = new Random();
            int row = -1;
            int column = -1;

            do
            {
                row = rand.Next(0, _rooms.GetLength(0));
                column = rand.Next(0, _rooms.GetLength(1));
            } while (_rooms[row, column] == "entrance");

            _rooms[row, column] = "fountain";

            //Console.WriteLine($"Fountain: {row}, {column}");
            return (row, column);
        }

        /// <summary>
        /// Generates a List of pits.
        /// They cannot be at the entrance or fountain
        /// Generates 1, 2, or 3 pits based on the size of the game
        /// </summary>
        /// <returns></returns>
        public List<(int row, int column)> GeneratePits()
        {
            Random rand = new Random();

            int row = -1;
            int column = -1;

            int numberOfPits = NumberOfTrapsToAdd();

            _pits = new List<(int row, int column)>(0);

            while (_pits.Count < numberOfPits)
            {
                row = rand.Next(0, _rooms.GetLength(0));
                column = rand.Next(0, _rooms.GetLength(1));

                if (_rooms[row, column] != "pit" && _rooms[row, column] != "entrance" && _rooms[row, column] != "fountain")
                {
                    _pits.Add((row, column));
                    _rooms[row, column] = "pit";
                }
            }

            return _pits;
        }

        /// <summary>
        /// Generates amaroks to put in random rooms
        /// Creates 1, 2, or 3 amaroks based on size of board
        /// Amaroks cannot be placed where an entrance, fountain, or pit is
        /// </summary>
        /// <returns></returns>
        public List<(int row, int column)> GenerateAmaroks()
        {
            Random rand = new Random();
            int row = -1;
            int column = -1;

            int numberOfAmaroks = NumberOfTrapsToAdd();

            _amaroks = new List<(int row, int column)>(0);

            while (_amaroks.Count < numberOfAmaroks)
            {
                row = rand.Next(0, _rooms.GetLength(0));
                column = rand.Next(0, _rooms.GetLength(1));
                if (_rooms[row, column] != "pit" && _rooms[row, column] != "amarok" && _rooms[row, column] != "entrance" && _rooms[row, column] != "fountain")
                {
                    _amaroks.Add((row, column));
                    _rooms[row, column] = "amarok";
                }
            }
            return _amaroks;

        }

        public int NumberOfTrapsToAdd()
        {
            int pitsToAdd;

            return pitsToAdd = _rooms.Length switch
            {
                16 => 1,
                36 => 2,
                64 => 3,
                _ => 1
            };
        }
    }
}

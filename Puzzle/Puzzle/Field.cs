using System;
using System.Text;
using System.Threading;

namespace Puzzle.Core
{
    /// <summary>
    /// Game state
    /// </summary>
    public enum GameState
    {
        GENERATION,
        PLAYING,
        SOLVED
    }

    /// <summary>
    /// Field logic
    /// </summary>
    public class Field
    {
        //Shift selected tile to grey tile
        private const int MOVE_LEFT_TILE = 0;
        private const int MOVE_RIGHT_TILE = 1;
        private const int MOVE_UP_TILE = 2;
        private const int MOVE_DOWN_TILE = 3;

        ///Used in field generation
        private const int SHUFFLE_MOVEMENTS = 100;

        //Field dimension
        public int ColumnCount { get; set; }
        public int RowCount { get; set; }

        //Field of tiles
        public Tile[,] Tiles { get; set; }

        //Game state
        public GameState State { get; set; }

        private GreyTile greyTile;

        /// <summary>
        /// Field constructor
        /// </summary>
        /// <param name="rowCount">Rows count</param>
        /// <param name="columnCount">Columns count</param>
        public Field(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
            Tiles = new Tile[rowCount, columnCount];

            Init();
        }

        /// <summary>
        /// Field initialization
        /// </summary>
        private void Init()
        {
            int value = 1;
            for (int row = 0; row < RowCount; row++) {
                for (int column = 0; column < ColumnCount; column++) {
                    if (value != RowCount * ColumnCount)
                    {
                        ValueTile valueTile = new ValueTile(value);
                        valueTile.Row = row;
                        valueTile.Col = column;
                        Tiles[row, column] = valueTile;
                        value++;
                    }
                    else {
                        greyTile = new GreyTile();
                        greyTile.Row = row;
                        greyTile.Col = column;
                        Tiles[row, column] = greyTile;
                    }
                }
            }
        }

        /// <summary>
        /// Field generator
        /// </summary>
        public void Generate()
        {
            //Set game state
            State = GameState.GENERATION;

            //Shuffle field's tiles
            Random rnd = new Random();
            int direction = MOVE_DOWN_TILE;
            for (int shifts = 0; shifts < SHUFFLE_MOVEMENTS; shifts++)
            {
                int newDirection;
                // It is possible to define next value as follows (it selects another direction from previous one): 
                //       while (direction == (newDirection = rnd.Next(0, 4)));
                // However, this generator is not so sophisticated. Use next one to get better result (field shuffling)
                newDirection = direction == 0 || direction == 1 ? rnd.Next(2, 4) : rnd.Next(0, 2);

                bool moved = false;
                switch (newDirection)
                {
                    case MOVE_LEFT_TILE: moved = MoveTile(greyTile.Row, greyTile.Col - 1); break;
                    case MOVE_RIGHT_TILE: moved = MoveTile(greyTile.Row, greyTile.Col + 1); break;
                    case MOVE_UP_TILE: moved = MoveTile(greyTile.Row - 1, greyTile.Col); break;
                    case MOVE_DOWN_TILE: moved = MoveTile(greyTile.Row + 1, greyTile.Col); break;
                }


                if (moved)
                {
                    direction = newDirection;
                }
                else
                {
                    shifts--;
                }
            }

            //Set game state
            State = GameState.PLAYING;
        }

        /// <summary>
        /// Moves specified tile identified by column and row index
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="col">Column</param>
        /// <returns>True if tile have been moved, false otherwise</returns>
        public bool MoveTile(int row, int col)
        {
            if (row >= 0 && row < RowCount && col >= 0 && col < ColumnCount)
            {
                return MoveTile(Tiles[row,col]);
            }

            return false;
        }

        /// <summary>
        /// Moves specified tile
        /// </summary>
        /// <param name="tile">Tile</param>
        /// <returns>True if tile have been moved, false otherwise</returns>
        public bool MoveTile(Tile tile)
        {
            if ((Math.Abs(greyTile.Col - tile.Col) == 1 && greyTile.Row - tile.Row == 0) || (Math.Abs(greyTile.Row - tile.Row) == 1 && greyTile.Col - tile.Col == 0))
            {
                int row = greyTile.Row;
                int column = greyTile.Col;

                greyTile.Row = tile.Row;
                greyTile.Col = tile.Col;

                tile.Row = row;
                tile.Col = column;

                Swap<Tile>(ref Tiles[tile.Row, tile.Col], ref Tiles[greyTile.Row, greyTile.Col]);

                if (IsSolved()) {
                    State = GameState.SOLVED;
                }
                return true;
            }
            return false;
        }

        static void Swap<T>(ref T x, ref T y)
        {
            T t = y;
            y = x;
            x = t;
        }

        /// <summary>
        /// Tests if the game is solved
        /// </summary>
        /// <returns>True if the game is in final state, false otherwise</returns>
        private bool IsSolved()
        {
            //throw new NotImplementedException("This method is not implemented yet.");
            return false;
        }

        /*
        static void Main(string[] args)
        {
            Field field = new Field(4, 4);
            Console.WriteLine("------ Field [4][4] initialization -------");
            ShowField(field);
            Console.WriteLine();
            Console.WriteLine("------ After generation -------");
            field.Generate();
            ShowField(field);

            field = new Field(4, 6);
            Console.WriteLine();
            Console.WriteLine("------ Field [4][6] initialization -------");
            ShowField(field);
            Console.WriteLine();
            Console.WriteLine("------ After generation -------");
            field.Generate();
            ShowField(field);

            Console.ReadLine();
        }

        static void ShowField(Field field)
        {
            for (int i = 0; i < field.RowCount; i++)
            {
                for (int j = 0; j < field.ColumnCount; j++)
                {
                    Tile t = field.Tiles[i, j];
                    if (t is ValueTile)
                    {
                        Console.Write("{0,3}", ((ValueTile)t).Value);
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine();
            }
        }*/
    }
}

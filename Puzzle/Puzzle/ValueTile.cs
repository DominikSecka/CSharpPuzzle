using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle.Core
{
    class ValueTile : Tile
    {
        public int Value { get; set; }


        public ValueTile(int value) {
            Value = value;
        }
    }
}

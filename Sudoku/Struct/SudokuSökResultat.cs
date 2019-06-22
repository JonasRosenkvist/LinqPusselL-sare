using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Struct
{
    public struct SudokuSökResultat
    {
        public int Rad { get; set; }
        public int Kolumn { get; set; }
        public int Siffra { get; set; }
    }
}

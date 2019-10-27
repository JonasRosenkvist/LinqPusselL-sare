using Sudoku.Enum;

namespace Sudoku.Struct
{
    public struct SudokuSökResultat
    {
        public int Rad { get; set; }
        public int Kolumn { get; set; }
        public int Siffra { get; set; }
        public SudokuTekniker Teknik { get; set; }
    }
}

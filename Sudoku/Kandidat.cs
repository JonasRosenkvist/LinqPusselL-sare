using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Kandidat
    {
        #region Variabler
        private readonly int sudokuRutId;
        private readonly int siffra;
        #endregion
        #region Egenskaper
        public bool Möjlig { get; set; }
        public int SudokuRutId => sudokuRutId;
        public int Siffra => siffra;
        #endregion
        #region Konstruktor
        public Kandidat(int sudokuRutId, int siffra)
        {
            this.sudokuRutId = sudokuRutId;
            this.siffra = siffra;
            Möjlig = true;
        }
        #endregion
    }
}

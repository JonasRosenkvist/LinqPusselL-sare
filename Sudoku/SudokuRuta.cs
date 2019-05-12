using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SudokuRuta
    {
        #region Variabler
        private int siffra;
        private int maxVärde;
        #endregion
        #region Egenskaper
        public int Siffra
        {
            get
            {
                return siffra;
            }
            set
            {
                if(value >= 0 && value <= maxVärde)
                {
                    siffra = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("SudokuRuta.Siffra", $"Siffran måste vara mellan 0 - {maxVärde}");
                }

            }
        }
        public int Rad { get; private set; }
        public int Kolumn { get; private set; }
        public int Id { get; private set; }
        public int Box { get; private set; }
        public bool StartSiffra { get; private set; }
        #endregion
        #region Konstruktor
        public SudokuRuta(int id,int rad,int kolumn,int box,int maxVärde)
        {
            Id = id;
            Rad = rad;
            Kolumn = kolumn;
            Box = box;
            this.maxVärde = maxVärde;
            Siffra = 0;
            StartSiffra = false;
        }
        #endregion
    }
}

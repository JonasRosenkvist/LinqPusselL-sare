using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Struct
{
    public struct Storlek
    {
        public int AntalrutorIBredd { get; set; }
        public int AntalRutorIHöjd { get; set; }
        #region Konstruktor
        public Storlek(int antalRutorIBredd,int antalRutorIHöjd)
        {
            AntalrutorIBredd = antalRutorIBredd;
            AntalRutorIHöjd = antalRutorIHöjd;
        }
        #endregion
    }
}

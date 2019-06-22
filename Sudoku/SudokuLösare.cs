using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Struct;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SudokuLösare
    {
        public static bool FinnsDetSingelIRad(SudokuPussel pussel)
        {
            List<SudokuSökResultat> sökresultat = new List<SudokuSökResultat>();
            return FinnsDetSingelIRad(pussel, out sökresultat);
        }
        public static bool FinnsDetSingelIRad(SudokuPussel pussel, out List<SudokuSökResultat> resultat)
        {
            List<SudokuSökResultat> sökresultat = new List<SudokuSökResultat>();
            var sudokurutor = (from sudokuruta in pussel.SpelPlan
                               join kandidat in pussel.MöjligaKandidater on sudokuruta.Id equals kandidat.SudokuRutId
                               group sudokuruta by new { sudokuruta.Rad, kandidat.Siffra } into ruta
                               select ruta).Where(x => x.Count() == 1);




            foreach (var ruta in sudokurutor)
            {
                foreach (var info in ruta)
                {
                    sökresultat.Add(new SudokuSökResultat { Rad = info.Rad, Kolumn = info.Kolumn, Siffra = ruta.Key.Siffra });
                }
            }
            resultat = sökresultat;
            return sudokurutor.Count() > 0;
        }


        public static bool FinnsDetSingelIKolumn(SudokuPussel pussel)
        {
            List<SudokuSökResultat> sökresultat = new List<SudokuSökResultat>();
            return FinnsDetSingelIKolumn(pussel, out sökresultat);
        }
        public static bool FinnsDetSingelIKolumn(SudokuPussel pussel, out List<SudokuSökResultat> resultat)
        {
            List<SudokuSökResultat> sökresultat = new List<SudokuSökResultat>();
            var sudokurutor = (from sudokuruta in pussel.SpelPlan
                               join kandidat in pussel.MöjligaKandidater on sudokuruta.Id equals kandidat.SudokuRutId
                               group sudokuruta by new { sudokuruta.Kolumn, kandidat.Siffra } into ruta
                               select ruta).Where(x => x.Count() == 1);




            foreach (var ruta in sudokurutor)
            {
                foreach (var info in ruta)
                {
                    sökresultat.Add(new SudokuSökResultat { Rad = info.Rad, Kolumn = info.Kolumn, Siffra = ruta.Key.Siffra });
                }
            }
            resultat = sökresultat;
            return sudokurutor.Count() > 0;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Struct;
using Sudoku.Enum;

namespace Sudoku
{
    public class SudokuLösare
    {
        public static bool FinnsDetSingelIRad(SudokuPussel pussel)
        {
            List<SudokuSökResultat> sökresultat = new List<SudokuSökResultat>();
            return FinnsDetSingelIRad(pussel, sökresultat);
        }
        public static bool FinnsDetSingelIRad(SudokuPussel pussel, List<SudokuSökResultat> resultat)
        {
            var sudokurutor = (from sudokuruta in pussel.SpelPlan
                               join kandidat in pussel.MöjligaKandidater on sudokuruta.Id equals kandidat.SudokuRutId
                               group sudokuruta by new { sudokuruta.Rad, kandidat.Siffra } into ruta
                               select ruta).Where(x => x.Count() == 1);




            foreach (var ruta in sudokurutor)
            {
                foreach (var info in ruta)
                {
                    resultat.Add(new SudokuSökResultat { Rad = info.Rad, Kolumn = info.Kolumn, Siffra = ruta.Key.Siffra, Teknik = SudokuTekniker.SingelIRad });
                }
            }
            return sudokurutor.Count() > 0;
        }


        public static bool FinnsDetSingelIKolumn(SudokuPussel pussel)
        {
            List<SudokuSökResultat> sökresultat = new List<SudokuSökResultat>();
            return FinnsDetSingelIKolumn(pussel, sökresultat);
        }
        public static bool FinnsDetSingelIKolumn(SudokuPussel pussel, List<SudokuSökResultat> resultat)
        {
            var sudokurutor = (from sudokuruta in pussel.SpelPlan
                               join kandidat in pussel.MöjligaKandidater on sudokuruta.Id equals kandidat.SudokuRutId
                               group sudokuruta by new { sudokuruta.Kolumn, kandidat.Siffra } into ruta
                               select ruta).Where(x => x.Count() == 1);




            foreach (var ruta in sudokurutor)
            {
                foreach (var info in ruta)
                {
                    resultat.Add(new SudokuSökResultat { Rad = info.Rad, Kolumn = info.Kolumn, Siffra = ruta.Key.Siffra, Teknik = SudokuTekniker.SingelIKolumn });
                }
            }
            return sudokurutor.Count() > 0;
        }

        public static bool FinnsDetSingelIBox(SudokuPussel pussel)
        {
            List<SudokuSökResultat> sökresultat = new List<SudokuSökResultat>();
            return FinnsDetSingelIBox(pussel, sökresultat);
        }
        public static bool FinnsDetSingelIBox(SudokuPussel pussel, List<SudokuSökResultat> resultat)
        {
            var sudokurutor = (from sudokuruta in pussel.SpelPlan
                               join kandidat in pussel.MöjligaKandidater on sudokuruta.Id equals kandidat.SudokuRutId
                               group sudokuruta by new { sudokuruta.Box, kandidat.Siffra } into ruta
                               select ruta).Where(x => x.Count() == 1);
            foreach (var ruta in sudokurutor)
            {
                foreach (var info in ruta)
                {
                    resultat.Add(new SudokuSökResultat { Rad = info.Rad, Kolumn = info.Kolumn, Siffra = ruta.Key.Siffra,Teknik=SudokuTekniker.SingelIBox });
                }
            }
            return sudokurutor.Count() > 0;
        }

        public static bool FinnsDetSingelKandidater(SudokuPussel pussel)
        {
            List<SudokuSökResultat> sökresultat = new List<SudokuSökResultat>();
            return FinnsDetSingelKandidater(pussel, sökresultat);
        }
        public static bool FinnsDetSingelKandidater(SudokuPussel pussel, List<SudokuSökResultat> resultat)
        {
            var sudokurutor = (from sudokuruta in pussel.SpelPlan
                               join kandidat in pussel.MöjligaKandidater on sudokuruta.Id equals kandidat.SudokuRutId
                               group kandidat by new { sudokuruta.Rad, sudokuruta.Kolumn } into ruta
                               select ruta).Where(x => x.Count() == 1);
            foreach (var ruta in sudokurutor)
            {
                foreach (var info in ruta)
                {
                    resultat.Add(new SudokuSökResultat { Rad = ruta.Key.Rad, Kolumn = ruta.Key.Kolumn, Siffra = info.Siffra,Teknik= SudokuTekniker.SingelKandidater });
                }
            }
            return sudokurutor.Count() > 0;
        }

    }
}

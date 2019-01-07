using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Struct;

namespace Sudoku
{
    public class SudokuPussel
    {
        #region Egenskaper
        public List<SudokuRuta> SpelPlan { get; private set; }
        public Storlek PusselStorlek { get; private set; }
        public Storlek BoxStorlek { get; private set; }
        public List<Kandidat> Kandidater { get; private set; }
        #endregion
        #region Konstruktor
        public SudokuPussel(Storlek boxStorlek)
        {
            int storlek = boxStorlek.AntalrutorIBredd * boxStorlek.AntalRutorIHöjd;
            int id = 1;
            BoxStorlek = boxStorlek;
            PusselStorlek = new Storlek(storlek, storlek);
            SpelPlan = new List<SudokuRuta>();
            Kandidater = new List<Kandidat>();
            for(int rad = 0; rad < PusselStorlek.AntalRutorIHöjd; rad++)
            {
                for(int kolumn = 0; kolumn < PusselStorlek.AntalrutorIBredd; kolumn++)
                {
                    SpelPlan.Add(new SudokuRuta(id, rad, kolumn, BeräknaBox(rad, kolumn),storlek));
                    for (int siffra = 1; siffra <= storlek; siffra++)
                    {
                        Kandidater.Add(new Kandidat(id, siffra));
                    }
                    id++;
                }
            }
        }
        #endregion
        #region Funktioner
        #region Publika
        public bool PlaceraSiffra(int rad,int kolumn,int siffra)
        {
            SudokuRuta ruta = (from s in SpelPlan
                              where s.Rad == rad && s.Kolumn == kolumn
                              select s).First();
            if (ruta.Siffra == 0)
            {
                ruta.Siffra = siffra;
                this.TabortAllaKandidaterIRuta(ruta.Id);
                this.TabortKandidater(rad, kolumn, siffra);
                return true;
            }
            else
                return false;
        }
        
        #endregion
        #region Privata
        private int BeräknaBox(int rad,int kolumn)
        {
            int antalBoxarIBredd = PusselStorlek.AntalrutorIBredd / BoxStorlek.AntalrutorIBredd;
            return (rad / BoxStorlek.AntalRutorIHöjd) * antalBoxarIBredd + (kolumn / BoxStorlek.AntalrutorIBredd);

        }
        private int TabortAllaKandidaterIRuta(int rutId)
        {
            var kandidater = from k in Kandidater
                             where k.SudokuRutId == rutId
                             select k;
            foreach(Kandidat kandidat in kandidater)
            {
                kandidat.Möjlig = false;
            }
            return kandidater.Count();
        }
        private int TabortKandidater(int rad, int kolumn, int siffra)
        {
            int box = this.BeräknaBox(rad, kolumn);
            var rutor = from k in Kandidater
                        join s in SpelPlan on k.SudokuRutId equals s.Id
                        where (s.Rad == rad || s.Kolumn == kolumn || s.Box == box) && k.Siffra == siffra
                        select k;
            foreach (var ruta in rutor)
            {
                ruta.Möjlig = false;
            }
            return rutor.Count();
        }
        #endregion
        #endregion
    }
}

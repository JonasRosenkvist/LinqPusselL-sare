﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Struct;

namespace Sudoku
{
    public class SudokuPussel
    {
        #region Egenskaper
        public List<SudokuRuta> SpelPlan { get; private set; }
        public Storlek PusselStorlek { get; private set; }
        public Storlek BoxStorlek { get; private set; }
        public List<Kandidat> AllaKandidater { get; private set; }
        public bool LösningFinns { get; private set; }
        public List<Kandidat> MöjligaKandidater
        {
            get
            {
                return (from k in AllaKandidater where k.Möjlig == true select k).ToList();
            }
        }
        private List<Func<SudokuPussel, List<SudokuSökResultat>, bool>> allaTekniker = new List<Func<SudokuPussel, List<SudokuSökResultat>, bool>>();
        private SudokuRuta[] lösning;
        #endregion
        #region Konstruktor
        public SudokuPussel(Storlek boxStorlek)
        {
            int storlek = boxStorlek.AntalrutorIBredd * boxStorlek.AntalRutorIHöjd;
            int id = 0;
            BoxStorlek = boxStorlek;
            PusselStorlek = new Storlek(storlek, storlek);
            SpelPlan = new List<SudokuRuta>();
            AllaKandidater = new List<Kandidat>();
            lösning = new SudokuRuta[(int)Math.Pow(storlek, 2)];
            for (int rad = 0; rad < PusselStorlek.AntalRutorIHöjd; rad++)
            {
                for (int kolumn = 0; kolumn < PusselStorlek.AntalrutorIBredd; kolumn++)
                {
                    SpelPlan.Add(new SudokuRuta(id, rad, kolumn, BeräknaBox(rad, kolumn), storlek));
                    for (int siffra = 1; siffra <= storlek; siffra++)
                    {
                        AllaKandidater.Add(new Kandidat(id, siffra));
                    }
                    id++;
                }
            }
            allaTekniker.Add(SudokuLösare.FinnsDetSingelIRad);
            allaTekniker.Add(SudokuLösare.FinnsDetSingelIKolumn);
            allaTekniker.Add(SudokuLösare.FinnsDetSingelIBox);
            allaTekniker.Add(SudokuLösare.FinnsDetSingelKandidater);
        }
        #endregion
        #region Funktioner
        #region Publika
        public bool Start()
        {
            List<SudokuRuta> givnaRutor = (from sudokuRuta in SpelPlan
                                           where sudokuRuta.Siffra != 0
                                           select sudokuRuta).ToList();

            foreach (SudokuRuta ruta in givnaRutor)
            {
                ruta.StartSiffra = true;
            }
            LösningFinns = FinnsDetEnLösning();
            if (LösningFinns)
            {
                SpelPlan.CopyTo(lösning);
                TabortEjGivnaSiffror();
            }
            else
            {
                TabortEjGivnaSiffror();
                foreach (SudokuRuta ruta in givnaRutor)
                {
                    ruta.StartSiffra = false;
                }
            }
            RäknaOmKandidater();
            return LösningFinns;
        }

        private void TabortEjGivnaSiffror()
        {
            List<SudokuRuta> ejGivnaRutor = (from sudokuRuta in SpelPlan
                                             where !sudokuRuta.StartSiffra
                                             select sudokuRuta).ToList();
            foreach (SudokuRuta ruta in ejGivnaRutor)
            {
                ruta.Siffra = 0;
            }
        }

        public bool PlaceraSiffra(int rad, int kolumn, int siffra)
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
        public void PlaceraSiffra(List<SudokuSökResultat> sökResultats)
        {
            foreach (SudokuSökResultat sök in sökResultats)
            {
                this.PlaceraSiffra(sök.Rad, sök.Kolumn, sök.Siffra);
            }
        }
        public bool TabortSiffra(int rad, int kolumn)
        {
            SudokuRuta ruta = (from s in SpelPlan
                               where s.Rad == rad && s.Kolumn == kolumn
                               select s).First();
            if (ruta.Siffra != 0)
            {
                ruta.Siffra = 0;
                this.RäknaOmKandidater();
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            string output = String.Empty;
            int rutNummer = 1;
            SudokuRuta[] rutor;
            output += "      0     1     2      3     4     5      6     7     8\n";
            output += "  ||=======================================================||\n";
            for (int radNummer = 0; radNummer < PusselStorlek.AntalRutorIHöjd; radNummer++)
            {
                rutor = (from s in SpelPlan
                         where s.Rad == radNummer
                         select s).ToArray();
                output += $"{radNummer} |";

                for (int radIRuta = 1; radIRuta <= 3; radIRuta++)
                {
                    foreach (SudokuRuta ruta in rutor)
                    {
                        if (ruta.Siffra != 0)
                        {
                            if (radIRuta != 2)
                                output += $"|     ";
                            else
                                output += $"|  {ruta.Siffra}  ";
                        }
                        else
                        {
                            int startSiffra = 1 + ((radIRuta - 1) * 3);
                            int slutSiffra = 3 + ((radIRuta - 1) * 3);
                            var kandidater = (from k in AllaKandidater
                                              where k.SudokuRutId == ruta.Id && k.Siffra >= startSiffra && k.Siffra <= slutSiffra
                                              orderby k.Siffra
                                              select k);
                            output += "| ";
                            foreach (Kandidat kand in kandidater)
                            {
                                //if (kand.Möjlig)
                                //output += $"{kand.Siffra}";
                                //else
                                output += " ";
                            }
                            output += " ";
                        }
                        if (rutNummer % 3 == 0)
                            output += "|";
                        rutNummer++;
                    }
                    output += "|\n  |";

                }


                //output += "|\n";

                if ((radNummer + 1) % 3 == 0)
                    output += "|=======================================================||\n";
                else
                    output += "|-----------------||-----------------||-----------------||\n";
            }
            return output;
        }

        #endregion
        #region Privata
        private bool FinnsDetEnLösning()
        {
            List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
            bool korrektPuzzel = true;
            do
            {
                resultat.Clear();
                foreach (Func<SudokuPussel, List<SudokuSökResultat>, bool> teknik in allaTekniker)
                {
                    if (teknik(this, resultat))
                        break;
                }
                this.PlaceraSiffra(resultat);
            }
            while (resultat.Count > 0);

            if (!SpelPlan.Any(ruta => ruta.Siffra == 0))
            {
                if (DubbletterIRad())
                    korrektPuzzel = false;
                if (DubletterIKolumn())
                    korrektPuzzel = false;
                if (DubbletterIBox())
                    korrektPuzzel = false;
            }
            else
            {
                korrektPuzzel = false;
            }
            return korrektPuzzel;
        }

        private bool DubbletterIBox()
        {
            return (from sudokuRuta in this.SpelPlan
                    group sudokuRuta by new { sudokuRuta.Box, sudokuRuta.Siffra } into sammanställnig
                    select sammanställnig).Any(siffra => siffra.Count() > 1);
        }

        private bool DubletterIKolumn()
        {
            return (from sudokuRuta in this.SpelPlan
                    group sudokuRuta by new { sudokuRuta.Kolumn, sudokuRuta.Siffra } into sammanställnig
                    select sammanställnig).Any(siffra => siffra.Count() > 1);
        }

        private bool DubbletterIRad()
        {
            return (from sudokuRuta in this.SpelPlan
                    group sudokuRuta by new { sudokuRuta.Rad, sudokuRuta.Siffra } into sammanställnig
                    select sammanställnig).Any(siffra => siffra.Count() > 1);
        }

        private void RäknaOmKandidater()
        {
            var rutor = from sudokuruta in SpelPlan
                        where sudokuruta.Siffra != 0
                        select sudokuruta;
            foreach (Kandidat kandidat in AllaKandidater)
                kandidat.Möjlig = true;
            foreach (SudokuRuta ruta in rutor)
            {
                this.TabortAllaKandidaterIRuta(ruta.Id);
                this.TabortKandidater(ruta.Rad, ruta.Kolumn, ruta.Siffra);
            }

        }
        private int BeräknaBox(int rad, int kolumn)
        {
            int antalBoxarIBredd = PusselStorlek.AntalrutorIBredd / BoxStorlek.AntalrutorIBredd;
            return (rad / BoxStorlek.AntalRutorIHöjd) * antalBoxarIBredd + (kolumn / BoxStorlek.AntalrutorIBredd);

        }
        private int TabortAllaKandidaterIRuta(int rutId)
        {
            var kandidater = from k in AllaKandidater
                             where k.SudokuRutId == rutId
                             select k;
            foreach (Kandidat kandidat in kandidater)
            {
                kandidat.Möjlig = false;
            }
            return kandidater.Count();
        }
        private int TabortKandidater(int rad, int kolumn, int siffra)
        {
            int box = this.BeräknaBox(rad, kolumn);
            var kandidater = from k in AllaKandidater
                             join s in SpelPlan on k.SudokuRutId equals s.Id
                             where (s.Rad == rad || s.Kolumn == kolumn || s.Box == box) && k.Siffra == siffra
                             select k;
            foreach (var kandidat in kandidater)
            {
                kandidat.Möjlig = false;
            }
            return kandidater.Count();
        }
        #endregion
        #endregion
    }
}

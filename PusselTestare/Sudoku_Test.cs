using System;
using Sudoku;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Struct;

namespace Sudoku
{
    [TestClass]
    public class Sudoku_Test
    {
       
        
        [TestMethod]
        public void BeräknaBox_KontrolleraSvar()
        {
            // Setup
            int rad = 5;
            int kolumn = 5;
            int förväntadBox = 4;
            SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
            List<SudokuRuta> spelPlan = pussel.SpelPlan;
            int box = -1;

            //utför
            foreach (SudokuRuta ruta in spelPlan)
            {
                if (ruta.Rad == rad && ruta.Kolumn == kolumn)
                    box = ruta.Box;
            }
            Assert.AreEqual(förväntadBox, box, "Något går fel i beräkningen av boxar");
        }
        [TestMethod]
        public void PlaceraSiffra_PlaceraSiffraTomBox()
        {
            //Setup 
            int rad = 2;
            int kolumn = 2;
            int siffra = 5;
            SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
            bool returvärde;

            //Utför
            returvärde = pussel.PlaceraSiffra(rad, kolumn, siffra);

            //Kontroll
            Assert.AreEqual(true, returvärde);
        }
        [TestMethod]
        public void PlaceraSiffra_PlaceraSiffraIFylldBox()
        {
            //Setup 
            int rad = 2;
            int kolumn = 2;
            int siffra = 5;
            SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
            bool returvärde;

            //Utför
            returvärde = pussel.PlaceraSiffra(rad, kolumn, siffra);
            returvärde = pussel.PlaceraSiffra(rad, kolumn, siffra);

            //Kontroll
            Assert.AreEqual(false, returvärde);
        }

        [TestMethod]
        public void PlaceraSiffra_KontrollSiffraPlacerasRätt()
        {
            //Setup 
            int rad = 2;
            int kolumn = 2;
            int siffra = 5;
            SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
            bool korrekt = false;

            //Utför
            pussel.PlaceraSiffra(rad, kolumn, siffra);
            foreach (SudokuRuta ruta in pussel.SpelPlan)
            {
                if (ruta.Rad == rad && ruta.Kolumn == kolumn && ruta.Siffra == siffra)
                {
                    korrekt = true;
                }
                if ((ruta.Rad != rad || ruta.Kolumn != kolumn) && ruta.Siffra != 0)
                {
                    korrekt = false;
                    break;
                }
            }

            //Kontroll
            Assert.AreEqual(true, korrekt);
        }
        [TestMethod]
        public void PlaceraSiffra_KontrollTabortKandidaterIRutaMedPlaceradSiffra()
        {
            //Setup
            int rad = 2;
            int kolumn = 2;
            int siffra = 5;
            SudokuPussel pussel = SkapaEttPusselOchPlaceraEnSiffra(rad, kolumn, siffra);
            bool korrekt = true;
            var kandidater = from k in pussel.AllaKandidater
                             join s in pussel.SpelPlan on k.SudokuRutId equals s.Id
                             where s.Rad == rad && s.Kolumn == kolumn
                             select new
                             {
                                 möjlig = k.Möjlig,
                             };
            //Utför
            foreach (var kandidat in kandidater)
            {
                if (kandidat.möjlig)
                    korrekt = false;
            }
            // Kontroll
            Assert.AreEqual(true, korrekt);
        }
        [TestMethod]
        public void PlaceraSiffra_KontrollTabortKandidaterIRutorSomPåverkasavPlaceradSiffra()
        {
            //Setup
            int rad = 3;
            int kolumn = 1;
            int box = 3;
            int siffra = 7;
            SudokuPussel pussel = SkapaEttPusselOchPlaceraEnSiffra(rad, kolumn, siffra);
            bool korrekt = true;
            var kandidater = from k in pussel.AllaKandidater
                             join s in pussel.SpelPlan on k.SudokuRutId equals s.Id
                             where s.Rad == rad || s.Kolumn == kolumn || s.Box == box
                             select new
                             {
                                 rad = s.Rad,
                                 kolumn = s.Kolumn,
                                 möjlig = k.Möjlig,
                                 siffra = k.Siffra
                             };
            //Utför
            foreach (var kandidat in kandidater)
            {
                if (kandidat.rad == rad && kandidat.kolumn == kolumn)
                {
                    continue;
                }
                if (kandidat.siffra == siffra)
                {
                    if (kandidat.möjlig)
                    {
                        korrekt = false;
                    }
                }
                else
                {
                    if (!kandidat.möjlig)
                    {
                        korrekt = false;
                    }
                }

            }
            Assert.AreEqual(true, korrekt);
        }
        [TestMethod]
        public void PlaceraSiffra_KontrollAvKandidaterIRutorSomIntePåverkasAvPlaceradSiffra()
        {
            //Setup
            int rad = 3;
            int kolumn = 1;
            int box = 3;
            int siffra = 7;
            SudokuPussel pussel = SkapaEttPusselOchPlaceraEnSiffra(rad, kolumn, siffra);
            bool korrekt = true;
            var kandidater = from k in pussel.AllaKandidater
                             join s in pussel.SpelPlan on k.SudokuRutId equals s.Id
                             where s.Rad != rad && s.Kolumn != kolumn && s.Box != box
                             select new
                             {
                                 möjlig = k.Möjlig
                             };
            //Utför
            foreach(var kandidat in kandidater)
            {
                if(!kandidat.möjlig)
                {
                    korrekt = false;
                }
            }
            //Kontroll
            Assert.AreEqual(true, korrekt);
        }
        [TestMethod]
        public void Start_MedKorrektSudoku()
        {
            //Setup
            SudokuPussel pussel = SkapaEttKorrektSudoku();
            bool resultat;

            //Utför
            resultat = pussel.Start();

            //kontroll
            Assert.AreEqual(true, resultat);

        }

        [TestMethod]
        public void Start_MedFelaktigtSudoku()
        {
            //Setup
            SudokuPussel pussel = SkapaEttFelaktigtSudoku();
            bool resultat;

            //Utför
            resultat = pussel.Start();

            //kontroll
            Assert.AreEqual(false, resultat);
        }

       



        #region Funktioner
        private SudokuPussel SkapaEttPusselOchPlaceraEnSiffra(int rad, int kolumn, int siffra)
        {
            SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
            pussel.PlaceraSiffra(rad, kolumn, siffra);
            return pussel;
        }
        private SudokuPussel SkapaEttKorrektSudoku()
        {
            SudokuPussel sudoku = new SudokuPussel(new Storlek(3, 3));
            List<SudokuSökResultat> iFylladrutor = new List<SudokuSökResultat>();
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 0, Kolumn = 4, Siffra = 8 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 1, Kolumn = 3, Siffra = 1 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 1, Kolumn = 8, Siffra = 9 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 2, Kolumn = 1, Siffra = 1 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 2, Kolumn = 8, Siffra = 3 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 3, Kolumn = 1, Siffra = 6 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 3, Kolumn = 3, Siffra = 2 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 4, Kolumn = 0, Siffra = 3 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 4, Kolumn = 2, Siffra = 9 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 4, Kolumn = 4, Siffra = 5 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 4, Kolumn = 7, Siffra = 6 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 4, Kolumn = 8, Siffra = 7 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 5, Kolumn = 1, Siffra = 5 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 5, Kolumn = 6, Siffra = 2 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 5, Kolumn = 8, Siffra = 4 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 6, Kolumn = 1, Siffra = 3 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 6, Kolumn = 2, Siffra = 6 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 6, Kolumn = 5, Siffra = 5 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 6, Kolumn = 7, Siffra = 9 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 6, Kolumn = 8, Siffra = 8 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 7, Kolumn = 1, Siffra = 9 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 7, Kolumn = 4, Siffra = 3 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 7, Kolumn = 8, Siffra = 5 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 8, Kolumn = 0, Siffra = 7 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 8, Kolumn = 4, Siffra = 9 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 8, Kolumn = 7, Siffra = 1 });
            sudoku.PlaceraSiffra(iFylladrutor);
            return sudoku;
        }

        private SudokuPussel SkapaEttFelaktigtSudoku()
        {
            SudokuPussel sudoku = new SudokuPussel(new Storlek(3, 3));
            List<SudokuSökResultat> iFylladrutor = new List<SudokuSökResultat>();
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 0, Kolumn = 4, Siffra = 8 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 1, Kolumn = 3, Siffra = 1 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 1, Kolumn = 8, Siffra = 9 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 2, Kolumn = 1, Siffra = 1 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 2, Kolumn = 8, Siffra = 3 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 3, Kolumn = 1, Siffra = 6 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 3, Kolumn = 3, Siffra = 2 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 3, Kolumn = 5, Siffra = 2 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 4, Kolumn = 0, Siffra = 3 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 4, Kolumn = 2, Siffra = 9 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 4, Kolumn = 4, Siffra = 5 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 4, Kolumn = 7, Siffra = 6 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 4, Kolumn = 8, Siffra = 7 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 5, Kolumn = 1, Siffra = 5 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 5, Kolumn = 6, Siffra = 2 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 5, Kolumn = 8, Siffra = 4 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 6, Kolumn = 1, Siffra = 3 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 6, Kolumn = 2, Siffra = 6 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 6, Kolumn = 5, Siffra = 5 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 6, Kolumn = 7, Siffra = 9 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 6, Kolumn = 8, Siffra = 8 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 7, Kolumn = 1, Siffra = 9 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 7, Kolumn = 4, Siffra = 3 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 7, Kolumn = 8, Siffra = 5 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 8, Kolumn = 0, Siffra = 7 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 8, Kolumn = 4, Siffra = 9 });
            iFylladrutor.Add(new SudokuSökResultat() { Rad = 8, Kolumn = 7, Siffra = 1 });
            sudoku.PlaceraSiffra(iFylladrutor);
            return sudoku;
        }

        #endregion
    }

}

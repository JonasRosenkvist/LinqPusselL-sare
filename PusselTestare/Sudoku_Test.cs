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
        public void SudokuPussel_BeräknaBox_KontrolleraSvar()
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
        public void Placera_Siffra_Tom_Box()
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
        public void Placera_Siffra_Fylld_Box()
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
        public void Kontroll_Siffra_Placeras_Rätt()
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
        public void Kontroll_TabortKandidater_I_Ruta_Med_PlaceradSiffra()
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
        public void Kontroll_TabortKandidater_I_Rutor_Som_Påverkas_av_Placerad_Siffra()
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
        public void Kontroll_Av_Kandidater_I_Rutor_Som_Inte_Påverkas_Av_Placerad_Siffra()
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
        

       
        #region Funktioner
        private SudokuPussel SkapaEttPusselOchPlaceraEnSiffra(int rad, int kolumn, int siffra)
        {
            SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
            pussel.PlaceraSiffra(rad, kolumn, siffra);
            return pussel;
        }
        
        #endregion
    }

}

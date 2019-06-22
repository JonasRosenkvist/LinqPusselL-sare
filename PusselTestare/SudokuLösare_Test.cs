using System;
using Sudoku;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Struct;

namespace Sudoku
{
    public class SudokuLösare_Test
    {
        #region SingelIRad
        [TestClass]
        public class SingelIRad_Test
        {
            [TestMethod]
            public void Det_Finns_Singel_I_Rad()
            {
                //Setup 
                SudokuPussel pussel;
                bool svar;

                //Utför
                pussel = SkapaEttPusselMedSingel_I_Rad();
                svar = SudokuLösare.FinnsDetSingelIRad(pussel);

                //Kontroll
                Assert.AreEqual(true, svar);
            }
            [TestMethod]
            public void Det_Finns_Singel_I_Rad_Kontrollerar_Siffra()
            {
                //SetUp
                SudokuPussel pussel;
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                pussel = SkapaEttPusselMedSingel_I_Rad();
                SudokuLösare.FinnsDetSingelIRad(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(3, resultat.First().Siffra);

            }
            [TestMethod]
            public void Det_Finns_Singel_I_Rad_Kontrollerar_Rad()
            {
                //SetUp
                SudokuPussel pussel;
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                pussel = SkapaEttPusselMedSingel_I_Rad();
                SudokuLösare.FinnsDetSingelIRad(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(2, resultat.First().Rad);
            }
            [TestMethod]
            public void Det_Finns_Singel_I_Rad_Kontrollerar_Kolumn()
            {
                //SetUp
                SudokuPussel pussel;
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                pussel = SkapaEttPusselMedSingel_I_Rad();
                SudokuLösare.FinnsDetSingelIRad(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(6, resultat.First().Kolumn);
            }
            public void Det_Finns_Ingen_Singel_I_Rad_Return_Tom_Lista()
            {
                //SetUp
                SudokuPussel pussel = new SudokuPussel(new Storlek(3, 3));
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                SudokuLösare.FinnsDetSingelIRad(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(0, resultat.Count());
            }
            [TestMethod]
            public void Det_Finns_Ingen_Singel_I_Rad()
            {
                //Setup 
                SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
                bool svar;

                //Utför
                pussel.PlaceraSiffra(0, 0, 3);
                pussel.PlaceraSiffra(1, 3, 3);
                pussel.PlaceraSiffra(2, 7, 4);
                svar = SudokuLösare.FinnsDetSingelIRad(pussel);

                //Kontroll
                Assert.AreEqual(false, svar);
            }

            private SudokuPussel SkapaEttPusselMedSingel_I_Rad()
            {
                SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
                pussel.PlaceraSiffra(0, 0, 3);
                pussel.PlaceraSiffra(1, 3, 3);
                pussel.PlaceraSiffra(2, 7, 4);
                pussel.PlaceraSiffra(2, 8, 5);
                return pussel;
            }
        }
        #endregion
        #region SingelIKolumn
        [TestClass]
        public class SingelIKolumn_Test
        {
            [TestMethod]
            public void Det_Finns_Singel_I_Kolumn()
            {
                //Setup 
                SudokuPussel pussel;
                bool svar;

                //Utför
                pussel = SkapaEttPusselMedSingel_I_Kolumn();
                svar = SudokuLösare.FinnsDetSingelIKolumn(pussel);

                //Kontroll
                Assert.AreEqual(true, svar);
            }

            [TestMethod]
            public void Det_Finns_Singel_I_Kolumn_Kontrollerar_Siffra()
            {
                //SetUp
                SudokuPussel pussel;
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                pussel = SkapaEttPusselMedSingel_I_Kolumn();
                SudokuLösare.FinnsDetSingelIKolumn(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(7, resultat.First().Siffra);

            }
            [TestMethod]
            public void Det_Finns_Singel_I_Kolumn_Kontrollerar_Rad()
            {
                //SetUp
                SudokuPussel pussel;
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                pussel = SkapaEttPusselMedSingel_I_Kolumn();
                SudokuLösare.FinnsDetSingelIKolumn(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(4, resultat.First().Rad);
            }
            [TestMethod]
            public void Det_Finns_Singel_I_Kolumn_Kontrollerar_Kolumn()
            {
                //SetUp
                SudokuPussel pussel;
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                pussel = SkapaEttPusselMedSingel_I_Kolumn();
                SudokuLösare.FinnsDetSingelIKolumn(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(5, resultat.First().Kolumn);
            }
            public void Det_Finns_Ingen_Singel_I_Rad_Return_Tom_Lista()
            {
                //SetUp
                SudokuPussel pussel = new SudokuPussel(new Storlek(3, 3));
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                SudokuLösare.FinnsDetSingelIKolumn(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(0, resultat.Count());
            }
            [TestMethod]
            public void Det_Finns_Ingen_Singel_I_Kolumn()
            {
                //Setup 
                SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
                bool svar;

                //Utför
                pussel.PlaceraSiffra(0, 0, 3);
                pussel.PlaceraSiffra(1, 3, 3);
                pussel.PlaceraSiffra(2, 7, 4);
                svar = SudokuLösare.FinnsDetSingelIKolumn(pussel);

                //Kontroll
                Assert.AreEqual(false, svar);
            }
            private SudokuPussel SkapaEttPusselMedSingel_I_Kolumn()
            {
                SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
                pussel.PlaceraSiffra(1, 4, 7);
                pussel.PlaceraSiffra(8, 3, 7);
                pussel.PlaceraSiffra(5, 5, 4);
                pussel.PlaceraSiffra(3, 5, 5);
                return pussel;
            }
            
        }
        #endregion
#region SingelIBox

        [TestClass]
        public class SingelIBox_Test
        {
            [TestMethod]
            public void Det_Finns_Singel_I_Box()
            {
                //Setup 
                SudokuPussel pussel;
                bool svar;

                //Utför
                pussel = SkapaEttPusselMedSingel_I_Box();
                svar = SudokuLösare.FinnsDetSingelIBox(pussel);

                //Kontroll
                Assert.AreEqual(true, svar);
            }

            [TestMethod]
            public void Det_Finns_Singel_I_Box_Kontrollerar_Siffra()
            {
                //SetUp
                SudokuPussel pussel;
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                pussel = SkapaEttPusselMedSingel_I_Box();
                SudokuLösare.FinnsDetSingelIBox(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(1, resultat.First().Siffra);

            }
            [TestMethod]
            public void Det_Finns_Singel_I_Box_Kontrollerar_Rad()
            {
                //SetUp
                SudokuPussel pussel;
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                pussel = SkapaEttPusselMedSingel_I_Box();
                SudokuLösare.FinnsDetSingelIBox(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(4, resultat.First().Rad);
            }
            [TestMethod]
            public void Det_Finns_Singel_I_Box_Kontrollerar_Kolumn()
            {
                //SetUp
                SudokuPussel pussel;
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                pussel = SkapaEttPusselMedSingel_I_Box();
                SudokuLösare.FinnsDetSingelIBox(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(2, resultat.First().Kolumn);
            }
            public void Det_Finns_Ingen_Singel_I_Rad_Return_Tom_Lista()
            {
                //SetUp
                SudokuPussel pussel = new SudokuPussel(new Storlek(3, 3));
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                SudokuLösare.FinnsDetSingelIBox(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(0, resultat.Count());
            }
            [TestMethod]
            public void Det_Finns_Ingen_Singel_I_Kolumn()
            {
                //Setup 
                SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
                bool svar;

                //Utför
                pussel.PlaceraSiffra(0, 0, 3);
                pussel.PlaceraSiffra(1, 3, 3);
                pussel.PlaceraSiffra(2, 7, 4);
                svar = SudokuLösare.FinnsDetSingelIKolumn(pussel);

                //Kontroll
                Assert.AreEqual(false, svar);
            }
            private SudokuPussel SkapaEttPusselMedSingel_I_Box()
            {
                SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
                pussel.PlaceraSiffra(0, 0, 1);
                pussel.PlaceraSiffra(7, 1, 1);
                pussel.PlaceraSiffra(5, 2, 4);
                pussel.PlaceraSiffra(3, 2, 5);
                return pussel;
            }

        }
        #endregion


        [TestClass]
        public class SingelIKandidat_Test
        {
            [TestMethod]
            public void Det_Finns_SingelKandidat()
            {
                //Setup 
                SudokuPussel pussel;
                bool svar;

                //Utför
                pussel = SkapaEttPusselMedSingelKandidat();
                svar = SudokuLösare.FinnsDetSingelKandidater(pussel);
                //Kontroll
                Assert.AreEqual(true, svar);
            }

            [TestMethod]
            public void Det_Finns_SingelKandidat_Kontrollerar_Siffra()
            {
                //SetUp
                SudokuPussel pussel;
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                pussel = SkapaEttPusselMedSingelKandidat();
                SudokuLösare.FinnsDetSingelKandidater(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(4, resultat.First().Siffra);

            }
            [TestMethod]
            public void Det_Finns_Singel_Kandidat_Kontrollerar_Rad()
            {
                //SetUp
                SudokuPussel pussel;
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                pussel = SkapaEttPusselMedSingelKandidat();
                SudokuLösare.FinnsDetSingelKandidater(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(0, resultat.First().Rad);
            }
            [TestMethod]
            public void Det_Finns_Singel_Kandidat_Kontrollerar_Kolumn()
            {
                //SetUp
                SudokuPussel pussel;
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                pussel = SkapaEttPusselMedSingelKandidat();
                SudokuLösare.FinnsDetSingelKandidater(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(8, resultat.First().Kolumn);
            }
            public void Det_Finns_Ingen_Singel_I_Rad_Return_Tom_Lista()
            {
                //SetUp
                SudokuPussel pussel = new SudokuPussel(new Storlek(3, 3));
                List<SudokuSökResultat> resultat = new List<SudokuSökResultat>();
                //Utför
                SudokuLösare.FinnsDetSingelKandidater(pussel, out resultat);

                //Kontroll
                Assert.AreEqual(0, resultat.Count());
            }
            [TestMethod]
            public void Det_Finns_Ingen_Singel_I_Kolumn()
            {
                //Setup 
                SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
                bool svar;

                //Utför
                pussel.PlaceraSiffra(0, 0, 3);
                pussel.PlaceraSiffra(1, 3, 3);
                pussel.PlaceraSiffra(2, 7, 4);
                svar = SudokuLösare.FinnsDetSingelIKolumn(pussel);

                //Kontroll
                Assert.AreEqual(false, svar);
            }
            private SudokuPussel SkapaEttPusselMedSingelKandidat()
            {
                SudokuPussel pussel = new SudokuPussel(new Sudoku.Struct.Storlek(3, 3));
                pussel.PlaceraSiffra(0, 0, 1);
                pussel.PlaceraSiffra(0, 1, 2);
                pussel.PlaceraSiffra(0, 2, 3);
                pussel.PlaceraSiffra(0, 4, 5);
                pussel.PlaceraSiffra(0, 5, 6);
                pussel.PlaceraSiffra(0, 6, 7);
                pussel.PlaceraSiffra(0, 7, 8);
                pussel.PlaceraSiffra(1, 8, 9);
                return pussel;
            }

        }
    }
}

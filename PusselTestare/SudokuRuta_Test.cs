using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sudoku
{
    [TestClass]
    public class SudokuRuta_Test
    {
        [TestMethod]
        public void Siffra_Sättvärde_Gilltigsiffra()
        {
            // Setup
            int siffra = 7;
            int maxVärde = 9;
            SudokuRuta ruta = new SudokuRuta(1, 1, 1, 1, maxVärde);

            //Utför
            ruta.Siffra = siffra;

            //Kontroll
            Assert.AreEqual(siffra, ruta.Siffra, "Siffran inte satt korrekt");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Siffra_Sättvärde_MindreÄnNoll()
        {
            // Setup
            int siffra = -3;
            int maxVärde = 9;
            SudokuRuta ruta = new SudokuRuta(1, 1, 1, 1, maxVärde);

            //Utför
            ruta.Siffra = siffra;

            //Kontrollen hanteras av den förväntade Exceptionhanteraren

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Siffra_Sättvärde_ÖverMaxVärde()
        {
            // Setup
            int siffra = 12;
            int maxVärde = 9;
            SudokuRuta ruta = new SudokuRuta(1, 1, 1, 1, maxVärde);

            //Utför
            ruta.Siffra = siffra;

            //Kontrollen hanteras av den förväntade Exceptionhanteraren

        }
    }
}

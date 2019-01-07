using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku;
using Sudoku.Struct;

namespace LinqPusselLösare
{
    class Program
    {
        static void Main(string[] args)
        {
            SudokuPussel sudoku = new SudokuPussel(new Storlek(3, 3));
            sudoku.PlaceraSiffra(2, 2, 5);
            var resultat = from k in sudoku.Kandidater
                           join s in sudoku.SpelPlan on k.SudokuRutId equals s.Id
                           where k.Siffra == 5 && s.Box != 0 && s.Rad != 2 && s.Kolumn != 2
                           select new
                           {
                               rad = s.Rad,
                               kolumn = s.Kolumn,
                               box = s.Box,
                               möjlig = k.Möjlig
                           };
            Console.WriteLine("Rad\tKolumn\tBox\tMöjlig");
            Console.WriteLine("================================================================================\n");
            foreach (var r in resultat)
            {
                Console.WriteLine($"{r.rad}\t{r.kolumn}\t{r.box}\t{r.möjlig}");
            }
            Console.ReadLine();
        }
    }
}

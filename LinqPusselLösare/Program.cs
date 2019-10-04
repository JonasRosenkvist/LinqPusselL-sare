using System;
using Sudoku;
using Sudoku.Struct;
using System.Collections.Generic;


namespace LinqPusselLösare
{
    class Program
    {
        static void Main(string[] args)
        {
            SudokuPussel sudoku = new SudokuPussel(new Storlek(3, 3));
            int val = 0;
            string felMeddelande = string.Empty;
            do
            {

                Console.WriteLine(sudoku + "\n");
                if(felMeddelande != string.Empty)
                {
                    Console.ForegroundColor = System.ConsoleColor.Red;
                    Console.WriteLine($"{felMeddelande}\n");
                    Console.ForegroundColor = System.ConsoleColor.White;
                    felMeddelande = string.Empty;
                }
                Console.WriteLine("1: Placera Siffra");
                Console.WriteLine("2: Tabort Siffra");
                Console.WriteLine("3: Börja Lös Puzzel");
                Console.WriteLine("4: Avsluta\n");
                Console.Write("Vad vill du göra ? ");
                try
                {
                    val = Convert.ToInt32(Console.ReadLine());
                    switch (val)
                    {
                        case 1:
                            PlaceraSiffra(sudoku);
                            break;
                        case 2:
                            TabortSiffra(sudoku);
                            break;
                        case 3:
                            if(!sudoku.Start())
                            {
                                felMeddelande = "Det finns ingen lösning på puzzlet kontrollera att alla ledtrådar är korrekt placerade";
                            }
                            break;
                    }
                    Console.Clear();
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Valen måste vara heltal\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Siffran som ska placeras måste vara mellan 1-9\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            while (val != 4);
        }
        static void PlaceraSiffra(SudokuPussel sudoku)
        {
            int siffra, rad, kolumn;
            Console.Write("Vilken siffra vill du placera ? ");
            siffra = Convert.ToInt32(Console.ReadLine());
            Console.Write($"I vilken rad vill du placera {siffra}:an ?");
            rad = Convert.ToInt32(Console.ReadLine());
            Console.Write($"I vilken kolum vill du placera {siffra}:an ?");
            kolumn = Convert.ToInt32(Console.ReadLine());
            sudoku.PlaceraSiffra(rad, kolumn, siffra);
        }
        static void TabortSiffra(SudokuPussel sudoku)
        {
            int rad, kolumn;
            Console.Write("I vilken rad finns runtan du vill tabort ? ");
            rad = Convert.ToInt32(Console.ReadLine());
            Console.Write($"Vilken kolumn i rad {rad} vill du tabort ? ");
            kolumn = Convert.ToInt32(Console.ReadLine());
            sudoku.TabortSiffra(rad, kolumn);
        }
        static void KorrektSudoku(SudokuPussel sudoku)
        {
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
        }
    }
}

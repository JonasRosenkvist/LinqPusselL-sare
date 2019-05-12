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
            int val = 0;
            do
            {
                
                Console.WriteLine(sudoku + "\n");
                Console.WriteLine("1: Placera Siffra");
                Console.WriteLine("2: Tabort Siffra");
                Console.WriteLine("3: Avsluta\n");
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
                    }
                    Console.Clear();
                }
                catch(FormatException)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Valen måste vara heltal\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch(ArgumentOutOfRangeException)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Siffran som ska placeras måste vara mellan 1-9\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            while (val != 3);
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
    }
}

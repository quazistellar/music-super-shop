using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    internal static class ArrowsMenu
    {
        public static int Show(int pos, int minStrelochka, int maxStrelochka)
        {

            ConsoleKeyInfo key;

            do
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, pos);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("->");
                Console.ResetColor();


                key = Console.ReadKey();

                Console.SetCursorPosition(0, pos);
                Console.WriteLine("  ");


                switch (key.Key)
                {
                    case ConsoleKey.UpArrow when pos != minStrelochka:
                        pos--;
                        break;
                    case ConsoleKey.DownArrow when pos != maxStrelochka:
                        pos++;
                        break;
                    case ConsoleKey.F1:

                        return (int)SystemKey.F1;

                    case ConsoleKey.F2:

                        return (int)SystemKey.F2;

                    case ConsoleKey.Escape:

                        return (int)SystemKey.Escape;

                    case ConsoleKey.S:

                        return (int)SystemKey.S;

                    case ConsoleKey.F10:

                        return (int)SystemKey.F10;

                    case ConsoleKey.OemMinus:

                        return (int)SystemKey.Minus;

                    case ConsoleKey.OemPlus:

                        return (int)SystemKey.Plus;

                    default:
                        continue;
                }

            } while (key.Key != ConsoleKey.Enter);

            Console.Clear();
            return pos;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public static class ConsoleHelper
{
    
    public static void ShowChangeSystemKeys()
    {
        Console.SetCursorPosition((int) (Console.WindowWidth * 0.8), 2);
        Console.WriteLine($"{SystemKey.F10} - Удалить");
        Console.SetCursorPosition((int) (Console.WindowWidth * 0.8), 3);
        Console.WriteLine($"{SystemKey.Escape} - В меню");
        Console.SetCursorPosition((int) (Console.WindowWidth * 0.8), 4);
        Console.WriteLine($"{SystemKey.S} - Засейвить");


            for (int i = 2; i < 17; i++)
            {
                Console.SetCursorPosition(90, i);
                Console.WriteLine("|");
            }
        }
    
    public static void ShowMainSystemKeys()
    {
            
        Console.SetCursorPosition((int) (Console.WindowWidth * 0.8), 2);
        Console.Write($"{SystemKey.F1} - Добавить запись");
        Console.SetCursorPosition((int) (Console.WindowWidth * 0.8), 3);
        Console.WriteLine($"{SystemKey.F2} - Поиск");
        Console.SetCursorPosition((int) (Console.WindowWidth * 0.8), 4);
        Console.WriteLine($"{SystemKey.Escape} - В меню");

            for (int i = 2; i < 17; i++)
            {
                Console.SetCursorPosition(90, i);
                Console.WriteLine("|");
            }

    }

        public static void ShowAddSystemKeys()
        {
           
            Console.SetCursorPosition((int) (Console.WindowWidth * 0.8), 2);
            Console.WriteLine($"{SystemKey.Escape} - В меню");
            Console.SetCursorPosition((int) (Console.WindowWidth * 0.8), 3);
            Console.WriteLine($"{SystemKey.S} - Засейвить");
            Console.SetCursorPosition((int)(Console.WindowWidth * 0.8), 4);
            Console.WriteLine("-----------------");
            Console.SetCursorPosition((int)(Console.WindowWidth * 0.8), 5);
            Console.WriteLine("1 - Кассир");
            Console.SetCursorPosition((int)(Console.WindowWidth * 0.8), 6);
            Console.WriteLine("2 - Кадровик");
            Console.SetCursorPosition((int)(Console.WindowWidth * 0.8), 7);
            Console.WriteLine("3 - Склад-менеджер");
            Console.SetCursorPosition((int)(Console.WindowWidth * 0.8), 8);
            Console.WriteLine("4 - Бухгалтер");

            for (int i = 2; i < 17; i++)
            {
                Console.SetCursorPosition(90, i);
                Console.WriteLine("|");
            }

        }

        public static  void ShowHeader(User user)
        {
            var employee = FileManager.GetAppDbContext().Employees.FirstOrDefault(emp => emp.UserID == user.ID);
            var displayName = employee == null ? user.Login : employee.FirstName;
            Console.SetCursorPosition((int)(Console.WindowWidth * 0.2), 0);
            Console.Write($"Добро пожаловать, {displayName}!");
            Console.SetCursorPosition((int)(Console.WindowWidth * 0.8), 0);
            Console.Write($"Роль: {user.Position}\n");
            Console.WriteLine(new string('-', Console.WindowWidth - 1));
        }


        public static void ShowMainCashierSystemKeys()
        {
          
            Console.SetCursorPosition((int) (Console.WindowWidth * 0.8), 2);
            Console.Write($"{SystemKey.S} - Купить");
            Console.SetCursorPosition((int) (Console.WindowWidth * 0.8), 3);
            Console.WriteLine($"{SystemKey.Escape} - В меню");

            for (int i = 2; i < 17; i++)
            {
                Console.SetCursorPosition(90, i);
                Console.WriteLine("|");
            }

        }

        public static void ShowAddCashierSystemKeys()
        {
            Console.SetCursorPosition((int) (Console.WindowWidth * 0.8), 2);
            Console.Write($"{SystemKey.Plus} - Добавить товар");
            Console.SetCursorPosition((int) (Console.WindowWidth * 0.8), 3);
            Console.Write($"{SystemKey.Minus} - Удалить товар");
            Console.SetCursorPosition((int) (Console.WindowWidth * 0.8), 4);
            Console.WriteLine($"{SystemKey.Escape} - В меню");


            for (int i = 2; i < 17; i++)
            {
                Console.SetCursorPosition(90, i);
                Console.WriteLine("|");
            }

        }

        private static void PrintChangeForm(params string[] variables)
        {
        
        }
    }
}

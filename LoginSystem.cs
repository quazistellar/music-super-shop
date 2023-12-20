using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    // админ пароль: = admin admin
    public static class LoginSystem
    {
        private static string _login = "";
        private static string _password = "";

        static LoginSystem()
        {
        }
        public static User ProceedLogin()
        {
            Console.Clear();
            User user = null;
            while (user == null)
            {
                ShowLoginMenu();

                var selectedOption = ArrowsMenu.Show(2, 2, 4);
                switch (selectedOption)
                {
                    case 2:
                        ShowLoginMenu();
                        Console.SetCursorPosition(8, 2);
                        _login = Console.ReadLine();
                        Console.Clear();
                        break;
                    case 3:
                        ShowLoginMenu();
                        Console.SetCursorPosition(11, 3);
                        _password = GetPasswordInput();
                        Console.Clear();
                        break;
                    case 4:
                        user = FileManager.GetAppDbContext().GetUser(_login, _password);
                        if (user == null)
                            ShowLoginInvalidAttempt();
                        break;
                }
            }

            return user;
        }

        private static void ShowLoginMenu()
        {
           
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("                               > ♫ Добро пожаловать в музыкальный магазин ChordWorld ♫ <");
            Console.ResetColor();
            Console.WriteLine(new String('-', Console.WindowWidth - 1));
            Console.WriteLine($"  Логин:{_login}");
            Console.WriteLine($"  Пароль:{new string('*', _password.Length)}");
            Console.WriteLine("  Авторизоваться");
        }

        private static void ShowLoginInvalidAttempt()
        {
            Console.SetCursorPosition(0, 5);
            Console.ForegroundColor= ConsoleColor.DarkRed;  
            Console.WriteLine("Неверные данные, попробуйте ещё раз!!");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
        }


        private static string GetPasswordInput()
        {
            var password = "";
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (password.Length <= 0) continue;
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
                else
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }
            return password;
        }
    }
}

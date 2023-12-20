using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public class AdminSystem : ICrudAdmin
    {
        private readonly int _idPos;
        private readonly int _loginPos;
        private readonly int _passwordPos;
        private readonly int _positionPos;
        private readonly AppDbContext _dbContext;

        public AdminSystem()
        {
            _idPos = (int)(Console.WindowWidth * 0.15);
            _loginPos = _idPos * 2;
            _passwordPos = _idPos * 3;
            _positionPos = _idPos * 4;
            _dbContext = FileManager.GetAppDbContext();
        }

        public AdminSystem(AppDbContext dbContext)

        {
            _dbContext = dbContext;
        }

        public void ProceedAdminSystem(User admin)
        {
            Console.Clear();

            if (admin.Position != Position.Admin)
                return;

            ShowAdminMenu(admin, ReadAllRecords());

        }

        private void ShowAdminMenu(User admin, List<User> users)
        {
            int choice = 0;
            while (choice != -1)
            {
                ConsoleHelper.ShowHeader(admin);
                ConsoleHelper.ShowMainSystemKeys();
                ShowUsers(users);
                var numberOfUsers = ReadAllRecords().Count;
                choice = ArrowsMenu.Show(3, 3, numberOfUsers + 2);
                switch (choice)
                {
                    case -5:
                        ShowAddForm(admin);
                        break;
                    case -4:
                        ShowFindForm(admin);
                        break;
                    case -6:
                    case -7:
                    case -3:
                        Delete(admin);
                        break;
                    case -2:
                    case -1:
                        break;
                    default:
                        ShowChangeForm(admin, choice - 3);
                        break;
                }
            }
        }

        private void ShowAddForm(User admin)
        {
            Console.Clear();
            int choice = 0;
            var id = 0;
            var login = "";
            var password = "";
            int intPos = (int)Position.Admin;
            while (choice != -1)
            {
                ConsoleHelper.ShowHeader(admin);
                PrintChangeForm(id.ToString(), login, password, ((Position)intPos).ToString());
                ConsoleHelper.ShowAddSystemKeys();
                choice = ArrowsMenu.Show(2, 2, 5);
                Console.Clear();
                ConsoleHelper.ShowAddSystemKeys();
                ConsoleHelper.ShowHeader(admin);
                switch (choice)
                {
                    case 2:
                        {
                            PrintChangeForm("", login, password, ((Position)intPos).ToString());
                            Console.SetCursorPosition(5, 2);
                            while (!int.TryParse(Console.ReadLine(), out id))
                            {
                                Console.SetCursorPosition(5, 2);
                            }
                            break;
                        }
                    case 3:
                        {
                            PrintChangeForm(id.ToString(), "", password, ((Position)intPos).ToString());
                            Console.SetCursorPosition(8, 3);
                            login = Console.ReadLine();
                            break;
                        }
                    case 4:
                        {
                            PrintChangeForm(id.ToString(), login, "", ((Position)intPos).ToString());
                            Console.SetCursorPosition(11, 4);
                            password = Console.ReadLine();
                            break;
                        }
                    case 5:
                        {
                            PrintChangeForm(id.ToString(), login, password, "");
                            Console.SetCursorPosition(11, 5);
                            while (!int.TryParse(Console.ReadLine(), out intPos))
                            {
                                Console.SetCursorPosition(11, 5);
                            }
                            break;
                        }
                    case -2:
                        {
                            Create(new User(id, login, password, (Position)intPos));
                            choice = -1;
                            Console.Clear();
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void ShowFindForm(User admin)
        {
            int choice = 0;

            while (choice != -1 && choice > 6 || choice < 3)
            {
                Console.Clear();
                ConsoleHelper.ShowHeader(admin);
                Console.SetCursorPosition(0, 2);
                Console.WriteLine("  > Выполнить поиск по: <");
                Console.WriteLine("  ID");
                Console.WriteLine("  Логин");
                Console.WriteLine("  Пароль");
                Console.WriteLine("  Роль");
                choice = ArrowsMenu.Show(3, 3, 6);
            }

            switch (choice)
            {
                case 3:
                    Console.WriteLine("Напишите ID пользователя: ");
                    int id = 0;
                    while (!int.TryParse(Console.ReadLine(), out id));
                    ShowAdminMenu(admin, ReadById(id));
                    break;
                case 4:
                    Console.WriteLine("Напишите логин: ");
                    ShowAdminMenu(admin, ReadByLogin(Console.ReadLine()));
                    break;
                case 5:
                    Console.WriteLine("Напишите пароль: ");
                    ShowAdminMenu(admin, ReadByPassword(Console.ReadLine()));
                    break;
                case 6:
                    Console.WriteLine("Напишите роль: ");
                    Console.WriteLine("1 - Кассир");
                    Console.WriteLine("2 - Кадровик");
                    Console.WriteLine("3 - Склад-менеджер");
                    Console.WriteLine("4 - Бухгалтер");
                    int pos;
                    while (!int.TryParse(Console.ReadLine(), out pos));
                    ShowAdminMenu(admin, ReadByPosition((Position)pos));
                    break;
                default:
                    break;
            }
            Console.Clear();

        }

        private void ShowChangeForm(User admin, int userPosition)
        {
            int choice = 0;
            var user = Read(userPosition);
            var id = user.ID;
            var login = user.Login;
            var password = user.Password;
            int intPos = (int)user.Position;
            while (choice != -1)
            {
                ConsoleHelper.ShowHeader(admin);
                PrintChangeForm(id.ToString(), login, password, ((Position)intPos).ToString());
                ConsoleHelper.ShowChangeSystemKeys();
                choice = ArrowsMenu.Show(2, 2, 5);
                Console.Clear();
                ConsoleHelper.ShowChangeSystemKeys();
                ConsoleHelper.ShowHeader(admin);
                switch (choice)
                {
                    case 2:
                        {
                            PrintChangeForm("", login, password, ((Position)intPos).ToString());
                            Console.SetCursorPosition(5, 2);
                            while (!int.TryParse(Console.ReadLine(), out id))
                            {
                                Console.SetCursorPosition(5, 2);
                            }
                            break;
                        }
                    case 3:
                        {
                            PrintChangeForm(id.ToString(), "", password, ((Position)intPos).ToString());
                            Console.SetCursorPosition(8, 3);
                            login = Console.ReadLine();
                            break;
                        }
                    case 4:
                        {
                            PrintChangeForm(id.ToString(), login, "", ((Position)intPos).ToString());
                            Console.SetCursorPosition(11, 4);
                            password = Console.ReadLine();
                            break;
                        }
                    case 5:
                        {
                            PrintChangeForm(id.ToString(), login, password, "");
                            Console.SetCursorPosition(11, 5);
                            while (!int.TryParse(Console.ReadLine(), out intPos))
                            {
                                Console.SetCursorPosition(11, 5);
                            }
                            break;
                        }
                    case -2:
                        {
                            Update(userPosition, id, login, password, (Position)intPos);
                            break;
                        }
                    case -3:
                        Delete(user);
                        break;
                    default:
                        break;
                }
            }
        }

        private void PrintChangeForm(string id, string login, string password, string position)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID:{id}");
            Console.WriteLine($"  Логин:{login}");
            Console.WriteLine($"  Пароль:{password}");
            Console.WriteLine($"  Роль:{position}");
        }


        private void ShowUsers(List<User> users)
        {
            Console.SetCursorPosition(_idPos, 2);
            Console.Write("ID");
            Console.SetCursorPosition(_loginPos, 2);
            Console.Write("Логин");
            Console.SetCursorPosition(_passwordPos, 2);
            Console.Write("Пароль");
            Console.SetCursorPosition(_positionPos, 2);
            Console.Write("Роль");

            var cursorHeight = 3;

            foreach (var user in users)
            {
                Console.SetCursorPosition(_idPos, cursorHeight);
                Console.Write($"{user.ID}");
                Console.SetCursorPosition(_loginPos, cursorHeight);
                Console.Write($"{user.Login}");
                Console.SetCursorPosition(_passwordPos, cursorHeight);
                Console.Write($"{user.Password}");
                Console.SetCursorPosition(_positionPos, cursorHeight);
                Console.Write($"{user.Position}");
                cursorHeight++;
            }
        }

        
        public void Create(User user)
        {
            _dbContext.Users.Add(user);
        }

        public User Read(int id)
        {
            return _dbContext.Users[id];
        }

        public void Update(int userPosition, int id, string login, string password, Position position)
        {
            _dbContext.Users[userPosition] = new User(id, login, password, position);
        }

        public void Delete(User user)
        {
            _dbContext.Users.Remove(user);
        }

        public List<User> ReadAllRecords()
        {
            return _dbContext.Users;
        }

        public List<User> ReadById(int id)
        {
            return _dbContext.Users.Where(x => x.ID == id).ToList();
        }

        public List<User> ReadByLogin(string login)
        {
            return _dbContext.Users.Where(x => Equals(x.Login, login)).ToList();
        }

        public List<User> ReadByPassword(string password)
        {
            return _dbContext.Users.Where(x => Equals(x.Password, password)).ToList();
        }

        public List<User> ReadByPosition(Position position)
        {
            return _dbContext.Users.Where(x => x.Position.Equals(position)).ToList();
        }
    }
}

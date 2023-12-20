using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public class HrSystem : ICrudHr
    {
    private readonly int _idPos;
    private readonly int _firstNamePos;
    private readonly int _lastNamePos;
    private readonly int _patronymicPos;
    private readonly int _positionPos;
    private readonly AppDbContext _dbContext;
    
    public HrSystem()
    {
        _idPos = (int)(Console.WindowWidth * 0.12);
        _firstNamePos = _idPos * 2;
        _lastNamePos = _idPos * 3;
        _patronymicPos = _idPos * 4;
        _positionPos = _idPos * 5;
        _dbContext = FileManager.GetAppDbContext();
    }

    public void ProceedHrSystem(User hr)
    {
        if (hr.Position != Position.Hr)
            return;
        ShowHrMenu(hr, ReadAllEmployee());
    }

    private void ShowHrMenu(User hr, List<Employee> employees)
    {
        int choice = 0;
        while (choice != -1)
        {
            ConsoleHelper.ShowHeader(hr);
            ConsoleHelper.ShowMainSystemKeys();
            ShowEmployees(employees);
            var numberOfEmployees = ReadAllEmployee().Count;
            choice = ArrowsMenu.Show(3, 3, numberOfEmployees + 2);
            switch (choice)
            {
                case -5:
                     ShowAddForm(hr);
                    break;
                case -4:
                    ShowFindForm(hr);
                    break;
                case -6:
                case -7:
                case -3:
                case -2:
                case -1:
                    break;
                default:
                     ShowChangeForm(hr,choice - 3);
                    break;
            }
        }
    }
    
    private void ShowChangeForm(User hr,int employeePosition)
    {
        Console.Clear();
        var employee = Read(employeePosition);
        var choice = 0;
        var id = employee.ID;
        var lastName = employee.LastName;
        var firstName = employee.FirstName;
        var patronymic = employee.Patronymic;
        var birthday = employee.BirthDate;
        var passport = employee.Passport;
        var salary = employee.Salary;
        var userId = employee.UserID;
        while (choice != -1)
        {
            ConsoleHelper.ShowHeader(hr);
            PrintChangeForm(id.ToString(), lastName, firstName, patronymic, birthday.ToString(),passport ,  salary.ToString(), userId.ToString());
            ConsoleHelper.ShowChangeSystemKeys();
            choice = ArrowsMenu.Show(2, 2, 9);
            ConsoleHelper.ShowChangeSystemKeys();
            ConsoleHelper.ShowHeader(hr);
            switch (choice)
            {
                case 2:
                {
                    PrintChangeForm("", lastName, firstName, patronymic, birthday.ToString(), passport,  salary.ToString(), userId.ToString());
                    Console.SetCursorPosition(5,2);
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.SetCursorPosition(5,2);
                    }
                    break;
                }
                case 3:
                {
                    PrintChangeForm(id.ToString(), "", firstName, patronymic, birthday.ToString(), passport,  salary.ToString(), userId.ToString());
                    Console.SetCursorPosition(11,3);
                    lastName = Console.ReadLine();
                    break;
                }
                case 4:
                {
                    PrintChangeForm(id.ToString(), lastName, "", patronymic, birthday.ToString(), passport,  salary.ToString(), userId.ToString());
                    Console.SetCursorPosition(12,4);
                    firstName = Console.ReadLine();
                    break;
                }
                case 5:
                {
                    PrintChangeForm(id.ToString(), lastName, firstName, "", birthday.ToString(), passport,  salary.ToString(), userId.ToString());
                    Console.SetCursorPosition(13,5);
                    patronymic = Console.ReadLine();
                    break;
                }
                case 6:
                {
                    PrintChangeForm(id.ToString(), lastName, firstName, patronymic, "", passport,  salary.ToString(), userId.ToString());
                    Console.SetCursorPosition(11,6);
                    while (!DateTime.TryParse(Console.ReadLine(), out birthday))
                    {
                        Console.SetCursorPosition(11,6);
                    }
                    break;
                }
                case 7:
                {
                    PrintChangeForm(id.ToString(), lastName, firstName, patronymic, birthday.ToString(), "",  salary.ToString(), userId.ToString());
                    Console.SetCursorPosition(11,7);
                    passport = Console.ReadLine();
                    break;
                }
                case 8:
                {
                    PrintChangeForm(id.ToString(), lastName, firstName, patronymic, birthday.ToString(), passport,  "", userId.ToString());
                    Console.SetCursorPosition(9,8);
                    while (!double.TryParse(Console.ReadLine(), out salary))
                    {
                        Console.SetCursorPosition(9,8);
                    }
                    break;
                }
                case 9:
                {
                    PrintChangeForm(id.ToString(), lastName, firstName, patronymic, birthday.ToString(), passport,  salary.ToString(), "");
                    Console.SetCursorPosition(10,9);
                    while (!int.TryParse(Console.ReadLine(), out userId) || !UserExistsById(userId))
                    {
                        Console.SetCursorPosition(10,9);
                                
                    }
                    break;
                }
                case -2:
                {
                    Update(employeePosition, id, userId, firstName, lastName, patronymic, birthday, salary, passport);
                    break;
                }
                case -3:
                    Delete(employee);
                    choice = -1;
                    Console.Clear();
                    break;
                default:
                    break;
            }
            Console.Clear();
        }
    }

    private void ShowFindForm(User hr)
    {
        int choice = 0;
        while (choice != -1 && choice > 10 || choice < 3 )
        {
            Console.Clear();
            ConsoleHelper.ShowHeader(hr);
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("  > Поиск по: <");
            Console.WriteLine("  ID");
            Console.WriteLine("  Имя");
            Console.WriteLine("  userId");
            Console.WriteLine("  Фамилия");
            Console.WriteLine("  Отчество");
            Console.WriteLine("  Дршка");
            Console.WriteLine("  Зпшка");
            Console.WriteLine("  Паспорт");
            choice = ArrowsMenu.Show(3, 3, 10);
        }

        switch (choice)
        {
            case 3:
                Console.WriteLine("Напишите id");
                int id = 0;
                while (!int.TryParse(Console.ReadLine(),  out id)) ;
                ShowHrMenu(hr, ReadById(id));
                break;
            case 4:
                Console.WriteLine("Напишите имя");
                ShowHrMenu(hr, ReadByFirstName(Console.ReadLine()));
                break;
            case 5:
                int userId;
                Console.WriteLine("Напишите userId");
                while (!int.TryParse(Console.ReadLine(), out userId) || !UserExistsById(userId))
                {
                    
                }
                ShowHrMenu(hr, ReadByUserId(userId));
                break;
            case 6:
                Console.WriteLine("Напишите фамилию");
                ShowHrMenu(hr, ReadByLastName(Console.ReadLine()));
                break;
            case 7 :
                Console.WriteLine("Напишите отчество");
                ShowHrMenu(hr, ReadByPatronymic(Console.ReadLine()));
                break;
            case 8:
                    Console.WriteLine("Напишите дршку");
                    DateTime birthday;
                while (!DateTime.TryParse(Console.ReadLine(), out birthday))
                {
                }
                ShowHrMenu(hr, ReadByBirthDate(birthday));
                break;
            case 9:
                    Console.WriteLine("Напишите зпшку");
                    double salary;
                while (!double.TryParse(Console.ReadLine(), out salary))
                {
                    
                }
                ShowHrMenu(hr, ReadBySalary(salary));
                break;
            case 10:
                Console.WriteLine("Напишите данные паспорта");
                ShowHrMenu(hr, ReadByPassport(Console.ReadLine()));
                break;
            default:
                break;
        }
        Console.Clear();
    }

    private void ShowAddForm(User hr)
    {
        Console.Clear();
        var choice = 0;
        var id = 0;
        var lastName = "";
        var firstName = "";
        var patronymic = "";
        var birthday = DateTime.Today;
        var passport = "";
        var salary = 0.0;
        var userId = 0;

        while (choice != -1)
        {
            ConsoleHelper.ShowHeader(hr);
            PrintChangeForm(id.ToString(), lastName, firstName, patronymic, birthday.ToString(),passport ,  salary.ToString(), userId.ToString());
            ConsoleHelper.ShowAddSystemKeys();
            choice = ArrowsMenu.Show(2, 2, 9);
            ConsoleHelper.ShowAddSystemKeys();
            ConsoleHelper.ShowHeader(hr);
            switch (choice)
            {
                case 2:
                {
                    PrintChangeForm("", lastName, firstName, patronymic, birthday.ToString(), passport,  salary.ToString(), userId.ToString());
                    Console.SetCursorPosition(5,2);
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.SetCursorPosition(5,2);
                    }
                    break;
                }
                case 3:
                {
                    PrintChangeForm(id.ToString(), "", firstName, patronymic, birthday.ToString(), passport,  salary.ToString(), userId.ToString());
                    Console.SetCursorPosition(11,3);
                    lastName = Console.ReadLine();
                    break;
                }
                case 4:
                {
                    PrintChangeForm(id.ToString(), lastName, "", patronymic, birthday.ToString(), passport,  salary.ToString(), userId.ToString());
                    Console.SetCursorPosition(12,4);
                    firstName = Console.ReadLine();
                    break;
                }
                case 5:
                {
                    PrintChangeForm(id.ToString(), lastName, firstName, "", birthday.ToString(), passport,  salary.ToString(), userId.ToString());
                    Console.SetCursorPosition(13,5);
                    patronymic = Console.ReadLine();
                    break;
                }
                case 6:
                {
                    PrintChangeForm(id.ToString(), lastName, firstName, patronymic, "", passport,  salary.ToString(), userId.ToString());
                    Console.SetCursorPosition(11,6);
                    while (!DateTime.TryParse(Console.ReadLine(), out birthday))
                    {
                        Console.SetCursorPosition(11,6);
                    }
                    break;
                }
                case 7:
                {
                    PrintChangeForm(id.ToString(), lastName, firstName, patronymic, birthday.ToString(), "",  salary.ToString(), userId.ToString());
                    Console.SetCursorPosition(11,7);
                    passport = Console.ReadLine();
                    break;
                }
                case 8:
                {
                    PrintChangeForm(id.ToString(), lastName, firstName, patronymic, birthday.ToString(), passport,  "", userId.ToString());
                    Console.SetCursorPosition(9,8);
                    while (!double.TryParse(Console.ReadLine(), out salary))
                    {
                        Console.SetCursorPosition(9,8);
                    }
                    break;
                }
                case 9:
                {
                    PrintChangeForm(id.ToString(), lastName, firstName, patronymic, birthday.ToString(), passport,  salary.ToString(), "");
                    Console.SetCursorPosition(10,9);

                    while (!int.TryParse(Console.ReadLine(), out userId) && !UserExistsById(userId))
                    {
                                if (userId == 0)
                                {
                                    userId = 0;
                               
                                }
                        Console.SetCursorPosition(10,9);

                    }
                    

                    break;
                }
                case -2:
                {
                    Create(new Employee(id, userId,firstName, lastName, patronymic, birthday, salary, patronymic));
                    choice = -1;
                    Console.Clear();
                    break;
                }
                default:
                    break;
            }
        }  
    }
    

    private void PrintChangeForm(string id, string lastName, string firstName, string patronymic, string birthday,string passport, string salary, string userId)
    {
        Console.SetCursorPosition(0,2);
        Console.WriteLine($"  ID:{id}");
        Console.WriteLine($"  Фамилия:{lastName}");
        Console.WriteLine($"  Имя:{firstName}");
        Console.WriteLine($"  Отчество:{patronymic}");
        Console.WriteLine($"  Днюха:{birthday}");
        Console.WriteLine($"  Паспорт:{passport}");
        Console.WriteLine($"  Зпшка:{salary}");
        Console.WriteLine($"  USER ID:{userId}");
    }
    private void ShowEmployees(List<Employee> employees)
    {
        Console.SetCursorPosition(_idPos, 2);
        Console.Write("ID");
        Console.SetCursorPosition(_firstNamePos, 2);
        Console.Write("Имя");
        Console.SetCursorPosition(_lastNamePos, 2);
        Console.Write("Фамилия");
        Console.SetCursorPosition(_patronymicPos, 2);
        Console.Write("Отчество");
        Console.SetCursorPosition(_positionPos, 2);
        Console.Write("Роль");

        var cursorHeight = 3;
        foreach (var employee in employees)
        {
            Console.SetCursorPosition(_idPos, cursorHeight);
            Console.Write($"{employee.ID}");
            Console.SetCursorPosition(_firstNamePos, cursorHeight);
            Console.Write($"{employee.FirstName}");
            Console.SetCursorPosition(_lastNamePos, cursorHeight);
            Console.Write($"{employee.LastName}");
            Console.SetCursorPosition(_patronymicPos, cursorHeight);
            Console.Write($"{employee.Patronymic}");
            Console.SetCursorPosition(_positionPos, cursorHeight);
            Console.Write($"{ReadPositionById(employee.UserID)}");
            cursorHeight++;
        }
    }

    public List<Employee> ReadAllEmployee()
    {
        return _dbContext.Employees;
    }

    public Position ReadPositionById(int id)
    {
        return _dbContext.Users.FirstOrDefault(x => x.ID == id).Position;
    }

    public void Create(Employee employee)
    {
        if (UserExistsById(employee.UserID))
        {
            _dbContext.Employees.Add(employee);
        }
    }

    public bool UserExistsById(int id)
    {
        return _dbContext.Users.Any(x => x.ID == id);
    }

    public List<Employee> ReadById(int id)
    {
        return _dbContext.Employees.Where(x => x.ID == id).ToList();
    }

    public List<Employee> ReadByFirstName(string firstName)
    {
        return _dbContext.Employees.Where(x => string.Equals(x.FirstName , firstName)).ToList();
    }

    public List<Employee> ReadByUserId(int userId)
    {
        return _dbContext.Employees.Where(x => x.UserID == userId).ToList();
    }

    public List<Employee> ReadByLastName(string lastName)
    {
        return _dbContext.Employees.Where(x => String.Equals(x.LastName , lastName)).ToList();
    }

    public List<Employee> ReadByPatronymic(string patronymic)
    {
        return _dbContext.Employees.Where(x => String.Equals(x.Patronymic ,patronymic)).ToList();
    }

    public List<Employee> ReadByBirthDate(DateTime birthdate)
    {
        return _dbContext.Employees.Where(x => DateTime.Equals(x.BirthDate , birthdate)).ToList();
    }

    public List<Employee> ReadBySalary(double salary)
    {
        return _dbContext.Employees.Where(x => Math.Abs(x.Salary - salary) < 0.001).ToList();
    }

    public List<Employee> ReadByPassport(string passport)
    {
        return _dbContext.Employees.Where(x => String.Equals(x.Passport , passport)).ToList();
    }

    public Employee Read(int employeePos)
    {
        return _dbContext.Employees[employeePos];
    }

    public void Update(int employeePos, int id, int userId, string firstName, string lastName, string patronymic,
        DateTime birthDate, double salary, string passport)
    {
        _dbContext.Employees[employeePos] =
            new Employee(id, userId, firstName, lastName, patronymic, birthDate, salary, passport);
    }

    public void Delete(Employee employee)
    {
        _dbContext.Employees.Remove(employee);
    }
    }
}
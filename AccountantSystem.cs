using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public class AccountantSystem : ICrudAccountant
    {
        private readonly int _idPos;
        private readonly int _namePos;
        private readonly int _sumPos;
        private readonly int _datePos;
        private readonly int _transactionType;
        private readonly AppDbContext _dbContext;

        public AccountantSystem()
        {
            _idPos = (int)(Console.WindowWidth * 0.15);
            _namePos = _idPos * 2;
            _sumPos = _idPos * 3;
            _datePos = _idPos * 4;
            _transactionType = _idPos * 5;
            _dbContext = FileManager.GetAppDbContext();
        }


        public void ProceedAccountantSystem(User accountant)
        {
            Console.Clear();
            if (accountant.Position != Position.Accountant)
                return;
            ShowAccountantMenu(accountant, ReadAllRecords());
        }
        private void ShowAccountantMenu(User accountant, List<AccountingRecord> records)
        {
            int choice = 0;
            while (choice != -1)
            {
                ConsoleHelper.ShowHeader(accountant);
                ConsoleHelper.ShowMainSystemKeys();
                ShowRecords(records);
                var numberOfRecords = ReadAllRecords().Count;
                choice = ArrowsMenu.Show(3, 3, numberOfRecords + 2);
                switch (choice)
                {
                    case -5:
                        ShowAddForm(accountant);
                        break;
                    case -4:
                        ShowFindForm(accountant);
                        break;
                    case -6:
                    case -7:
                    case -3:
                    case -2:
                    case -1:
                        break;
                    default:
                        ShowChangeForm(accountant, choice - 3);
                        break;
                }
            }
        }
        private void ShowAddForm(User accountant)
        {
            Console.Clear();

            int choice = 0;
            var id = 0;
            var name = "";
            double sum = 0;
            DateTime date = DateTime.Today;
            bool transactionType = true;

            while (choice != -1)
            {
                ConsoleHelper.ShowHeader(accountant);
                PrintChangeForm(id.ToString(), name, sum.ToString(), date.ToString(), transactionType.ToString());
                ConsoleHelper.ShowChangeSystemKeys();
                choice = ArrowsMenu.Show(2, 2, 6);
                Console.Clear();
                ConsoleHelper.ShowChangeSystemKeys();
                ConsoleHelper.ShowHeader(accountant);
                switch (choice)
                {
                    case 2:
                        {
                            PrintChangeForm("", name, sum.ToString(), date.ToString(), transactionType.ToString());
                            Console.SetCursorPosition(5, 2);
                            while (!int.TryParse(Console.ReadLine(), out id))
                            {
                                Console.SetCursorPosition(5, 2);
                            }
                            break;
                        }
                    case 3:
                        {
                            PrintChangeForm(id.ToString(), "", sum.ToString(), date.ToString(), transactionType.ToString());
                            Console.SetCursorPosition(3, 3);
                            name = Console.ReadLine();
                            break;
                        }
                    case 4:
                        {
                            PrintChangeForm(id.ToString(), name, "", date.ToString(), transactionType.ToString());
                            Console.SetCursorPosition(6, 4);
                            while (!double.TryParse(Console.ReadLine(), out sum))
                            {
                                Console.SetCursorPosition(6, 4);
                            }
                            break;
                        }
                    case 5:
                        {
                            PrintChangeForm(id.ToString(), name, sum.ToString(), "", transactionType.ToString());
                            Console.SetCursorPosition(7, 5);
                            while (!DateTime.TryParse(Console.ReadLine(), out date))
                            {
                                Console.SetCursorPosition(7, 5);
                            }
                            break;
                        }
                    case 6:
                        {
                            PrintChangeForm(id.ToString(), name, sum.ToString(), date.ToString(), "");
                            Console.SetCursorPosition(18, 6);
                            while (!bool.TryParse(Console.ReadLine(), out transactionType))
                            {
                                Console.SetCursorPosition(18, 6);
                            }
                            break;
                        }
                    case -2:
                        {
                            Create(new AccountingRecord(id, name, sum, date, transactionType));
                            choice = -1;
                            Console.Clear();
                            break;
                        }
                    default:
                        break;
                }
            }
        }


        private void ShowFindForm(User accountant)
        {
            int choice = 0;
            while (choice != -1 && choice > 7 || choice < 3)
            {
                Console.Clear();
                ConsoleHelper.ShowHeader(accountant);
                Console.SetCursorPosition(0, 2);
                Console.WriteLine("  > Поиск по: <");
                Console.WriteLine("  ID");
                Console.WriteLine("  Наименование");
                Console.WriteLine("  Сумма");
                Console.WriteLine("  Дата");
                Console.WriteLine("  Приход/вычет");
                choice = ArrowsMenu.Show(3, 3, 7);
            }

            switch (choice)
            {
                case 3:
                    Console.WriteLine("напишите id");
                    int id = 0;
                    while (!int.TryParse(Console.ReadLine(), out id)) ;
                    ShowAccountantMenu(accountant, ReadById(id));
                    break;
                case 4:
                    Console.WriteLine("напишите название");
                    ShowAccountantMenu(accountant, ReadByName(Console.ReadLine()));
                    break;
                case 5:
                    Console.WriteLine("напишите сумму");
                    int sum = 0;
                    while (!int.TryParse(Console.ReadLine(), out sum) || sum < 0) ;
                    ShowAccountantMenu(accountant, ReadBySum(sum));
                    break;
                case 6:
                    Console.WriteLine("напишите дату");
                    DateTime date;
                    while (!DateTime.TryParse(Console.ReadLine(), out date)) ;
                    ShowAccountantMenu(accountant, ReadByDate(date));
                    break;
                case 7:
                    Console.WriteLine("напишите тип операции (приход/вычет: true|false)");
                    bool transactionType;
                    while (!bool.TryParse(Console.ReadLine(), out transactionType)) ;
                    ShowAccountantMenu(accountant, ReadByTransactionType(transactionType));
                    break;
                default:
                    break;
            }
            Console.Clear();

        }

        private void ShowChangeForm(User accountant, int recordPosition)
        {
            int choice = 0;
            var record = Read(recordPosition);
            var id = record.Id;
            var name = record.Name;
            var sum = record.Amount;
            var date = record.Date;
            var transactionType = record.TransactionType;

            while (choice != -1)
            {
                ConsoleHelper.ShowHeader(accountant);
                PrintChangeForm(id.ToString(), name, sum.ToString(), date.ToString(), transactionType.ToString());
                ConsoleHelper.ShowChangeSystemKeys();

                choice = ArrowsMenu.Show(2, 2, 6);

                Console.Clear();
                ConsoleHelper.ShowChangeSystemKeys();
                ConsoleHelper.ShowHeader(accountant);
                switch (choice)
                {
                    case 2:
                        {
                            PrintChangeForm("", name, sum.ToString(), date.ToString(), transactionType.ToString());
                            Console.SetCursorPosition(5, 2);
                            while (!int.TryParse(Console.ReadLine(), out id))
                            {
                                Console.SetCursorPosition(5, 2);
                            }
                            break;
                        }
                    case 3:
                        {
                            PrintChangeForm(id.ToString(), "", sum.ToString(), date.ToString(), transactionType.ToString());
                            Console.SetCursorPosition(7, 3);
                            name = Console.ReadLine();
                            break;
                        }
                    case 4:
                        {
                            PrintChangeForm(id.ToString(), name, "", date.ToString(), transactionType.ToString());
                            Console.SetCursorPosition(6, 4);
                            while (!double.TryParse(Console.ReadLine(), out sum))
                            {
                                Console.SetCursorPosition(6, 4);
                            }
                            break;
                        }
                    case 5:
                        {
                            PrintChangeForm(id.ToString(), name, sum.ToString(), "", transactionType.ToString());
                            Console.SetCursorPosition(7, 5);
                            while (!DateTime.TryParse(Console.ReadLine(), out date))
                            {
                                Console.SetCursorPosition(7, 5);
                            }
                            break;
                        }
                    case 6:
                        {
                            PrintChangeForm(id.ToString(), name, sum.ToString(), date.ToString(), "");
                            Console.SetCursorPosition(18, 6);
                            while (!bool.TryParse(Console.ReadLine(), out transactionType))
                            {
                                Console.SetCursorPosition(18, 6);
                            }
                            break;
                        }
                    case -2:
                        {
                            Update(recordPosition, id, name, sum, date, transactionType);
                            break;
                        }
                    case -3:
                        Delete(record);
                        break;
                    default:
                        break;
                }
            }
        }

        private void PrintChangeForm(string id, string name, string sum, string date, string transactionType)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID:{id}");
            Console.WriteLine($"  Название:{name}");
            Console.WriteLine($"  Сумма:{sum}");
            Console.WriteLine($"  Дата:{date}");
            Console.WriteLine($"  Приход/вычет:{transactionType}");
        }


        private void ShowRecords(List<AccountingRecord> records)
        {
            Console.SetCursorPosition(_idPos-8, 2);
            Console.Write("ID");
            Console.SetCursorPosition(_namePos-12, 2);
            Console.Write("Наименование");
            Console.SetCursorPosition(_sumPos-12, 2);
            Console.Write("Сумма");
            Console.SetCursorPosition((_datePos-12), 2);
            Console.Write("Дата");
            Console.SetCursorPosition((_transactionType - 15), 2);
            Console.Write("Приход/вычет");
            double sum = 0;

            var cursorHeight = 3;

            Console.SetCursorPosition(91, cursorHeight + 3);
            Console.WriteLine("***************************");
            Console.SetCursorPosition((int)(0.8 * Console.WindowWidth), cursorHeight + 4);
            Console.Write($"Итог = {sum}");

            foreach (var record in records)
            {
                Console.SetCursorPosition(_idPos-8, cursorHeight);
                Console.Write($"{record.Id}");
                Console.SetCursorPosition(_namePos-14, cursorHeight);
                Console.Write($"{record.Name}");
                Console.SetCursorPosition(_sumPos-12, cursorHeight);
                Console.Write($"{record.Amount}");
                Console.SetCursorPosition(_datePos-18, cursorHeight);
                Console.Write($"{record.Date}");
                Console.SetCursorPosition((_transactionType-9), cursorHeight);
                Console.Write($"{record.TransactionType}");

                cursorHeight++;
                sum = record.TransactionType ? sum + record.Amount : sum - record.Amount;
            }

        
        }

        public void Create(AccountingRecord record)
        {
            _dbContext.AccountingRecords.Add(record);
        }

        public AccountingRecord Read(int idPos)
        {
            return _dbContext.AccountingRecords[idPos];
        }

        public void Update(int recordPosition, int id, string name, double amount, DateTime date, bool transactionType)
        {
            _dbContext.AccountingRecords[recordPosition] = new AccountingRecord(id, name, amount, date, transactionType);
        }

        public void Delete(AccountingRecord record)
        {
            _dbContext.AccountingRecords.Remove(record);
        }

        public List<AccountingRecord> ReadAllRecords()
        {
            return _dbContext.AccountingRecords;
        }

        public List<AccountingRecord> ReadById(int id)
        {
            return _dbContext.AccountingRecords.Where(x => x.Id == id).ToList();
        }

        public List<AccountingRecord> ReadByName(string name)
        {
            return _dbContext.AccountingRecords.Where(x => String.Equals(x.Name, name)).ToList();
        }

        public List<AccountingRecord> ReadBySum(double sum)
        {
            return _dbContext.AccountingRecords.Where(x => Math.Abs(x.Amount - sum) < 0.001).ToList();
        }

        public List<AccountingRecord> ReadByDate(DateTime dateTime)
        {
            return _dbContext.AccountingRecords.Where(x => DateTime.Equals(x.Date, dateTime)).ToList();
        }

        public List<AccountingRecord> ReadByTransactionType(bool transactionType)
        {
            return _dbContext.AccountingRecords.Where(x => x.TransactionType == transactionType).ToList();
        }
    }
}

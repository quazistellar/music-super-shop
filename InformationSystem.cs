using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public class InformationSystem
    {
        private static AppDbContext _appDbContext;

        public InformationSystem()
        {
            _appDbContext = FileManager.GetAppDbContext();
        }

        public void Start()
        {
            Console.Clear();
            while (true)
            {
                var user = LoginSystem.ProceedLogin();

                switch (user.Position)
                {
                    case Position.Admin:
                        new AdminSystem().ProceedAdminSystem(user);
                        break;
                    case Position.Hr:
                        new HrSystem().ProceedHrSystem(user);
                        break;
                    case Position.WarehouseManager:
                        new WareHouseSystem().ProceedWareHouseSystem(user);
                        break;
                    case Position.Cashier:
                        new CashierSystem().ProceedCashierSystem(user);
                        break;
                    case Position.Accountant:
                        new AccountantSystem().ProceedAccountantSystem(user);
                        break;

                }
                Console.Clear();
                Console.WriteLine("Чтобы выйти и сохранить изменения - нажмите N, иначе Enter, чтобы вернуться в программу");

                if (Console.ReadKey().Key == ConsoleKey.N)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Вы завершили работу с программой!");
                    Console.ResetColor();
                    break;
                }
            }
            _appDbContext.Dispose();
        }
    }
}

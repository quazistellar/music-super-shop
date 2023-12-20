using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public class AppDbContext : IDisposable
    {
        public List<User> Users { get; private set; }
        public List<Employee> Employees { get; private set; }
        public List<Product> Products { get; private set; }
        public List<SelectedProduct> SelectedProducts { get; private set; }
        public List<AccountingRecord> AccountingRecords { get; private set; }
        public AppDbContext(AppDbContextWrapper wrapper)
        {
            Users = wrapper.Users;
            Employees = wrapper.Employees;
            Products = wrapper.Products;
            SelectedProducts = wrapper.SelectedProducts;
            AccountingRecords = wrapper.AccountingRecords;
        }

        public AppDbContext()
        {
            Users = new List<User>
        {
            new User(1, "admin", "admin", Position.Admin)
        };
            Employees = new List<Employee>();
            Products = new List<Product>();
            SelectedProducts = new List<SelectedProduct>();
            AccountingRecords = new List<AccountingRecord>();
        }

        public User GetUser(string login, string password)
        {
            return Users.FirstOrDefault(user => user.Login == login && user.Password == password);
        }
        public void Dispose()
        {
            FileManager.SaveAppDbContext(this);
        }
    }
}

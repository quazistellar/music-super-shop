using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{ 
        public class AppDbContextWrapper
        {
            public List<User> Users { get; set; }
            public List<Employee> Employees { get; set; }
            public List<Product> Products { get; set; }
            public List<SelectedProduct> SelectedProducts { get; set; }
            public List<AccountingRecord> AccountingRecords { get; set; }
        }
}

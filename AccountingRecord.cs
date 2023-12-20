using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public class AccountingRecord
    {
        public AccountingRecord(int id, string name, double amount, DateTime date, bool transactionType)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Date = date;
            TransactionType = transactionType;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public bool TransactionType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public interface ICrudAccountant
    {
        void Create(AccountingRecord record);
        AccountingRecord Read(int idPos);
        void Update(int recordPosition, int id, string name, double amount, DateTime date, bool transactionType);
        void Delete(AccountingRecord record);
        List<AccountingRecord> ReadAllRecords();
        List<AccountingRecord> ReadById(int id);
        List<AccountingRecord> ReadByName(string name);
        List<AccountingRecord> ReadBySum(double sum);
        List<AccountingRecord> ReadByDate(DateTime date);
        List<AccountingRecord> ReadByTransactionType(bool transactionType);
    }
}

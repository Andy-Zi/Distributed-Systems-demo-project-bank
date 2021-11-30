using MyBank.Server.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MyBank.Server.Backend.Repository
{
    public class TransactionRepository : BaseRepository<Transaction>
    {
        public TransactionRepository() : base("Transactions.json")
        {

        }

        public IEnumerable<Transaction> GetTransactions(string accountNumber)
        {
            var result = new List<Transaction>();
            lock (LockObject)
            {
                result.AddRange(Entities.Values.Where(t => t.SenderAccount == accountNumber || t.RecieverAccount == accountNumber));
                result = result.OrderByDescending(x => x.Date).ToList();
            }
            return result;
        }
    }
}

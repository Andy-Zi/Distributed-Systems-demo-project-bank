using MyBank.Interfaces;
using MyBank.Nameservice;
using System;

namespace MyBank.Server.Backend.Model
{
    public class Transaction : ITransaction, IEntity
    {
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public string SenderAccount { get; set; }
        public string RecieverAccount { get; set; }
        public string ID { get; set; }

        public Transaction()
        {
            ID = Guid.NewGuid().ToString("N");
        }
        public string GetMappingKey()
        {
            return ID;
        }
    }
}

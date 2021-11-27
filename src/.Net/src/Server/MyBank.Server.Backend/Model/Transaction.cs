using MyBank.Interfaces;
using MyBank.Nameservice;
using System;

namespace MyBank.Server.Backend.Model
{
    internal class Transaction : ITransaction
    {
        public TransactionStyle Style { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
    }
}

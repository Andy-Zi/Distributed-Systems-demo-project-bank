using MyBank.Interfaces;
using System;
using System.Collections.Generic;

namespace MyBank.Server.Backend.Model
{
    public class Account : IAccount
    {
        public string Owner { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
        public List<ITransaction> Transactions { get; set; }

        public Account()
        {
            OnInitialize();
        }

        private void OnInitialize()
        {
            AccountNumber = Guid.NewGuid().ToString("N");
        }
    }
}

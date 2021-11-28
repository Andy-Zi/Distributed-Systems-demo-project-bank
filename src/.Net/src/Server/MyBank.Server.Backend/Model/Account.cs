﻿using MyBank.Interfaces;
using System;
using System.Collections.Generic;

namespace MyBank.Server.Backend.Model
{
    public class Account : IAccount, IEntity
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

        public Account(Account other,bool detailed=true)
        {
            this.Value = other.Value;
            this.AccountNumber = other.AccountNumber;
            this.Description = other.Description;
            this.Owner = other.Owner;
            this.Transactions = new List<ITransaction>();

            //Copy and break referenzes
            if (detailed)
                foreach (var transaktion in other.Transactions)
                    this.Transactions.Add(transaktion);

        }

        private void OnInitialize()
        {
            AccountNumber = Guid.NewGuid().ToString("N");
            Transactions = new List<ITransaction>();

        }

        public string GetMappingKey()
        {
            return AccountNumber;
        }
    }
}

using MyBank.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyBank.Server.Backend.Model
{
    [DataContract]
    [KnownType(typeof(List<Transaction>))]
    [KnownType(typeof(Transaction))]
    public class Account : IAccount, IEntity
    {
        [DataMember]
        public string Owner { get; set; }
        [DataMember]
        public string AccountNumber { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public float Value { get; set; }
        [DataMember]
        public List<ITransaction> Transactions { get; set; }

        public Account()
        {
            OnInitialize();
        }

        public Account(Account other)
        {
            this.Value = other.Value;
            this.AccountNumber = other.AccountNumber;
            this.Description = other.Description;
            this.Owner = other.Owner;
            this.Transactions = new List<ITransaction>();
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

using MyBank.Interfaces;
using MyBank.Nameservice;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace MyBank.Server.Backend.Model
{
    [DataContract]
    public class Transaction : ITransaction, IEntity
    {
        [DataMember]
        public float Amount { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string Comment { get; set; }
        [DataMember]
        public string SenderAccount { get; set; }
        [DataMember]
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

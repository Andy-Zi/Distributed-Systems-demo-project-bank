using MyBank.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.CCOMConnector.Model
{
    internal class CCOMAccount : IAccount
    {
        [JsonIgnore]
        public string Owner 
        {
            get => OwnerID.ToString();
            set => OwnerID = Int32.Parse(value);
        }

        public string Description { get; set; }
        public float Value { get; set; }

        [JsonIgnore]
        public List<ITransaction> Transactions { get; set; }

        [JsonIgnore]
        public string AccountNumber
        {
            get => AccNumber.ToString();
            set => AccNumber = Int32.Parse(value);
        }
        public int OwnerID { get; set; }
        public int AccNumber { get; set; }
    }
}

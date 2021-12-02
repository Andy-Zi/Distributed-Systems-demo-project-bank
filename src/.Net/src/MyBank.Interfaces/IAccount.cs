
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyBank.Interfaces
{
    public interface IAccount
    {
        string Owner { get; set; }
        string AccountNumber { get; set; }
        string Description { get; set; }
        float Value { get; set; }
        List<ITransaction> Transactions { get; set; }

    }
}

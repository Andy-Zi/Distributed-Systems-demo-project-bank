using MyBank.Nameservice;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank.Interfaces
{
    public interface ITransaction
    {
        TransactionStyle Style { get; set; }
        float Amount { get; set; }
        DateTime Date { get; set; }
        string Comment { get; set; }
    }
}

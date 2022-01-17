using System;

namespace MyBank.Interfaces
{
    public interface ITransaction
    {
        float Amount { get; set; }
        DateTime Date { get; set; }
        string Comment { get; set; }
        string SenderAccount { get; set; }
        string RecieverAccount { get; set; }
        string RecieverName { get; set; }
    }
}

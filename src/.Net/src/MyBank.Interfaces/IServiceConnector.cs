
using System.Collections.Generic;

namespace MyBank.Interfaces
{
    public interface IServiceConnector
    {
        string Login(string username, string password);
        void NewUser(string token, string username, string password);
        string NewAccount(string token, string username, string description);
        List<(string AccountNumber, string Description)> ListAccounts(string token);
        void PayInto(string token, string accountNumber, float amount);
        void Transfere(string token, string from_accountNumber, string to_accountNumber, string recieverName, float amount, string comment = "");
        List<IAccount> Statement(string token, string account_number = "", bool detailed = true);
        void Bye(string token);

        void Connect(string address, int port);
        void Disconnect();

        bool IsConnected();
    }
}

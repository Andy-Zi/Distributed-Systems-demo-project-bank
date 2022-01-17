
using MyBank.Interfaces;
using MyBank.Server.Backend.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace MyBank.Server.WCF
{
    [ServiceContract]

    public interface IWCFBankService
    {
        [FaultContract(typeof(UserFault))]
        [OperationContract]
        string Login(string username, string password);

        [FaultContract(typeof(UserFault))]
        [OperationContract]
        void NewUser(string token, string username, string password);
        [FaultContract(typeof(UserFault))]
        [OperationContract]
        string NewAccount(string token, string username, string description);
        [FaultContract(typeof(UserFault))]
        [OperationContract]
        List<(string AccountNumber, string Description)> ListAccounts(string token);
        [FaultContract(typeof(UserFault))]
        [OperationContract]
        void PayInto(string token, string accountNumber, float amount);
        [FaultContract(typeof(UserFault))]
        [OperationContract]
        void Transfere(string token, string from_accountNumber, string to_accountNumber, string recieverName ,float amount, string comment = "");
        [FaultContract(typeof(UserFault))]
        [OperationContract]
        List<Account> Statement(string token, string account_number = "", bool detailed = true);
        [FaultContract(typeof(UserFault))]
        [OperationContract]
        void Bye(string token);

    }
}

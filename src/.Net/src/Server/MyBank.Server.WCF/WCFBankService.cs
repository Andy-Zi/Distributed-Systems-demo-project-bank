using MyBank.Interfaces;
using MyBank.Server.Backend;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using Unity;

namespace MyBank.Server.WCF
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class WCFBankService : IWCFBankService
    {
        [Dependency] public BankService BankService { get; set; }
        public WCFBankService()
        {

        }

        /// <summary>
        /// Wrapps the Exception to be send over HTTP
        /// </summary>
        protected void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                var fault = new UserFault(ex);
                var reason = new FaultReason(ex.Message);
                throw new FaultException<UserFault>(fault, reason);
            }
        }
        public List<(string AccountNumber, string Description)> ListAccounts(string token)
        {
            var result = new List<(string AccountNumber, string Description)>();
            Execute(() => { result = BankService.ListAccounts(token); });
            return result;
        }

        public string Login(string username, string password)
        {
            var result = string.Empty;
            Execute(() => { result = BankService.Login(username, password); });
            return result;
        }

        public string NewAccount(string token, string username, string description)
        {
            var result = string.Empty;
            Execute(() => { result = BankService.NewAccount(token, username, description); });
            return result;
        }

        public void NewUser(string token, string username, string password)
        {
            Execute(() =>
            {
                BankService.NewUser(token, username, password);
            });
        }

        public void PayInto(string token, string accountNumber, float amount)
        {
            Execute(() =>
            {
                BankService.PayInto(token, accountNumber, amount);
            });
        }

        public List<IAccount> Statement(string token, string account_number = "", bool detailed = true)
        {
            var result = new List<IAccount>();
            Execute(() =>
            {
                result = BankService.Statement(token, account_number, detailed);
            });
            return result;
        }

        public void Transfere(string token, string from_accountNumber, string to_accountNumber, float amount, string comment = "")
        {
            Execute(() =>
            {
                BankService.Transfere(token, from_accountNumber, to_accountNumber, amount, comment);
            });
        }

        public void Bye(string token)
        {
            Execute(() =>
            {
                BankService.Bye(token);
            });
        }
    }
}

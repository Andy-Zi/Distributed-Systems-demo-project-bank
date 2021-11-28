using MyBank.Interfaces;
using MyBank.Nameservice;
using MyBank.Server.Backend.Model;
using MyBank.Server.Backend.Repository;
using System;
using System.Collections.Generic;
using Unity;

namespace MyBank.Server.Backend
{
    public class BankService : IDisposable
    {
        [Dependency] public UserRepository UserRepository { get; set; }
        [Dependency] public AccountRepository AccountRepository { get; set; }
        [Dependency] public AuthenticationService AuthenticationService { get; set; }

        public BankService()
        {

        }

        protected void Authenticate(string token, Privileges privilege = Privileges.User)
        {
            if (!AuthenticationService.Authenticate(token, privilege))
                throw new Exception("Not permitted to use this funktion! Are you logged in with the correct user-account?");
        }

        public void Initialize()
        {
            UserRepository.Load();
            AccountRepository.Load();
        }

        public void Dispose()
        {
            UserRepository.Save();
            AccountRepository.Save();
        }

        public string Login(string username,string password)
        {
            return AuthenticationService.LogIn(username, password);
        }

        public void NewUser(string token, string username, string password)
        {
            Authenticate(token, Privileges.Admin);
            if (UserRepository.Entities.ContainsKey(username))
                throw new Exception("Username already Exists!");

            var user = new User()
            {
                Username = username,
                Password = password
            };

            UserRepository.Entities.TryAdd(user.GetMappingKey(), user);
        }

        public string NewAccount(string token, string username, string description)
        {
            Authenticate(token, Privileges.Admin);
            if (!UserRepository.Entities.ContainsKey(username))
                throw new Exception("Username doesn't exist!");

            var account = new Account()
            {
                Owner = username,
                Value = 0.0f,
                Description = description
            };
            AccountRepository.Entities.TryAdd(account.GetMappingKey(), account);
            return account.AccountNumber;
        }

        public List<(string AccountNumber,string Description)> ListAccounts(string token)
        {
            Authenticate(token, Privileges.User);
            List<(string AccountNumber, string Description)> result = new List<(string AccountNumber, string Description)>();

            var username = AuthenticationService.LoggedInUsers[token];
            foreach(var account in AccountRepository.GetAccounts(username))
            {
                result.Add((account.AccountNumber, account.Description));
            }
            return result;
        }

        public void PayInto(string token,string accountNumber,float amount)
        {
            Authenticate(token, Privileges.Admin);
            if (!AccountRepository.Entities.ContainsKey(accountNumber))
                throw new Exception($"No account with number '{accountNumber}' exists!");

            AccountRepository.Entities[accountNumber].Value += amount;
        }

        public void Transfere(string token, string from_accountNumber,string to_accountNumber, float amount, string comment = "")
        {
            Authenticate(token, Privileges.User);

            if (!AccountRepository.Entities.ContainsKey(from_accountNumber))
                throw new Exception($"Source Account with number '{from_accountNumber}' doesn't exist!");

            if (!AccountRepository.Entities.ContainsKey(to_accountNumber))
                throw new Exception($"Target Account with number '{to_accountNumber}'  doesn't exist!");

            var from_Account = AccountRepository.Entities[from_accountNumber];
            var username = AuthenticationService.LoggedInUsers[token];
            if (from_Account.Owner != username)
                throw new Exception($"You dont have access to Source Account with number '{from_accountNumber}'!");

            //Force a Lock here to ensure a threadsave List.Add()
            lock (AccountRepository.LockObject)
            {
                var to_Account = AccountRepository.Entities[to_accountNumber];
                var executionTime = DateTime.Now;

                from_Account.Value -= amount;
                to_Account.Value += amount;

                from_Account.Transactions.Add(new Transaction()
                {
                    Date = executionTime,
                    Amount = amount,
                    Comment = comment,
                    Style = TransactionStyle.Send
                });

                to_Account.Transactions.Add(new Transaction()
                {
                    Date = executionTime,
                    Amount = amount,
                    Comment = comment,
                    Style = TransactionStyle.Recieved
                });
            }
        }

        public List<IAccount> Statement(string token,string account_number = "",bool detailed=true)
        {
            Authenticate(token, Privileges.User);

            var result = new List<IAccount>();
            if (!string.IsNullOrEmpty(account_number))
            {
                if (!AccountRepository.Entities.ContainsKey(account_number))
                    throw new Exception($"Account with number '{account_number}' doesn't exist!");

                var account = AccountRepository.Entities[account_number];
                var username = AuthenticationService.LoggedInUsers[token];

                if(account.Owner != username)
                    throw new Exception($"You dont have access to account with number'{account_number}'!");

                result.Add(new Account(account, detailed));

            }
            else
            {
                var username = AuthenticationService.LoggedInUsers[token];
                foreach (var account in AccountRepository.GetAccounts(username))
                {
                    result.Add(new Account(account, detailed));
                }
            }
            return result;

        }

    }
}

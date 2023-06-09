﻿using MyBank.Interfaces;
using MyBank.Nameservice;
using MyBank.Nameservice.Exceptions;
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
        [Dependency] public TransactionRepository TransactionRepository { get; set; }
        [Dependency] public AuthenticationService AuthenticationService { get; set; }

        public BankService()
        {

        }

        protected void Authenticate(string token, Privileges privilege = Privileges.User)
        {
            if (!AuthenticationService.Authenticate(token, privilege))
                throw new AuthenticationException("Not permitted to use this funktion! Are you logged in with the correct user-account?");
        }

        public void Initialize()
        {
            UserRepository.Load();
            AccountRepository.Load();
            TransactionRepository.Load();
        }

        public void Dispose()
        {
            UserRepository.Save();
            AccountRepository.Save();
            TransactionRepository.Save();
        }

        public string Login(string username,string password)
        {
            return AuthenticationService.LogIn(username, password);
        }

        public void NewUser(string token, string username, string password)
        {
            Authenticate(token, Privileges.Admin);
            if (UserRepository.Entities.ContainsKey(username))
                throw new ArgumentException("Username already Exists!");

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
                throw new ArgumentException("Username doesn't exist!");

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
            var user = UserRepository.Entities[username];
            foreach(var account in AccountRepository.GetAccounts(username, user.Privilege == Privileges.Admin))
            {
                result.Add((account.AccountNumber, account.Description));
            }
            return result;
        }

        public void PayInto(string token,string accountNumber,float amount)
        {
            Authenticate(token, Privileges.Admin);
            if (!AccountRepository.Entities.ContainsKey(accountNumber))
                throw new ArgumentException($"No account with number '{accountNumber}' exists!");

            if (amount < 0.01)
                throw new ArgumentException($"PayInto Amount has to be 0,01 or higher!");

           
            AccountRepository.Entities[accountNumber].Value += amount;

            var transaction = new Transaction
            {
                Amount = amount,
                SenderAccount = "ADMIN",
                RecieverAccount = accountNumber,
                Date = DateTime.Now,
                Comment = "ADMIN Payed into your accont",
                RecieverName = AccountRepository.Entities[accountNumber].Owner
            };

            TransactionRepository.Entities.TryAdd(transaction.GetMappingKey(), transaction);
        }

        public void Transfere(string token, string from_accountNumber,string to_accountNumber, string recieverName, float amount, string comment = "")
        {
            Authenticate(token, Privileges.User);

            if (!AccountRepository.Entities.ContainsKey(from_accountNumber))
                throw new ArgumentException($"Source Account with number '{from_accountNumber}' doesn't exist!");

            if (!AccountRepository.Entities.ContainsKey(to_accountNumber))
                throw new ArgumentException($"Target Account with number '{to_accountNumber}'  doesn't exist!");

            if (amount < 0.01)
                throw new ArgumentException($"Transaction Amount has to be 0,01 or higher!");

            if (!string.IsNullOrEmpty(recieverName))
            {
                if (AccountRepository.Entities.TryGetValue(to_accountNumber, out var targetAccount))
                {
                    if (!targetAccount.Owner.Equals(recieverName, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new ArgumentException($"Reciever '{recieverName}' does not own Account '{to_accountNumber}' the Transaction was canceled");
                    }
                }
            }

            var from_Account = AccountRepository.Entities[from_accountNumber];
            var username = AuthenticationService.LoggedInUsers[token];
            if (from_Account.Owner != username)
                throw new ArgumentException($"You dont have access to Source Account with number '{from_accountNumber}'!");

            if(from_Account.Value < amount)
                throw new ArgumentException($"You dont have enough money to make this transaction!");

            //Force a Lock here to ensure a threadsave List.Add()
            lock (AccountRepository.LockObject)
            {
                var to_Account = AccountRepository.Entities[to_accountNumber];

                from_Account.Value -= amount;
                to_Account.Value += amount;

                var tranaction = new Transaction()
                {
                    SenderAccount = from_Account.AccountNumber,
                    RecieverAccount = to_Account.AccountNumber,
                    Date = DateTime.Now,
                    Amount = amount,
                    Comment = comment,
                    RecieverName = recieverName ?? string.Empty,
                };

                TransactionRepository.Entities.TryAdd(tranaction.GetMappingKey(), tranaction);
            }
        }

        public List<IAccount> Statement(string token,string account_number = "",bool detailed=true)
        {

            Account CollectAccountData(Account a)
            {
                var cloned_account = new Account(a);
                if (detailed)
                    cloned_account.Transactions.AddRange(TransactionRepository.GetTransactions(cloned_account.AccountNumber));
                return cloned_account;
            }
            Authenticate(token, Privileges.User);

            var result = new List<IAccount>();
            if (!string.IsNullOrEmpty(account_number))
            {
                if (!AccountRepository.Entities.ContainsKey(account_number))
                    throw new ArgumentException($"Account with number '{account_number}' doesn't exist!");

                var account = AccountRepository.Entities[account_number];
                var username = AuthenticationService.LoggedInUsers[token];
                var user = UserRepository.Entities[username];

                if (account.Owner != username && user.Privilege == Privileges.User)
                    throw new AuthenticationException($"You dont have access to account with number'{account_number}'!");

                result.Add(CollectAccountData(account));
            }
            else
            {
                var username = AuthenticationService.LoggedInUsers[token];
                var user = UserRepository.Entities[username];
                foreach (var account in AccountRepository.GetAccounts(username, user.Privilege == Privileges.Admin))
                {
                    result.Add(CollectAccountData(account));
                }
            }
            return result;

        }

        public void Bye(string token)
        {
            Authenticate(token, Privileges.User);
            AuthenticationService.LogOut(token);
        }
    }
}

using MyBank.CCOMConnector.Model;
using MyBank.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyBank.CCOMConnector
{
    public class CCOMServiceConnector : IServiceConnector
    {
        const string CCOMDLLGUID = "e96db8ed-48d8-47ae-b9d6-9cdf447ccc57";
        MyBankCCOMLib.IMyBankATL ccomClient;
        JsonSerializer serializer;

        public CCOMServiceConnector()
        {
            serializer = new JsonSerializer() { NullValueHandling = NullValueHandling.Ignore };
        }

        /// <summary>
        /// Remap HRESULT Exceptions to matching Exception Types
        /// </summary>
        protected void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                //switch (faultException.Detail.Type)
                //{
                //    case nameof(AuthenticationException):
                //        throw new AuthenticationException(faultException.Detail.Message);
                //    case nameof(LoginException):
                //        throw new LoginException(faultException.Detail.Message);
                //    case nameof(ArgumentException):
                //        throw new ArgumentException(faultException.Detail.Message);
                //}
                throw ex;
            }
        }

        /// <summary>
        ///  Deserialize JSON-String to Objects
        /// </summary>
        protected TType Deserialize<TType>(string input)
        {
            if (string.IsNullOrEmpty(input))
                return default;

            using (TextReader sr = new StringReader(input))
            {
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<TType>(reader);
                }
            }
        }


        public void Connect(string address = "127.0.0.1", int port = 0)
        {
            Type type = Type.GetTypeFromCLSID(new Guid(CCOMDLLGUID), address, false);
            ccomClient = (MyBankCCOMLib.IMyBankATL)Activator.CreateInstance(type);
        }

        public void Disconnect()
        {
            ccomClient = null;
        }

        public bool IsConnected()
        {
            return ccomClient != null;
        }

        public void Bye(string token)
        {
            Execute(() =>
            {
                ccomClient.Logout(Int32.Parse(token));
            });
        }

        public List<(string AccountNumber, string Description)> ListAccounts(string token)
        {
            var result = new List<(string AccountNumber, string Description)>();
            Execute(() =>
            {
               var json = ccomClient.ListAccounts(Int32.Parse(token));
               var objects = Deserialize<List<CCOMAccountDescription>>(json);
                if(objects != null)
                {
                    //Map Objects to Tupples
                    foreach (var accountDescription in objects)
                    {
                        result.Add((accountDescription.AccountNumber.ToString(), accountDescription.Description));
                    }
                }
            });
            return result;
        }

        public string Login(string username, string password)
        {
            string token = string.Empty;
            Execute(() =>
            {
                token = ccomClient.Login(username, password).ToString();
            });

            return token;
        }

        public string NewAccount(string token, string username, string description)
        {
            string accountNumber = string.Empty;
            Execute(() =>
            {
                accountNumber = ccomClient.NewAccount(Int32.Parse(token), username,description).ToString();
            });
            return accountNumber;
        }

        public void NewUser(string token, string username, string password)
        {
            Execute(() =>
            {
                ccomClient.NewUser(Int32.Parse(token), username, password);
            });
        }

        public void PayInto(string token, string accountNumber, float amount)
        {
            Execute(() =>
            {
                ccomClient.PayInto(Int32.Parse(token), Int32.Parse(accountNumber), amount);
            });
        }


        public void Transfere(string token, string from_accountNumber, string to_accountNumber, float amount, string comment = "")
        {
            Execute(() =>
            {
                ccomClient.Transfer(Int32.Parse(token), Int32.Parse(from_accountNumber), Int32.Parse(to_accountNumber), amount, comment);
            });
        }

        public List<IAccount> Statement(string token, string account_number = "", bool detailed = true)
        {
            var result = new List<IAccount>();
            Execute(() =>
            {
                var json = ccomClient.Statement(Int32.Parse(token), Int32.Parse(account_number), detailed ? 1 : 0);
                var statements = Deserialize<List<CCOMStatement>>(json);

                //Map and Cast CCOMAccount to IAccount

                foreach(var statement in statements)
                {
                    statement.Account.Transactions = new List<ITransaction>(statement.Transactions.Cast<ITransaction>());
                    result.Append(statement.Account);
                }
            });

            return result;
        }

    
    }
}

using MyBank.CCOMConnector.Model;
using MyBank.Interfaces;
using MyBank.Nameservice.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

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
                if(ex is COMException comException)
                {
                    //Error Code for not reachable
                    if(comException.ErrorCode == -2147023174)
                    {
                        throw new ServerNotReachableException(ex);
                    }
                }
                if(ex is ArgumentException)
                {
                    var errorMessage = ccomClient.GetError();
                    throw new ArgumentException(errorMessage);
                }
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
            try
            {
                Type type = Type.GetTypeFromCLSID(new Guid(CCOMDLLGUID), address, false);
                ccomClient = (MyBankCCOMLib.IMyBankATL)Activator.CreateInstance(type);
            }catch(Exception ex)
            {
                throw new ServerNotReachableException(ex);
            }
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


        public void Transfere(string token, string from_accountNumber, string to_accountNumber,string recieverName ,float amount, string comment = "")
        {
            Execute(() =>
            {
                ccomClient.Transfer(Int32.Parse(token), Int32.Parse(from_accountNumber), Int32.Parse(to_accountNumber), recieverName, amount, comment);
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
                    result.Add(statement.Account);
                }
            });

            return result;
        }

    
    }
}

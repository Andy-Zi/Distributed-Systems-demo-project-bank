using MyBank.Interfaces;
using MyBank.Nameservice.Exceptions;
using MyBank.Server.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using IWCFBankService = MyBank.Server.WCF.IWCFBankService;

namespace MyBank.WCFConnector
{
    public class WCFServiceConnector : IServiceConnector
    {
        ChannelFactory<IWCFBankService> factory = new ChannelFactory<IWCFBankService>(new BasicHttpBinding());

        IWCFBankService Client;

        /// <summary>
        /// Remap HTTP Exceptions to matching Exception Types
        /// </summary>
        protected void Execute(Action action)
        {
            try
            {
                action();
            }catch(FaultException<UserFault> faultException)
            {
                switch (faultException.Detail.Type)
                {
                    case nameof(AuthenticationException):
                        throw new AuthenticationException(faultException.Detail.Message);
                    case nameof(LoginException):
                        throw new LoginException(faultException.Detail.Message);
                    case nameof(ArgumentException):
                        throw new ArgumentException(faultException.Detail.Message);
                }
            }catch(Exception ex)
            {
                if (ex is EndpointNotFoundException)
                    throw new ServerNotReachableException(ex);
                throw;
            }
        }
        public void Bye(string token)
        {
            Execute(() => { Client.Bye(token); });
        }

        public void Connect(string address, int port)
        {
            var url = $"http://{address}:{port}/WCFBankService";
            Client = factory.CreateChannel(new EndpointAddress(url));
        }

        public void Disconnect()
        {
            Client = null;
        }

        public bool IsConnected()
        {
            return Client != null;
        }

        public List<(string AccountNumber, string Description)> ListAccounts(string token)
        {
            var result = new List<(string AccountNumber, string Description)>();
            Execute(() => { result = Client.ListAccounts(token).ToList(); });
            return result;
        }

        public string Login(string username, string password)
        {
            var result = string.Empty;
            Execute(() =>
            {
                result = Client.Login(username, password);
            });
            return result;
        }

        public string NewAccount(string token, string username, string description)
        {
            var result = string.Empty;
            Execute(() =>
            {
                result = Client.NewAccount(token, username, description);
            });
            return result;
        }

        public void NewUser(string token, string username, string password)
        {
            Execute(() =>
            {
                Client.NewUser(token, username, password);
            });
        }

        public void PayInto(string token, string accountNumber, float amount)
        {
            Execute(() =>
            {
                Client.PayInto(token, accountNumber, amount);
            });
        }

        public List<IAccount> Statement(string token, string account_number = "", bool detailed = true)
        {
            var result = new List<IAccount>();
            Execute(() =>
            {
                result = Client.Statement(token, account_number, detailed).Cast<IAccount>().ToList();
            });
            return result;
        }

        public void Transfere(string token, string from_accountNumber, string to_accountNumber, float amount, string comment = "")
        {
            Execute(() =>
            {
                Client.Transfere(token, from_accountNumber, to_accountNumber, amount, comment);
            });
        }
    }
}

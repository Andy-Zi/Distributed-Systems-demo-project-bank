using MyBank.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank.CCOMConnector
{
    public class CCOMServiceConnector : IServiceConnector
    {
        MyBankCCOMLib.IMyBankATL ccomClient;
        public void Bye(string token)
        {
            throw new NotImplementedException();
        }

        public void Connect(string address, int port)
        {
            Type type = Type.GetTypeFromCLSID(new Guid("e96db8ed-48d8-47ae-b9d6-9cdf447ccc57"),"127.0.0.1", false);
            ccomClient = (MyBankCCOMLib.IMyBankATL)Activator.CreateInstance(type);
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public List<(string AccountNumber, string Description)> ListAccounts(string token)
        {

        }

        public string Login(string username, string password)
        {
            var test = ccomClient.Login(username, password);
            return test.ToString();
        }

        public string NewAccount(string token, string username, string description)
        {
            throw new NotImplementedException();
        }

        public void NewUser(string token, string username, string password)
        {
            throw new NotImplementedException();
        }

        public void PayInto(string token, string accountNumber, float amount)
        {
            throw new NotImplementedException();
        }

        public List<IAccount> Statement(string token, string account_number = "", bool detailed = true)
        {
            throw new NotImplementedException();
        }

        public void Transfere(string token, string from_accountNumber, string to_accountNumber, float amount, string comment = "")
        {
            throw new NotImplementedException();
        }
    }
}

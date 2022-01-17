using MyBank.Interfaces;
using MyBank.Server.Backend;
using System.Collections.Generic;
using System.Web.Http;

namespace MyBank.Server.RestAPI.Controllers
{
    [RoutePrefix("{controller}/{action}")]
    public class BankController : ApiController
    {
        private readonly BankService bankService;

        public BankController(BankService bankService)
        {
            this.bankService = bankService;
        }
  
        [HttpGet]
        [ActionName("Login")]
        public string Login(string username,string password)
        {
            return this.bankService.Login(username, password);
        }
        [HttpGet]
        [ActionName("NewUser")]
        public void NewUser(string token, string username, string password)
        {
            this.bankService.NewUser(token, username, password);
        }

        [HttpGet]
        [ActionName("ListAccounts")]
        public List<(string AccountNumber, string Description)> ListAccounts(string token)
        {
            return this.bankService.ListAccounts(token);
        }

        [HttpGet]
        [ActionName("PayInto")]
        public void PayInto(string token, string accountNumber, float amount)
        {
            this.bankService.PayInto(token, accountNumber, amount);
        }

        [HttpGet]
        [ActionName("NewAccount")]
        public string NewAccount(string token, string username, string description)
        {
            return this.bankService.NewAccount(token, username, description);
        }

        [HttpGet]
        [ActionName("Statement")]
        public List<IAccount> Statement(string token, string account_number, bool detailed)
        {
            return this.bankService.Statement(token, account_number, detailed);
        }

        [HttpGet]
        [ActionName("Transfere")]
        public void Transfere(string token, string from_accountNumber, string to_accountNumber,string recieverName, float amount, string comment = "")
        {
            this.bankService.Transfere(token, from_accountNumber, to_accountNumber, recieverName, amount, comment);
        }

        [HttpGet]
        [ActionName("Bye")]
        public void Bye(string token)
        {
            this.bankService.Bye(token);
        }
    }
}
using MyBank.Interfaces;
using MyBank.Nameservice.Exceptions;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyBank.Client
{
    /// <summary>
    /// Class to catch the errors from the ServiceConnector and wrap them into an Messagebox
    /// </summary>
    public class ErrorHandlingServiceConnector : IServiceConnector
    {
        private readonly IServiceConnector service;
        MainForm mainForm;
        public bool Faulted;

        public void RegisterMainForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }
        protected void HandleError(Exception exception)
        {
            Faulted = true;
            switch (exception)
            {
                case AuthenticationException _:
                    MessageBox.Show(mainForm, "Your Session Expired!\nThe Application will close now!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    mainForm.Close();
                    break;
                default:
                    MessageBox.Show(mainForm, exception.Message,"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

            }
        }
        public ErrorHandlingServiceConnector(IServiceConnector service)
        {
            this.service = service;
        }

        public void Bye(string token)
        {
            Faulted = false;
            try
            {
                service.Bye(token);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public void Connect(string address, int port)
        {
            Faulted = false;
            try
            {
                service.Connect(address, port);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public void Disconnect()
        {
            Faulted = false;
            try
            {
                service.Disconnect();
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }



        public bool IsConnected()
        {
            Faulted = false;
            try
            {
                return service.IsConnected();
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return false;
            }
        }

        public List<(string AccountNumber, string Description)> ListAccounts(string token)
        {
            Faulted = false;
            try
            {
                return service.ListAccounts(token);
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return new List<(string AccountNumber, string Description)>();
            }
        }

        public string Login(string username, string password)
        {
            Faulted = false;
            try
            {
                return service.Login(username, password);
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return string.Empty;
            }
        }

        public string NewAccount(string token, string username, string description)
        {
            Faulted = false;
            try
            {
                return service.NewAccount(token, username, description);
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return string.Empty;
            }
        }

        public void NewUser(string token, string username, string password)
        {
            Faulted = false;
            try
            {
                service.NewUser(token, username, password);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public void PayInto(string token, string accountNumber, float amount)
        {
            Faulted = false;
            try
            {
                service.PayInto(token, accountNumber, amount);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        public List<IAccount> Statement(string token, string account_number = "", bool detailed = true)
        {
            Faulted = false;
            try
            {
                return service.Statement(token, account_number, detailed);
            }
            catch (Exception ex)
            {
                HandleError(ex);
                return new List<IAccount>();
            }
        }

        public void Transfere(string token, string from_accountNumber, string to_accountNumber, float amount, string comment = "")
        {
            Faulted = false;
            try
            {
                service.Transfere(token, from_accountNumber, to_accountNumber, amount, comment);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }
    }
}

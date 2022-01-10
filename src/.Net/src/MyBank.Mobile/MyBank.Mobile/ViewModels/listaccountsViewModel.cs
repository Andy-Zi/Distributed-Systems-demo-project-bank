using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    public class listaccountsViewModel : ContentPage
    {
        public listaccountsViewModel()
        {
            ListAccounts = new Command(OnListAccounts);
        }
        public ICommand ListAccounts { get; }
        string _listAccounts = "new Account";
        public List<(string AccountNumber, string Description)> ListAccount_res;

        public string ListAccountsstr
        {
            get { return _listAccounts; }
            set { _listAccounts = value; OnPropertyChanged(); }
        }

        void OnListAccounts()
        {
            ListAccount_res = App.mybank.ListAccounts(App.Token);

        }
    }
}
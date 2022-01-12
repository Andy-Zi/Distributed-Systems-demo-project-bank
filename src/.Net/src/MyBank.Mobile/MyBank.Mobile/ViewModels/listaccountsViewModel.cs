using MyBank.Mobile.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.CommunityToolkit;
using System.Threading.Tasks;
using Command = MvvmHelpers.Commands.Command;
using MyBank.Mobile.Views;



namespace MyBank.Mobile.ViewModels
{
    public class listaccountsViewModel : BaseViewModel
    {
        public AsyncCommand<object> SelectedCommand { get; }
        public listaccountsViewModel()
        {
            ListAccounts = new Command(OnListAccounts);
            SelectedCommand = new AsyncCommand<object>(Selected);
        }
        public ICommand ListAccounts { get; }
        string _listAccounts = "list Accounts";
        private List<Statement_item> _accountlist;

        Statement_item _selectedaccount;
        public Statement_item SelectedAccount
        {
            get { return _selectedaccount; }
            set => SetProperty(ref _selectedaccount, value);
        }

        async Task Selected(object args)
        {
            var s = args as Statement_item;
            if(s == null)
                return;

            SelectedAccount = null;
            var route = $"{nameof(statement)}?Account_Number_Statement={s.Accountnumber}";
            await Shell.Current.GoToAsync(route);
        }

        public string ListAccountsstr
        {
            get { return _listAccounts; }
            set { _listAccounts = value; OnPropertyChanged(); }
        }

        public List<Statement_item> ListAccount_res
        {
            get { return _accountlist; }
            set { _accountlist = value; OnPropertyChanged(); }
        }

        void OnListAccounts()
        {
            try
            {
                IsBusy = true;
                ListAccount_res = null;
                ListAccount_res = App.mybank.ListAccounts(App.Token).Select(tuple => new Statement_item(tuple)).ToList();
                IsBusy = false;
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error:", ex.Message, "Ok");
                IsBusy = false;
            }
    
        }
    }
}
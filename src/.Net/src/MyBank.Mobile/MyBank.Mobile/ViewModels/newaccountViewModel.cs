using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    public class newaccountViewModel : BaseViewModel
    {
        public newaccountViewModel()
        {
            NewAccount = new Command(OnNewAccount);
        }
        public ICommand NewAccount { get; }
        string _username;
        string _description;
        string _newAccount = "new Account";

        public string NewAccountstr
        {
            get { return _newAccount; }
            set { _newAccount = value; OnPropertyChanged(); }
        }

        public string Username_new_acc
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        public string Description_new_acc
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        void OnNewAccount()
        {
            try
            {
                IsBusy = true;
                NewAccountstr = App.mybank.NewAccount(App.Token, _username, _description);
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

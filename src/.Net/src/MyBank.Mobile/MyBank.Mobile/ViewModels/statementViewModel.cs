using MyBank.Interfaces;
using MyBank.Server.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    [QueryProperty(nameof(Account_Number_Statement),nameof(Account_Number_Statement))]
    public class statementViewModel : ContentPage
    {
        public string Account_Number_Statement { get; set; }
        public statementViewModel()
        {
            Statement = new Command(OnStatement);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            OnStatement();
        }
        public ICommand Statement { get; }

        List<IAccount> _statement;

        public List<IAccount> Statement_res
        {
            get { return _statement; }
            set { _statement = value; OnPropertyChanged(); }
        }

        void OnStatement()
        {
            try
            {
                IsBusy = true;
                Statement_res = null;
                Statement_res = App.mybank.Statement(App.Token, Account_Number_Statement, true);
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
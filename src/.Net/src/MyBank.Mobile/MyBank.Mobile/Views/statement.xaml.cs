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
using MyBank.Server.Backend.Model;
using MyBank.Interfaces;

namespace MyBank.Mobile.Views
{
    [QueryProperty(nameof(Account_Number_Statement), nameof(Account_Number_Statement))]
    public partial class statement : ContentPage
    {
        public string Account_Number_Statement { get; set; }
        public statement()
        {
            InitializeComponent();
            Statement = new Command(OnStatement);
        }

        public ICommand Statement { get; }

        Transaction _selectedtransaction;
        public Transaction SelectedTransaction
        {
            get { return _selectedtransaction; }
            set {
                if (value != null)
                {
                    value = null;
                }
                _selectedtransaction = value;
                OnPropertyChanged();
                }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = this;
            OnStatement();
        }

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
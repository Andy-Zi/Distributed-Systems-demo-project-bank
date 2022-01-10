using MyBank.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    public class statementViewModel : ContentPage
    {
        public statementViewModel()
        {
            Statement = new Command(OnStatement);
        }
        public ICommand Statement { get; }
        string _account_number;
        bool _detailed;
        string _statement = "Statement";
        List<IAccount> Statement_res;
        public string Statementstr
        {
            get { return _statement; }
            set { _statement = value; OnPropertyChanged(); }
        }

        public string Account_Number_Statement
        {
            get { return _account_number; }
            set { _account_number = value; OnPropertyChanged(); }
        }

        public bool Detailed_Statement
        {
            get { return _detailed; }
            set { _detailed = (bool)value; OnPropertyChanged(); }
        }

        void OnStatement()
        {
            Statement_res = App.mybank.Statement(App.Token, _account_number, _detailed);

        }
    }
}
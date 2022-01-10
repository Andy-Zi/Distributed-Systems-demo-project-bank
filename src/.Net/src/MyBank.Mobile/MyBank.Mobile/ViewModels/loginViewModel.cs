using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    internal class loginViewModel : BindableObject
    {
        
        public loginViewModel()
        {
            Login = new Command(OnLogin);
        }
        public ICommand Login { get; }
        string _username = "lukas";
        string _password = "1234";
        string _token = "login";
        public string Token
        {
            get { return _token; }
            set { _token = value; OnPropertyChanged(); }
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        void OnLogin()
        {
            App.Token = App.mybank.Login(this._username,this._password);
            Token = App.Token;
        }
    }
}

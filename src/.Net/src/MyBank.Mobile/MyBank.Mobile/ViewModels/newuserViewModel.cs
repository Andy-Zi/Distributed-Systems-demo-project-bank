﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    public class newuserViewModel : ContentPage
    {
        public newuserViewModel()
        {
            NewUser = new Command(OnNewUser);
        }
        public ICommand NewUser { get; }
        string _username;
        string _password;
        string _newAccount = "new User";

        public string NewUserstr
        {
            get { return _newAccount; }
            set { _newAccount = value; OnPropertyChanged(); }
        }

        public string Username_new_usr
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        public string Password_new_usr
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        void OnNewUser()
        {
            App.mybank.NewUser(App.Token, _username, _password);

        }
    }
}
using MyBank.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    [QueryProperty(nameof(Connect_color),nameof(Connect_color)),
     QueryProperty(nameof(Login_color), nameof(Login_color))]
    public class startViewModel : ContentPage
    {
        public startViewModel()
        {
            Connect_Clicked = new Command(OnConnect_Clicked);
            Login_Clicked = new Command(OnLogin_Clicked);
            if(App.Token != "") { Login_color = "green"; }
            else { Login_color = "red"; }
            if (App.mybank.IsConnected()) { Connect_color = "green"; }
            else { Connect_color = "red"; }
        }

        public ICommand Login_Clicked { get; }
        private string _login_color = "red";
        public string Login_color
        {
            get { return _login_color; }
            set { _login_color = value; OnPropertyChanged(); }
        }

        async void OnLogin_Clicked()
        {
            var route = $"{nameof(login)}";
            await Shell.Current.GoToAsync(route);
        }

        public ICommand Connect_Clicked { get; }
        private string _connect_color = "red";
        public string Connect_color
        {
            get { return _connect_color; }
            set { _connect_color = value; OnPropertyChanged(); }
        }

        async void OnConnect_Clicked()
        {
            var route = $"{nameof(connect)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}
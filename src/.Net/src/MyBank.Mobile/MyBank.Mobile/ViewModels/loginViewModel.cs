using MyBank.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    public class loginViewModel : BaseViewModel
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

        async void OnLogin()
        {
            try
            {
                IsBusy = true;
                App.Token = App.mybank.Login(this._username, this._password);
                Token = App.Token;
                IsBusy = false;
                if(Token != null)
                {
                    var route = $"..?Login_color=green";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {
                    var route = $"..?Login_color=red";
                    await Shell.Current.GoToAsync(route);
                    await Application.Current.MainPage.DisplayAlert("Error:", "Connecttion Failed", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error:", ex.Message, "Ok");
                IsBusy = false;
            }
            
        }
    }
}

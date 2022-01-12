using MyBank.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    public class connectViewModel : BaseViewModel
    {
        public connectViewModel()
        {
            Connect = new Command(OnConnect);
        }
        public ICommand Connect { get; }
        string _ip_address = "192.168.178.58";
        int _port = 8080;

        public string IP_Address
        {
            get { return _ip_address; }
            set { _ip_address = value; OnPropertyChanged(); }
        }

        public int Port
        {
            get { return _port; }
            set { _port = (int)value; OnPropertyChanged(); }
        }

        async void OnConnect()
        {
            try
            {
                IsBusy = true;
                App.mybank.Connect(this._ip_address, this._port);
                IsBusy = false;
                if (App.mybank.IsConnected())
                {
                    var route = $"..?Connect_color=green";
                    await Shell.Current.GoToAsync(route);
                }
                else
                {
                    var route = $"..?Connect_color=red";
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
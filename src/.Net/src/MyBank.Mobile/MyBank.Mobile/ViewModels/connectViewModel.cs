using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    public class connectViewModel : ContentPage
    {
        public connectViewModel()
        {
            Connect = new Command(OnConnect);
        }
        public ICommand Connect { get; }
        string _ip_address = "192.168.178.58";
        int _port = 8080;
        string _connect = "connect";

        public string Connected
        {
            get { return _connect; }
            set { _connect = value; OnPropertyChanged(); }
        }

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

        void OnConnect()
        {
            App.mybank.Connect(this._ip_address, this._port);
            if (App.mybank.IsConnected()) { Connected = "Juhu!"; }
        }
    }
}
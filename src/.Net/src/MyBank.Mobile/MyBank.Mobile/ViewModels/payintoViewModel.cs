using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    public class payintoViewModel : BaseViewModel
    {
        public payintoViewModel()
        {
            PayInto = new Command(OnPayInto);
        }
        public ICommand PayInto { get; }
        string _accountnumber = "35abfe63138f43098eb3d7f9a6eb45c3";
        float _amount;
        string _payinto = "Pay Into";

        public string PayIntostr
        {
            get { return _payinto; }
            set { _payinto = value; OnPropertyChanged(); }
        }

        public string Accountnumber_payinto
        {
            get { return _accountnumber; }
            set { _accountnumber = value; OnPropertyChanged(); }
        }

        public float Amount_payinto
        {
            get { return _amount; }
            set { _amount = (float)value; OnPropertyChanged(); }
        }

        void OnPayInto()
        {
            try
            {
                IsBusy = true;
                App.mybank.PayInto(App.Token, _accountnumber, _amount);
                Application.Current.MainPage.DisplayAlert("Success:", "Amount Deposited", "Ok");
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
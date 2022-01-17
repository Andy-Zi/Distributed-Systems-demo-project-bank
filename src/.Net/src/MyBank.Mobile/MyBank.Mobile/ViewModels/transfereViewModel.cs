using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    public class transfereViewModel : BaseViewModel
    {
        public transfereViewModel()
        {
            Transfere = new Command(OnTransfere);
        }
        public ICommand Transfere { get; }
        string _from_acc_nr = "791e840e0848468caa161a7beb665524";
        string _to_acc_nr = "34ad2e8ad5914b72bc8463e79f362184";
        string _transfere = "Transfere";
        float _amount = 10;
        string _comment = "Das ist ein Komentar";
        string _recipiant_name = "lukas";

        public string Transferestr
        {
            get { return _transfere; }
            set { _transfere = value; OnPropertyChanged(); }
        }
        public string From_AccountNumber_Transfere
        {
            get { return _from_acc_nr; }
            set { _from_acc_nr = value; OnPropertyChanged(); }
        }

        public string To_AccountNumber_Transfere
        {
            get { return _to_acc_nr; }
            set { _to_acc_nr = value; OnPropertyChanged(); }
        }

        public string Recipiant_Name_Transfere
        {
            get { return _recipiant_name; }
            set { _recipiant_name = value; OnPropertyChanged(); }
        }
        public float Amount_Transfere
        {
            get { return _amount; }
            set { _amount = (float)value; OnPropertyChanged(); }
        }

        public string Comment_Transfere
        {
            get { return _comment; }
            set { _comment = value; OnPropertyChanged(); }
        }


        void OnTransfere()
        {
            try
            {
                IsBusy = true;
                App.mybank.Transfere(App.Token, _from_acc_nr, _to_acc_nr, _recipiant_name, _amount, _comment);
                Application.Current.MainPage.DisplayAlert("Succes:", "Money has been transfered.", "Ok");
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
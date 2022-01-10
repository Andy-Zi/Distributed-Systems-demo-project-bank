using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    public class transfereViewModel : ContentPage
    {
        public transfereViewModel()
        {
            Transfere = new Command(OnTransfere);
        }
        public ICommand Transfere { get; }
        string _from_acc_nr;
        string _to_acc_nr;
        string _transfere = "Transfere";
        float _amount;
        string _comment;

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
            App.mybank.Transfere(App.Token, _from_acc_nr,_to_acc_nr,_amount,_comment) ;

        }
    }
}
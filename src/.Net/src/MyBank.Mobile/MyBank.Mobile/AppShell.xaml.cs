using MyBank.Mobile.ViewModels;
using MyBank.Mobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyBank.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(connect), typeof(connect));
            Routing.RegisterRoute(nameof(listaccounts), typeof(listaccounts));
            Routing.RegisterRoute(nameof(login), typeof(login));
            Routing.RegisterRoute(nameof(newaccount), typeof(newaccount));
            Routing.RegisterRoute(nameof(newuser), typeof(newuser));
            Routing.RegisterRoute(nameof(payinto), typeof(payinto));
            Routing.RegisterRoute(nameof(statement), typeof(statement));
            Routing.RegisterRoute(nameof(transfere), typeof(transfere));
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            //try
            //{
                App.mybank.Bye(App.Token);
                App.Token = "";
            //}
            //catch (Exception ex)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error:", ex.Message, "Ok");
            //}
            
        }

        private void OnDisconnectClicked(object sender, EventArgs e)
        {
            App.mybank.Disconnect();
        }
    }
}

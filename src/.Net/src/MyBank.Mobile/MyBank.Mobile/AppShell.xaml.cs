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
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            App.mybank.Bye(App.Token);
        }

        private void OnDisconnectClicked(object sender, EventArgs e)
        {
            App.mybank.Disconnect();
        }

        //private async void OnMenuItemClicked(object sender, EventArgs e)
        //{
        //    await Shell.Current.GoToAsync("//LoginPage");
        //}
    }
}

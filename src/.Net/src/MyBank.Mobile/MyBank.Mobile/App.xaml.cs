using MyBank.Mobile.Services;
using MyBank.Mobile.Views;
using MyBank.RESTConnector;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyBank.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            mybank = new RESTServiceConnector();
            InitializeComponent();
            
            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }
        public static RESTServiceConnector mybank;
        public static string Token = "";
        public static string Username = "";

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

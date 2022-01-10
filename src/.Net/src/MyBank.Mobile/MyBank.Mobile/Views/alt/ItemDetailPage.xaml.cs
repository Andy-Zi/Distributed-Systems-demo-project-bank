using MyBank.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyBank.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
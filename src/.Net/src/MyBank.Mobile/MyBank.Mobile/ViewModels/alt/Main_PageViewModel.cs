using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyBank.Mobile.ViewModels
{
    public class Main_PageViewModel : BindableObject
    {
        public Main_PageViewModel()
        {
            IncreaseCount = new Command(OnIncrease);
        }

        public ICommand IncreaseCount { get; }
        int count = 0;
        string countDisplay = "ClickMe!";
        public string CountDisplay
        {
            get => countDisplay;
            set
            {
                if (value == countDisplay)
                    return;
                countDisplay = value;
                OnPropertyChanged(nameof(CountDisplay));
            }
        }

        void OnIncrease()
        {
            count++;
            CountDisplay = $"You clicked {count} time(s)";
        }
    }
}

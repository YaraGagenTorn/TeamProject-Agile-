using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutoCartApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryList : ContentPage
    {
        public HistoryList()
        {
            InitializeComponent();            
            HistoryItems.ItemsSource = App.History;
        }
    }
}

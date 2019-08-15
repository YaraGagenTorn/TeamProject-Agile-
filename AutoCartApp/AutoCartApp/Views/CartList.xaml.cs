using AutoCartApp.Handlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutoCartApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartList : ContentPage
    {
        public List<CartHandler.Item> Items { get; set; }
        public int total { get; set; }

        public CartList()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Items = App.database.GetCart(App.currentUser.Id);
            CartListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}

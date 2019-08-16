using AutoCartApp.Handlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutoCartApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartList : ContentPage, INotifyPropertyChanged
    {
        List<CartHandler.Item> items;
        public List<CartHandler.Item> Items
        {
            get
            {
                if (items == null)
                    items = App.database.GetCart(App.currentUser.Id);
                return items;
            }
            set => items = value;
        }
        public string Total
        {
            get
            {
                float total = 0;
                foreach (CartHandler.Item item in Items)
                    total += item.GetCost();
                return "$ " + total;
            }
        }
        public Command CompleteOrder { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public CartList()
        {
            BindingContext = this;
            Title = "Cart";
            CompleteOrder = new Command(() => Device.BeginInvokeOnMainThread(ProcessOrder), () => Total != "$ 0");

            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Items = App.database.GetCart(App.currentUser.Id);
            CartListView.ItemsSource = Items;
            NotifyPropertyChanged("Total");
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            await App.Current.MainPage.Navigation.PushModalAsync(
                new ProductDetails(((CartHandler.Item)((ListView)sender).SelectedItem).Product));
        }
        async void ProcessOrder()
        {
            System.Console.Out.WriteLine("Processing Order");
            bool result = await this.DisplayAlert("Congratulation", "Order completed Sucessfull", "Yes", "Cancel");
            if (result)
            {
                App.database.SaveCart(App.currentUser.Id, new CartHandler.Item[0]);
                await Navigation.PushAsync(new ProductsList());
                await Navigation.PopAsync();
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(propertyName));
        }
    }
}

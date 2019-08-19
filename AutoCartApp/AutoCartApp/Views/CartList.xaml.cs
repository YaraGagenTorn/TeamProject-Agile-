using AutoCartApp.Handlers;
using AutoCartApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static AutoCartApp.Handlers.CartHandler;

namespace AutoCartApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartList : ContentPage
    {
        CartSorting sorted = CartSorting.New_Old;
        ObservableCollection<Item> items;
        public ObservableCollection<Item> Items
        {
            get
            {
                if (items == null)
                    items = new ObservableCollection<Item>(App.Database.GetCart(App.CurrentUser.Id));
                return items;
            }
            set => items = value;
        }
        public string Total
        {
            get
            {
                float total = 0;
                foreach (Item item in Items)
                    total += item.GetCost();
                return "$ " + total;
            }
        }
        public Command CompleteOrder { get; set; }
        public Command SortCommand { get; set; }
        public CartList()
        {
            BindingContext = this;
            Title = "Cart";
            CompleteOrder = new Command(() => Device.BeginInvokeOnMainThread(ProcessOrder), () => Total != "$ 0");
            SortCommand = new Command(() => SortCart());
            InitializeComponent();
            RefreshCart();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            items = null;
            SortCart(false);
            RefreshCart();
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            await App.Current.MainPage.Navigation.PushModalAsync(
                new ProductDetails(((Item)((ListView)sender).SelectedItem).Product));
        }
        async void ProcessOrder()
        {
            System.Console.Out.WriteLine("Processing Order");
            bool result = await this.DisplayAlert("Confirmation", $"Order will remove a total of {Total} from your acount\n Press Yes to confirm", "Yes", "Cancel");
            if (result)
            {
                App.History.Add(new HistoryItem(HistoryType.OrderCompleted) { Title = Total });
                App.Database.SaveCart(App.CurrentUser.Id, new Item[0]);
                items = null;
                RefreshCart();
            }
        }
        void SortCart(bool proceed = true)
        {
            if (proceed) sorted = (CartSorting)((((int)sorted) + 1) % 3);
            switch (sorted)
            {
                case CartSorting.New_Old:
                    items = null;
                    break;
                case CartSorting.Cheap_Expensive:
                    items = new ObservableCollection<Item>(Items.OrderBy(x => x.Product.Price));
                    break;
                case CartSorting.Alpha_Zeta:
                    items = new ObservableCollection<Item>(Items.OrderBy(x => x.Product.Title));
                    break;
            }
            RefreshCart();
        }
        void RefreshCart()
        {
            CartListView.ItemsSource = null;
            CartListView.ItemsSource = Items;
            SortButton.Text = SortButtonLabel();
        }
        string SortButtonLabel() {

            switch (sorted)
            {
                default: return "Date Added";
                case CartSorting.Cheap_Expensive: return "Item Price";
                case CartSorting.Alpha_Zeta: return "Alphabetical";
            }
        }
    }
}

using AutoCartApp.Handlers;
using AutoCartApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutoCartApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetails : ContentPage
    {
        public Product Product { get; }
        public int Quantity { get; set; }
        public bool InCart { get => App.Cart.Contains(new CartHandler.Item(Product.Id, 1)); }
        public int CartQuantity { get => InCart ? GetCartQuantity(Product.Id) : 0; }
        public Command AddToCart { get; }
        public Command RemoveProduct { get; }
        public Command Cancel { get; }
        public ProductDetails(Product product)
        {
            BindingContext = this;
            Product = product;
            Title = product.Title;
            Quantity = 1;
            AddToCart = new Command(PutInCart, () => Quantity > 0);
            RemoveProduct = new Command(RemoveFromCart);
            Cancel = new Command(() => App.Current.MainPage.Navigation.PopModalAsync());
            InitializeComponent();
            productImg.Source = ImageSource.FromResource($"AutoCartApp.Assets.stock_{Product.ImageId}.jpg");
        }
        void RemoveFromCart()
        {
            CartHandler.Item ci = new CartHandler.Item(Product.Id, Quantity);
            List<CartHandler.Item> Items = App.Database.GetCart(App.CurrentUser.Id);
            if (Items.Contains(ci))
            {
                Items.Remove(Items[Items.FindIndex(ci.Equals)]);
                App.Database.SaveCart(App.CurrentUser.Id, Items.ToArray());
                App.History.Add(new HistoryItem(HistoryType.RemoveProduct)
                    { Product = Product, Title = $"Removed {Product.Title} from cart" });
            }
            App.Current.MainPage.Navigation.PopModalAsync();
        }
        void PutInCart()
        {
            if(Quantity < 1) {
                this.DisplayAlert("Invalid Quantity", "Must be aleast 1 item", "Okay");
                return;
            }

            CartHandler.Item ci = new CartHandler.Item(Product.Id, Quantity);
            User user = App.CurrentUser;
            List<CartHandler.Item> Items = App.Database.GetCart(user.Id);
            if (Items.Contains(ci)) {
                Items[Items.FindIndex(ci.Equals)].Quantity += Quantity;
                App.History.Add(new HistoryItem(HistoryType.ChangeQuantity)
                    { Product = Product, Title = $"Merged {Quantity} of {Product.Title} into cart" });
            }
            else
            {
                Items.Add(ci);
                App.History.Add(new HistoryItem(HistoryType.AddProduct)
                    { Product = Product, Title = $"Added {Quantity} of {Product.Title}" });
            }
            App.Database.SaveCart(user.Id, Items.ToArray());
            App.Current.MainPage.Navigation.PopModalAsync();
            //System.Console.Out.WriteLine(CartHandler.Compress(Items.ToArray()));
        }
        int GetCartQuantity(int Id)
        {
            int index = App.Cart.FindIndex(new CartHandler.Item(Id, 1).Equals);
            return App.Cart[index].Quantity;
        }
    }
}
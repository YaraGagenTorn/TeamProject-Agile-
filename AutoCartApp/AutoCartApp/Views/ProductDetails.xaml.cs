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
        public Command AddToCart { get; }
        public Command Cancel { get; }
        public ProductDetails(Product product)
        {
            BindingContext = this;
            Product = product;
            Title = product.Title;
            Quantity = 1;
            AddToCart = new Command(PutInCart, () => Quantity > 0);
            Cancel = new Command(() => App.Current.MainPage.Navigation.PopModalAsync());
            InitializeComponent();
            productImg.Source = ImageSource.FromResource($"AutoCartApp.Assets.stock_{Product.ImageId}.jpg");
        }
        void PutInCart()
        {
            CartHandler.Item ci = new CartHandler.Item(Product.Id, Quantity);
            List<CartHandler.Item> Items = App.database.GetCart(App.currentUser.Id);
            if (App.Cart.Contains(ci)) {
                int index = App.Cart.FindIndex(ci.Equals);
                Items[index].Quantity += Quantity;
                App.database.SaveCart(App.currentUser.Id, Items.ToArray());
            }
            else
            {
                Items.Add(ci);
                App.database.SaveCart(App.currentUser.Id, Items.ToArray());
            }
            App.database.SaveCart(App.currentUser.Id, App.Cart.ToArray());
            System.Console.Out.WriteLine(CartHandler.Compress(App.Cart.ToArray()));
            App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
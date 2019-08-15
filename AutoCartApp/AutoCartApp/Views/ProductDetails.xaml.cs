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
        public Product product { get; }
        public int Quantity { get; set; }
        public Command AddToCart { get; }
        public Command Cancel { get; }
        public ProductDetails(Product product)
        {
            BindingContext = this;
            this.product = product;
            Title = product.title;
            Quantity = 1;
            AddToCart = new Command(addToCart, () => Quantity > 0);
            Cancel = new Command(() => App.Current.MainPage.Navigation.PopModalAsync());
            InitializeComponent();
            productImg.Source = ImageSource.FromResource($"AutoCartApp.Assets.stock_{product.imageId}.jpg");
        }
        void addToCart()
        {
            CartHandler.Item ci = new CartHandler.Item(product.Id, Quantity);
            if (App.Cart.Contains(ci)) {
                App.Cart[App.Cart.FindIndex(0, 1, ci.Equals)].Quantity += Quantity;
            }
            else
                App.Cart.Add(ci);
            System.Console.Out.WriteLine("Adding item to cart");
            System.Console.Out.WriteLine(App.Cart);
            App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
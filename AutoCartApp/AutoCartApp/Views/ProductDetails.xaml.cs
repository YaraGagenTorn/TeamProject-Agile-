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
            if(Quantity < 1)
            {
                this.DisplayAlert("Invalid Quantity", "Must be aleast 1 item", "Okay");
                return;
            }

            CartHandler.Item ci = new CartHandler.Item(Product.Id, Quantity);
            User user = App.CurrentUser;
            List<CartHandler.Item> Items = App.Database.GetCart(user.Id);
            if (App.Cart.Contains(ci)) {
                int index = App.Cart.FindIndex(ci.Equals);
                Items[index].Quantity += Quantity;
                App.Database.SaveCart(user.Id, Items.ToArray());
                App.History.Add(new HistoryItem(HistoryType.ChangeQuantity) { Product = ci.Product, Title = "Product merged"});
            }
            else
            {
                Items.Add(ci);
                App.Database.SaveCart(user.Id, Items.ToArray());
                App.History.Add(new HistoryItem(HistoryType.AddProduct) { Product = ci.Product });
            }
            App.Database.SaveCart(user.Id, App.Cart.ToArray());
            System.Console.Out.WriteLine(CartHandler.Compress(App.Cart.ToArray()));
            App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
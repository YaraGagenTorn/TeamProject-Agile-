﻿using AutoCartApp.Models;
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
    public partial class ProductsList : ContentPage
    {
        public List<Product> Products { get; set; }

        public ProductsList()
        {
            Title = "Products list view";
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Products = App.database.GetProducts();
            ProductsListView.ItemsSource = Products;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            await App.Current.MainPage.Navigation.PushModalAsync(new ProductDetails((Product)((ListView)sender).SelectedItem));
        }
    }
}

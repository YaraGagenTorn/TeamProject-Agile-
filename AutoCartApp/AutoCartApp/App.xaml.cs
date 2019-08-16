using AutoCartApp.Handlers;
using AutoCartApp.Models;
using AutoCartApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutoCartApp
{
    public partial class App : Application
    {
        static Database DB;
        public static Database database
        {
            get
            {
                if (DB == null) DB = new Database();
                return DB;
            }
        }
        public static User currentUser { get; set; }
        public static List<CartHandler.Item> Cart {
            get => database.GetCart(currentUser.Id);
            set => database.SaveCart(currentUser.Id, value.ToArray());
        }

        public App()
        {
            currentUser = database.GetUser("admin");
            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new Main();
            //MainPage = new NavigationPage(new CartList());
            InitializeComponent();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

using AutoCartApp.Handlers;
using AutoCartApp.Models;
using AutoCartApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutoCartApp
{
    public partial class App : Application
    {
        static Database DB;
        public static Database Database
        {
            get
            {
                if (DB == null) DB = new Database();
                return DB;
            }
        }
        public static User CurrentUser { get; set; }
        public static ObservableCollection<HistoryItem> History { get; set; }
        public static List<CartHandler.Item> Cart {
            get => Database.GetCart(CurrentUser.Id);
        }

        public App()
        {
            History = new ObservableCollection<HistoryItem>();
            CurrentUser = Database.GetUser("admin");
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

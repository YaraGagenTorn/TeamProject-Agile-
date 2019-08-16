using AutoCartApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AutoCartApp.Handlers
{
    public class Database
    {
        static object locker = new object();

        SQLiteConnection database;

        public Database()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            //database.DropTable<Product>();
            database.CreateTable<Product>();
            database.CreateTable<User>();
            if (database.Table<User>().Count() < 1)
                AddUser(new User("admin", "yeet", "", ""));
            if (database.Table<Product>().Count() < 1)
            {
                AddProduct(new Product("Bunch O' Bananas", 5.99f, "The No1 choice for a healthy fruit thats high in vitamins", "Fruit, Potasium", "bbananas"));
                AddProduct(new Product("Organic Rice 1kg", 4.49f, "Imported fresh to you", "Grain, Fiber", "orice"));
                AddProduct(new Product("Tegel frozen chicken", 11f, "Free range chicken", "Meat, Protien, Selenium", "tchicken"));
                //AddProduct(new Product("Placeholder", 10.99f, "Missing Image file test", "Meat, Protien, Selenium", "yeet"));
            }
        }
        public bool AddProduct(Product Product)
        {
            lock (locker)
            {
                return database.Insert(Product) > 0;
            };
        }
        public Product GetProduct(int id)
        {
            lock (locker)
            {
                return database.Find<Product>(id);
            }
        }
        public List<Product> GetProducts()
        {
            lock (locker)
            {
                return database.Query<Product>("SELECT * FROM Product");
            }
        }
        public int GetProductKey()
        {
            lock (locker)
            {
                return database.Table<Product>().OrderByDescending(t => t.Id).First().Id;
            }
        }
        public bool SaveProduct(Product Product)
        {
            lock (locker)
            {
                return database.InsertOrReplace(Product) > 0;
            };
        }
        public int DelProduct(int id)
        {
            lock (locker)
            {
                return database.Delete<Product>(id);
            }
        }

        public bool AddUser(User User)
        {
            lock (locker)
            {
                return database.Insert(User) > 0;
            };
        }
        public User GetUser(int id)
        {
            lock (locker)
            {
                return database.Find<User>(id);
            }
        }
        public User GetUser(string userName)
        {
            lock (locker)
            {
                List<User> users = database.Query<User>($"SELECT * FROM User WHERE UserName='{userName}'");
                if (users.Count > 0) return users[0];
                else return null;
            }
        }
        public List<User> GetUsers()
        {
            lock (locker)
            {
                return database.Query<User>("SELECT * FROM User");
            }
        }
        public bool SaveUser(User User)
        {
            lock (locker)
            {
                return database.InsertOrReplace(User) > 0;
            };
        }
        public int DelUser(int id)
        {
            lock (locker)
            {
                return database.Delete<User>(id);
            }
        }
        public List<CartHandler.Item> GetCart(int id)
        {
            lock (locker)
            {
                return CartHandler.Decompress(database.Find<User>(id).ProductCart);
            }
        }
        public bool SaveCart(int id, CartHandler.Item[] cart)
        {
            lock (locker)
            {
                User user = database.Find<User>(id);
                user.ProductCart = CartHandler.Compress(cart);
                return database.InsertOrReplace(user) > 0;
            }
        }
    }
}

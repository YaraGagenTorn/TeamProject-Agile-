using AutoCartApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static AutoCartApp.Handlers.CartHandler;

namespace AutoCartApp.Handlers
{
    public enum CartSorting
    {
        New_Old,
        Alpha_Zeta,
        Cheap_Expensive
    }
    public class CartHandler
    {
        public class Item : IEquatable<Item>
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
            Product product;
            public Product Product {
                get {
                    if (product == null)
                        product = App.Database.GetProduct(Id);
                    return product;
                }
            }
            public float GetCost()
            {
                return Quantity * Product.FinalPrice;
            }

            public Item(int Id, int Quantity)
            {
                this.Id = Id;
                this.Quantity = Quantity;
            }
           
            public bool Equals(Item other)
            {
                return Id.Equals(other.Id);
            }
            public override bool Equals(object obj) => Equals(obj as Item);
            public override int GetHashCode() => (Id, Quantity).GetHashCode();
        }
        public static string Compress(Item[] products)
        {
            byte[] bytes = new byte[products.Length * 8];
            for (int i = 0; i < products.Length; i++)
            {
                BitConverter.GetBytes(products[i].Id).CopyTo(bytes, i * 8);
                BitConverter.GetBytes(products[i].Quantity).CopyTo(bytes, i * 8 + 4);
            }
            return Convert.ToBase64String(bytes);
        }
        public static List<Item> Decompress(string hash)
        {
            List<Item> cart = new List<Item>();
            if (hash == null || hash == "") return cart;
            byte[] bytes = Convert.FromBase64String(hash);
            for (int i = 0; i < bytes.Length / 8; i++)
                cart.Add(new Item(BitConverter.ToInt32(bytes, i * 8), BitConverter.ToInt32(bytes, i * 8 + 4)));
            return cart;
        }
    }
    public class ItemIdComparer : IEqualityComparer<Item>
    {
        public bool Equals(Item b1, Item b2)
        {
            if (object.ReferenceEquals(b1, b2))
                return true;

            if (b1 is null || b2 is null)
                return false;

            return b1.Id == b2.Id;
        }

        public int GetHashCode(Item item) => item.Id.GetHashCode();
    }
}

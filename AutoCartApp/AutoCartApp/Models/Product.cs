using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCartApp.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string title { get; set; }
        public float price { get; set; }
        public float discount { get; set; }
        public string description { get; set; }
        public string tags { get; set; }
        public string imageId { get; set; }
        public Product() { }
        public Product(string title, float price, string description, string tags, string imageId, int discount = 0)
        {
            this.title = title;
            this.price = price;
            this.description = description;
            this.tags = tags;
            this.imageId = imageId;
            this.discount = discount;
        }
        public override string ToString()
        {
            return title;
        }
    }
}

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
        public string Title { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string ImageId { get; set; }
        public Product() { }
        public Product(string title, float price, string description, string tags, string imageId, int discount = 1)
        {
            Title = title;
            Price = price;
            Description = description;
            Tags = tags;
            ImageId = imageId;
            Discount = discount;
        }
        public override string ToString()
        {
            return Title;
        }
    }
}

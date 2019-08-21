using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCartApp.Models
{
    public enum HistoryType
    {
        LoginAccount,
        RegisterAccount,
        AddProduct,
        RemoveProduct,
        ChangeQuantity,
        OrderCompleted
    }
    public class HistoryItem
    {
        public HistoryType Type { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public string Description {
            get { return Product != null ? Product.Title : ""; }
        }
        public Product Product { get; set; }
        public HistoryItem(HistoryType type)
        {
            Type = type;
            Time = DateTime.Now.ToString();
        }
    }
}

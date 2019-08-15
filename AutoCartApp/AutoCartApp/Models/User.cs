using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoCartApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProductCart { get; set; }
        public User() { }
        public User(string UserName, string Password, string Email, string Phone) {
            this.UserName = UserName;
            this.Password = Password;
            this.Email = Email;
            this.PhoneNumber = Phone;
            ProductCart = "";
        }
        public override string ToString()
        {
            return UserName;
        }
    }
}

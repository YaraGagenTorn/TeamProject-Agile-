using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AutoCartApp.Models;
using System.Text.RegularExpressions;

namespace AutoCartApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrationPage : ContentPage
	{
		public RegistrationPage ()
		{
            Title = "Auto Cart Registration";
            InitializeComponent();
		}

        private void RegButton_Clicked(object sender, EventArgs e)
        {
            Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (EntryUserName.Text.Length < 4)
                this.DisplayAlert("Invalid Information", "User Name must be atleast 4 characters", "Okay");
            else if (EntryUserPassword.Text.Length < 6)
                this.DisplayAlert("Invalid Information", "User Password must be atleast 6 characters", "Okay");
            else if (EntryUserEmail.Text == null || !EmailRegex.IsMatch(EntryUserEmail.Text))
                this.DisplayAlert("Invalid Information", "Email Invalid please enter your email", "Okay");
            else if (EntryUserPhoneNumber.Text.Length < 8)
                this.DisplayAlert("Invalid Information", "Phone number must be atleast 8 digits", "Okay");
            else
            {
                var user = new User(EntryUserName.Text, EntryUserPassword.Text, EntryUserEmail.Text, EntryUserPhoneNumber.Text);
                App.Database.SaveUser(user);
                App.History.Add(new HistoryItem(HistoryType.RegisterAccount) { Title = user.UserName });

                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Congratulation", "User Registration Sucessfull", "Yes", "Cancel");
                    if (result)
                        await Navigation.PushAsync(new LoginPage());

                });
            }
            
        }
    }
}
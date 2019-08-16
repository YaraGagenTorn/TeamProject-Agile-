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

namespace AutoCartApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrationPage : ContentPage
	{
		public RegistrationPage ()
		{
			InitializeComponent ();
		}

        private void RegButton_Clicked(object sender, EventArgs e)
        {
            var user = new User()
            {
                UserName = EntryUserName.Text,
                Password = EntryUserPassword.Text,
                Email = EntryUserEmail.Text,
                PhoneNumber = EntryUserPhoneNumber.Text

            };
            App.database.SaveUser(user);

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Congratulation", "User Registration Sucessfull", "Yes", "Cancel");
                if (result)
                    await Navigation.PushAsync(new LoginPage());

        });
        }
    }
}
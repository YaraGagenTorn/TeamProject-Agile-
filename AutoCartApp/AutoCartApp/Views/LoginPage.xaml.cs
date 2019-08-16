using AutoCartApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutoCartApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

         async void SignUp_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        async void Login_Button_Clicked(object sender, EventArgs e)
        {

            User user = App.database.GetUser(EntryUser.Text);
            if (user != null && user.Password.Equals(EntryPassword.Text))
            {
                App.currentUser = user;
                App.Current.MainPage = new Main();
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Error", "Failed User Name and Password", "Yes", "Cancel");

                    if (result)
                        await Navigation.PushAsync(new LoginPage());
                    else
                    {
                        await Navigation.PushAsync(new LoginPage());
                    }

                });
            }                
        }
    }
}
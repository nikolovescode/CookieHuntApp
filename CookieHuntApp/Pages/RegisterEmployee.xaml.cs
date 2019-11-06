using CookieHuntApp.Models;
using CookieHuntApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookieHuntApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterEmployee : ContentPage
	{
        private UserHasRole _role;

        public RegisterEmployee (UserHasRole role)
		{
            _role = role;
			InitializeComponent ();
		}

        private async void BtnRegisterEmployee_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.RegisterUserHasRole(EntEmail.Text, "employee", _role.StoreId);
            if (!response)
            {
                await DisplayAlert("Alert", "Something wrong...", "Cancel");
            }
            else
            {
                await DisplayAlert("Hi", "Email is registered as belonging to employee", "Allright");
                await Navigation.PopToRootAsync();
            }
        }
    }
}
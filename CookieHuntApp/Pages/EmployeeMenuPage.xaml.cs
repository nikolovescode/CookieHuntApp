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
	public partial class EmployeeMenuPage : ContentPage
	{
        private UserHasRole _role;

        public EmployeeMenuPage (UserHasRole role)
		{
            _role = role;
			InitializeComponent ();
		}
        private void Tblogout_OnClicked(object sender, EventArgs e)
        {
            Settings.UserName = "";
            Settings.Password = "";
            Settings.Accesstoken = "";
            Navigation.InsertPageBefore(new SignInPage(), this);
            Navigation.PopAsync();

        }

        private async void BtnAddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddItemPage(_role));
            

        }

        private async void BtnRegEmployee_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterEmployee(_role));

        }

        private async void BtnAddCoupon_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PickDate(_role));

        }
    }
}
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
	public partial class CustomerMenuPage : ContentPage
	{
		public CustomerMenuPage ()
		{
			InitializeComponent ();
		}

        private async void BtnAddSubscription_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewCouponsPage());

        }

        private async void BtnViewCoupons_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddSubscriptionStore());
        }
        private void Tblogout_OnClicked(object sender, EventArgs e)
        {
            Settings.UserName = "";
            Settings.Password = "";
            Settings.Accesstoken = "";
            Navigation.InsertPageBefore(new SignInPage(), this);
            Navigation.PopAsync();

        }
    }
}
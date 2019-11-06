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
	public partial class RegisterStandardCategoryPage : ContentPage
	{
		public RegisterStandardCategoryPage ()
		{
			InitializeComponent ();
		}

        private async void BtnAddSC_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.RegisterStandardCategory(EntTitle.Text);
            if (!response)
            {
                await DisplayAlert("Alert", "Something wrong...", "Cancel");
            }
            else
            {
                await DisplayAlert("Info", "Category Added", "OK");
                await Navigation.PopToRootAsync();
            }
        }
    }
}
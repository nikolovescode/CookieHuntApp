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
	public partial class SignPurchase : ContentPage
	{
        private Item _selectedItem;
        private GroupACategory _selectedACategory;

        public SignPurchase (Models.Item selectedItem, Models.GroupACategory selectedACategory)
		{
            _selectedItem = selectedItem;
            _selectedACategory = selectedACategory;
			InitializeComponent ();
		}

        private async void BtnSignPurchase_Clicked(object sender, EventArgs e)
        {
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.RegisterPurchaseHistory(Settings.UserName, _selectedItem.StoreId, _selectedACategory.Points);
            if (!response)
            {
                await DisplayAlert("Alert", "Something wrong...", "Cancel");
            }
            else
            {
                await DisplayAlert("Hi", "Points added to membership account", "Allright");
                Navigation.InsertPageBefore(new CustomerMenuPage(), this);
                await Navigation.PopAsync();
            }
        }
    }
}
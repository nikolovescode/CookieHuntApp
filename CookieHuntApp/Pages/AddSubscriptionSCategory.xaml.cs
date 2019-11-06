using CookieHuntApp.Models;
using CookieHuntApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookieHuntApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddSubscriptionSCategory : ContentPage
	{                                                                       
        public ObservableCollection<StandardCategory> SCategories;
        private Store _selectedStore;


        public AddSubscriptionSCategory (Store selectedStore)
		{
            SCategories = new ObservableCollection<StandardCategory>();
            _selectedStore = selectedStore;
            InitializeComponent();
            FindSCategories();
		}
        private async void FindSCategories()
        {
            ApiServices apiServices = new ApiServices();
            var resultSCategories = await apiServices.GetStandardCategories();

            foreach (var item in resultSCategories)
            {
                SCategories.Add(item);
            }

            LvSCategories.ItemsSource = SCategories;
        }

        private async void LvSCategories_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedSCategory = e.SelectedItem as StandardCategory;
            ApiServices apiServices = new ApiServices();
            bool storeResponse = await apiServices.RegisterUserHasSubscription(Settings.UserName, selectedSCategory.Id, _selectedStore.Id);
            if (!storeResponse)
            {
                await DisplayAlert("Alert", "Store not added", "Cancel");
            }
            await Navigation.PopToRootAsync();

        }
    }
}
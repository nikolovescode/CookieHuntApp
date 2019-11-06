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
	public partial class AddItemPage : ContentPage
	{
        public ObservableCollection<StandardCategory> Categories;

        private UserHasRole _role;

        public AddItemPage (Models.UserHasRole role)
		{
            _role = role;
            Categories = new ObservableCollection<StandardCategory>();
            InitializeComponent();
            FindCategories();
		}

        private async void FindCategories()
        {
            ApiServices apiServices = new ApiServices();
            var resultSCategories = await apiServices.GetStandardCategories();

            foreach (var cat in resultSCategories)
            {
                Categories.Add(cat);
                    
            }
            LvCat.ItemsSource = Categories;

        }

        private async void LvCat_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string nameOfItem = EntNameItem.Text;
            var selectedSCategory = e.SelectedItem as StandardCategory;
            int sCId = selectedSCategory.Id;
            ApiServices apiServices = new ApiServices();

            bool itemResponse = await apiServices.RegisterItem(nameOfItem, _role.StoreId, sCId);
            if (!itemResponse)
            {
                await DisplayAlert("Alert", "Item not added", "Cancel");
            }
            else
            {
                await Navigation.PopToRootAsync();

            }
        }
    }
}

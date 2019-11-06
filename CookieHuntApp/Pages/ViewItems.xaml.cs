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
	public partial class ViewItems : ContentPage
	{
        public ObservableCollection<Item> Items;
        private GroupACategory _selectedACategory;


        public ViewItems (GroupACategory selectedACategory)
		{
            _selectedACategory = selectedACategory;
            Items = new ObservableCollection<Item>();

            InitializeComponent();
            FindItems();
		}

        private async void FindItems()
        {
            ApiServices apiServices = new ApiServices();
            var resultItems = await apiServices.GetItemFromStoreId(_selectedACategory.StoreId);

            foreach (var resI in resultItems)
            {
                if(resI.StandardCategoryId==_selectedACategory.StandardCategoryId) { 
                Items.Add(resI);
                }
            }

            LvItem.ItemsSource = Items;
        }

        private async void LvItem_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = e.SelectedItem as Item;
            await Navigation.PushAsync(new SignPurchase(selectedItem, _selectedACategory));
        }
    }
}
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
	public partial class PickDate : ContentPage
	{
        public ObservableCollection<Item> Items;

        private UserHasRole _role;
        private DateTime lastDate;
        private Item pickedItem;
		public PickDate (UserHasRole role)
		{
            _role = role;
            lastDate = new DateTime();
            pickedItem = new Item();
            Items = new ObservableCollection<Item>();
            InitializeComponent();
            FindItems();
		}

        private async void FindItems()
        {
            ApiServices apiServices = new ApiServices();
            var resultSCategories = await apiServices.GetItemFromStoreId(_role.StoreId);
            

            foreach (var item in resultSCategories)
            {
                Items.Add(item);

            }
            LvItems.ItemsSource = Items;
            

        }

        private async void BtnPickDate_Clicked(object sender, EventArgs e)
        {
           
            ApiServices apiServices = new ApiServices();

            bool itemResponse = await apiServices.RegisterCoupon(lastDate, pickedItem.Id, pickedItem.StandardCategoryId, _role.StoreId, Convert.ToInt32(EntPrice.Text), Convert.ToInt32(EntPoints.Text));
            if (!itemResponse)
            {
                await DisplayAlert("Alert", "Coupon not added", "Cancel");
            }
            else
            {
                await Navigation.PopToRootAsync();

            }
        }


        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            lastDate = e.NewDate;
            MainLabel.Text = e.NewDate.ToString();
        }

        private void LvItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = e.SelectedItem as Item;
            pickedItem = selectedItem;
        }
    }
}
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
	public partial class AddSubscriptionStore : ContentPage
	{
        public ObservableCollection<Store> Stores;

        public AddSubscriptionStore ()
		{
            Stores = new ObservableCollection<Store>();
            InitializeComponent();
            FindStores();

        }

        private async void FindStores()
        {
            ApiServices apiServices = new ApiServices();
            var resultStores = await apiServices.GetStores();

            foreach (var item in resultStores)
            {
                Stores.Add(item);
            }

            LvStore.ItemsSource = Stores;
        }
        private async void LvStore_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedStore = e.SelectedItem as Store;
            await Navigation.PushAsync(new AddSubscriptionSCategory(selectedStore));

        }
    }
}
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
	public partial class ViewCouponsPage : ContentPage
	{
        public ObservableCollection<UserHasSubscription> Subscriptions;
        public ObservableCollection<Coupon> Coupons;
        public ObservableCollection<Item> Items;
        public ObservableCollection<Store> Stores;


        public ViewCouponsPage ()
		{
            Subscriptions = new ObservableCollection<UserHasSubscription>();
            Coupons = new ObservableCollection<Coupon>();
            Items = new ObservableCollection<Item>();
            Stores = new ObservableCollection<Store>();
            InitializeComponent();
            FindCoupons();
		}

        private async void FindCoupons()
        {
            ApiServices apiServices = new ApiServices();
            var resultSubscriptions = await apiServices.GetUserHasSubscriptionsFromEmail(Settings.UserName);

            foreach (var res in resultSubscriptions)
            {
                Stores.Add(res);
            }
 


            LvCoupons.ItemsSource = Stores;

        }
        private async void LvSCoupons_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedStore = e.SelectedItem as Store;
            await Navigation.PushAsync(new ViewStandardCategories(selectedStore));
        }
    }
}
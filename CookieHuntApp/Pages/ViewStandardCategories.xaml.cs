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
	public partial class ViewStandardCategories : ContentPage
	{
        public ObservableCollection<GroupACategory> GroupAs;
        public ObservableCollection<GroupBCategory> GroupBs;
        public ObservableCollection<GroupCCateory> GroupCs;
        public ObservableCollection<StandardCategory> SCGroupAs;

        private Store _selectedStore;


        public ViewStandardCategories (Store selectedStore)
		{
            _selectedStore = selectedStore;
            GroupAs = new ObservableCollection<GroupACategory>();
            GroupBs = new ObservableCollection<GroupBCategory>();
            GroupCs = new ObservableCollection<GroupCCateory>();
            SCGroupAs = new ObservableCollection<StandardCategory>();
            InitializeComponent ();
            FindGroupA();
		}

        private async void FindGroupA()
        {
            ApiServices apiServices = new ApiServices();
            var resultGroupAs = await apiServices.GetGroupAFromStoreId(_selectedStore.Id);

            foreach (var resA in resultGroupAs)
            {
                GroupAs.Add(resA);
            }

            LvGroupA.ItemsSource = GroupAs;
        }

        private async void LvGroupA_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var selectedACategory = e.SelectedItem as GroupACategory;
            await Navigation.PushAsync(new ViewItems(selectedACategory));
        }
    }
}
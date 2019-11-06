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
	public partial class GroupCRules : ContentPage
	{
        private string _nameOfStore;
        private int _sCIdGroupA;
        private int _percentOffGroupA;
        private int _pointsNeededGroupA;
        private int _sCIdGroupB;
        private int _percentOffGroupB;
        private int _pointsNeededGroupB;
        public ObservableCollection<StandardCategory> SCategories;


        public GroupCRules (string nameOfStore, int sCIdGroupA, int percentOffGroupA, int pointsNeededGroupA, int sCIdGroupB, int percentOffGroupB, int pointsNeededGroupB)
		{
            _nameOfStore = nameOfStore;
            _sCIdGroupA = sCIdGroupA;
            _percentOffGroupA = percentOffGroupA;
            _pointsNeededGroupA = pointsNeededGroupA;
            _sCIdGroupB = sCIdGroupB;
            _percentOffGroupB = percentOffGroupB;
            _pointsNeededGroupB = pointsNeededGroupB;
            SCategories = new ObservableCollection<StandardCategory>();

            InitializeComponent ();
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
            int percentOffGroupC = Convert.ToInt32(EntPercentOff.Text);
            int pointsNeededGroupC = Convert.ToInt32(EntPointsRequested.Text);
            var selectedSCategory = e.SelectedItem as StandardCategory;
            int sCIdGroupC = selectedSCategory.Id;
            ApiServices apiServices = new ApiServices();
            bool storeResponse = await apiServices.RegisterStore(_nameOfStore);
            if (!storeResponse)
            {
              await DisplayAlert("Alert", "Store not added", "Cancel");
             }
         //    else
         //    {
           // await DisplayAlert("Info", "Store added", "OK");
           // await Navigation.PopToRootAsync();
         //   }
            
            //Hämta Id från databas gällande store som nyss skapades
            var resultStores = await apiServices.GetStoreFromName(_nameOfStore);

            int storeId = resultStores.First().Id;

            bool gAResponse = await apiServices.RegisterGroupACategory(storeId, _sCIdGroupA, _pointsNeededGroupA, _percentOffGroupA);
            if (!gAResponse)
            {
                await DisplayAlert("Alert", "Group A item not added", "Cancel");
            }

            bool gBResponse = await apiServices.RegisterGroupBCategory(storeId, _sCIdGroupB, _pointsNeededGroupB, _percentOffGroupB);
            if (!gBResponse)
            {
                await DisplayAlert("Alert", "Group B item not added", "Cancel");
            }

            bool gCResponse = await apiServices.RegisterGroupCCategory(storeId, sCIdGroupC, pointsNeededGroupC, percentOffGroupC);
            if (!gCResponse)
            {
                await DisplayAlert("Alert", "Group C item not added", "Cancel");
            }

            var resultGroupA = await apiServices.GetGroupAFromStoreId(storeId);

            int groupAId = resultGroupA.First().Id;

            var resultGroupB = await apiServices.GetGroupBFromStoreId(storeId);

            int groupBId = resultGroupB.First().Id;

            var resultGroupC = await apiServices.GetGroupCFromStoreId(storeId);

            int groupCId = resultGroupC.First().Id;

            bool StoreHasGroupsResponse = await apiServices.RegisterStoreHasGroups(storeId, groupAId, groupBId, groupCId);
            if (!StoreHasGroupsResponse)
            {
                await DisplayAlert("Alert", "Groups are not added to store", "Cancel");
            }
            else
            {
                await DisplayAlert("Info", "Store registered", "OK");

            }


            await Navigation.PopToRootAsync();
            
        }
    }
}
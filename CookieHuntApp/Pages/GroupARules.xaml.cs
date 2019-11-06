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
	public partial class GroupARules : ContentPage
	{
        private string _nameOfStore;
        public ObservableCollection<StandardCategory> SCategories;


        public GroupARules (string nameOfStore)
		{
            _nameOfStore = nameOfStore;
			InitializeComponent ();
            SCategories = new ObservableCollection<StandardCategory>();
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
            int percentOffGroupA = Convert.ToInt32(EntPercentOff.Text);
            int pointsNeededGroupA = Convert.ToInt32(EntPointsRequested.Text);
            var selectedSCategory = e.SelectedItem as StandardCategory;
            int sCIdGroupA = selectedSCategory.Id;
            await Navigation.PushAsync(new GroupBRules(_nameOfStore, sCIdGroupA, percentOffGroupA, pointsNeededGroupA));

        }
    }
}
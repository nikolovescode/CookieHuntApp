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
	public partial class GroupBRules : ContentPage
	{
        private string _nameOfStore;
        private int _sCIdGroupA;
        private int _percentOffGroupA;
        private int _pointsNeededGroupA;
        public ObservableCollection<StandardCategory> SCategories;


        public GroupBRules (string nameOfStore, int sCIdGroupA, int percentOffGroupA, int pointsNeededGroupA)
		{
            _nameOfStore = nameOfStore;
            _sCIdGroupA = sCIdGroupA;
            _percentOffGroupA = percentOffGroupA;
            _pointsNeededGroupA = pointsNeededGroupA;
            SCategories = new ObservableCollection<StandardCategory>();


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
            int percentOffGroupB = Convert.ToInt32(EntPercentOff.Text);
            int pointsNeededGroupB = Convert.ToInt32(EntPointsRequested.Text);
            var selectedSCategory = e.SelectedItem as StandardCategory;
            int sCIdGroupB = selectedSCategory.Id;
            await Navigation.PushAsync(new GroupCRules(_nameOfStore, _sCIdGroupA, _percentOffGroupA, _pointsNeededGroupA, sCIdGroupB, percentOffGroupB, pointsNeededGroupB));

        }
    }
}
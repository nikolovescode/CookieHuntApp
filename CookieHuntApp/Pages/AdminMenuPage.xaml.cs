using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookieHuntApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminMenuPage : ContentPage
    {
        public AdminMenuPage()
        {
            InitializeComponent();
        }

 

        private async void BtnRegStore_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateStorePage());

        }

        private async void BtnRegStandCat_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterStandardCategoryPage());

        }

        private async void BtnRegEmployee_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminEmployeeAtStore());

        }

        private void Tblogout_OnClicked(object sender, EventArgs e)
        {
            Settings.UserName = "";
            Settings.Password = "";
            Settings.Accesstoken = "";
            Navigation.InsertPageBefore(new SignInPage(), this);
            Navigation.PopAsync();

        }
    }
}